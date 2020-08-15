using GloboTicket.Services.ShoppingBasket.DbContexts;
using GloboTicket.Services.ShoppingBasket.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace GloboTicket.Services.ShoppingBasket.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly ShoppingBasketDbContext shoppingBasketDbContext;

        public EventRepository(ShoppingBasketDbContext shoppingBasketDbContext)
        {
            this.shoppingBasketDbContext = shoppingBasketDbContext;
        }

        public async Task<bool> EventExists(Guid eventId)
        {
            return await shoppingBasketDbContext.Events.AnyAsync(e => e.EventId == eventId);
        }

        public void AddEvent(Event theEvent)
        {
            shoppingBasketDbContext.Events.Add(theEvent);

        }

        public async Task<bool> SaveChanges()
        {
            return (await shoppingBasketDbContext.SaveChangesAsync() > 0);
        }
    }
}
