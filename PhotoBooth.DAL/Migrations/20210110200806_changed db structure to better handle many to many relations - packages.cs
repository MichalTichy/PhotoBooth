using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PhotoBooth.DAL.Migrations
{
    public partial class changeddbstructuretobetterhandlemanytomanyrelationspackages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ItemPackages_ItemPackageId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_RentalItems_ItemPackages_ItemPackageId",
                table: "RentalItems");

            migrationBuilder.DropIndex(
                name: "IX_RentalItems_ItemPackageId",
                table: "RentalItems");

            migrationBuilder.DropIndex(
                name: "IX_Products_ItemPackageId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ItemPackageId",
                table: "RentalItems");

            migrationBuilder.DropColumn(
                name: "ItemPackageId",
                table: "Products");

            migrationBuilder.CreateTable(
                name: "ItemPackageProduct",
                columns: table => new
                {
                    ItemPackageId = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemPackageProduct", x => new { x.ItemPackageId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_ItemPackageProduct_ItemPackages_ItemPackageId",
                        column: x => x.ItemPackageId,
                        principalTable: "ItemPackages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemPackageProduct_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemPackageRentalItem",
                columns: table => new
                {
                    ItemPackageId = table.Column<Guid>(nullable: false),
                    RentalItemId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemPackageRentalItem", x => new { x.ItemPackageId, x.RentalItemId });
                    table.ForeignKey(
                        name: "FK_ItemPackageRentalItem_ItemPackages_ItemPackageId",
                        column: x => x.ItemPackageId,
                        principalTable: "ItemPackages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemPackageRentalItem_RentalItems_RentalItemId",
                        column: x => x.RentalItemId,
                        principalTable: "RentalItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemPackageProduct_ProductId",
                table: "ItemPackageProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPackageRentalItem_RentalItemId",
                table: "ItemPackageRentalItem",
                column: "RentalItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemPackageProduct");

            migrationBuilder.DropTable(
                name: "ItemPackageRentalItem");

            migrationBuilder.AddColumn<Guid>(
                name: "ItemPackageId",
                table: "RentalItems",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ItemPackageId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RentalItems_ItemPackageId",
                table: "RentalItems",
                column: "ItemPackageId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ItemPackageId",
                table: "Products",
                column: "ItemPackageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ItemPackages_ItemPackageId",
                table: "Products",
                column: "ItemPackageId",
                principalTable: "ItemPackages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RentalItems_ItemPackages_ItemPackageId",
                table: "RentalItems",
                column: "ItemPackageId",
                principalTable: "ItemPackages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
