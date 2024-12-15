using Microsoft.EntityFrameworkCore;
using Restaurant.Core.Models;
using Restaurant.DataAccess.Configurations;

namespace Restaurant.DataAccess.Data
{
    public class RestaurantDB : DbContext
    {
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-K0386GG;Database=RestaurantDb;Trusted_Connection=True;TrustServerCertificate=true");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
            modelBuilder.ApplyConfiguration(new MenuItemConfiguration());
        }
    }
}
    