using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Entities
{
    public class ReportConfiguration : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder
                .Property(r => r.CreatorId)
                .IsRequired();
            builder
                .Property(r => r.Summary)
                .HasMaxLength(1000)
                .IsRequired();
            builder
                .HasOne(r => r.Creator)
                .WithMany(u => u.Reports)
                .HasForeignKey(r => r.CreatorId);
        }
    }
}
