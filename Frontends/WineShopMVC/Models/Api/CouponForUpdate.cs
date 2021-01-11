using System;
using System.ComponentModel.DataAnnotations;

namespace Models.Api
{
    public class CouponForUpdate
    {
        [Required]
        public Guid CouponId { get; set; }
    }
}
