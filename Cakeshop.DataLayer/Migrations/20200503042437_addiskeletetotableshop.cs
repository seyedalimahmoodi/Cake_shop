using Microsoft.EntityFrameworkCore.Migrations;

namespace Cakeshop.DataLayer.Migrations
{
    public partial class addiskeletetotableshop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShopProduct_Product_ProductId",
                table: "ShopProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_ShopProduct_Shops_ShopId",
                table: "ShopProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShopProduct",
                table: "ShopProduct");

            migrationBuilder.RenameTable(
                name: "ShopProduct",
                newName: "ShopProducts");

            migrationBuilder.RenameIndex(
                name: "IX_ShopProduct_ShopId",
                table: "ShopProducts",
                newName: "IX_ShopProducts_ShopId");

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Shops",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShopProducts",
                table: "ShopProducts",
                columns: new[] { "ProductId", "ShopId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ShopProducts_Product_ProductId",
                table: "ShopProducts",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShopProducts_Shops_ShopId",
                table: "ShopProducts",
                column: "ShopId",
                principalTable: "Shops",
                principalColumn: "ShopId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShopProducts_Product_ProductId",
                table: "ShopProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_ShopProducts_Shops_ShopId",
                table: "ShopProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShopProducts",
                table: "ShopProducts");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Shops");

            migrationBuilder.RenameTable(
                name: "ShopProducts",
                newName: "ShopProduct");

            migrationBuilder.RenameIndex(
                name: "IX_ShopProducts_ShopId",
                table: "ShopProduct",
                newName: "IX_ShopProduct_ShopId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShopProduct",
                table: "ShopProduct",
                columns: new[] { "ProductId", "ShopId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ShopProduct_Product_ProductId",
                table: "ShopProduct",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShopProduct_Shops_ShopId",
                table: "ShopProduct",
                column: "ShopId",
                principalTable: "Shops",
                principalColumn: "ShopId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
