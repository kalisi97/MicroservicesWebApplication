using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WineShopMVC.Models
{
    public class Settings
    {
        public string BasketIdCookieName => "BasketId";

        public Guid  BasketId { get; set; }
    }
}
