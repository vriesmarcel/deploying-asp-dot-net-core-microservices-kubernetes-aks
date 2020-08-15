using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace GloboTicket.Services.ShoppingBasket.Entities
{
    public class Basket
    {
        public Guid BasketId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        public Collection<BasketLine> BasketLines { get; set; }

        public Guid? CouponId { get; set; }
    }
}
