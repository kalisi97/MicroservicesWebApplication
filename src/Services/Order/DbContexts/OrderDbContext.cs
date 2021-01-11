
using Microsoft.EntityFrameworkCore;

namespace Ordering.DbContexts
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options)
            : base(options)
        {
        }

        public DbSet<Entities.Order> Orders { get; set; }
        
    }
}
