using System;
using System.ComponentModel.DataAnnotations;

namespace ShoppingBasket.Models
{
    public class BasketLineForCreation
    { 
        [Required]
        public Guid WineId { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
