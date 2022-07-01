using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Entities
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder
                .Property(o => o.Address)
                .HasMaxLength(100)
                .IsRequired();
            builder
               .Property(o => o.City)
               .HasMaxLength(100)
               .IsRequired();
            builder
               .Property(o => o.Country)
               .HasMaxLength(100)
               .IsRequired();
            builder
                .HasOne(o => o.Cart)
                .WithOne(c => c.Order)
                .HasForeignKey<Order>(o => o.CartId);
            builder
                .HasOne(o => o.Courier)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.CourierId);
        }
    }
}
