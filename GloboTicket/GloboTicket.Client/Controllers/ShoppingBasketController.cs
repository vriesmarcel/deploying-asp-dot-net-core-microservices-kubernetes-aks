using GloboTicket.Web.Extensions;
using GloboTicket.Web.Models;
using GloboTicket.Web.Models.Api;
using GloboTicket.Web.Models.View;
using GloboTicket.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GloboTicket.Web.Controllers
{
    public class ShoppingBasketController : Controller
    {
        private readonly IShoppingBasketService _basketService;
        private readonly IDiscountService _discountService;
        private readonly Settings _settings;

        public ShoppingBasketController(IShoppingBasketService basketService, Settings settings, IDiscountService discountService)
        {
            _basketService = basketService;
            _settings = settings;
            _discountService = discountService;
        }

        public async Task<IActionResult> Index()
        {
            var basketLines = await _basketService.GetLinesForBasket(Request.Cookies.GetCurrentBasketId(_settings));
            var lineViewModels = basketLines.Select(bl => new BasketLineViewModel
            {
                LineId = bl.BasketLineId,
                EventId = bl.EventId,
                EventName = bl.Event.Name,
                Date = bl.Event.Date,
                Price = bl.Price,
                Quantity = bl.TicketAmount
            }
            );

            //TODO: code ophalen voor basket

            BasketViewModel basketViewModel = new BasketViewModel {BasketLines = lineViewModels.ToList()};
            return View(basketViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddLine(BasketLineForCreation basketLine)
        {
            var basketId = Request.Cookies.GetCurrentBasketId(_settings);
            var newLine = await _basketService.AddToBasket(basketId, basketLine);
            Response.Cookies.Append(_settings.BasketIdCookieName, newLine.BasketId.ToString());

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateLine(BasketLineForUpdate basketLineUpdate)
        {
            var basketId = Request.Cookies.GetCurrentBasketId(_settings);
            await _basketService.UpdateLine(basketId, basketLineUpdate);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RemoveLine(Guid lineId)
        {
            var basketId = Request.Cookies.GetCurrentBasketId(_settings);
            await _basketService.RemoveLine(basketId, lineId);
            return RedirectToAction("Index");
        }

        public IActionResult Checkout()
        {
            return RedirectToAction("Checkout");
        }

        [HttpPost]
        public IActionResult Checkout(BasketCheckoutViewModel basketCheckoutViewModel)
        {
            return View();
        }

        [HttpPost]
        public IActionResult ApplyDiscountCode(BasketViewModel basketViewModel)//TODO: refactor to use just single value
        {
            string code = basketViewModel.Code;

            return View("Index");
        }
    }
}
