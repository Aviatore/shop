using Microsoft.EntityFrameworkCore.Migrations;

namespace shop.Migrations
{
    public partial class bookRemAuthor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "author",
                table: "books");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "author",
                table: "books",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
