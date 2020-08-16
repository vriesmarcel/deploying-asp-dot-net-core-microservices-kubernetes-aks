using GloboTicket.Services.ShoppingBasket.Extensions;
using GloboTicket.Services.ShoppingBasket.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GloboTicket.Services.ShoppingBasket.Services
{
    public class DiscountService: IDiscountService
    {
        private readonly HttpClient client;

        public DiscountService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<Coupon> GetCoupon(Guid couponId)
        {
            var response = await client.GetAsync($"/api/discount/{couponId}");
            return await response.ReadContentAs<Coupon>();
        }

        public async Task<Coupon> GetCouponWithError(Guid couponId)
        {
            var response = await client.GetAsync($"/api/discount/error/{couponId}");
            return await response.ReadContentAs<Coupon>();
        }
    }
}
