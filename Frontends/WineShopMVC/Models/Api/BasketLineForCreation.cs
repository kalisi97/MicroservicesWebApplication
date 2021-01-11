using System;
using System.ComponentModel.DataAnnotations;

namespace Models.Api
{
    public class BasketLineForCreation
    {
        [Required]
        public Guid WineId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}
