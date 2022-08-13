using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Entities
{
    public class WareConfiguration : IEntityTypeConfiguration<Ware>
    {
        public void Configure(EntityTypeBuilder<Ware> builder)
        {
            builder
                .Property(w => w.Title)
                .HasMaxLength(100)
                .IsRequired();
            builder
                .Property(w => w.Description)
                .HasMaxLength(1000)
                .IsRequired();
            builder
                .HasOne(w => w.Category)
                .WithMany(c => c.Wares)
                .HasForeignKey(w => w.CategoryId);
            builder
                .HasOne(w => w.User)
                .WithMany(u => u.Wares)
                .HasForeignKey(w => w.CreatorId);
        }
    }
}
