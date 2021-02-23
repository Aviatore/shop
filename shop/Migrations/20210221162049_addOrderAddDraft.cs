using Microsoft.EntityFrameworkCore.Migrations;

namespace shop.Migrations
{
    public partial class addOrderAddDraft : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "draft",
                table: "orders",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "draft",
                table: "orders");
        }
    }
}
