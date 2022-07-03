using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Entities
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder
                .Property(p => p.Token)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(p => p.UserId)
                .IsRequired();

            builder
                .HasOne(p => p.User)
                .WithMany(p => p.RefreshTokens)
                .HasForeignKey(p => p.UserId);
        }
    }
}
