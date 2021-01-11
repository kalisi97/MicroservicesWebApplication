
using ShoppingBasket.Entities;
using System;
using System.Threading.Tasks;

namespace ShoppingBasket.Repositories
{
    public interface IWineRepository
    {
        void AddWine(Wine wine);
        Task<Wine> WineExists(Guid wineId);
        Task<bool> SaveChanges();
    }
}