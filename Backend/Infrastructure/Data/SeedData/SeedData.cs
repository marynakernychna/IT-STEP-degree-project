using Core.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure.Data.SeedData
{
    public static class SeedData
    {
        #region Identity roles' ids

        private static readonly string ROLE_USER_ID = Guid.NewGuid().ToString();

        #endregion

        public static void Seed(this ModelBuilder builder)
        {
            SeedIdentityRole(builder);
        }

        #region SeedIdentityRoles

        public static void SeedIdentityRole(ModelBuilder builder) =>
            builder.Entity<IdentityRole>()
                   .HasData(
                        new IdentityRole()
                        {
                            Id = ROLE_USER_ID,
                            Name = IdentityRoleNames.User.ToString(),
                            NormalizedName = IdentityRoleNames.User.ToString().ToUpper(),
                            ConcurrencyStamp = ROLE_USER_ID
                        });

        #endregion
    }
}
