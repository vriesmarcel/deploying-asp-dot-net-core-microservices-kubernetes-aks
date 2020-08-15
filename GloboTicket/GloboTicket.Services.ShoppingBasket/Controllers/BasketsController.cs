using AutoMapper;
using GloboTicket.Integration.MessagingBus;
using GloboTicket.Services.ShoppingBasket.Messages;
using GloboTicket.Services.ShoppingBasket.Models;
using GloboTicket.Services.ShoppingBasket.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GloboTicket.Services.ShoppingBasket.Services;

namespace GloboTicket.Services.ShoppingBasket.Controllers
{
    [Route("api/baskets")]
    [ApiController]
    public class BasketsController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;
        private readonly IMessageBus _messageBus;
        private readonly IDiscountService _discountService;

        public BasketsController(IBasketRepository basketRepository, IMapper mapper, IMessageBus messageBus, IDiscountService discountService)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
            _messageBus = messageBus;
            _discountService = discountService;
        }

        [HttpGet("{basketId}", Name = "GetBasket")]
        public async Task<ActionResult<Basket>> Get(Guid basketId)
        {
            var basket = await _basketRepository.GetBasketById(basketId);
            if (basket == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<Basket>(basket);
            result.NumberOfItems = basket.BasketLines.Sum(bl => bl.TicketAmount);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Basket>> Post(BasketForCreation basketForCreation)
        {
            var basketEntity = _mapper.Map<Entities.Basket>(basketForCreation);

            _basketRepository.AddBasket(basketEntity);
            await _basketRepository.SaveChanges();

            var basketToReturn = _mapper.Map<Basket>(basketEntity);

            return CreatedAtRoute(
                "GetBasket",
                new { basketId = basketEntity.BasketId },
                basketToReturn);
        }

        [HttpPut("{basketId}/coupon")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ApplyCouponToBasket(Guid basketId, Coupon coupon)
        {
            var basket = await _basketRepository.GetBasketById(basketId);

            if (basket == null)
            {
                return BadRequest();
            }

            basket.CouponId = coupon.CouponId;
            await _basketRepository.SaveChanges();
            
            return Accepted();
        }

        [HttpPost("checkout")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CheckoutBasketAsync([FromBody] BasketCheckout basketCheckout)
        {
            //based on basket checkout, fetch the basket lines from repo
            var basket = await _basketRepository.GetBasketById(basketCheckout.BasketId);

            if (basket == null)
            {
                return BadRequest();
            }

            BasketCheckoutMessage basketCheckoutMessage = _mapper.Map<BasketCheckoutMessage>(basketCheckout);
            basketCheckoutMessage.BasketLines = new List<BasketLineMessage>();
            int total = 0;

            foreach (var b in basket.BasketLines)
            {
                var basketLineMessage = new BasketLineMessage
                {
                    BasketLineId = b.BasketLineId, Price = b.Price, TicketAmount = b.TicketAmount
                };

                total += b.Price * b.TicketAmount;

                basketCheckoutMessage.BasketLines.Add(basketLineMessage);
            }

            //apply discountt by talking to the discount service
            var discount = await _discountService.GetCoupon(basket.CouponId);
            if (discount != null)
            {
                basketCheckoutMessage.BasketTotal = total - discount.Amount;
            }
            else
            {
                basketCheckoutMessage.BasketTotal = total;
            }

            try
            {
                await _messageBus.PublishMessage(basketCheckoutMessage, "checkoutmessage");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            await _basketRepository.ClearBasket(basketCheckout.BasketId);
            return Accepted(basketCheckoutMessage);
        }
    }
}
