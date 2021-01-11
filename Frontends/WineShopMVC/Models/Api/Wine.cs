using System;

namespace Models.Api
{
    public class Wine
    {
        public Guid WineId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Producer { get; set; }
        public string CountryOfOrigin { get; set; }
        public string Description { get; set; }
        public string ImageFile { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
