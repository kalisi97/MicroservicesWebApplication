using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WineCatalog.Entites;

namespace WineCatalog.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategories();
        Task<Category> GetCategoryById(string categoryId);
    }
}
