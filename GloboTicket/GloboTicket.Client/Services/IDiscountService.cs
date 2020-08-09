using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloboTicket.Web.Services
{
    public interface IDiscountService
    {
        Task<int> GetDiscountForCode(string code);
        Task ApplyCode(string code);
    }
}
