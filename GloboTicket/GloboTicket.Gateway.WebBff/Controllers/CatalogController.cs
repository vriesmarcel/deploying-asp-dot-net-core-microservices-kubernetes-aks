using GloboTicket.Gateway.WebBff.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GloboTicket.Gateway.WebBff.Controllers
{
    [ApiController]
    [Route("api/bffweb/events")]
    public class CatalogController : ControllerBase
    {
        private readonly ICatalogService catalogService;

        public CatalogController(ICatalogService catalogService)
        {
            this.catalogService = catalogService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEvents(Guid categoryId)
        {
            var allevents = await catalogService.GetAllEvents();
            return Ok(allevents);
        }

        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetEventsPerCategory(Guid categoryId)
        {
            var eventsPerCategory = await catalogService.GetEventsPerCategory(categoryId);
            return Ok(eventsPerCategory);
        }

        [HttpGet("event/{eventId}")]
        public async Task<IActionResult> GetEventById(Guid eventId)
        {
            var eventById = await catalogService.GetEventById(eventId);
            return Ok(eventById);
        }

        [HttpGet("categories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await catalogService.GetAllCategories();
            return Ok(categories);
        }
    }
}
