using GloboTicket.Services.Marketing.DbContexts;
using GloboTicket.Services.Marketing.Entities;
using System.Threading.Tasks;

namespace GloboTicket.Services.Marketing.Repositories
{
    public class BasketChangeEventRepository :IBasketChangeEventRepository
    {
        private readonly MarketingDbContext marketingDbContext;

        public BasketChangeEventRepository(MarketingDbContext marketingDbContext)
        {
            this.marketingDbContext = marketingDbContext;
        }

        public async Task AddBasketChangeEvent(BasketChangeEvent basketChangeEvent)
        {
            await marketingDbContext.BasketChangeEvents.AddAsync(basketChangeEvent);
            await marketingDbContext.SaveChangesAsync();
        }
    }
}
