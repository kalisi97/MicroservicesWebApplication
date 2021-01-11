using System.ComponentModel.DataAnnotations;

namespace ShoppingBasket.Models
{
    public class BasketLineForUpdate
    {
        [Required]
        public int Quantity { get; set; }
    }
}
