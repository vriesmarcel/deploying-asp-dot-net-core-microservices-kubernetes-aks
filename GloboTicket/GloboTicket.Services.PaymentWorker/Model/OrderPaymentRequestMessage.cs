using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Services.PaymentWorker.Model
{
    public class OrderPaymentRequestMessage
    {
        public Guid OrderId { get; set; }
        public int Total { get; set; }
        public string CardFirstName { get; set; }
    }
}
