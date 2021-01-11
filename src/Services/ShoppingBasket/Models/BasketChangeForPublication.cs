using ShoppingBasket.Entities;
using System;

namespace ShoppingBasket.Models
{
    public class BasketChangeForPublication
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid WineId { get; set; }
        public DateTimeOffset InsertedAt { get; set; }
        public BasketChangeTypeEnum BasketChangeType { get; set; }
    }
}
