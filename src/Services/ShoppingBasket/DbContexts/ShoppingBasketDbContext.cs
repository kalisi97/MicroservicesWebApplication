using Microsoft.EntityFrameworkCore;
using ShoppingBasket.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingBasket.DbContexts
{
    public class ShoppingBasketDbContext : DbContext
    {
        public ShoppingBasketDbContext(DbContextOptions<ShoppingBasketDbContext> options)
        : base(options)
        {
        }

        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketLine> BasketLines { get; set; }
        public DbSet<Wine> Wines { get; set; }
        public DbSet<BasketChange> BasketChanges { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // seed the database with dummy data
            modelBuilder.Entity<Wine>().HasData(new Wine
            {
                WineId = Guid.Parse("{EE272F8B-6096-4CB6-8625-BB4BB2D89E8B}"),
                Name = "Toplički Vinogradi Epigenia Prokupac 0.75l",
            });

            modelBuilder.Entity<Wine>().HasData(new Wine
            {
                WineId = Guid.Parse("{3448D5A4-0F72-4DD7-BF15-C14A46B26C00}"),
                Name = "Doja Cabernet Sauvignon 50% Merlot 50% 0.75l",
            });

            modelBuilder.Entity<Wine>().HasData(new Wine
            {
                WineId = Guid.Parse("{B419A7CA-3321-4F38-BE8E-4D7B6A529319}"),
                Name = "Kostić Prokupac Barrique 0.75l",
            });

            modelBuilder.Entity<Wine>().HasData(new Wine
            {
                WineId = Guid.Parse("{62787623-4C52-43FE-B0C9-B7044FB5929B}"),
                Name = "Toplički Vinogradi Epigenia Chardonnay 0.75l",
            });

            modelBuilder.Entity<Wine>().HasData(new Wine
            {
                WineId = Guid.Parse("{1BABD057-E980-4CB3-9CD2-7FDD9E525668}"),
                Name = "Spasić Temjanika Dry White 0.75l",
            });

            modelBuilder.Entity<Wine>().HasData(new Wine
            {
                WineId = Guid.Parse("{ADC42C09-08C1-4D2C-9F96-2D15BB1AF299}"),
                Name = "Aleksandrović Rodoslov 0.75l",
            });

            modelBuilder.Entity<Wine>().HasData(new Wine
            {
                WineId = Guid.Parse("{ADC42C08-08C1-4D2C-9F96-2D15BB1AF299}"),
                Name = "Chardonney Radovanović 0.75l",
            });

            modelBuilder.Entity<Wine>().HasData(new Wine
            {
                WineId = Guid.Parse("{ADC54A08-07C1-4D2C-9F96-2D15BB1AF299}"),
                Name = "Varijanta Aleksandrović Muskat Hamburg 0.75l",
            });

            modelBuilder.Entity<Wine>().HasData(new Wine
            {
                WineId = Guid.Parse("{ADC54A08-07C1-4D2C-9F96-2D15BB1AF211}"),
                Name = "Rose Radovanović  0.75l",
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
