using System;

namespace Models.View
{
    public class BasketLineViewModel
    {
        public Guid LineId { get; set; }
        public Guid WineId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
