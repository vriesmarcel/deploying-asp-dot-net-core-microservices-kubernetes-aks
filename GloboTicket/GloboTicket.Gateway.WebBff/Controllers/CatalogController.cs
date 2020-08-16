using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace GloboTicket.Gateway.WebBff.Controllers
{
    [ApiController]
    public class CatalogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
