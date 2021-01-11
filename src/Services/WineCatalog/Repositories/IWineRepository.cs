using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WineCatalog.Entites;

namespace WineCatalog.Repositories
{
    public interface IWineRepository
    {
        Task<IEnumerable<Wine>> GetWines(Guid categoryId);
        Task<Wine> GetWineById(Guid WineId);

        Task<bool> SaveChanges();
    }
}
