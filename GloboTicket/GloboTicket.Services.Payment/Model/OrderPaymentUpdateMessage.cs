using System;

namespace GloboTicket.Services.Payment.Model
{
    public class OrderPaymentUpdateMessage
    {
        public Guid OrderId { get; set; }
        public bool PaymentSuccess { get; set; }
    }
}
