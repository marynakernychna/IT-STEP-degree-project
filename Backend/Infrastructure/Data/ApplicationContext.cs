using Core.Entities;
using Infrastructure.Data.SeedData;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ApplicationContext : IdentityDbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options) { }

        public DbSet<Cart> Carts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Characteristic> Characteristics { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Ware> Wares { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<WareCart> WareCarts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CartConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new CharacteristicConfiguration());
            builder.ApplyConfiguration(new OrderConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new WareConfiguration());
            builder.ApplyConfiguration(new RefreshTokenConfiguration());
            builder.ApplyConfiguration(new WareCartConfiguration());

            builder
                .Entity<Category>()
                .HasMany(c => c.Wares)
                .WithOne(w => w.Category)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Seed();

            base.OnModelCreating(builder);
        }
    }
}
