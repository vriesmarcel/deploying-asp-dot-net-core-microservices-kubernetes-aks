using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GloboTicket.MobileApp.Models
{
    public interface IEventRepository
    {
        Task<List<Event>> GetAllEventsAsync();
        Task<Event> GetEventByIdAsync(Guid id);
    }
}
