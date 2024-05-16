using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using System.Reflection.Emit;
using WebDemo.Models;

namespace WebDemo
{
    public class WebDemoDatabase : DbContext
    {
        public WebDemoDatabase(
             DbContextOptions<WebDemoDatabase> options) : base(options)
        {
        }
        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<Information> Informations => Set<Information>();
        public DbSet<Warehouse> Warehouse => Set<Warehouse>();
        public DbSet<ProductInCart> ProductsInCart => Set<ProductInCart>();
        public DbSet<Sex> Sexs => Set<Sex>();
        public DbSet<Bill> Bills => Set<Bill>();
        public DbSet<Shop> Shops => Set<Shop>();
        public DbSet<BillDetail> BillsDetail => Set<BillDetail>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<Warehouse>().HasKey(x => x.Id);
            modelBuilder.Entity<Role>().HasKey(x => x.Id);
            modelBuilder.Entity<Information>().HasKey(x => x.Id);
            modelBuilder.Entity<Shop>().HasKey(x => x.Id);
            modelBuilder.Entity<Bill>().HasKey(x => x.Id);
            modelBuilder.Entity<Sex>().HasKey(x => x.Id);
            modelBuilder.Entity<ProductInCart>().HasKey(x => new { x.UserId, x.ProductId });
            modelBuilder.Entity<BillDetail>().HasKey(x => new { x.BillId, x.ProductId });

            modelBuilder.Entity<Role>()
                .HasMany(e => e.Users)
                .WithOne(e => e.Roll)
                .HasForeignKey(e => e.RoleId);

            modelBuilder.Entity<Sex>()
                .HasMany(e => e.Users)
                .WithOne(e => e.Sex)
                .HasForeignKey(e => e.SexId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Information>()
                .HasMany(e => e.Bills)
                .WithOne(e => e.Information)
                .HasForeignKey(e => e.InformationId);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Informations)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId);
            modelBuilder.Entity<User>()
                .HasMany(e => e.ProductsInCart)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<User>()
                .HasMany(e => e.Bills)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Warehouse>()
                .HasMany(e => e.ProductsInCart)
                .WithOne(e => e.Product)
                .HasForeignKey(e => e.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Warehouse>()
                .HasMany(e => e.BillsDetail)
                .WithOne(e => e.Product)
                .HasForeignKey(e => e.ProductId);

            modelBuilder.Entity<Shop>()
                .HasMany(e => e.Products)
                .WithOne(e => e.Shop)
                .HasForeignKey(e => e.ShopId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Shop>()
                .HasOne(e => e.User)
                .WithOne(e => e.Shop)
                .HasForeignKey<Shop>()
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Shop>()
                .HasMany(e => e.Bills)
                .WithOne(e => e.Shop)
                .HasForeignKey(e => e.ShopId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Bill>()
                .HasMany(e => e.BillsDetail)
                .WithOne(e => e.Bill)
                .HasForeignKey(e => e.BillId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
