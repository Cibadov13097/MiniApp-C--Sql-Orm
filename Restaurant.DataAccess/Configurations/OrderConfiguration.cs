using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Restaurant.DataAccess.Configurations
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {

        public void Configure(EntityTypeBuilder<Order> builder)
        {
            {
                builder.Property(o => o.Date).IsRequired();
                builder.Property(o => o.TotalAmount).IsRequired();
                builder.HasKey(o => o.Id);
            }

        }
    }
}
