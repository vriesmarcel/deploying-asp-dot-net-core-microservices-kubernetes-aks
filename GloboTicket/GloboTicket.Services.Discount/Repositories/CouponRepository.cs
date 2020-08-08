using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GloboTicket.Services.Discount.DbContexts;
using GloboTicket.Services.Discount.Entities;
using Microsoft.EntityFrameworkCore;

namespace GloboTicket.Services.Discount.Repositories
{
    public class CouponRepository: ICouponRepository
    {
        private readonly DiscountDbContext _discountDbContext;

        public CouponRepository(DiscountDbContext discountDbContext)
        {
            _discountDbContext = discountDbContext;
        }

        public async Task<Coupon> GetCouponByCode(string couponCode)
        {
            return await _discountDbContext.Coupons.Where(x => x.Code == couponCode).FirstOrDefaultAsync();
        }

        public async Task UseCoupon(string couponCode)
        {
            var couponToUpdate =
                await _discountDbContext.Coupons.Where(x => x.Code == couponCode).FirstOrDefaultAsync();

            if(couponToUpdate == null)
                throw new Exception();//TODO custom exception

            couponToUpdate.AlreadyUsed = true;
            await _discountDbContext.SaveChangesAsync();
        }
    }
}
