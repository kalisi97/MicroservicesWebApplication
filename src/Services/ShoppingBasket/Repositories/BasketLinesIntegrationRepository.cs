using ShoppingBasket.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingBasket.Repositories
{
    public class BasketLinesIntegrationRepository: IBasketLinesIntegrationRepository
    {
        private readonly DbContextOptions<ShoppingBasketDbContext> dbContextOptions;


        public BasketLinesIntegrationRepository(DbContextOptions<ShoppingBasketDbContext> dbContextOptions)
        {
            this.dbContextOptions = dbContextOptions;
        }

        public async Task UpdatePrices(Models.PriceChanged priceUpdate)
        {
            await using (var shoppingBasketDbContext = new ShoppingBasketDbContext(dbContextOptions))
            {
                var basketLinesToUpdate = shoppingBasketDbContext.BasketLines.Where(x => x.WineId == priceUpdate.WineId);

                await basketLinesToUpdate.ForEachAsync((basketLineToUpdate) =>
                    basketLineToUpdate.Price = priceUpdate.Price
                );
                await shoppingBasketDbContext.SaveChangesAsync();
            }
        }
    }
}
