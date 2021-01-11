using ShoppingBasket.Entities;
using System;
using System.Threading.Tasks;

namespace ShoppingBasket.Services
{
    public interface IWineCatalogService
    {
        Task<Wine> GetWine(Guid id);
    }
}