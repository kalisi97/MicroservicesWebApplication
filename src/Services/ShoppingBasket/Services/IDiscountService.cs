using ShoppingBasket.Models;
using System;
using System.Threading.Tasks;

namespace ShoppingBasket.Services
{
    public interface IDiscountService
    {
        Task<Coupon> GetCoupon(Guid userId);
        Task ChangeCouponStatus(Coupon coupon);
    }
}
