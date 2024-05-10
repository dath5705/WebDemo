using Microsoft.EntityFrameworkCore;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<Warehouse>().HasKey(x => x.Id);
            modelBuilder.Entity<Role>().HasKey(x => x.Id);
            modelBuilder.Entity<Information>().HasKey(x => x.Id);
            modelBuilder.Entity<ProductInCart>().HasKey(x => new { x.UserId, x.ProductId });

            modelBuilder.Entity<Role>()
                .HasMany(e => e.Users)
                .WithOne(e => e.Roll)
                .HasForeignKey(e => e.RoleId);
            modelBuilder.Entity<User>()
                .HasMany(e => e.Products)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId);
            modelBuilder.Entity<User>()
                .HasMany(e => e.Informations)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId);
            modelBuilder.Entity<User>()
                .HasMany(e => e.ProductsInCart)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<Warehouse>()
                .HasMany(e => e.ProductsInCart)
                .WithOne(e => e.Product)
                .HasForeignKey(e => e.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
