using Microsoft.EntityFrameworkCore.Migrations;

namespace Cakeshop.DataLayer.Migrations
{
    public partial class AddFildIsDeleteToRolesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "CakeshopRoles",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "CakeshopRoles");
        }
    }
}
