using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WineCatalog.DbContexts;
using WineCatalog.Entites;

namespace WineCatalog.Repositories
{
    public class WineRepository : IWineRepository
    {
        private readonly WineCatalogDbContext _WineCatalogDbContext;

        public WineRepository(WineCatalogDbContext WineCatalogDbContext)
        {
            _WineCatalogDbContext = WineCatalogDbContext;
        }

        public async Task<IEnumerable<Wine>> GetWines(Guid categoryId)
        {
            return await _WineCatalogDbContext.Wines
                .Include(x => x.Category)
                .Where(x => (x.CategoryId == categoryId || categoryId == Guid.Empty)).ToListAsync();
        }

        public async Task<Wine> GetWineById(Guid WineId)
        {
            return await _WineCatalogDbContext.Wines.Include(x => x.Category).Where(x => x.WineId == WineId).FirstOrDefaultAsync();
        }

        public async Task<bool> SaveChanges()
        {
            return (await _WineCatalogDbContext.SaveChangesAsync() > 0);
        }
    }
}
