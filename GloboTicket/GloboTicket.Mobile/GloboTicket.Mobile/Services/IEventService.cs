using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GloboTicket.Mobile.Models;

namespace GloboTicket.Mobile.Services
{
    public interface IEventService
    {
        Task<List<EventDto>> GetAllEvents();
        Task<List<EventDto>> GetEventByCategoryId(Guid categoryId);
        Task<EventDto> GetEventDetail(Guid eventId);
    }
}
