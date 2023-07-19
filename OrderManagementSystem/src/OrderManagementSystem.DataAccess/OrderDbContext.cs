using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Core.Models;

namespace OrderManagementSystem.DataAccess
{
    public class OrderDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }

        public OrderDbContext(DbContextOptions<OrderDbContext> options): base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .ToTable("Products")
                .HasKey(p => p.Id);

            modelBuilder.Entity<Order>()
                .ToTable("Orders")
                .HasKey(p => p.Id);
            modelBuilder.Entity<Order>()
                .HasOne<Product>(p => p.Product).WithMany();

        }
    }
}

