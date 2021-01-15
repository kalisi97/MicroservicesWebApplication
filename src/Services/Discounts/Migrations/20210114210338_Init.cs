using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Discounts.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coupons",
                columns: table => new
                {
                    CouponId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    AlreadyUsed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupons", x => x.CouponId);
                });

            migrationBuilder.InsertData(
                table: "Coupons",
                columns: new[] { "CouponId", "AlreadyUsed", "Amount", "UserId" },
                values: new object[] { new Guid("d8d02b4f-5a86-4472-8049-a7551a199baf"), false, 10m, new Guid("be936f3d-ddd8-4a5c-92c2-e91b9a25a702") });

            migrationBuilder.InsertData(
                table: "Coupons",
                columns: new[] { "CouponId", "AlreadyUsed", "Amount", "UserId" },
                values: new object[] { new Guid("bf49a548-7bda-4275-999b-a0b7ff2c785a"), false, 5m, new Guid("849c17ab-45df-4ffd-835b-1d4ed8a8f486") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coupons");
        }
    }
}
