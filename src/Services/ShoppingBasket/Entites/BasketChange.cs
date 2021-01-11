using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingBasket.Entities
{
    public class BasketChange
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid WineId { get; set; }
        public DateTimeOffset InsertedAt { get; set; }
        public BasketChangeTypeEnum BasketChangeType { get; set; }
    }
}
