using Microsoft.EntityFrameworkCore;
using Restaurant.Core.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;


namespace Restaurant.DataAccess.Configurations
{
    internal class MenuItemConfiguration : IEntityTypeConfiguration<MenuItem>
    {

        public void Configure(EntityTypeBuilder<MenuItem> builder)
        {
            {
                builder.HasKey(o => o.Id);
                builder.HasIndex(m => m.Name).IsUnique();

            }
        }
    }
}
