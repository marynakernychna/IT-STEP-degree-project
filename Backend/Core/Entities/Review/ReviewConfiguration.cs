using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Entities
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder
                .Property(r => r.CreatorId)
                .IsRequired();
            builder
                .Property(r => r.Title)
                .HasMaxLength(100)
                .IsRequired();
            builder
                .Property(r => r.Description)
                .HasMaxLength(1000)
                .IsRequired();
            builder
                .HasOne(r => r.Creator)
                .WithMany(w => w.Reviews)
                .HasForeignKey(r => r.CreatorId);
        }
    }
}
