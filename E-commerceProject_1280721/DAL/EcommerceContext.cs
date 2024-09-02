using E_commerceProject_1280721.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace E_commerceProject_1280721.DAL
{
    public class EcommerceContext : IdentityDbContext<ApplicationUser>
    {
        public EcommerceContext() : base("EcommerceContext")
        {
        }

        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductSubCategory> ProductSubCategories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderCancel> OrderCancels { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<WishlistItem> WishlistItems { get; set; }
        public DbSet<ProductSupplier> ProductSuppliers { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .HasRequired(p => p.ProductCategory)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.ProductCategoryId);

            modelBuilder.Entity<ProductSubCategory>()
                .HasRequired(psc => psc.ProductCategory)
                .WithMany(pc => pc.ProductSubCategories)
                .HasForeignKey(psc => psc.ProductCategoryId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasRequired(p => p.Unit)
                .WithMany(u => u.Products)
                .HasForeignKey(p => p.UnitId);

            modelBuilder.Entity<Product>()
                .HasRequired(p => p.Brand)
                .WithMany(b => b.Products)
                .HasForeignKey(p => p.BrandId);

            modelBuilder.Entity<WishlistItem>()
                .HasRequired(wi => wi.Wishlist)
                .WithMany(w => w.WishlistItems)
                .HasForeignKey(wi => wi.WishlistId);

            modelBuilder.Entity<WishlistItem>()
                .HasRequired(wi => wi.Product)
                .WithMany()
                .HasForeignKey(wi => wi.ProductId);

            modelBuilder.Entity<Product>()
                .HasOptional(p => p.Discount)
                .WithMany(d => d.Products)
                .HasForeignKey(p => p.DiscountId);

            modelBuilder.Entity<Product>()
                .HasRequired(p => p.ProductSupplier)
                .WithMany(ps => ps.Products)
                .HasForeignKey(p => p.ProductSupplierId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrderCancel>()
                .HasRequired(oc => oc.Order)
                .WithMany(po => po.OrderCancels)
                .HasForeignKey(oc => oc.OrderId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Order>()
                .HasRequired(o => o.User)
                .WithMany()
                .HasForeignKey(o => o.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderDetails)
                .WithRequired(od => od.Order)
                .HasForeignKey(od => od.OrderId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<OrderDetail>()
                .HasRequired(od => od.Product)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(od => od.ProductId)
                .WillCascadeOnDelete(false);
        }

        public static EcommerceContext Create()
        {
            return new EcommerceContext();
        }
    }
}