using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using WebDemo.Models;

namespace WebDemo
{
    public class WebDemoDatabase: DbContext
    {
        public WebDemoDatabase(
             DbContextOptions<WebDemoDatabase> options) : base(options)
        {
        }
        public DbSet<User> Users => Set<User>();
        public DbSet<Roles> Roles => Set<Roles>();
        public DbSet<Informations> Informations => Set<Informations>();
        public DbSet<Warehouse> Warehouse => Set<Warehouse>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<Warehouse>().HasKey(x => x.Id);
            modelBuilder.Entity<Roles>().HasKey(x=> x.Id);
            modelBuilder.Entity<Informations>().HasKey(x => x.Id);

            modelBuilder.Entity<Roles>()
                .HasMany(e => e.Users)
                .WithOne(e => e.Roll)
                .HasForeignKey(e => e.RoleId);
            modelBuilder.Entity<User>()
                .HasMany(e => e.Products)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId);
            modelBuilder.Entity<User>()
                .HasMany(e=> e.Informations)
                .WithOne(e=> e.User)
                .HasForeignKey(e => e.UserId);
        }
    }
}
