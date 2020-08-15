using GloboTicket.Services.Marketing.Extensions;
using GloboTicket.Services.Marketing.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace GloboTicket.Services.Marketing.Services
{
    public class BasketChangeEventService: IBasketChangeEventService
    {
        private readonly HttpClient client;

        public BasketChangeEventService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<List<BasketChangeEvent>> GetBasketChangeEvents(DateTimeOffset startDate, int max)
        {
            var response = await client.GetAsync($"/api/basketevent?startDate={startDate}&max={max}");
            return await response.ReadContentAs<List<BasketChangeEvent>>();
        }
    }
}
