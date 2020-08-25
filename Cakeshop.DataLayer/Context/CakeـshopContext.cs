using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cakeshop.DataLayer.Entities.Order;
using Cakeshop.DataLayer.Entities.Permissions;
using Cakeshop.DataLayer.Entities.Product;
using Cakeshop.DataLayer.Entities.Shop;
using Cakeshop.DataLayer.Entities.User;
using Microsoft.EntityFrameworkCore;


namespace Cakeshop.DataLayer.Context
{
    public class CakeshopContext : DbContext
    {

        public CakeshopContext(DbContextOptions<CakeshopContext> options) : base(options)
        {

        }

        public DbSet<Role> CakeshopRoles { get; set; }
        public DbSet<User> CakeshopUsers { get; set; }
        public DbSet<UserRole> CakeshopUserRoles { get; set; }
        public DbSet<UserLocation> Locations { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<Permission> Permission { get; set; }
        public DbSet<RolePermission> RolePermission { get; set; }

        public DbSet<ProductGroup> ProductGroups { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductComment> ProductComments { get; set; }

        public DbSet<Shop> Shops { get; set; }
        public DbSet<ShopProduct> ShopProducts { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            modelBuilder.Entity<User>()
                .HasQueryFilter(u => !u.IsDelete);
            modelBuilder.Entity<Role>()
                .HasQueryFilter(r => !r.IsDelete);
            modelBuilder.Entity<ProductGroup>()
                .HasQueryFilter(g => !g.IsDelete);
            modelBuilder.Entity<Product>()
                .HasQueryFilter(g => !g.IsDelete);
            modelBuilder.Entity<Shop>()
                .HasQueryFilter(s => !s.IsDelete);
            modelBuilder.Entity<Order>()
                 .HasQueryFilter(s => !s.IsDelete);


            modelBuilder.Entity<ShopProduct>()
                .HasKey(t => new { t.ProductId, t.ShopId });

            modelBuilder.Entity<ShopProduct>()
                .HasOne(sp => sp.Product)
                .WithMany(s => s.ShopProduct)
                .HasForeignKey(sp => sp.ProductId);

            modelBuilder.Entity<ShopProduct>()
                .HasOne(sp => sp.Shop)
                .WithMany(p => p.ShopProduct)
                .HasForeignKey(sp => sp.ShopId);
            base.OnModelCreating(modelBuilder);
        }
    }
}
