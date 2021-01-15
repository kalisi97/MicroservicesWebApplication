using System;

namespace Models.Api
{
    public class Coupon
    {
        public Guid CouponId { get; set; }

        public decimal Amount { get; set; }
        public bool AlreadyUsed { get; set; }
    }
}
