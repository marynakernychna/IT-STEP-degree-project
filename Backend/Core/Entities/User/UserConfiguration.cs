using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Entities
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .Property(u => u.Name)
                .HasMaxLength(100)
                .IsRequired();
            builder
                .Property(u => u.Surname)
                .HasMaxLength(100)
                .IsRequired();
            builder
                .Property(u => u.RegistrationDate)
                .IsRequired();
            builder
                .Property(u => u.PhotoLink)
                .HasMaxLength(100);
            builder
                .Property(u => u.UserName)
                .HasMaxLength(100)
                .IsRequired();
            builder
                .Property(u => u.NormalizedUserName)
                .HasMaxLength(100)
                .IsRequired();
            builder
                .Property(u => u.Email)
                .HasMaxLength(100)
                .IsRequired();
            builder
                .Property(u => u.NormalizedEmail)
                .HasMaxLength(100)
                .IsRequired();
            builder
                .Property(u => u.PasswordHash)
                .IsRequired();
            builder
                .Property(u => u.PhoneNumber)
                .HasMaxLength(20);
        }
    }
}
