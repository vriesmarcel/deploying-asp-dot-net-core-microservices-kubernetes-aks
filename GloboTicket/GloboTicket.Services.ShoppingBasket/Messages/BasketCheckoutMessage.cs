using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GloboTicket.Integration.Messages;

namespace GloboTicket.Services.ShoppingBasket.Messages
{
    public class BasketCheckoutMessage: IntegrationBaseMessage
    {
        public Guid BasketId { get; set; }
        public string FirstName { get; set; }
    }
}
