using GloboTicket.Web.Models.View;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace GloboTicket.Web.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            var orderViewModels = new List<OrderViewModel>
            {
                new OrderViewModel()
                {
                    OrderPlaced = DateTime.Now, OrderId = Guid.NewGuid(), OrderTotal = 100, Paid = true
                },
                new OrderViewModel()
                {
                    OrderPlaced = DateTime.Now, OrderId = Guid.NewGuid(), OrderTotal = 200, Paid = true
                },
                new OrderViewModel()
                {
                    OrderPlaced = DateTime.Now, OrderId = Guid.NewGuid(), OrderTotal = 1400, Paid = true
                }
            };

            return View(orderViewModels);
        }
    }
}
