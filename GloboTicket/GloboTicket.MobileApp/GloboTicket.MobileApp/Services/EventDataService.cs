using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GloboTicket.MobileApp.Models;

namespace GloboTicket.MobileApp.Services
{
    public class EventDataService : IEventDataService
    {
        private readonly IEventRepository eventRepository;

        public EventDataService(IEventRepository eventRepository)
        {
            this.eventRepository = eventRepository;
        }

        public async Task<List<Event>> GetAllEventsAsync(bool force)
        {
            return await eventRepository.GetAllEventsAsync();
        }

        public async Task<Event> GetEventByIdAsync(Guid id)
        {
            return await eventRepository.GetEventByIdAsync(id);
        }
    }
}