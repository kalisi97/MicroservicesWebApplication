using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WineShopMVC.Models.Api
{
    public class PriceChange
    {
        public Guid WineId { get; set; }
        public decimal Price { get; set; }
    }
}
