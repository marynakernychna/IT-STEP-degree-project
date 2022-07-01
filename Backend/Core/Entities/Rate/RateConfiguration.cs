using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Entities
{
    public class RateConfiguration : IEntityTypeConfiguration<Rate>
    {
        public void Configure(EntityTypeBuilder<Rate> builder)
        {
            builder
                .Property(r => r.CreatorId)
                .IsRequired();
            builder
                .Property(r => r.Estimate)
                .IsRequired();
            builder
                .HasOne(r => r.Creator)
                .WithMany(u => u.Rates)
                .HasForeignKey(r => r.CreatorId);
            builder
                .HasOne(r => r.Ware)
                .WithMany(W => W.Rates)
                .HasForeignKey(r => r.WareId);
        }
    }
}
