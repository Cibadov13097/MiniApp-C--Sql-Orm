using Microsoft.EntityFrameworkCore;
using Restaurant.Core.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;


namespace Restaurant.DataAccess.Configurations
{
    internal class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {

        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            {
                builder.HasKey(o => o.Id);            
                builder.HasOne(o => o.Order).WithMany(oi=>oi.OrderItems).HasForeignKey(o=>o.OrderId);
                builder.HasOne(m => m.MenuItem).WithMany(oi => oi.OrderItems).HasForeignKey(m => m.MenuItemId); 
            }
        }
    }
}    
