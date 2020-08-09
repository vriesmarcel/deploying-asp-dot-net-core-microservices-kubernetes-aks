using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GloboTicket.Services.Ordering.DbContexts;
using GloboTicket.Services.Ordering.Entities;
using Microsoft.EntityFrameworkCore;

namespace GloboTicket.Services.Ordering.Repositories
{
    public class OrderRepository: IOrderRepository
    {
        private readonly OrderDbContext _orderDbContext;

        public OrderRepository(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }

        public async Task<List<Order>> GetOrdersForUser(Guid userId)
        {
            return await _orderDbContext.Orders.Where(o => o.UserId == userId).ToListAsync();
        }

        public async Task AddOrder(Order order)
        {
            await _orderDbContext.Orders.AddAsync(order);
            await _orderDbContext.SaveChangesAsync();
        }

        public async Task<Order> GetOrderById(Guid orderId)
        {
            return await _orderDbContext.Orders.Where(o => o.Id == orderId).FirstOrDefaultAsync();
        }

        public async Task<bool> SaveChanges()
        {
            return (await _orderDbContext.SaveChangesAsync() > 0);
        }
    }
}
