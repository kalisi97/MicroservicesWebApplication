using System;

namespace Models.Api
{
    public class BasketLine
    {
        public Guid BasketLineId { get; set; }
        public Guid BasketId { get; set; }
        public Guid WineId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Wine Wine { get; set; }
    }
}
