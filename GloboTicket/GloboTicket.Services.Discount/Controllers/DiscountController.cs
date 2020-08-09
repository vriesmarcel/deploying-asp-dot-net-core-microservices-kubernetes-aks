using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using GloboTicket.Services.Discount.Models;
using GloboTicket.Services.Discount.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GloboTicket.Services.Discount.Controllers
{
    [ApiController]
    [Route("api/discount")]
    public class DiscountController : Controller
    {
        private readonly ICouponRepository _couponRepository;
        private readonly IMapper _mapper;

        public DiscountController(ICouponRepository couponRepository, IMapper mapper)
        {
            _couponRepository = couponRepository;
            _mapper = mapper;
        }

        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpGet]
        public async Task<IActionResult> GetDiscountForCode(string code)
        {
            var coupon = await _couponRepository.GetCouponByCode(code);

            if (coupon == null)
                return NotFound();

            return Ok( _mapper.Map<CouponDto>(coupon));
        }

        [HttpPost("use/{code}")]
        public async Task<IActionResult> UseCoupon(string code)
        {
            await _couponRepository.UseCoupon(code);
            return Ok();
        }
    }
}
