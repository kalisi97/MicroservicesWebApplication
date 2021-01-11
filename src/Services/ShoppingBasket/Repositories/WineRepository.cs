
using Microsoft.EntityFrameworkCore;
using ShoppingBasket.DbContexts;
using ShoppingBasket.Entities;
using System;
using System.Threading.Tasks;

namespace ShoppingBasket.Repositories
{
    public class WineRepository : IWineRepository
    {
        private readonly ShoppingBasketDbContext shoppingBasketDbContext;

        public WineRepository(ShoppingBasketDbContext shoppingBasketDbContext)
        {
            this.shoppingBasketDbContext = shoppingBasketDbContext;
        }

        public async Task<Wine> WineExists(Guid WineId)
        {
           var wine = await shoppingBasketDbContext.Wines.FirstOrDefaultAsync(e => e.WineId == WineId);
           return wine;
        }

        public void AddWine(Wine theWine)
        {
            shoppingBasketDbContext.Wines.Add(theWine);

        }

        public async Task<bool> SaveChanges()
        {
            return (await shoppingBasketDbContext.SaveChangesAsync() > 0);
        }
    }
}
