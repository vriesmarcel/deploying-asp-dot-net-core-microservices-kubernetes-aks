using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloboTicket.Services.Discount.Models
{
    public class CouponDto
    {
        public int Amount { get; set; }
        public bool AlreadyUsed { get; set; }
    }
}
