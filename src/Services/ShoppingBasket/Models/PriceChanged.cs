using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingBasket.Models
{
    public class PriceChanged
    {
        public Guid WineId { get; set; }

        public decimal Price { get; set; }
    }
}
