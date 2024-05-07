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
        public DbSet<Role> Roles => Set<Role>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            //    modelBuilder.Entity<Customer>().HasKey(x => x.Id);
            //    modelBuilder.Entity<Bill>().HasKey(x => x.Id);
            modelBuilder.Entity<Warehouse>().HasKey(x => x.Id);
            modelBuilder.Entity<Role>().HasKey(x=> x.Id);
            modelBuilder.Entity<Role>()
                .HasMany(e => e.Users)
                .WithOne(e => e.Roll)
                .HasForeignKey(e => e.RoleId);

            //    modelBuilder.Entity<BillDetail>().HasKey(x => new { x.ProductId, x.BillId });

            //    modelBuilder.Entity<Staff>()
            //        .HasMany(e => e.Customers)
            //        .WithOne(e => e.Staff)
            //        .HasForeignKey(e => e.StaffId);
            //    modelBuilder.Entity<Staff>()
            //        .HasMany(e => e.Bills)
            //        .WithOne(e => e.Staff)
            //        .HasForeignKey(e => e.StaffId);
            //    modelBuilder.Entity<Customer>()
            //        .HasMany(e => e.Bills)
            //        .WithOne(e => e.Customer)
            //        .HasForeignKey(e => e.CustomerId);
            //    modelBuilder.Entity<Bill>()
            //        .HasMany(e => e.BillsDetail)
            //        .WithOne(e => e.Bill)
            //        .HasForeignKey(e => e.BillId)
            //        .OnDelete(DeleteBehavior.Cascade);
            //    modelBuilder.Entity<Warehouse>()
            //        .HasMany(e => e.BillsDetail)
            //        .WithOne(e => e.Warehouse)
            //        .HasForeignKey(e => e.ProductId);
            //}
        }
    }
}
