using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Core.Models;


namespace Restaurant.DataAccess.Configurations
{
    internal class MenuItemConfiguration : IEntityTypeConfiguration<MenuItem>
    {

        public void Configure(EntityTypeBuilder<MenuItem> builder)
        {
            {
                builder.HasKey(o => o.Id);
                builder.Property(o=> o.Name).IsRequired();
                builder.Property(o => o.Price).IsRequired();
                builder.HasIndex(m => m.Name).IsUnique();

            }
        }
    }
}
