using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GloboTicket.Mobile.Models;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace GloboTicket.Mobile.Services
{
    public class EventService : IEventService
    {
        private readonly HttpClient _httpClient;

        private readonly string baseUrl = Device.RuntimePlatform == Device.Android ? "https://10.0.2.2:5011/api" : "https://localhost:5011/api";

        public EventService()
        {
            _httpClient = new HttpClient(DependencyService.Get<IHttpClientHandlerService>().GetInsecureHandler());
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public async Task<List<EventDto>> GetAllEvents()
        {
            var url = new Uri(baseUrl + "/events");

            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<EventDto>>(content);
            }
            return null;
        }

        public Task<List<EventDto>> GetEventByCategoryId(Guid categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<EventDto> GetEventDetail(Guid eventId)
        {
            throw new NotImplementedException();
        }
    }
}
