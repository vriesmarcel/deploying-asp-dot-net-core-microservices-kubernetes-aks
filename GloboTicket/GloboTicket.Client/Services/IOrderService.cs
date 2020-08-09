using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GloboTicket.Web.Models.View;

namespace GloboTicket.Web.Services
{
    public interface IOrderService
    {
        Task<List<OrderViewModel>> GetOrdersForUser(int userId);
        Task<OrderViewModel> GetOrderDetails(int orderId);
    }
}
