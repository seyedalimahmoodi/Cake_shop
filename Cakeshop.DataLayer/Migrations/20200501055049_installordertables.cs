using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cakeshop.DataLayer.Migrations
{
    public partial class installordertables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CakeshopUserRoles_CakeshopRoles_RoleId",
                table: "CakeshopUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_CakeshopUserRoles_CakeshopUsers_UserId",
                table: "CakeshopUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_Carts_CakeshopUsers_UserId",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductGroups_GroupId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermission_Permission_PermissionId",
                table: "RolePermission");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermission_CakeshopRoles_RoleId",
                table: "RolePermission");

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    OrderSum = table.Column<int>(nullable: false),
                    IsFinaly = table.Column<bool>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_CakeshopUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "CakeshopUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    DetailId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OrderId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    Count = table.Column<int>(nullable: false),
                    Price = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.DetailId);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductId",
                table: "OrderDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CakeshopUserRoles_CakeshopRoles_RoleId",
                table: "CakeshopUserRoles",
                column: "RoleId",
                principalTable: "CakeshopRoles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CakeshopUserRoles_CakeshopUsers_UserId",
                table: "CakeshopUserRoles",
                column: "UserId",
                principalTable: "CakeshopUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_CakeshopUsers_UserId",
                table: "Carts",
                column: "UserId",
                principalTable: "CakeshopUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductGroups_GroupId",
                table: "Product",
                column: "GroupId",
                principalTable: "ProductGroups",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermission_Permission_PermissionId",
                table: "RolePermission",
                column: "PermissionId",
                principalTable: "Permission",
                principalColumn: "PermissionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermission_CakeshopRoles_RoleId",
                table: "RolePermission",
                column: "RoleId",
                principalTable: "CakeshopRoles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CakeshopUserRoles_CakeshopRoles_RoleId",
                table: "CakeshopUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_CakeshopUserRoles_CakeshopUsers_UserId",
                table: "CakeshopUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_Carts_CakeshopUsers_UserId",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductGroups_GroupId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermission_Permission_PermissionId",
                table: "RolePermission");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermission_CakeshopRoles_RoleId",
                table: "RolePermission");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.AddForeignKey(
                name: "FK_CakeshopUserRoles_CakeshopRoles_RoleId",
                table: "CakeshopUserRoles",
                column: "RoleId",
                principalTable: "CakeshopRoles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CakeshopUserRoles_CakeshopUsers_UserId",
                table: "CakeshopUserRoles",
                column: "UserId",
                principalTable: "CakeshopUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_CakeshopUsers_UserId",
                table: "Carts",
                column: "UserId",
                principalTable: "CakeshopUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductGroups_GroupId",
                table: "Product",
                column: "GroupId",
                principalTable: "ProductGroups",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermission_Permission_PermissionId",
                table: "RolePermission",
                column: "PermissionId",
                principalTable: "Permission",
                principalColumn: "PermissionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermission_CakeshopRoles_RoleId",
                table: "RolePermission",
                column: "RoleId",
                principalTable: "CakeshopRoles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
