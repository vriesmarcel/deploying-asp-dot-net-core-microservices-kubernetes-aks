using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GloboTicket.Services.Ordering.Entities;

namespace GloboTicket.Services.Ordering.Repositories
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetOrdersForUser(Guid userId);

        Task AddOrder(Order order);
        Task<Order> GetOrderById(Guid orderId);

        Task<bool> SaveChanges();
    }
}