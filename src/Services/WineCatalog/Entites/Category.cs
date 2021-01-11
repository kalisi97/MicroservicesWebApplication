using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WineCatalog.Entites
{
    public class Category
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public List<Wine> Wines { get; set; }
    }
}
