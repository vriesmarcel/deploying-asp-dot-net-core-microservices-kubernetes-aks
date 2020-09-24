using AutoMapper;
using GloboTicket.Services.ShoppingBasket.Entities;
using GloboTicket.Services.ShoppingBasket.Models;
using GloboTicket.Services.ShoppingBasket.Repositories;
using GloboTicket.Services.ShoppingBasket.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BasketLine = GloboTicket.Services.ShoppingBasket.Models.BasketLine;

namespace GloboTicket.Services.ShoppingBasket.Controllers
{
    [Route("api/baskets/{basketId}/basketlines")]
    [ApiController]
    public class BasketLinesController : ControllerBase
    {
        private readonly IBasketRepository basketRepository;
        private readonly IBasketLinesRepository basketLinesRepository;
        private readonly IEventRepository eventRepository;
        private readonly IEventCatalogService eventCatalogService;
        private readonly IMapper mapper;
        private readonly IBasketChangeEventRepository basketChangeEventRepository;

        public BasketLinesController(IBasketRepository basketRepository,
            IBasketLinesRepository basketLinesRepository, IEventRepository eventRepository,
            IEventCatalogService eventCatalogService, IMapper mapper, IBasketChangeEventRepository basketChangeEventRepository)
        {
            this.basketRepository = basketRepository;
            this.basketLinesRepository = basketLinesRepository;
            this.eventRepository = eventRepository;
            this.eventCatalogService = eventCatalogService;
            this.basketChangeEventRepository = basketChangeEventRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BasketLine>>> Get(Guid basketId)
        {
            if (!await basketRepository.BasketExists(basketId))
            {
                return NotFound();
            }

            var basketLines = await basketLinesRepository.GetBasketLines(basketId);
            return Ok(mapper.Map<IEnumerable<BasketLine>>(basketLines));
        }

        [HttpGet("{basketLineId}", Name = "GetBasketLine")]
        public async Task<ActionResult<BasketLine>> Get(Guid basketId,
            Guid basketLineId)
        {
            if (!await basketRepository.BasketExists(basketId))
            {
                return NotFound();
            }

            var basketLine = await basketLinesRepository.GetBasketLineById(basketLineId);
            if (basketLine == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<BasketLine>(basketLine));
        }

        [HttpPost]
        public async Task<ActionResult<BasketLine>> Post(Guid basketId, [FromBody] BasketLineForCreation basketLineForCreation)
        {
            var basket = await basketRepository.GetBasketById(basketId);

            if (basket == null)
            {
                return NotFound();
            }

            if (!await eventRepository.EventExists(basketLineForCreation.EventId))
            {
                var eventFromCatalog = await eventCatalogService.GetEvent(basketLineForCreation.EventId);
                eventRepository.AddEvent(eventFromCatalog);
                await eventRepository.SaveChanges();
            }

            var basketLineEntity = mapper.Map<Entities.BasketLine>(basketLineForCreation);

            var processedBasketLine = await basketLinesRepository.AddOrUpdateBasketLine(basketId, basketLineEntity);
            await basketLinesRepository.SaveChanges();

            var basketLineToReturn = mapper.Map<BasketLine>(processedBasketLine);

            //log also to the event repo
            BasketChangeEvent basketChangeEvent = new BasketChangeEvent
            {
                BasketChangeType = BasketChangeTypeEnum.Add,
                EventId = basketLineForCreation.EventId,
                InsertedAt = DateTime.Now,
                UserId = basket.UserId
            };
            await basketChangeEventRepository.AddBasketEvent(basketChangeEvent);

            return CreatedAtRoute(
                "GetBasketLine",
                new { basketId = basketLineEntity.BasketId, basketLineId = basketLineEntity.BasketLineId },
                basketLineToReturn);
        }

        [HttpPut("{basketLineId}")]
        public async Task<ActionResult<BasketLine>> Put(Guid basketId,
            Guid basketLineId,
            [FromBody] BasketLineForUpdate basketLineForUpdate)
        {
            if (!await basketRepository.BasketExists(basketId))
            {
                return NotFound();
            }

            var basketLineEntity = await basketLinesRepository.GetBasketLineById(basketLineId);

            if (basketLineEntity == null)
            {
                return NotFound();
            }

            // map the entity to a dto
            // apply the updated field values to that dto
            // map the dto back to an entity
            mapper.Map(basketLineForUpdate, basketLineEntity);

            basketLinesRepository.UpdateBasketLine(basketLineEntity);
            await basketLinesRepository.SaveChanges();

            return Ok(mapper.Map<BasketLine>(basketLineEntity));
        }

        [HttpDelete("{basketLineId}")]
        public async Task<IActionResult> Delete(Guid basketId, Guid basketLineId)
        {
            //if (!await basketRepository.BasketExists(basketId))
            //{
            //    return NotFound();
            //}

            var basket = await basketRepository.GetBasketById(basketId);

            if (basket == null)
            {
                return NotFound();
            }

            var basketLineEntity = await basketLinesRepository.GetBasketLineById(basketLineId);

            if (basketLineEntity == null)
            {
                return NotFound();
            }

            basketLinesRepository.RemoveBasketLine(basketLineEntity);
            await basketLinesRepository.SaveChanges();

            //publish removal event
            BasketChangeEvent basketChangeEvent = new BasketChangeEvent
            {
                BasketChangeType = BasketChangeTypeEnum.Remove,
                EventId = basketLineEntity.EventId,
                InsertedAt = DateTime.Now,
                UserId = basket.UserId
            };
            await basketChangeEventRepository.AddBasketEvent(basketChangeEvent);

            return NoContent();
        }
    }
}
