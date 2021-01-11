using System;
using System.ComponentModel.DataAnnotations;

namespace Models.Api
{
    public class BasketForCreation
    {
        [Required]
        public Guid UserId { get; set; }
    }
}
