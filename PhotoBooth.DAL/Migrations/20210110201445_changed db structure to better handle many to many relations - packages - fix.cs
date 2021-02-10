using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace PhotoBooth.DAL.Migrations
{
    public partial class changeddbstructuretobetterhandlemanytomanyrelationspackagesfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemPackageRentalItem_RentalItems_RentalItemId",
                table: "ItemPackageRentalItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemPackageRentalItem",
                table: "ItemPackageRentalItem");

            migrationBuilder.DropIndex(
                name: "IX_ItemPackageRentalItem_RentalItemId",
                table: "ItemPackageRentalItem");

            migrationBuilder.DropColumn(
                name: "RentalItemId",
                table: "ItemPackageRentalItem");

            migrationBuilder.AddColumn<int>(
                name: "RentalItemType",
                table: "ItemPackageRentalItem",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemPackageRentalItem",
                table: "ItemPackageRentalItem",
                columns: new[] { "ItemPackageId", "RentalItemType" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemPackageRentalItem",
                table: "ItemPackageRentalItem");

            migrationBuilder.DropColumn(
                name: "RentalItemType",
                table: "ItemPackageRentalItem");

            migrationBuilder.AddColumn<Guid>(
                name: "RentalItemId",
                table: "ItemPackageRentalItem",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemPackageRentalItem",
                table: "ItemPackageRentalItem",
                columns: new[] { "ItemPackageId", "RentalItemId" });

            migrationBuilder.CreateIndex(
                name: "IX_ItemPackageRentalItem_RentalItemId",
                table: "ItemPackageRentalItem",
                column: "RentalItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemPackageRentalItem_RentalItems_RentalItemId",
                table: "ItemPackageRentalItem",
                column: "RentalItemId",
                principalTable: "RentalItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}