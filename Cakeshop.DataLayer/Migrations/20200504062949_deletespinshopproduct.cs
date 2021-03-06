﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Cakeshop.DataLayer.Migrations
{
    public partial class deletespinshopproduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_ShopProducts_sp",
                table: "ShopProducts");

            migrationBuilder.DropColumn(
                name: "sp",
                table: "ShopProducts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "sp",
                table: "ShopProducts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_ShopProducts_sp",
                table: "ShopProducts",
                column: "sp");
        }
    }
}
