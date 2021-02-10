using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace PhotoBooth.DAL.Migrations
{
    public partial class addeditempackageentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ItemPackageId",
                table: "RentalItems",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ItemPackageId",
                table: "Products",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ItemPackages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    CurrentlyAvailable = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemPackages", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ItemPackages_ItemPackageId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_RentalItems_ItemPackages_ItemPackageId",
                table: "RentalItems");

            migrationBuilder.DropTable(
                name: "ItemPackages");

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
        }
    }
}