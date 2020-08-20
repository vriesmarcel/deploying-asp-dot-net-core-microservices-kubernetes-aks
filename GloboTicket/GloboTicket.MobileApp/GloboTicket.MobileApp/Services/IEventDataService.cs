using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GloboTicket.MobileApp.Models;

namespace GloboTicket.MobileApp.Services
{
    public interface IEventDataService
    {
        Task<List<Event>> GetAllEventsAsync(bool force);
        Task<Event> GetEventByIdAsync(Guid id);
    }
}
