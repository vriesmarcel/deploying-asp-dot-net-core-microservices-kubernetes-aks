using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GloboTicket.Web.Services
{
    public class DiscountService: IDiscountService
    {
        private readonly HttpClient _client;

        public DiscountService(HttpClient client)
        {
            _client = client;
        }

        public Task<int> GetDiscountForCode(string code)
        {
            throw new NotImplementedException();
        }

        public Task ApplyCode(string code)
        {
            throw new NotImplementedException();
        }
    }
}
