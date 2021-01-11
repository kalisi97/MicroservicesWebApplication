using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WineCatalog.Entites
{
    public class Wine
    {

        [Required]
        public Guid WineId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageFile { get; set; }
        public decimal Price { get; set; }
        public string Producer { get; set; }
        public string CountryOfOrigin { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public override bool Equals(object obj)
        {
            if (obj is Wine)
            {
                Wine Wine = obj as Wine;
                if (Wine.WineId == this.WineId) return true;
                if (Wine.Name == this.Name && Wine.Category == this.Category
                    && Wine.Producer == this.Producer && Wine.CountryOfOrigin == this.CountryOfOrigin)
                    return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() ^ Category.GetHashCode() ^ Producer.GetHashCode() ^ CountryOfOrigin.GetHashCode();
        }
    }
}
