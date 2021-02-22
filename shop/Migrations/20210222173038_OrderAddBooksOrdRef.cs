using Microsoft.EntityFrameworkCore.Migrations;

namespace shop.Migrations
{
    public partial class OrderAddBooksOrdRef : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderId1",
                table: "books_ordered",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_books_ordered_OrderId1",
                table: "books_ordered",
                column: "OrderId1");

            migrationBuilder.AddForeignKey(
                name: "FK_books_ordered_orders_OrderId1",
                table: "books_ordered",
                column: "OrderId1",
                principalTable: "orders",
                principalColumn: "order_id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_books_ordered_orders_OrderId1",
                table: "books_ordered");

            migrationBuilder.DropIndex(
                name: "IX_books_ordered_OrderId1",
                table: "books_ordered");

            migrationBuilder.DropColumn(
                name: "OrderId1",
                table: "books_ordered");
        }
    }
}
