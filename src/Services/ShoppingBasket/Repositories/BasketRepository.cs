 
using Microsoft.EntityFrameworkCore;
using ShoppingBasket.DbContexts;
using ShoppingBasket.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingBasket.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly ShoppingBasketDbContext shoppingBasketDbContext;

        public BasketRepository(ShoppingBasketDbContext shoppingBasketDbContext)
        {
            this.shoppingBasketDbContext = shoppingBasketDbContext;
        }

        public async Task<Basket> GetBasketById(Guid basketId)
        {
            return await shoppingBasketDbContext.Baskets.Include(sb => sb.BasketLines)
                .Where(b => b.BasketId == basketId).FirstOrDefaultAsync();
        }

        public async Task<bool> BasketExists(Guid basketId)
        {
            return await shoppingBasketDbContext.Baskets
                .AnyAsync(b => b.BasketId == basketId);
        }

        public async Task ClearBasket(Guid basketId)
        {
            var basketLinesToClear = shoppingBasketDbContext.BasketLines.Where(b => b.BasketId == basketId);
            shoppingBasketDbContext.BasketLines.RemoveRange(basketLinesToClear);

            var basket = shoppingBasketDbContext.Baskets.FirstOrDefault(b => b.BasketId == basketId);

            await SaveChanges();
        }

        public void AddBasket(Basket basket)
        {
            shoppingBasketDbContext.Baskets.Add(basket);
        }

        public async Task<bool> SaveChanges()
        {
            try
            {
                return (await shoppingBasketDbContext.SaveChangesAsync() > 0);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<Basket> GetBasketByUserId(Guid userId)
        {
            return await shoppingBasketDbContext.Baskets.Include(sb => sb.BasketLines)
               .Where(b => b.UserId == userId).FirstOrDefaultAsync();
        }
    }
}
