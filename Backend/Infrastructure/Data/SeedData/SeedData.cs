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
    }
}
