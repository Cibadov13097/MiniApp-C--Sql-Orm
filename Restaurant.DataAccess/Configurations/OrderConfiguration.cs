using Microsoft.EntityFrameworkCore;
using Restaurant.Core.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Restaurant.DataAccess.Configurations
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {

        public void Configure(EntityTypeBuilder<Order> builder)
        {
            {
                builder.HasKey(o => o.Id);
            }

        }
    }
}
