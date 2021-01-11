using Models.Api;
using System;
using System.Collections.Generic;

namespace Models.View
{
    public class WineListModel
    {
        public IEnumerable<Wine> Wines { get; set; }
        public Guid SelectedCategory { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public int NumberOfItems { get; set; }
    }
}
