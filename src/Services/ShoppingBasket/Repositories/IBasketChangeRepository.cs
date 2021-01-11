using ShoppingBasket.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ShoppingBasket.Repositories
{
    public interface IBasketChangeRepository
    {
        Task AddBasketChange(BasketChange basketChange);
        Task<List<BasketChange>> GetBasketChanges(DateTime startDate, int max);
    }
}
