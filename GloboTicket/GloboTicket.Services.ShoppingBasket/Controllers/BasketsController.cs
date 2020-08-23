using AutoMapper;
using GloboTicket.Integration.MessagingBus;
using GloboTicket.Services.ShoppingBasket.Messages;
using GloboTicket.Services.ShoppingBasket.Models;
using GloboTicket.Services.ShoppingBasket.Repositories;
using GloboTicket.Services.ShoppingBasket.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace GloboTicket.Services.ShoppingBasket.Controllers
{
    [Route("api/baskets")]
    [ApiController]
    public class BasketsController : ControllerBase
    {
        private readonly IBasketRepository basketRepository;
        private readonly IMapper mapper;
        private readonly IMessageBus messageBus;
        private readonly IDiscountService discountService;

        public BasketsController(IBasketRepository basketRepository, IMapper mapper, IMessageBus messageBus, IDiscountService discountService)
        {
            this.basketRepository = basketRepository;
            this.mapper = mapper;
            this.messageBus = messageBus;
            this.discountService = discountService;
        }

        [HttpGet("{basketId}", Name = "GetBasket")]
        public async Task<ActionResult<Basket>> Get(Guid basketId)
        {
            var basket = await basketRepository.GetBasketById(basketId);
            if (basket == null)
            {
                return NotFound();
            }

            var result = mapper.Map<Basket>(basket);
            result.NumberOfItems = basket.BasketLines.Sum(bl => bl.TicketAmount);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Basket>> Post(BasketForCreation basketForCreation)
        {
            var basketEntity = mapper.Map<Entities.Basket>(basketForCreation);

            basketRepository.AddBasket(basketEntity);
            await basketRepository.SaveChanges();

            var basketToReturn = mapper.Map<Basket>(basketEntity);

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
            var basket = await basketRepository.GetBasketById(basketId);

            if (basket == null)
            {
                return BadRequest();
            }

            basket.CouponId = coupon.CouponId;
            await basketRepository.SaveChanges();

            return Accepted();
        }

        [HttpPost("checkout")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CheckoutBasketAsync([FromBody] BasketCheckout basketCheckout)
        {
            try
            {


                //based on basket checkout, fetch the basket lines from repo
                var basket = await basketRepository.GetBasketById(basketCheckout.BasketId);

                if (basket == null)
                {
                    return BadRequest();
                }

                BasketCheckoutMessage basketCheckoutMessage = mapper.Map<BasketCheckoutMessage>(basketCheckout);
                basketCheckoutMessage.BasketLines = new List<BasketLineMessage>();
                int total = 0;

                foreach (var b in basket.BasketLines)
                {
                    var basketLineMessage = new BasketLineMessage
                    {
                        BasketLineId = b.BasketLineId,
                        Price = b.Price,
                        TicketAmount = b.TicketAmount
                    };

                    total += b.Price * b.TicketAmount;

                    basketCheckoutMessage.BasketLines.Add(basketLineMessage);
                }

                //apply discountt by talking to the discount service
                Coupon coupon = null;

                if (basket.CouponId.HasValue)
                    coupon = await discountService.GetCoupon(basket.CouponId.Value);
                //
                //if (basket.CouponId.HasValue)
                //    coupon = await discountService.GetCouponWithError(basket.CouponId.Value);

                if (coupon != null)
                {
                    basketCheckoutMessage.BasketTotal = total - coupon.Amount;
                }
                else
                {
                    basketCheckoutMessage.BasketTotal = total;
                }

                try
                {
                    await messageBus.PublishMessage(basketCheckoutMessage, "checkoutmessage");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

                await basketRepository.ClearBasket(basketCheckout.BasketId);
                return Accepted(basketCheckoutMessage);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.StackTrace);
            }
        }
    }
}
