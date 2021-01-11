using System;

namespace Ordering.Messages
{
    public class BasketLineMessage
    {
        public Guid BasketLineId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
   }
}
