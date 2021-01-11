using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShoppingBasket.DbContexts;
using ShoppingBasket.Entities;

namespace ShoppingBasket.Repositories
{
    public class BasketChangeRepository: IBasketChangeRepository
    {
        private readonly ShoppingBasketDbContext shoppingBasketDbContext;

        public BasketChangeRepository(ShoppingBasketDbContext shoppingBasketDbContext)
        {
            this.shoppingBasketDbContext = shoppingBasketDbContext;
        }

        public async Task AddBasketChange(BasketChange basketChange)
        {
            await shoppingBasketDbContext.BasketChanges.AddAsync(basketChange);
            await shoppingBasketDbContext.SaveChangesAsync();
        }

        public async Task<List<BasketChange>> GetBasketChanges(DateTime startDate, int max)
        {
            return await shoppingBasketDbContext.BasketChanges.Where(b => b.InsertedAt > startDate)
                .OrderBy(b => b.InsertedAt).Take(max).ToListAsync();
        }
    }
}
