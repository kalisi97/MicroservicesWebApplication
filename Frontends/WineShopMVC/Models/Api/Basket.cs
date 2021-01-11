using System;

namespace Models.Api
{
    public class Basket
    {
        public Guid BasketId { get; set; }
        public Guid UserId { get; set; }
        public int NumberOfItems { get; set; }
        public string Code { get; set; }
        public decimal Discount { get; set; }
        public Guid? CouponId { get; set; }
    }
}
