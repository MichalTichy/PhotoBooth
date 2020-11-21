using Microsoft.EntityFrameworkCore.Migrations;

namespace PhotoBooth.DAL.Migrations
{
    public partial class addedamountlefttoproductcs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AmountLeft",
                table: "Products",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountLeft",
                table: "Products");
        }
    }
}
