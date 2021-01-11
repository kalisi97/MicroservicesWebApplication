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
                    Amount = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupons", x => x.CouponId);
                });

            migrationBuilder.InsertData(
                table: "Coupons",
                columns: new[] { "CouponId", "Amount", "UserId" },
                values: new object[] { new Guid("e0eae93f-8742-4714-8d82-648e3a163692"), 10m, new Guid("e455a3df-7fa5-47e0-8435-179b300d531f") });

            migrationBuilder.InsertData(
                table: "Coupons",
                columns: new[] { "CouponId", "Amount", "UserId" },
                values: new object[] { new Guid("8f2a3adf-f517-403f-b192-1014ddcbb0af"), 20m, new Guid("bbf594b0-3761-4a65-b04c-eec4836d9070") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coupons");
        }
    }
}
