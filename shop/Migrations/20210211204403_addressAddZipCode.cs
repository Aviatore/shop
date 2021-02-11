using Microsoft.EntityFrameworkCore.Migrations;

namespace shop.Migrations
{
    public partial class addressAddZipCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "zipcode",
                table: "addresses",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "zipcode",
                table: "addresses");
        }
    }
}
