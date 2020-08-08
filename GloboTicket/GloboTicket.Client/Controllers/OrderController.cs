using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GloboTicket.Web.Models.View;
using Microsoft.AspNetCore.Mvc;

namespace GloboTicket.Web.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Details()
        {

            return View();
        }

        public async Task<IActionResult> Checkout(OrderViewModel orderViewModel)
        {
            //return View();
            return null;
        }

      
    }
}
