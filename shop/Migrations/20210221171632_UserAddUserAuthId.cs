using Microsoft.EntityFrameworkCore.Migrations;

namespace shop.Migrations
{
    public partial class UserAddUserAuthId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "user_auth_id",
                table: "users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "user_auth_id",
                table: "users");
        }
    }
}
