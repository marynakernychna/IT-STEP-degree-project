using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Entities
{
    public class CharacteristicConfiguration : IEntityTypeConfiguration<Characteristic>
    {
        public void Configure(EntityTypeBuilder<Characteristic> builder)
        {
            builder
                .Property(c => c.Name)
                .HasMaxLength(50)
                .IsRequired();
            builder
               .Property(c => c.Value)
               .HasMaxLength(200)
               .IsRequired();
            builder
                .HasOne(c => c.Ware)
                .WithMany(w => w.Characteristics)
                .HasForeignKey(c => c.WareId);
        }
    }
}
