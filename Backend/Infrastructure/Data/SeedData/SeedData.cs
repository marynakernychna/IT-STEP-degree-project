using Core.Constants;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure.Data.SeedData
{
    public static class SeedData
    {
        #region Identity roles ids

        private static readonly string ROLE_CLIENT_ID = Guid.NewGuid().ToString();
        private static readonly string ROLE_COURIER_ID = Guid.NewGuid().ToString();
        private static readonly string ROLE_ADMIN_ID = Guid.NewGuid().ToString();

        #endregion

        #region Users ids

        private static readonly string CLIENT_ID = Guid.NewGuid().ToString();
        private static readonly string COURIER_ID = Guid.NewGuid().ToString();
        private static readonly string ADMIN_ID = Guid.NewGuid().ToString();

        #endregion

        private static readonly PasswordHasher<User> passwordHasher = new();

        public static void Seed(this ModelBuilder builder)
        {
            SeedUser(builder);
            SeedIdentityRole(builder);
            SeedIdentityUserRole(builder);
            SeedCart(builder);
            SeedCategory(builder);
            SeedWare(builder);
            SeedCharacteristic(builder);
        }

        #region SeedUser

        public static void SeedUser(ModelBuilder builder)
        {
            var defaultAdmin = new User()
            {
                Id = ADMIN_ID,
                Name = "Mary",
                Surname = "Lou",
                PhoneNumber = "+380986734245",
                UserName = "marylou@gmail.com",
                NormalizedEmail = "marylou@gmail.com".ToUpper(),
                EmailConfirmed = true,
                NormalizedUserName = "marylou@gmail.com".ToUpper(),
                Email = "marylou@gmail.com"
            };

            var defaultUser = new User()
            {
                Id = CLIENT_ID,
                Name = "Etsuko",
                Surname = "Mami",
                PhoneNumber = "+380988931245",
                UserName = "etsukomami@gmail.com",
                NormalizedEmail = "etsukomami@gmail.com".ToUpper(),
                EmailConfirmed = true,
                NormalizedUserName = "etsukomami@gmail.com".ToUpper(),
                Email = "etsukomami@gmail.com"
            };

            var defaultCourier = new User()
            {
                Id = COURIER_ID,
                Name = "Yuuri",
                Surname = "Morishita",
                PhoneNumber = "+380988931245",
                UserName = "yuurimorishita@gmail.com",
                NormalizedEmail = "yuurimorishita@gmail.com".ToUpper(),
                EmailConfirmed = true,
                NormalizedUserName = "yuurimorishita@gmail.com".ToUpper(),
                Email = "yuurimorishita@gmail.com"
            };

            defaultAdmin.PasswordHash = passwordHasher
                .HashPassword(defaultAdmin, "Password_1");
            defaultUser.PasswordHash = passwordHasher
                .HashPassword(defaultUser, "Password_1");
            defaultCourier.PasswordHash = passwordHasher
                .HashPassword(defaultCourier, "Password_1");

            builder.Entity<User>()
                   .HasData(
                       defaultAdmin,
                       defaultUser,
                       defaultCourier);
        }

        #endregion

        #region SeedIdentityRoles

        public static void SeedIdentityRole(ModelBuilder builder) =>
            builder.Entity<IdentityRole>()
                   .HasData(
                        new IdentityRole()
                        {
                            Id = ROLE_CLIENT_ID,
                            Name = IdentityRoleNames.Client.ToString(),
                            NormalizedName = IdentityRoleNames.Client.ToString().ToUpper(),
                            ConcurrencyStamp = ROLE_CLIENT_ID
                        },
                        new IdentityRole()
                        {
                            Id = ROLE_ADMIN_ID,
                            Name = IdentityRoleNames.Admin.ToString(),
                            NormalizedName = IdentityRoleNames.Admin.ToString().ToUpper(),
                            ConcurrencyStamp = ROLE_ADMIN_ID
                        },
                        new IdentityRole()
                        {
                            Id = ROLE_COURIER_ID,
                            Name = IdentityRoleNames.Courier.ToString(),
                            NormalizedName = IdentityRoleNames.Courier.ToString().ToUpper(),
                            ConcurrencyStamp = ROLE_COURIER_ID
                        });

        #endregion

        #region SeedIdentityUserRole

        public static void SeedIdentityUserRole(ModelBuilder builder) =>
            builder.Entity<IdentityUserRole<string>>()
                   .HasData(
                       new IdentityUserRole<string>()
                       {
                           RoleId = ROLE_ADMIN_ID,
                           UserId = ADMIN_ID
                       },
                       new IdentityUserRole<string>()
                       {
                           RoleId = ROLE_CLIENT_ID,
                           UserId = CLIENT_ID
                       },
                       new IdentityUserRole<string>()
                       {
                           RoleId = ROLE_COURIER_ID,
                           UserId = COURIER_ID
                       });

        #endregion

        #region SeedCart

        public static void SeedCart(ModelBuilder builder)
        {
            var defaultUserCart = new Cart()
            {
                Id = 1,
                CreatorId = CLIENT_ID
            };

            builder.Entity<Cart>()
                   .HasData(defaultUserCart);
        }

        #endregion

        #region SeedCategory

        public static void SeedCategory(ModelBuilder builder)
        {
            builder.Entity<Category>()
                   .HasData(
                       new Category()
                       {
                           Id = Constants.NO_CATEGORY_ID,
                           Title = Constants.NO_CATEGORY_TITLE
                       },
                       new Category()
                       {
                           Id = 2,
                           Title = "Laptops"
                       },
                       new Category()
                       {
                           Id = 3,
                           Title = "Books"
                       },
                       new Category()
                       {
                           Id = 4,
                           Title = "Footwear"
                       });
        }

        #endregion

        #region SeedWare

        public static void SeedWare(ModelBuilder builder)
        {
            builder.Entity<Ware>()
                   .HasData(
                       new Ware()
                       {
                           Id = 1,
                           Title = "Ankle Boots",
                           Description = "GIANVITO ROSSI",
                           Cost = 40450,
                           PhotoLink = "3d67cfc9-341f-4c3d-8f4c-8da09f2ce953.jpeg",
                           AvailableCount = 5,
                           CreationDate = DateTimeOffset.UtcNow,
                           CategoryId = 4,
                           CreatorId = CLIENT_ID
                       },
                       new Ware()
                       {
                           Id = 2,
                           Title = "Halo Leather Sneaker",
                           Description = "EYTYS",
                           Cost = 17362,
                           PhotoLink = "f80bde70-20a4-45f2-b755-5282a46308e1.jpeg",
                           AvailableCount = 3,
                           CreationDate = DateTimeOffset.UtcNow,
                           CategoryId = 4,
                           CreatorId = CLIENT_ID
                       },
                       new Ware()
                       {
                           Id = 3,
                           Title = "Chuck Taylor All Star",
                           Description = "CONVERSE",
                           Cost = 2770,
                           PhotoLink = "9c2e3e64-07f5-44fe-97f8-ffc4fb02d52e.jpeg",
                           AvailableCount = 15,
                           CreationDate = DateTimeOffset.UtcNow,
                           CategoryId = 4,
                           CreatorId = CLIENT_ID
                       },
                       new Ware()
                       {
                           Id = 4,
                           Title = "Verity",
                           Description = "Whose truth is the lie? Stay up all night reading" +
                            "the sensational psychological thriller that has readers obsessed," +
                            "from the #1 New York Times bestselling author of It Ends With Us.",
                           Cost = 257,
                           PhotoLink = "0901295c-e213-4a9c-9463-5744499c839a.jpeg",
                           AvailableCount = 38,
                           CreationDate = DateTimeOffset.UtcNow,
                           CategoryId = 3,
                           CreatorId = CLIENT_ID
                       },
                       new Ware()
                       {
                           Id = 5,
                           Title = "The Myth of Normal",
                           Description = "The Myth of Normal: Trauma, Illness, and Healing in a Toxic Culture",
                           Cost = 841,
                           PhotoLink = "8a16df30-8159-4499-ab51-456d87c9d113.jpeg",
                           AvailableCount = 13,
                           CreationDate = DateTimeOffset.UtcNow,
                           CategoryId = 3,
                           CreatorId = CLIENT_ID
                       },
                       new Ware()
                       {
                           Id = 6,
                           Title = "What If? 2",
                           Description = "What If? 2: Additional Serious Scientific Answers to" +
                            "Absurd Hypothetical Questions.",
                           Cost = 701,
                           PhotoLink = "d4a9e5ea-ae19-49b5-a406-55faf60ba19a.jpeg",
                           AvailableCount = 11,
                           CreationDate = DateTimeOffset.UtcNow,
                           CategoryId = 3,
                           CreatorId = CLIENT_ID
                       },
                       new Ware()
                       {
                           Id = 7,
                           Title = "ASUS ZenBook 14 Flip OLED",
                           Description = "OLED UP5401EA-KN094W (90NB0V41-M004V0)",
                           Cost = 59999,
                           PhotoLink = "4e8967e8-c1d1-45e9-9a6e-758e980e0614.jpeg",
                           AvailableCount = 17,
                           CreationDate = DateTimeOffset.UtcNow,
                           CategoryId = 2,
                           CreatorId = CLIENT_ID
                       },
                       new Ware()
                       {
                           Id = 8,
                           Title = "Apple MacBook Pro",
                           Description = "MK183UA/A",
                           Cost = 114999,
                           PhotoLink = "91dd7d98-b00f-424e-8b2a-960eecf4ca2e.jpeg",
                           AvailableCount = 19,
                           CreationDate = DateTimeOffset.UtcNow,
                           CategoryId = 2,
                           CreatorId = CLIENT_ID
                       },
                       new Ware()
                       {
                           Id = 9,
                           Title = "Apple MacBook Air",
                           Description = "Chip Gold (MGND3)",
                           Cost = 42999,
                           PhotoLink = "e8bb3505-7ca2-4d5d-b2df-9232767abae8.jpeg",
                           AvailableCount = 19,
                           CreationDate = DateTimeOffset.UtcNow,
                           CategoryId = 2,
                           CreatorId = CLIENT_ID
                       });
        }

        #endregion

        #region SeedCharacteristic

        public static void SeedCharacteristic(ModelBuilder builder)
        {
            builder.Entity<Characteristic>()
                   .HasData(
                       new Characteristic()
                       {
                           Id = 1,
                           Name = "Color",
                           Value = "Pine Grey",
                           WareId = 7
                       },
                       new Characteristic()
                       {
                           Id = 2,
                           Name = "Processor",
                           Value = "Intel Core i7-1165G7",
                           WareId = 7
                       },
                       new Characteristic()
                       {
                           Id = 3,
                           Name = "RAM",
                           Value = "16 GB",
                           WareId = 7
                       },
                       new Characteristic()
                       {
                           Id = 4,
                           Name = "SSD",
                           Value = "1 TB",
                           WareId = 7
                       },
                       new Characteristic()
                       {
                           Id = 5,
                           Name = "Weight",
                           Value = "2.1 kg",
                           WareId = 8
                       },
                       new Characteristic()
                       {
                           Id = 6,
                           Name = "Color",
                           Value = "Gray",
                           WareId = 8
                       },
                       new Characteristic()
                       {
                           Id = 7,
                           Name = "Diagonal",
                           Value = "16.2",
                           WareId = 8
                       });
        }

        #endregion
    }
}
