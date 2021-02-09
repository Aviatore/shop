using Microsoft.EntityFrameworkCore.Migrations;

namespace shop.Migrations
{
    public partial class secondSnapshot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "country_id",
                table: "addresses",
                newName: "country");

            migrationBuilder.RenameColumn(
                name: "city_id",
                table: "addresses",
                newName: "city");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "country",
                table: "addresses",
                newName: "country_id");

            migrationBuilder.RenameColumn(
                name: "city",
                table: "addresses",
                newName: "city_id");
        }
    }
}
