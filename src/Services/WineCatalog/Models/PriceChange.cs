using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WineCatalog.Models
{
    public class PriceChange
    {
        public Guid WineId { get; set; }
        public decimal Price { get; set; }
    }
}
