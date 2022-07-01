using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Entities
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder
                .Property(c => c.CreatorId)
                .IsRequired();
            builder
                .HasOne(c => c.Creator)
                .WithMany(u => u.Carts)
                .HasForeignKey(c => c.CreatorId);
        }
    }
}
