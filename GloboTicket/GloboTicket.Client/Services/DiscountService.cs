using System;
using GloboTicket.Web.Extensions;
using GloboTicket.Web.Models;
using System.Net.Http;
using System.Threading.Tasks;
using GloboTicket.Web.Models.Api;

namespace GloboTicket.Web.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly HttpClient client;
        private readonly Settings settings;

        public DiscountService(HttpClient client, Settings settings)
        {
            this.client = client;
            this.settings = settings;
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
