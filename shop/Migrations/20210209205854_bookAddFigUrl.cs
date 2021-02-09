using Microsoft.EntityFrameworkCore.Migrations;

namespace shop.Migrations
{
    public partial class bookAddFigUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "fig_url",
                table: "books",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fig_url",
                table: "books");
        }
    }
}
