using System.Threading.Tasks;

namespace ShoppingBasket.Repositories
{
    public interface IBasketLinesIntegrationRepository
    {
        Task UpdatePrices(Models.PriceChanged priceUpdate);
    }
}
