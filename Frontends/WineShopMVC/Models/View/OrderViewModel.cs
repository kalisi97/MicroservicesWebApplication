using Models.Api;
using System.Collections.Generic;

namespace Models.View
{
    public class OrderViewModel
    {
        public IEnumerable<Order> Orders { get; set; }
    }
}
