using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GloboTicket.MobileApp.Services;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace GloboTicket.MobileApp.Models
{
    public class EventRepository : IEventRepository
    {
        private readonly HttpClient _httpClient;

        //local
        private string baseUrl = Device.RuntimePlatform == Device.Android ? "https://10.0.2.2:5001/api/events" : "https://localhost:5001/api/events";
        
        //remote
        //private const string baseUrl = 
        //    "https://bethanyspieshopstockapprestservice.azurewebsites.net/api/piestock";

        public EventRepository()
        {
            //_httpClient = new HttpClient();
            _httpClient = new HttpClient(DependencyService.Get<IHttpClientHandlerService>().GetInsecureHandler());
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public async Task<List<Event>> GetAllEventsAsync()
        {
            var url = new Uri(baseUrl);

            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Event>>(content);
            }
            return null;
        }

        public async Task<Event> GetEventByIdAsync(Guid id)
        {
            var url = new Uri($"{baseUrl}/{id}");

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Event>(content);
            }
            return null;
        }
    }
}
