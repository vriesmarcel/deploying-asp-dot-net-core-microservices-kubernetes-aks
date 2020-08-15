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

        public async Task<List<BasketChangeEvent>> GetBasketChangeEvents(DateTime startDate, int max)
        {
            var formattedDate = $"{startDate.Year}/{startDate.Month}/{startDate.Day} {startDate.Hour}:{startDate.Minute}:{startDate.Second}";
            var s = $"/api/basketevent?fromDate={formattedDate}&max={max}";
            var response = await client.GetAsync(s);
            return await response.ReadContentAs<List<BasketChangeEvent>>();
        }
    }
}
