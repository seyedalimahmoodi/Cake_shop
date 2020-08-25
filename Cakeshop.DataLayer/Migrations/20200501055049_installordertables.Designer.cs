﻿// <auto-generated />
using System;
using Cakeshop.DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Cakeshop.DataLayer.Migrations
{
    [DbContext(typeof(CakeshopContext))]
    [Migration("20200501055049_installordertables")]
    partial class installordertables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Cakeshop.DataLayer.Entities.Order.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate");

                    b.Property<bool>("IsFinaly");

                    b.Property<int>("OrderSum");

                    b.Property<int>("UserId");

                    b.HasKey("OrderId");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Cakeshop.DataLayer.Entities.Order.OrderDetail", b =>
                {
                    b.Property<int>("DetailId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Count");

                    b.Property<int>("OrderId");

                    b.Property<int>("Price");

                    b.Property<int>("ProductId");

                    b.HasKey("DetailId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("Cakeshop.DataLayer.Entities.Permissions.Permission", b =>
                {
                    b.Property<int>("PermissionId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ParentID");

                    b.Property<string>("PermissionTitle")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("PermissionId");

                    b.HasIndex("ParentID");

                    b.ToTable("Permission");
                });

            modelBuilder.Entity("Cakeshop.DataLayer.Entities.Permissions.RolePermission", b =>
                {
                    b.Property<int>("RP_Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PermissionId");

                    b.Property<int>("RoleId");

                    b.HasKey("RP_Id");

                    b.HasIndex("PermissionId");

                    b.HasIndex("RoleId");

                    b.ToTable("RolePermission");
                });

            modelBuilder.Entity("Cakeshop.DataLayer.Entities.Product.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Count");

                    b.Property<int>("GroupId");

                    b.Property<bool>("IsExist");

                    b.Property<string>("ProductDescription")
                        .IsRequired();

                    b.Property<string>("ProductImageName")
                        .HasMaxLength(50);

                    b.Property<int>("ProductPrice");

                    b.Property<string>("ProductTitle")
                        .IsRequired()
                        .HasMaxLength(450);

                    b.Property<int?>("SubGroup");

                    b.HasKey("ProductId");

                    b.HasIndex("GroupId");

                    b.HasIndex("SubGroup");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("Cakeshop.DataLayer.Entities.Product.ProductGroup", b =>
                {
                    b.Property<int>("GroupId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("GroupTitle")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<bool>("IsDelete");

                    b.Property<int?>("ParentId");

                    b.HasKey("GroupId");

                    b.HasIndex("ParentId");

                    b.ToTable("ProductGroups");
                });

            modelBuilder.Entity("Cakeshop.DataLayer.Entities.User.Cart", b =>
                {
                    b.Property<int>("CartId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Amount");

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("Description")
                        .HasMaxLength(500);

                    b.Property<bool>("IsPay");

                    b.Property<int>("UserId");

                    b.HasKey("CartId");

                    b.HasIndex("UserId");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("Cakeshop.DataLayer.Entities.User.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsDelete");

                    b.Property<string>("RoleTitle")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("RoleId");

                    b.ToTable("CakeshopRoles");
                });

            modelBuilder.Entity("Cakeshop.DataLayer.Entities.User.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ActiveCode")
                        .HasMaxLength(50);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDelete");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<DateTime>("RegisterDate");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("UserId");

                    b.ToTable("CakeshopUsers");
                });

            modelBuilder.Entity("Cakeshop.DataLayer.Entities.User.UserRole", b =>
                {
                    b.Property<int>("UrId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("RoleId");

                    b.Property<int>("UserId");

                    b.HasKey("UrId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("CakeshopUserRoles");
                });

            modelBuilder.Entity("Cakeshop.DataLayer.Entities.Order.Order", b =>
                {
                    b.HasOne("Cakeshop.DataLayer.Entities.User.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Cakeshop.DataLayer.Entities.Order.OrderDetail", b =>
                {
                    b.HasOne("Cakeshop.DataLayer.Entities.Order.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Cakeshop.DataLayer.Entities.Product.Product", "Product")
                        .WithMany("OrderDetails")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Cakeshop.DataLayer.Entities.Permissions.Permission", b =>
                {
                    b.HasOne("Cakeshop.DataLayer.Entities.Permissions.Permission")
                        .WithMany("Permissions")
                        .HasForeignKey("ParentID");
                });

            modelBuilder.Entity("Cakeshop.DataLayer.Entities.Permissions.RolePermission", b =>
                {
                    b.HasOne("Cakeshop.DataLayer.Entities.Permissions.Permission", "Permission")
                        .WithMany("RolePermissions")
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Cakeshop.DataLayer.Entities.User.Role", "Role")
                        .WithMany("RolePermissions")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Cakeshop.DataLayer.Entities.Product.Product", b =>
                {
                    b.HasOne("Cakeshop.DataLayer.Entities.Product.ProductGroup", "ProductGroup")
                        .WithMany("Products")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Cakeshop.DataLayer.Entities.Product.ProductGroup", "Group")
                        .WithMany("SubGroup")
                        .HasForeignKey("SubGroup");
                });

            modelBuilder.Entity("Cakeshop.DataLayer.Entities.Product.ProductGroup", b =>
                {
                    b.HasOne("Cakeshop.DataLayer.Entities.Product.ProductGroup")
                        .WithMany("ProductGroups")
                        .HasForeignKey("ParentId");
                });

            modelBuilder.Entity("Cakeshop.DataLayer.Entities.User.Cart", b =>
                {
                    b.HasOne("Cakeshop.DataLayer.Entities.User.User", "User")
                        .WithMany("Carts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Cakeshop.DataLayer.Entities.User.UserRole", b =>
                {
                    b.HasOne("Cakeshop.DataLayer.Entities.User.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Cakeshop.DataLayer.Entities.User.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
