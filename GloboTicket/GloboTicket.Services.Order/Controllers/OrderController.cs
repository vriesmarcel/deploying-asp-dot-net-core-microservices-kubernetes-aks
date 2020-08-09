using System;
using System.Threading.Tasks;
using GloboTicket.Services.Ordering.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace GloboTicket.Services.Ordering.Controllers
{
    [ApiController]
    [Route("api/order")]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> List(Guid id)
        {
            var orders = await _orderRepository.GetOrdersForUser(id);
            return Ok(orders);
        }
    }
}
