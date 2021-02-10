using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace PhotoBooth.DAL.Migrations
{
    public partial class addedorderconfirmationdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ConfirmationDate",
                table: "Orders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmationDate",
                table: "Orders");
        }
    }
}