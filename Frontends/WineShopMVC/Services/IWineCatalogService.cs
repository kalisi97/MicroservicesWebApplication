using Models.Api;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WineShopMVC.Models.Api;

namespace Services
{
    public interface IWineCatalogService
    {
        Task<IEnumerable<Wine>> GetAll();
        Task<IEnumerable<Wine>> GetByCategoryId(Guid categoryid);
        Task<Wine> GetWine(Guid id);
        Task<IEnumerable<Category>> GetCategories();
        Task<PriceChange> UpdatePrice(PriceChange priceChange);
    }
}
