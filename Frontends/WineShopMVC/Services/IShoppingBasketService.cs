using Models.Api;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public interface IShoppingBasketService
    {
        Task<BasketLine> AddToBasket(Guid basketId, BasketLineForCreation basketLine);
        Task<IEnumerable<BasketLine>> GetLinesForBasket(Guid basketId);
        Task<Basket> GetBasket(Guid basketId);
        Task UpdateLine(Guid basketId, BasketLineForUpdate basketLineForUpdate);
        Task RemoveLine(Guid basketId, Guid lineId);
        Task<BasketForCheckout> Checkout(Guid basketId, BasketForCheckout basketForCheckout);
        Task<Basket> GetBasketForUser(Guid userId);
        Task<Basket> CreateBasketForUser(Guid userId);
        Task<Coupon> GetDiscountForBasket(Guid userId);
    }
}
