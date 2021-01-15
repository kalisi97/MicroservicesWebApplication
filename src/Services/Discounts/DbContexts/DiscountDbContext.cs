using Discounts.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discounts.DbContexts
{
    public class DiscountDbContext : DbContext
    {
        public DiscountDbContext(DbContextOptions<DiscountDbContext> options) : base(options)
        {

        }

        public DbSet<Coupon> Coupons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                CouponId = Guid.NewGuid(),
                Amount = 10,
                UserId = Guid.Parse("{be936f3d-ddd8-4a5c-92c2-e91b9a25a702}"),
                AlreadyUsed = false
            });

            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                CouponId = Guid.NewGuid(),
                Amount = 5,
                UserId = Guid.Parse("{849c17ab-45df-4ffd-835b-1d4ed8a8f486}"),
                AlreadyUsed = false
            });
        }
    }
}
