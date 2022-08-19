using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Entities
{
    public class WareCartConfiguration : IEntityTypeConfiguration<WareCart>
    {
        public void Configure(EntityTypeBuilder<WareCart> builder)
        {
            builder
                .HasOne(wc => wc.Cart)
                .WithMany(c => c.WareCarts)
                .HasForeignKey(wc => wc.CartId);
            builder
                .HasOne(wc => wc.Ware)
                .WithMany(w => w.WareCarts)
                .HasForeignKey(wc => wc.WareId);
        }
    }
}
