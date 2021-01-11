using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WineCatalog.DbContexts;
using WineCatalog.Entites;

namespace WineCatalog.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly WineCatalogDbContext _WineCatalogDbContext;

        public CategoryRepository(WineCatalogDbContext WineCatalogDbContext)
        {
            _WineCatalogDbContext = WineCatalogDbContext;
        }


        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await _WineCatalogDbContext.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryById(string categoryId)
        {
            return await _WineCatalogDbContext.Categories.Where(x => x.CategoryId.ToString() == categoryId).FirstOrDefaultAsync();
        }
    
    }
}
