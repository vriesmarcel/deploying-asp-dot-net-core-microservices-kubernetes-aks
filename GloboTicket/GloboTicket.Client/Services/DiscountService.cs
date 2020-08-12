using GloboTicket.Web.Extensions;
using GloboTicket.Web.Models.Api;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GloboTicket.Web.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly HttpClient client;

        public DiscountService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<Coupon> GetCouponByCode(string code)
        {
            if (code == string.Empty)
                return null;

            var response = await client.GetAsync($"/api/disount/{code}");
            return await response.ReadContentAs<Coupon>();
        }

        public async Task UseCoupon(Guid couponId)
        {
            await client.PutAsJson($"/api/disount/use/{couponId}", new CouponForUpdate());
        }
    }
}
