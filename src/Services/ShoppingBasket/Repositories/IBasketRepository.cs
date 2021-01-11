
using ShoppingBasket.Entities;
using System;
using System.Threading.Tasks;

namespace ShoppingBasket.Repositories
{
    public interface IBasketRepository
    {
        Task<bool> BasketExists(Guid basketId);

        Task<Basket> GetBasketById(Guid basketId);

        void AddBasket(Basket basket);

        Task<bool> SaveChanges();

        Task ClearBasket(Guid basketId);
        Task<Basket> GetBasketByUserId(Guid userId);
    }
}