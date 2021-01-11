using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WineCatalog.Entites;

namespace WineCatalog.DbContexts
{
    public class WineCatalogDbContext : DbContext
    {
        public WineCatalogDbContext(DbContextOptions<WineCatalogDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Wine> Wines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var redWineGuid = Guid.Parse("{B0788D2F-8003-43C1-92A4-EDC76A7C5DDE}");
            var whiteWineGuid = Guid.Parse("{6313179F-7837-473A-A4D5-A5571B43E6A6}");
            var roseWineGuid = Guid.Parse("{BF3F3002-7E53-441E-8B76-F6280BE284AA}");
            var sparklingWineGuid = Guid.Parse("{FE98F549-E790-4E9F-AA16-18C2292A2EE9}");

            modelBuilder.Entity<Category>().HasData(new Category
            {
                CategoryId = redWineGuid,
                Name = "Red wines"
            });
            modelBuilder.Entity<Category>().HasData(new Category
            {
                CategoryId = whiteWineGuid,
                Name = "White wines"
            });
            modelBuilder.Entity<Category>().HasData(new Category
            {
                CategoryId = roseWineGuid,
                Name = "Rose wines"
            });
            modelBuilder.Entity<Category>().HasData(new Category
            {
                CategoryId = sparklingWineGuid,
                Name = "Sparkling wines"
            });

            modelBuilder.Entity<Wine>().HasData(new Wine
            {
                WineId = Guid.Parse("{EE272F8B-6096-4CB6-8625-BB4BB2D89E8B}"),
                ImageFile = "http://www.toplickivinogradi.com/images/product/small/Prokupac-2018.jpg",
                Price = 10.00M,
                Name = "Toplički Vinogradi Epigenia Prokupac 0.75l",
                CountryOfOrigin = "Serbia",
                Producer = "Vinarija Toplički Vinogradi",
                Description = "Intense red color with shades of purple. In the nose, primary aromas are more dominant than tertiary ones. Well-packaged aromas from black and red fruits (blackberries, raspberries and gooseberries) are complemented by aromas from barrels (black pepper, cinnamon and toast). In the mouth it has an elegant structure, medium consistency, with mature and velvety tannins. Nice acidity and fine finish with medium durability. Polyvalent wine, it is excellent in combination with meat or fish.",
                CategoryId = redWineGuid
            });

            modelBuilder.Entity<Wine>().HasData(new Wine
            {
                WineId = Guid.Parse("{3448D5A4-0F72-4DD7-BF15-C14A46B26C00}"),
                ImageFile = "data:image/jpeg;base64,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",
                Price = 14.00M,
                Name = "Doja Cabernet Sauvignon 50% Merlot 50% 0.75l",
                CategoryId = redWineGuid,
                CountryOfOrigin = "Serbia",
                Producer = "Vinarija Doja Blace",
                Description = "Cabernet Sauvignon 50% Merlot 50%. The land, which is a combination of lime, gravel and deeper layers of clay and mica, shapes our Cabernet and Merlot wines into wines with an intense and fruity character. Cabernet-Merlot blend is a special selection from our vineyards which, following the example of French baths, is aged for a year in new French barrels and then stored for 6 months in bottles until it is fully ready for market."
            });

            modelBuilder.Entity<Wine>().HasData(new Wine
            {
                WineId = Guid.Parse("{B419A7CA-3321-4F38-BE8E-4D7B6A529319}"),
                ImageFile = "https://winestyle.rs/wp-content/uploads/2020/01/izbor-2019-Kostic.jpg",
                Price = 10.00M,
                Name = "Kostić Prokupac Barrique 0.75l",
                CountryOfOrigin = "Serbia",
                Producer = "Vinarija Kostić",
                CategoryId = redWineGuid,
                Description = "Not so dark red color and with the perception of glowing when on the light. On the nose extensive blackberry and cherry notes with fine traces of smoke and coffee. Also, the fresh oak wood note is distinctive. Spicy, smooth and well balanced on the palate. Pleasant medium acidity and low on tannins. Creamy after taste with some dark fruit that stays quite a long in your mouth. Must be decanted for 30 minutes at least. When tried directly from the bottle it is a bit of harsh experience."
            
            });

            modelBuilder.Entity<Wine>().HasData(new Wine
            {
                WineId = Guid.Parse("{62787623-4C52-43FE-B0C9-B7044FB5929B}"),
                ImageFile = "http://www.toplickivinogradi.com/images/product/medium/tribus-villa-charonnay.jpg",
                Price = 15.8M,
                Name = "Toplički Vinogradi Epigenia Chardonnay 0.75l",
                CountryOfOrigin = "Serbia",
                Producer = "Vinarija Toplički Vinogradi",
                CategoryId = whiteWineGuid,
                Description = "White wine made from the Chardonnay variety of the Toplicki vinogradi winery. Made from exceptional fruit, this wine has a yellow color with green tones. Nice and pleasant smell."
            
            });

            modelBuilder.Entity<Wine>().HasData(new Wine
            {
                WineId = Guid.Parse("{1BABD057-E980-4CB3-9CD2-7FDD9E525668}"),
                ImageFile = "https://www.restoran-neptun.rs/image/spasic-tamnjanika.jpg",
                Price = 10.22M,
                CategoryId = whiteWineGuid,
                CountryOfOrigin = "Serbia",
                Producer = "Vinarija Spasić",
                Name = "Spasić Temjanika Dry White 0.75l",
                Description = "Rich, layered and soft wine dominated by aromas of grapefruit, lime and gooseberry, complemented by the aromas of ripe peaches and passion fruit with gentle notes of sweet spices. RECOMMENDATION UZ: Sea bream with pesto sauce, ravioli stuffed with celery and shrimp, pasta with vegetables and Mediterranean herbs, chicken with asparagus; Serving temperature: 10-12 ° C."
            
            });

            modelBuilder.Entity<Wine>().HasData(new Wine
            {
                WineId = Guid.Parse("{ADC42C09-08C1-4D2C-9F96-2D15BB1AF299}"),
                ImageFile = "https://vinskivodic.rs/pub/worker/rodoslov-reserve-aleksandrovic.jpg",
                Price = 27.22M,
                CategoryId = redWineGuid,
                CountryOfOrigin = "Serbia",
                Producer = "Vinarija Aleksandrović",
                Name = "Aleksandrović Rodoslov 0.75l",
                Description = "Very rich and complex aromatic complex composed of tones of cherries, blackberries and black mulberries, with shades of dark chocolate and tobacco leaves. The wine is extremely extractive, the taste is soft, and the tannins are sweet and juicy. The finish of the taste is rounded, harmonious, long-lasting and fruity, with gentle tones of oak. The pedigree is a rich wine with great potential for long marking. FOOD Excellent with duck, turkey, grilled and roasted red meat as well as lamb. Ideal with: roasted turkey; barbecue; all kinds of livers; roast beef; all kinds of steak and steak tartare; all goat and lamb specialties; red meat in red wine sauce. Remarkable with dark chocolate. SERVING Temperature: 16 - 18ºC"
            });

            modelBuilder.Entity<Wine>().HasData(new Wine
            {
                WineId = Guid.Parse("{ADC42C08-08C1-4D2C-9F96-2D15BB1AF299}"),
                ImageFile = "https://vinoitakoto.com/wp-content/uploads/2015/02/Untitled-1-247.gif",
                Price = 17.22M,
                CategoryId = whiteWineGuid,
                CountryOfOrigin = "Serbia",
                Producer = "Podrum Radovanović",
                Name = "Chardonney Radovanović 0.75l",
                Description = "It fermented and then aged in oak barrels for 12 months, which resulted in great complexity. Golden yellow with a greenish tinge. The intense and complex aroma recognizes a wide range of aromas: wildflowers, chamomile, cut grass, basil, hazelnuts, biscuits, baked bread, ripe yellow apples and mangoes, rounded with a note of oak. The taste is nicely permeated with notes of citrus and ripe apricot, with a vanilla tone of oak and minerality. Lots, buttery, perfectly balanced, very stable and complex wine, with calm and pleasant acids. Elegance and harmony are expressed in the finish."
            });

            modelBuilder.Entity<Wine>().HasData(new Wine
            {
                WineId = Guid.Parse("{ADC54A08-07C1-4D2C-9F96-2D15BB1AF299}"),
                ImageFile = "https://www.vinskivodic.rs/pub/worker/varijanta-aleksandrovic.jpg",
                Price = 17.22M,
                CategoryId = roseWineGuid,
                CountryOfOrigin = "Serbia",
                Producer = "Vinarija Aleksandrović",
                Name = "Varijanta Aleksandrović Muskat Hamburg 0.75l",
                Description = "Crystal bright ruby ​​color. Impressive scent of wild strawberries, with tones of wild rose, ratluk and sweet spices. Refreshing, fruity, very drinkable wine, with a long aromatic finish. In terms of structure and strength, the wine is at the very transition between rosy red wine. FOOD Excellent with Serbian appetizers (prosciutto, cheese), barbecue, as well as with fruit desserts. Ideal with: dried meat; sausages and bacon; roast pork; fruit salads."
            });

            modelBuilder.Entity<Wine>().HasData(new Wine
            {
                WineId = Guid.Parse("{ADC54A08-07C1-4D2C-9F96-2D15BB1AF211}"),
                ImageFile = "data:image/jpeg;base64,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",
                Price = 8.00M,
                CategoryId = roseWineGuid,
                CountryOfOrigin = "Serbia",
                Producer = "Vinarija Radovanović Krnjevo",
                Name = "Rose Radovanović  0.75l",
                Description = "The pronounced pink color of the wine indicates the richness of aromas. The taste of ripe red fruit is dominant, and as the impression goes on, the notes of herbs are repeated. Delicate wine, pleasant acidity, moderate fullness. Ideal as a transition from white to red wines."
            });
        }
    }
}
