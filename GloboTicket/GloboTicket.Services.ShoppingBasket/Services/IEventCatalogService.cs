using GloboTicket.Services.ShoppingBasket.Entities;
using System;
using System.Threading.Tasks;

namespace GloboTicket.Services.ShoppingBasket.Services
{
    public interface IEventCatalogService
    {
        Task<Event> GetEvent(Guid id);
    }
}