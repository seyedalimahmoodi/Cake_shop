using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cakeshop.DataLayer.Migrations
{
    public partial class addtableuserlocations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserLocationId",
                table: "Orders",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    UserLocationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.UserLocationId);
                    table.ForeignKey(
                        name: "FK_Locations_CakeshopUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "CakeshopUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserLocationId",
                table: "Orders",
                column: "UserLocationId",
                unique: true,
                filter: "[UserLocationId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_UserId",
                table: "Locations",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Locations_UserLocationId",
                table: "Orders",
                column: "UserLocationId",
                principalTable: "Locations",
                principalColumn: "UserLocationId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Locations_UserLocationId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Orders_UserLocationId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "UserLocationId",
                table: "Orders");
        }
    }
}
