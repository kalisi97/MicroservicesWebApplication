using Discounts.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discounts.Repositories
{
    public interface ICouponRepository
    {
        Task<Coupon> GetCouponByUserId(Guid userId);
        Task<Coupon> GetCouponById(Guid couponId);
    }
}
