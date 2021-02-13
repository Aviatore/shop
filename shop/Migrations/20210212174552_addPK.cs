using Microsoft.EntityFrameworkCore.Migrations;

namespace shop.Migrations
{
    public partial class addPK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "books_ordered_id",
                table: "books_ordered",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "author_book_id",
                table: "author_book",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_books_ordered",
                table: "books_ordered",
                column: "books_ordered_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_author_book",
                table: "author_book",
                column: "author_book_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_books_ordered",
                table: "books_ordered");

            migrationBuilder.DropPrimaryKey(
                name: "PK_author_book",
                table: "author_book");

            migrationBuilder.DropColumn(
                name: "books_ordered_id",
                table: "books_ordered");

            migrationBuilder.DropColumn(
                name: "author_book_id",
                table: "author_book");
        }
    }
}
