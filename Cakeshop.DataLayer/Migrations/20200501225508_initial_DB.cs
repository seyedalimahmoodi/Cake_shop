using Microsoft.EntityFrameworkCore.Migrations;

namespace Cakeshop.DataLayer.Migrations
{
    public partial class initial_DB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "ProductComments",
                maxLength: 700,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 700,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "ProductComments",
                maxLength: 700,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 700);
        }
    }
}
