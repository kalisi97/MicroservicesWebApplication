using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppingBasket.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BasketChanges",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    WineId = table.Column<Guid>(nullable: false),
                    InsertedAt = table.Column<DateTimeOffset>(nullable: false),
                    BasketChangeType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasketChanges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Baskets",
                columns: table => new
                {
                    BasketId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Baskets", x => x.BasketId);
                });

            migrationBuilder.CreateTable(
                name: "Wines",
                columns: table => new
                {
                    WineId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wines", x => x.WineId);
                });

            migrationBuilder.CreateTable(
                name: "BasketLines",
                columns: table => new
                {
                    BasketLineId = table.Column<Guid>(nullable: false),
                    BasketId = table.Column<Guid>(nullable: false),
                    WineId = table.Column<Guid>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasketLines", x => x.BasketLineId);
                    table.ForeignKey(
                        name: "FK_BasketLines_Baskets_BasketId",
                        column: x => x.BasketId,
                        principalTable: "Baskets",
                        principalColumn: "BasketId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BasketLines_Wines_WineId",
                        column: x => x.WineId,
                        principalTable: "Wines",
                        principalColumn: "WineId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Wines",
                columns: new[] { "WineId", "Name" },
                values: new object[,]
                {
                    { new Guid("ee272f8b-6096-4cb6-8625-bb4bb2d89e8b"), "Toplički Vinogradi Epigenia Prokupac 0.75l" },
                    { new Guid("3448d5a4-0f72-4dd7-bf15-c14a46b26c00"), "Doja Cabernet Sauvignon 50% Merlot 50% 0.75l" },
                    { new Guid("b419a7ca-3321-4f38-be8e-4d7b6a529319"), "Kostić Prokupac Barrique 0.75l" },
                    { new Guid("62787623-4c52-43fe-b0c9-b7044fb5929b"), "Toplički Vinogradi Epigenia Chardonnay 0.75l" },
                    { new Guid("1babd057-e980-4cb3-9cd2-7fdd9e525668"), "Spasić Temjanika Dry White 0.75l" },
                    { new Guid("adc42c09-08c1-4d2c-9f96-2d15bb1af299"), "Aleksandrović Rodoslov 0.75l" },
                    { new Guid("adc42c08-08c1-4d2c-9f96-2d15bb1af299"), "Chardonney Radovanović 0.75l" },
                    { new Guid("adc54a08-07c1-4d2c-9f96-2d15bb1af299"), "Varijanta Aleksandrović Muskat Hamburg 0.75l" },
                    { new Guid("adc54a08-07c1-4d2c-9f96-2d15bb1af211"), "Rose Radovanović  0.75l" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BasketLines_BasketId",
                table: "BasketLines",
                column: "BasketId");

            migrationBuilder.CreateIndex(
                name: "IX_BasketLines_WineId",
                table: "BasketLines",
                column: "WineId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BasketChanges");

            migrationBuilder.DropTable(
                name: "BasketLines");

            migrationBuilder.DropTable(
                name: "Baskets");

            migrationBuilder.DropTable(
                name: "Wines");
        }
    }
}
