using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloboTicket.Services.Payment.Model
{
    public class OrderPaymentRequestMessage
    {
        public Guid OrderId { get; set; }
        public int Total { get; set; }
        public string CardFirstName { get; set; }
    }
}
