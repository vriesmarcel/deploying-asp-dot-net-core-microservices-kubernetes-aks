using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GloboTicket.Web.Models.View;

namespace GloboTicket.Web.Services
{
    public class OrderService: IOrderService

    {
        public Task<List<OrderViewModel>> GetOrdersForUser(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<OrderViewModel> GetOrderDetails(int orderId)
        {
            throw new NotImplementedException();
        }
    }
}
