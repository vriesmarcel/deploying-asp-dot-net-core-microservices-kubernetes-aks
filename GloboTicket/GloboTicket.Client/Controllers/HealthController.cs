using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GloboTicket.Web.Models;
using GloboTicket.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace GloboTicket.Web.Controllers
{
    public class HealthController : Controller
    {
        private readonly IEventCatalogService eventCatalogService;
        private readonly IShoppingBasketService shoppingBasketService;
        private readonly Settings settings;

        public HealthController(IEventCatalogService eventCatalogService, IShoppingBasketService shoppingBasketService, Settings settings)
        {
            this.eventCatalogService = eventCatalogService;
            this.shoppingBasketService = shoppingBasketService;
            this.settings = settings;

        }
        public async Task<IActionResult> Index()
        {
            // we return 200 ok, when we can access the DB categories and events
            var getCategories = eventCatalogService.GetCategories();
            var getEvents = eventCatalogService.GetAll();
            await Task.WhenAll(new Task[] { getCategories, getEvents });

            return Ok();
        }
    }
}
