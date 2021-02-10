using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace PhotoBooth.DAL.Migrations
{
    public partial class seeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ItemPackages",
                columns: new[] { "Id", "CurrentlyAvailable", "Name" },
                values: new object[,]
                {
                    { new Guid("89a0b45a-5946-4e9b-90ef-c47b2f8b85cc"), true, "Balik L" },
                    { new Guid("07c50b28-53f4-4a95-9203-9e71b37a8f9a"), true, "Balik M" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AmountLeft", "DescriptionHtml", "Name", "PictureUrl", "Price" },
                values: new object[,]
                {
                    { new Guid("42bc60eb-baf0-4fb7-9172-126d229db4da"), 20L, "Usb so zhotovenymi fotkami", "USB kluc", "https://photos.smileshoot.sk/usb.jpg", 5.0 },
                    { new Guid("5ac9f8e2-30bb-4030-8ace-5b15cdb7cbce"), 10L, "Fotokniha s drevenou prednou stranou + gravirovanie", "Fotokniha", "https://photos.smileshoot.sk/fotokiha.jpg", 10.0 }
                });

            migrationBuilder.InsertData(
                table: "RentalItems",
                columns: new[] { "Id", "DescriptionHtml", "Name", "PictureUrl", "PricePerHour", "Type" },
                values: new object[,]
                {
                    { new Guid("1d06696b-4dff-4faf-a8a8-f5cc100d5a71"), "Unikátna, skvele vyzerajúca fotobúdka s neobmedzenou možnosťou tlače v krásnom retro dizajne.", "Retro fotobudka", "https://photos.smileshoot.sk/photobooth.jpg", 80.0, 0 },
                    { new Guid("aac48b3f-5cdb-49e2-92e0-24864c1a5c65"), "Najlepsi zamestnanec roka", "Dominik", null, 10.0, 3 },
                    { new Guid("4996090c-da51-4435-8e15-88a87767bb05"), "2. najlepsi zamestnanec roka", "Milos", null, 10.0, 3 },
                    { new Guid("31545f44-9a49-4df2-993b-5b420754ea7c"), "Zlate pozadie s gulickami", "Pozadie A", "https://photos.smileshoot.sk/pozadie-A.jpg", 10.0, 1 },
                    { new Guid("4e33b967-31c0-49c3-896c-2ab2ea257a7b"), "Kvetinove pozadie", "Pozadie B", "https://photos.smileshoot.sk/pozadie-B.jpg", 10.0, 1 },
                    { new Guid("ca8209a8-e046-4509-ade0-aef8359baae9"), "Vintage pozadie", "Pozadie C", "https://photos.smileshoot.sk/pozadie-C.jpg", 10.0, 1 },
                    { new Guid("c1f18413-3665-4f00-87a7-f1f39393e395"), "Vianocne pozadie s vlockami", "Pozadie D", "https://photos.smileshoot.sk/pozadie-D.jpg", 10.0, 1 },
                    { new Guid("0d4c06ca-f53e-4d8f-8fa4-4bb42f88821b"), "Svieze modre pozadie", "Pozadie E", "https://photos.smileshoot.sk/pozadie-E.jpg", 10.0, 1 },
                    { new Guid("414fbd63-a7ee-480e-b2bc-2ea3c5d9e41c"), "Santa claus ciapky, vianocne okuliare...", "Vianocne rekvizity", "https://photos.smileshoot.sk/props1.jpg", 10.0, 2 },
                    { new Guid("57efe2f1-a48e-48a5-9287-042a16ca3cdf"), "Tabulky 'nabuduce sa vydavam ja', 'parketovy lev', parochne, klobuky...", "Svadobne rekvizity", "https://photos.smileshoot.sk/props2.jpg", 10.0, 2 },
                    { new Guid("889084ca-450a-425e-8323-32bdae035862"), "Smiesne parochne, okuliare...", "Party mix rekvizity", "https://photos.smileshoot.sk/props3.jpg", 10.0, 2 }
                });

            migrationBuilder.InsertData(
                table: "ItemPackageProduct",
                columns: new[] { "ItemPackageId", "ProductId" },
                values: new object[,]
                {
                    { new Guid("89a0b45a-5946-4e9b-90ef-c47b2f8b85cc"), new Guid("42bc60eb-baf0-4fb7-9172-126d229db4da") },
                    { new Guid("89a0b45a-5946-4e9b-90ef-c47b2f8b85cc"), new Guid("5ac9f8e2-30bb-4030-8ace-5b15cdb7cbce") }
                });

            migrationBuilder.InsertData(
                table: "ItemPackageRentalItem",
                columns: new[] { "ItemPackageId", "RentalItemType" },
                values: new object[,]
                {
                    { new Guid("89a0b45a-5946-4e9b-90ef-c47b2f8b85cc"), 1 },
                    { new Guid("89a0b45a-5946-4e9b-90ef-c47b2f8b85cc"), 0 },
                    { new Guid("89a0b45a-5946-4e9b-90ef-c47b2f8b85cc"), 3 },
                    { new Guid("89a0b45a-5946-4e9b-90ef-c47b2f8b85cc"), 2 },
                    { new Guid("07c50b28-53f4-4a95-9203-9e71b37a8f9a"), 0 },
                    { new Guid("07c50b28-53f4-4a95-9203-9e71b37a8f9a"), 3 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ItemPackageProduct",
                keyColumns: new[] { "ItemPackageId", "ProductId" },
                keyValues: new object[] { new Guid("89a0b45a-5946-4e9b-90ef-c47b2f8b85cc"), new Guid("42bc60eb-baf0-4fb7-9172-126d229db4da") });

            migrationBuilder.DeleteData(
                table: "ItemPackageProduct",
                keyColumns: new[] { "ItemPackageId", "ProductId" },
                keyValues: new object[] { new Guid("89a0b45a-5946-4e9b-90ef-c47b2f8b85cc"), new Guid("5ac9f8e2-30bb-4030-8ace-5b15cdb7cbce") });

            migrationBuilder.DeleteData(
                table: "ItemPackageRentalItem",
                keyColumns: new[] { "ItemPackageId", "RentalItemType" },
                keyValues: new object[] { new Guid("07c50b28-53f4-4a95-9203-9e71b37a8f9a"), 0 });

            migrationBuilder.DeleteData(
                table: "ItemPackageRentalItem",
                keyColumns: new[] { "ItemPackageId", "RentalItemType" },
                keyValues: new object[] { new Guid("07c50b28-53f4-4a95-9203-9e71b37a8f9a"), 3 });

            migrationBuilder.DeleteData(
                table: "ItemPackageRentalItem",
                keyColumns: new[] { "ItemPackageId", "RentalItemType" },
                keyValues: new object[] { new Guid("89a0b45a-5946-4e9b-90ef-c47b2f8b85cc"), 0 });

            migrationBuilder.DeleteData(
                table: "ItemPackageRentalItem",
                keyColumns: new[] { "ItemPackageId", "RentalItemType" },
                keyValues: new object[] { new Guid("89a0b45a-5946-4e9b-90ef-c47b2f8b85cc"), 1 });

            migrationBuilder.DeleteData(
                table: "ItemPackageRentalItem",
                keyColumns: new[] { "ItemPackageId", "RentalItemType" },
                keyValues: new object[] { new Guid("89a0b45a-5946-4e9b-90ef-c47b2f8b85cc"), 2 });

            migrationBuilder.DeleteData(
                table: "ItemPackageRentalItem",
                keyColumns: new[] { "ItemPackageId", "RentalItemType" },
                keyValues: new object[] { new Guid("89a0b45a-5946-4e9b-90ef-c47b2f8b85cc"), 3 });

            migrationBuilder.DeleteData(
                table: "RentalItems",
                keyColumn: "Id",
                keyValue: new Guid("0d4c06ca-f53e-4d8f-8fa4-4bb42f88821b"));

            migrationBuilder.DeleteData(
                table: "RentalItems",
                keyColumn: "Id",
                keyValue: new Guid("1d06696b-4dff-4faf-a8a8-f5cc100d5a71"));

            migrationBuilder.DeleteData(
                table: "RentalItems",
                keyColumn: "Id",
                keyValue: new Guid("31545f44-9a49-4df2-993b-5b420754ea7c"));

            migrationBuilder.DeleteData(
                table: "RentalItems",
                keyColumn: "Id",
                keyValue: new Guid("414fbd63-a7ee-480e-b2bc-2ea3c5d9e41c"));

            migrationBuilder.DeleteData(
                table: "RentalItems",
                keyColumn: "Id",
                keyValue: new Guid("4996090c-da51-4435-8e15-88a87767bb05"));

            migrationBuilder.DeleteData(
                table: "RentalItems",
                keyColumn: "Id",
                keyValue: new Guid("4e33b967-31c0-49c3-896c-2ab2ea257a7b"));

            migrationBuilder.DeleteData(
                table: "RentalItems",
                keyColumn: "Id",
                keyValue: new Guid("57efe2f1-a48e-48a5-9287-042a16ca3cdf"));

            migrationBuilder.DeleteData(
                table: "RentalItems",
                keyColumn: "Id",
                keyValue: new Guid("889084ca-450a-425e-8323-32bdae035862"));

            migrationBuilder.DeleteData(
                table: "RentalItems",
                keyColumn: "Id",
                keyValue: new Guid("aac48b3f-5cdb-49e2-92e0-24864c1a5c65"));

            migrationBuilder.DeleteData(
                table: "RentalItems",
                keyColumn: "Id",
                keyValue: new Guid("c1f18413-3665-4f00-87a7-f1f39393e395"));

            migrationBuilder.DeleteData(
                table: "RentalItems",
                keyColumn: "Id",
                keyValue: new Guid("ca8209a8-e046-4509-ade0-aef8359baae9"));

            migrationBuilder.DeleteData(
                table: "ItemPackages",
                keyColumn: "Id",
                keyValue: new Guid("07c50b28-53f4-4a95-9203-9e71b37a8f9a"));

            migrationBuilder.DeleteData(
                table: "ItemPackages",
                keyColumn: "Id",
                keyValue: new Guid("89a0b45a-5946-4e9b-90ef-c47b2f8b85cc"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("42bc60eb-baf0-4fb7-9172-126d229db4da"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("5ac9f8e2-30bb-4030-8ace-5b15cdb7cbce"));
        }
    }
}