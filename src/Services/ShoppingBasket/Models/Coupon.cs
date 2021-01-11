using System;

namespace ShoppingBasket.Models
{
    public class Coupon
    {
        public Guid CouponId { get; set; }
        public string Code { get; set; }
        public decimal Amount { get; set; }
        public bool AlreadyUsed { get; set; }
    }
}
