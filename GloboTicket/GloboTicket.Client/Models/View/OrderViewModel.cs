using System;

namespace GloboTicket.Web.Models.View
{
    public class OrderViewModel
    {
        public Guid OrderId { get; set; }
        public DateTime OrderPlaced { get; set; }
        public bool Paid { get; set; }
        public int OrderTotal { get; set; }
    }
}
