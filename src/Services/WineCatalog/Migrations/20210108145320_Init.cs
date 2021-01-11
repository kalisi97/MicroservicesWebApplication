﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WineCatalog.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Wines",
                columns: table => new
                {
                    WineId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ImageFile = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    Producer = table.Column<string>(nullable: true),
                    CountryOfOrigin = table.Column<string>(nullable: true),
                    CategoryId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wines", x => x.WineId);
                    table.ForeignKey(
                        name: "FK_Wines_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Name" },
                values: new object[,]
                {
                    { new Guid("b0788d2f-8003-43c1-92a4-edc76a7c5dde"), "Red wines" },
                    { new Guid("6313179f-7837-473a-a4d5-a5571b43e6a6"), "White wines" },
                    { new Guid("bf3f3002-7e53-441e-8b76-f6280be284aa"), "Rose wines" },
                    { new Guid("fe98f549-e790-4e9f-aa16-18c2292a2ee9"), "Sparkling wines" }
                });

            migrationBuilder.InsertData(
                table: "Wines",
                columns: new[] { "WineId", "CategoryId", "CountryOfOrigin", "Description", "ImageFile", "Name", "Price", "Producer" },
                values: new object[,]
                {
                    { new Guid("ee272f8b-6096-4cb6-8625-bb4bb2d89e8b"), new Guid("b0788d2f-8003-43c1-92a4-edc76a7c5dde"), "Serbia", "Intense red color with shades of purple. In the nose, primary aromas are more dominant than tertiary ones. Well-packaged aromas from black and red fruits (blackberries, raspberries and gooseberries) are complemented by aromas from barrels (black pepper, cinnamon and toast). In the mouth it has an elegant structure, medium consistency, with mature and velvety tannins. Nice acidity and fine finish with medium durability. Polyvalent wine, it is excellent in combination with meat or fish.", "http://www.toplickivinogradi.com/images/product/small/Prokupac-2018.jpg", "Toplički Vinogradi Epigenia Prokupac 0.75l", 10.00m, "Vinarija Toplički Vinogradi" },
                    { new Guid("3448d5a4-0f72-4dd7-bf15-c14a46b26c00"), new Guid("b0788d2f-8003-43c1-92a4-edc76a7c5dde"), "Serbia", "Cabernet Sauvignon 50% Merlot 50%. The land, which is a combination of lime, gravel and deeper layers of clay and mica, shapes our Cabernet and Merlot wines into wines with an intense and fruity character. Cabernet-Merlot blend is a special selection from our vineyards which, following the example of French baths, is aged for a year in new French barrels and then stored for 6 months in bottles until it is fully ready for market.", "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQAAAQABAAD/2wCEAAkGBxAPEA4PDg8ODw8PEBANDQ4NDg8PEA0NFhIWFhURFRUYICggGBolHhUVITEhJTUtLjowFyAzODMtNygvLisBCgoKDg0OFxAQGCseHR0vLTcrLTc3NS0rNzcuKysrLS0tLS0rKy0tKy01MC8rNy0rLDIzMy0uLi04ODg1Ky0tN//AABEIAVMAlQMBIgACEQEDEQH/xAAcAAEAAQUBAQAAAAAAAAAAAAAABQIDBAYHAQj/xABTEAABAwICBAkECw0FCQEAAAABAAIDBBEFEgYhMXEHEyIyM0FRcrFhgZGyCBQjNXN0kpSzwdEVFiRCRVJUYmSCk6HCoqPDxNJDRFNVdYOE4fAX/8QAGgEBAQEBAQEBAAAAAAAAAAAAAAEFBAMCBv/EACoRAQACAQIEBQMFAAAAAAAAAAABAgMEEQUSMTNBUXGR8CGBwRQjMkJS/9oADAMBAAIRAxEAPwDuKIiAiIgIiIC1XhF0imw+mifThnGzTtga6QFzYxkc4uy9Z5NvOtqXL+F97n1GG05J4p3GyuaNV5AWtDr7dQc4edHRpaRfLWsoHSDTbFYA0srRc7bU1Pa/ku1QA4TsZvb24Pm1N/oVrTGmDOLDc1tmtzneK1O2vzo1M+HHW+0Vh0eh4ScWFi6enl8klM0D+wWldowHEPbVLTVJbkM8McxZe+QuaCRfr2r5ppIh2u+UV3jgsqXyYZAHm/FPlp2G1jxUby1gO4WHmUcusw1rji1Y2+rbURFWaIiICIiAiIgIiICIiAiIgLmvDFTgHDp2lwkEr4BsLcjgHE2PXdo9JXSlzvhi5mHD9pd6iOnR9+rm2mTCAwkl3lIA8FqDecN4W56bnVHvWmM5zd48Unq2dT/NN0zDq1/yXeeDSlEeF0liSZWunfe3PkcXEDya1wmDq3rv2gHvZQfAMUhycQ+mOvq2BERVkCIiAiIgIiICIiAiIgIiIC51wwbMNH7Q/wBULoq5zwwH3s+Hk9VqOnR9+rnOnR6Ib1qUcLyONDHmNrgHSBpyBxNgM2y/kW2aff7Lc5YmOxvMTXzyTPNwIHzUs9OHAkHKNZYOS24Fhsuk9Wvqp2yLNN1Lv3B/714f8XZ4LgFKu/8AB/714d8Wi9VSHLxDt19WwIiKskREQEREBERAREQEREBERAXOOGDbhfw0vgxdHXNuGA8vCvhJz9Ejq0Xfr88HOtPTri852XUViTYAJDA5rpnPb7caJpXhhz3OQOjbduewuS47LajdSenDw7iyNeojwUDNi1RNyJZXPa5wLmlrACQb31BJ6tbUx+4kacLv2gPvXh3xWL1VwGFy79oB714d8Vi9UKQ5uIduvqn0RFWQIiICIiAiIgIiICIiAiIgLmnDI28mBN/OxAN9LQulrnHC902Af9Sb1X/FQhzLhqpDTvocpLc7Zicuq9izs3rndBK7jY7uceUOsrp/D+678N7OLn9N41yuj6SPvDxR9Tad3QaQckebxXfdAPerCviFJ9C1cBoDqb3m+IXf9AferCviFJ9AxC0p5ERHyIiICIiAiIgIiICIiAiIgLmHDBVWrNHYhtdWPlI/VbxYPrldIrqyKnjfNPIyKKNpfJJIQ1rG9pK+XuEPTr7oYoyrgzCCkyx0gddpc1rszpCOouJPmtfYgk+G+bNJQ69jJtV9mti5tTc9neb4rP0hxyWul4yU6mjLG3bkb2KMaSDcGxGwost+w14AaS46iDrK+h9BCDheGW2Cipm+cRNBH8l8m0uMSN1O5Q7dhXb+A7TuGSIYXUPDJo3vdRl5sJ4nOL+KBP47STYdlrbChu7AiIiCIiAiIgIiICIiAiIgLAxzF4KGnlqqp4jhiGZzjtJ6mtHW4nUAs9fMvDXpocQrHUsLvwOie5jbHVNUjkvlPaBra3yXI5yCI4Q9P6nGJTmLoqRjiaela7kjsfJbnP8AL1XsOu+nLofBnwfOxAiqqQW0rScjNhncOvyNB9NlDcJWCiirnsYAI3ta9ltQ7CLeZF2aqi8XqILLoadz3cklpbyg4dTupYrGkkAaydQWy4dAI2269rj2lFh1bgo4T3ufHhmLPvKbMpKuQ65TsbFIetx2B3XsOvWezL5FrYGzNLTYOGtp7Cu98DumDsSozFUOvWURbDOTtljN+Ll8pIBB8rb9aI39ERAREQEREBERAREQazwk48cPwusqWG0uTiYCNomkORrh3b5v3V8r6M4O6uq6elZcca8Nc4fixjW93mAK7X7JOuy0uH0+v3WeWfyHiow3/GWpex/w8Pramc6+JgDG+R0j9voYfSg7fR0LKaBkUTQ1kbAxrRqAaBYBfPHDFX8ZiHFXFoI2g9oe7lEHzZV9I1J5J3L5Q05qONxKvd+0yM8zDkHqoqCRERGfhVrknaNm5SZnUHSPs4eW4WVJOqrPM62Tg5xs0OLUcwNoqlwoqkdRZIQGuO52Q/urSDUL19QcuokEEOBBsQQdoQfayLFwup46CCb/AIsUcvymB31rKUQREQEREBERAREQcO9kx+SP/O/y6wfY68/Et1L/AIyzvZL7cI3Vv+XWB7Hce6Yl3abxlQdlrTyCvkzSoj29XWFvwqf6Ry+sMQ5hXyhpW69dWm1vwmbUe+UXwRKIiI9aVUXKhEHt17fUVSvUH2RoV72YZfb7RpL/AMBimlD6Gi2G4aP2Gk+gYphAREQEREBERAREQcZ9kLA2R+Fh17BtWRY22mD7FrHBxVOoXVBp8o44Mz8YM/NLrW7OcVtnD70mGd2q8YVpui3OfuCQ3uGafFk5eesT1/Le6rSmqc0gmL+H/wC1yrEMIhlmlleDmkkdI+ziBmcSTYedb1PzTuWqS8529WWrqdHp67bUj2RP3Ap/zX/LKfcCn/Nf8sqVXijl/S4f8R7Ir7gQbA19+oBx1qg4HB1B3yypmKUsc17XFrmOD2ubta4G4cNy2WTSCjmY44jQtnqxl4qakvSioZ1ulcxwudzT5keWTBir0xxPtv8APu567BIex3ylYlwmIA6nfKWz43UxPe0QU0dMxgIyNkkmeXE688jzyiPJa2tQtQdTtxR52wYtt+SIfT2hpvhuGntoaQ/3DFMKI0PFsOw4dlFSj+5YpdGBbrIiIiCIiAiIgIiIOO8PvSYZ3KrxhWnaLbX7gtw4felwzuVXrQrT9F9r9wSH6ThH9Pv+U/PzTuWqy8471tNQeSdy1eTnHerLY1fgpXi9K8Uca7BUZGyAi+YDKfzJBcB3yXP1dpB6lRnDBldqcdh64b/Wevs37LZVbai0cjNfLLSOwWvmvv1ehR52h4+cNiMRac3L6hl5RjLXA9oDT8r0xFTzXbisuRYdTzXbiq8Lxs+o9ExbD8PH7HTfQtUqozRcfgND8Up/omqTR+bt1kRERBERAREQEREHGfZC0FRM/DPa8M82VtXn4iKSTLcw2vlGrYfQsHgOwepjlrjV0tRG0sg4s1NPIwE5n3y5xu2LrOO9LBuf4hSFHsRYmY6ShcVoAYzlhBP6sQv/ACC+c9KMAxI1lUYqLECwyuLDHTVBYW9osLL6uKskq7rz283yH972K/oWJ/Nqr7F5972K/oWJ/Nqn7F9dEqklQ57eb5H+9/Ff0LEvm1T9i8OAYp+h4l82qfsX1q4rHlKHPbzfJ5wPEuukxDz09R9iodg2IddLXW67wT/YvqeYqKrzyXbkOa3m27A22paUWtaCEW2W9zCzVj4f0MPwbPVCyEfIiIgIiICIiAiIgg8e6WDuv8QpGi2KNx7pYe67xCkaHYgySrLlecrDkFJVBVRUNNVTGtNOx8bY/aoqBeLO4ScaWWvmHJ1X+tBJvWPKtfbpHK50dLljbVmulw+R4a4wtbHB7YdO1pN+VHks0nU5+0ga7tTiUsdRLSvc1xNI+sppcljyHBkjHtGo2LoyCLanEW1XIZ8yia88l25YVVjNQMOp6lrWSVMtPHVOYGkNLRHx0rWtvfmgtGva5t1k1UrXx52G7XtD2EbHNIuD6EG9UHRRfBs9UK+rFD0UXwbPVCvoCIiAiIgIiICIiCCx/pYe67xCkaDYo3H+mh7rvEKRoNiDKcrDlfcsdyCkqImw+X22apkkQBpxTCN8byRaQvz3Dh22t/NSxVJQa6dGw3i5Gy3qmVcle6dzBllmkjMUjCwHUzizkGu4ytN3WN7dbhshfUVJyyTupjSU8TTkjiY45nEvOs5nZSTbYwWBO3YnrGlQa3TYNkbAyXiZo6elipY2uiuQ9oAe+5NiHZWarasu3WsKioHU1M2Bzw8RAsjLWltoQTkYbk3yizb+RbJMomv5rtyDeqPo4+4z1QrytUnRx9xvgFdQEREBERAREQEREEBpB00Pdd4hSNBsUbpB00Pcd4hSWH7EGU9WHK+9Y7kFJVJVRVBQW3rGlWS9Y0qDCnUTiHNduUtOojEOa7cg32l5kfcb4BXVbp+Yzut8FcQEREBERAREQEREGv6Q9ND3HeKksP2KM0h6aHuHxUnh/NQZT1jlX3qwUFJVBVZVBQW3rFlWS9Y0qDCnUTiHNduUtMonEOa7cUG/wc1ndHgq1RFzW7h4KtAREQEREBERAREQa9pD08PcPrKSw7mqM0h6eHuH1lJ4dzUGU9WCr71juQUlUFVlUFBbesaRZL1jSIMKdROIc125S06iMQ5rtxQdBj2DcPBVKlmwbgqkBERAREQEREBERBrukPTxdw+spLDuao7SDp4u5/UVI4bzUGVIrBV6VWCg8KoKqKpKC09Y0iyXrHkQYUyiMQ5rtymJgomv5rkHQG7AvUCICIiAiIgIiICIiDX8e6ePuf1FXqOha53GXcHatQtbUWm9v3QrWOj3ePuf1FSGH7EFk4cGta0PcWi23abFWPaB5PujgGw8ULAjl2txm3brOry7VJzKwgwJ6B7nAiZzQNeVoNr6vLss0au2561bOHv1+7OuX59jrBuYOy2LvJbcTtUiVSgwJaN1rNlc03kNwCec4kbT1ah5vKsaXD7gDO64axt+3LfXvPWpV6wMRa8sIjvmJFi1wYQL7bn/AO3oI2fDWm+ZziCLOFhlOsEm3VewFtms9pWJUsAZYEkAWBJuT5SetXKqiqHCxkFjYSDM4B+p2bq1B12i3VbyqiZjhGA85nAAOd2lB0JERAREQEREBF4V4Sg9ul1bLlQZEEVjPTR9z+orOoTqWm6daaU2HVMDKhk7uMhLw6FjHhoDyNd3A+he4Fwj4bUB5jllbxeXOJIJG2zXtsv2FBu8ysKEdpvhp/3tg7zJW+LVZGnGFH8o0Y6uVOxvignyqVCfflhZ/KVB87hH1odMcL/5lh/zuD/UgmHLHkUTJpnhY/KND5qmI+BWHNp1hQF/b9Me64v/AJNBQTEqjKwalD1HCNhQvaqLu5T1B/pUPPwlYe+SOKMVMjpJGxtIhDWgucACcxBtrQdquvbrGEqra9BeuvVbBVYQeoiIC8IXqILL2FYc9wpJeEIOS8KGjprRHMzXLC10eW9i5hN+T5Qb6vKuUUTjSukZIHi9gRaxFr9u9fVFVQRyiz2NPmWmY5wY0VS7MePaf1J5APReyDgslbcmzjZa/UtGdxIO0+fWu71XA1AebLMN5afqUXPwLj8WokG8MP1K7jjOcJmC627gYf1VDvkNVP8A+MyfpDv4bUHJ43C42LMJ1bPMuoR8Czuupk8zGBZtPwKR/jzzH5I+pBx0xk7Sxo8mvwW1aCaKvqKmCZxe2CKRkrnuAaJA1wcGNB1m5G3fuXTaTgWoxYvfO7/ulvgtzwLQikpABGx7rdcsskh/tEqDIpZy5SkLCsiOFrRZrQNwCuILbWKsBeogIiICIiAiIgIiIC8yhEQeZB2LzIOxEQeho7F7ZEQeoiICIiAiIgIiIP/Z", "Doja Cabernet Sauvignon 50% Merlot 50% 0.75l", 14.00m, "Vinarija Doja Blace" },
                    { new Guid("b419a7ca-3321-4f38-be8e-4d7b6a529319"), new Guid("b0788d2f-8003-43c1-92a4-edc76a7c5dde"), "Serbia", "Not so dark red color and with the perception of glowing when on the light. On the nose extensive blackberry and cherry notes with fine traces of smoke and coffee. Also, the fresh oak wood note is distinctive. Spicy, smooth and well balanced on the palate. Pleasant medium acidity and low on tannins. Creamy after taste with some dark fruit that stays quite a long in your mouth. Must be decanted for 30 minutes at least. When tried directly from the bottle it is a bit of harsh experience.", "https://winestyle.rs/wp-content/uploads/2020/01/izbor-2019-Kostic.jpg", "Kostić Prokupac Barrique 0.75l", 10.00m, "Vinarija Kostić" },
                    { new Guid("adc42c09-08c1-4d2c-9f96-2d15bb1af299"), new Guid("b0788d2f-8003-43c1-92a4-edc76a7c5dde"), "Serbia", "Very rich and complex aromatic complex composed of tones of cherries, blackberries and black mulberries, with shades of dark chocolate and tobacco leaves. The wine is extremely extractive, the taste is soft, and the tannins are sweet and juicy. The finish of the taste is rounded, harmonious, long-lasting and fruity, with gentle tones of oak. The pedigree is a rich wine with great potential for long marking. FOOD Excellent with duck, turkey, grilled and roasted red meat as well as lamb. Ideal with: roasted turkey; barbecue; all kinds of livers; roast beef; all kinds of steak and steak tartare; all goat and lamb specialties; red meat in red wine sauce. Remarkable with dark chocolate. SERVING Temperature: 16 - 18ºC", "https://vinskivodic.rs/pub/worker/rodoslov-reserve-aleksandrovic.jpg", "Aleksandrović Rodoslov 0.75l", 27.22m, "Vinarija Aleksandrović" },
                    { new Guid("62787623-4c52-43fe-b0c9-b7044fb5929b"), new Guid("6313179f-7837-473a-a4d5-a5571b43e6a6"), "Serbia", "White wine made from the Chardonnay variety of the Toplicki vinogradi winery. Made from exceptional fruit, this wine has a yellow color with green tones. Nice and pleasant smell.", "http://www.toplickivinogradi.com/images/product/medium/tribus-villa-charonnay.jpg", "Toplički Vinogradi Epigenia Chardonnay 0.75l", 15.8m, "Vinarija Toplički Vinogradi" },
                    { new Guid("1babd057-e980-4cb3-9cd2-7fdd9e525668"), new Guid("6313179f-7837-473a-a4d5-a5571b43e6a6"), "Serbia", "Rich, layered and soft wine dominated by aromas of grapefruit, lime and gooseberry, complemented by the aromas of ripe peaches and passion fruit with gentle notes of sweet spices. RECOMMENDATION UZ: Sea bream with pesto sauce, ravioli stuffed with celery and shrimp, pasta with vegetables and Mediterranean herbs, chicken with asparagus; Serving temperature: 10-12 ° C.", "https://www.restoran-neptun.rs/image/spasic-tamnjanika.jpg", "Spasić Temjanika Dry White 0.75l", 10.22m, "Vinarija Spasić" },
                    { new Guid("adc42c08-08c1-4d2c-9f96-2d15bb1af299"), new Guid("6313179f-7837-473a-a4d5-a5571b43e6a6"), "Serbia", "It fermented and then aged in oak barrels for 12 months, which resulted in great complexity. Golden yellow with a greenish tinge. The intense and complex aroma recognizes a wide range of aromas: wildflowers, chamomile, cut grass, basil, hazelnuts, biscuits, baked bread, ripe yellow apples and mangoes, rounded with a note of oak. The taste is nicely permeated with notes of citrus and ripe apricot, with a vanilla tone of oak and minerality. Lots, buttery, perfectly balanced, very stable and complex wine, with calm and pleasant acids. Elegance and harmony are expressed in the finish.", "https://vinoitakoto.com/wp-content/uploads/2015/02/Untitled-1-247.gif", "Chardonney Radovanović 0.75l", 17.22m, "Podrum Radovanović" },
                    { new Guid("adc54a08-07c1-4d2c-9f96-2d15bb1af299"), new Guid("bf3f3002-7e53-441e-8b76-f6280be284aa"), "Serbia", "Crystal bright ruby ​​color. Impressive scent of wild strawberries, with tones of wild rose, ratluk and sweet spices. Refreshing, fruity, very drinkable wine, with a long aromatic finish. In terms of structure and strength, the wine is at the very transition between rosy red wine. FOOD Excellent with Serbian appetizers (prosciutto, cheese), barbecue, as well as with fruit desserts. Ideal with: dried meat; sausages and bacon; roast pork; fruit salads.", "https://www.vinskivodic.rs/pub/worker/varijanta-aleksandrovic.jpg", "Varijanta Aleksandrović Muskat Hamburg 0.75l", 17.22m, "Vinarija Aleksandrović" },
                    { new Guid("adc54a08-07c1-4d2c-9f96-2d15bb1af211"), new Guid("bf3f3002-7e53-441e-8b76-f6280be284aa"), "Serbia", "The pronounced pink color of the wine indicates the richness of aromas. The taste of ripe red fruit is dominant, and as the impression goes on, the notes of herbs are repeated. Delicate wine, pleasant acidity, moderate fullness. Ideal as a transition from white to red wines.", "https://lh3.googleusercontent.com/proxy/7xjxKqIrVHwQWsRj4G3nrS6WQiv-cDZbFKSTNW04_d7oKzPHAg3FkLldMkvouxIdx0AIcgt57B7W7wfnsI2RCLVXI9G2hglTnhW3xK8cIiojXl6AjJlenvDxBu7Buz2tF-PaGq8PQc2D020CDkYNZ6PsCUM_gM0to3K8PILRJfhFb4K2HQbg-SFn-1tDz8xataBJQs7K0pmd", "Rose Radovanović  0.75l", 8.00m, "Vinarija Radovanović Krnjevo" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Wines_CategoryId",
                table: "Wines",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Wines");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
