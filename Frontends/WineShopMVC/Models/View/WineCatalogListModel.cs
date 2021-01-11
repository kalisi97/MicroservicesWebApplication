using Models.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WineShopMVC.Models.View
{
    public class WineCatalogListModel
    {
        public IEnumerable<Wine> Wines { get; set; }
        public Guid SelectedCategory { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
