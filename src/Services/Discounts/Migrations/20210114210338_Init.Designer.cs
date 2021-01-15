﻿// <auto-generated />
using System;
using Discounts.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Discounts.Migrations
{
    [DbContext(typeof(DiscountDbContext))]
    [Migration("20210114210338_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Discounts.Entites.Coupon", b =>
                {
                    b.Property<Guid>("CouponId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("AlreadyUsed")
                        .HasColumnType("bit");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CouponId");

                    b.ToTable("Coupons");

                    b.HasData(
                        new
                        {
                            CouponId = new Guid("d8d02b4f-5a86-4472-8049-a7551a199baf"),
                            AlreadyUsed = false,
                            Amount = 10m,
                            UserId = new Guid("be936f3d-ddd8-4a5c-92c2-e91b9a25a702")
                        },
                        new
                        {
                            CouponId = new Guid("bf49a548-7bda-4275-999b-a0b7ff2c785a"),
                            AlreadyUsed = false,
                            Amount = 5m,
                            UserId = new Guid("849c17ab-45df-4ffd-835b-1d4ed8a8f486")
                        });
                });
#pragma warning restore 612, 618
        }
    }
}