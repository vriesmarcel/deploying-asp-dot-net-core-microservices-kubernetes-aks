using GloboTicket.Services.ShoppingBasket.DbContexts;
using GloboTicket.Services.ShoppingBasket.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GloboTicket.Services.ShoppingBasket.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly ShoppingBasketDbContext shoppingBasketDbContext;

        public BasketRepository(ShoppingBasketDbContext shoppingBasketDbContext)
        {
            this.shoppingBasketDbContext = shoppingBasketDbContext;
        }

        public async Task<Basket> GetBasketById(Guid basketId)
        {
            return await shoppingBasketDbContext.Baskets.Include(sb => sb.BasketLines)
                .Where(b => b.BasketId == basketId).FirstOrDefaultAsync();
        }

        public async Task<bool> BasketExists(Guid basketId)
        {
            return await shoppingBasketDbContext.Baskets
                .AnyAsync(b => b.BasketId == basketId);
        }

        public async Task ClearBasket(Guid basketId)
        {
            var basketLinesToClear = shoppingBasketDbContext.BasketLines.Where(b => b.BasketId == basketId);
            shoppingBasketDbContext.BasketLines.RemoveRange(basketLinesToClear);

            var basket = shoppingBasketDbContext.Baskets.FirstOrDefault(b => b.BasketId == basketId);
            if (basket != null) basket.CouponId = null;

            await SaveChanges();
        }

        public void AddBasket(Basket basket)
        {
            shoppingBasketDbContext.Baskets.Add(basket);
        }

        public async Task<bool> SaveChanges()
        {
            return (await shoppingBasketDbContext.SaveChangesAsync() > 0);
        }
    }
}
