﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discounts.Entites
{
    public class Coupon
    {
        public Guid CouponId { get; set; }
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
        public bool AlreadyUsed { get; set;}

      
    }
}
