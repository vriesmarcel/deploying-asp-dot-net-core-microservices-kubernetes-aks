using System;

namespace GloboTicket.Services.Payment.Model
{
    public class OrderPaymentUpdate
    {
        public Guid OrderId { get; set; }
        public bool PaymentSuccess { get; set; }
    }
}
