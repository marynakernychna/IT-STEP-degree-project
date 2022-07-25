using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class AddPhoneNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "816fa023-3a86-42f5-8b2f-42a00374f6d4", "0440a2c3-b4c8-42e5-9efb-f6cfad0a41f6" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "73e377b2-ff33-4160-8466-810b2c11732e", "f5d5eb44-307a-4b16-9942-d3d828d3bc72" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "73e377b2-ff33-4160-8466-810b2c11732e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "816fa023-3a86-42f5-8b2f-42a00374f6d4");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0440a2c3-b4c8-42e5-9efb-f6cfad0a41f6");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f5d5eb44-307a-4b16-9942-d3d828d3bc72");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "89e7ed04-8efa-47a1-b6af-dea249b42129", "89e7ed04-8efa-47a1-b6af-dea249b42129", "User", "USER" },
                    { "408c5c10-7170-4a4e-a824-6627ddfad31a", "408c5c10-7170-4a4e-a824-6627ddfad31a", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PhotoLink", "RegistrationDate", "SecurityStamp", "Surname", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "857f8158-7f6f-4253-baf2-cc8da583d63c", 0, "50aa30d5-7264-40c2-a0bb-d9e3a95d48d6", "User", "marylou@gmail.com", true, false, null, "Mary", "MARYLOU@GMAIL.COM", "MARYLOU@GMAIL.COM", "AQAAAAEAACcQAAAAEOGgDKt1RNs86CsuS7HEsM2aYT1qzoSGyE+2DFAFSwnEIbSjTo5mFazEKWkIlcxDTg==", "+380986734245", false, null, new DateTimeOffset(new DateTime(2022, 7, 25, 16, 38, 9, 311, DateTimeKind.Unspecified).AddTicks(1231), new TimeSpan(0, 0, 0, 0, 0)), "0cdf5e08-3cea-44fa-aca7-7a2f14b7e73a", "Lou", false, "marylou@gmail.com" },
                    { "5606c370-37a6-4cdd-93c4-b4f605ac8abf", 0, "d02dcfc8-0f7c-4c32-a47d-60281b7c9524", "User", "etsukomami@gmail.com", true, false, null, "Etsuko", "ETSUKOMAMI@GMAIL.COM", "ETSUKOMAMI@GMAIL.COM", "AQAAAAEAACcQAAAAEC4Dr0dJ8eA7vCt2w1DcFNL/+p06HUAunw6K7wte6Ewgl6l9VixHsCCz2T7jnRZ3og==", "+380988931245", false, null, new DateTimeOffset(new DateTime(2022, 7, 25, 16, 38, 9, 313, DateTimeKind.Unspecified).AddTicks(6130), new TimeSpan(0, 0, 0, 0, 0)), "212fa3ba-7c70-4db7-878e-c3a17bea3cfb", "Mami", false, "etsukomami@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "89e7ed04-8efa-47a1-b6af-dea249b42129", "5606c370-37a6-4cdd-93c4-b4f605ac8abf" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "408c5c10-7170-4a4e-a824-6627ddfad31a", "857f8158-7f6f-4253-baf2-cc8da583d63c" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "89e7ed04-8efa-47a1-b6af-dea249b42129", "5606c370-37a6-4cdd-93c4-b4f605ac8abf" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "408c5c10-7170-4a4e-a824-6627ddfad31a", "857f8158-7f6f-4253-baf2-cc8da583d63c" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "408c5c10-7170-4a4e-a824-6627ddfad31a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "89e7ed04-8efa-47a1-b6af-dea249b42129");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5606c370-37a6-4cdd-93c4-b4f605ac8abf");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "857f8158-7f6f-4253-baf2-cc8da583d63c");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "73e377b2-ff33-4160-8466-810b2c11732e", "73e377b2-ff33-4160-8466-810b2c11732e", "User", "USER" },
                    { "816fa023-3a86-42f5-8b2f-42a00374f6d4", "816fa023-3a86-42f5-8b2f-42a00374f6d4", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PhotoLink", "RegistrationDate", "SecurityStamp", "Surname", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "0440a2c3-b4c8-42e5-9efb-f6cfad0a41f6", 0, "e454323e-d29f-4a3d-a0f4-50e7df385a25", "User", "marylou@gmail.com", true, false, null, "Mary", "MARYLOU@GMAIL.COM", "MARYLOU@GMAIL.COM", "AQAAAAEAACcQAAAAEAKiAfFahsAy9Dbn4NtU3GGJXOYtfJ0Y8MtrJMrlFITzygowCeA5IBywgXwezZqlog==", null, false, null, new DateTimeOffset(new DateTime(2022, 7, 11, 16, 23, 41, 937, DateTimeKind.Unspecified).AddTicks(7134), new TimeSpan(0, 0, 0, 0, 0)), "248eb8b7-c566-4154-b94b-9a60924231a2", "Lou", false, "marylou@gmail.com" },
                    { "f5d5eb44-307a-4b16-9942-d3d828d3bc72", 0, "c2f7e68e-6510-4e30-9041-54028f4bcbfc", "User", "etsukomami@gmail.com", true, false, null, "Etsuko", "ETSUKOMAMI@GMAIL.COM", "ETSUKOMAMI@GMAIL.COM", "AQAAAAEAACcQAAAAENsAJfzE+vWdceiIl/3nXLKHAV77EVwXwHpyYx/e2AJ+WGZcjwHC8xwN9VTPlVRBLQ==", null, false, null, new DateTimeOffset(new DateTime(2022, 7, 11, 16, 23, 41, 940, DateTimeKind.Unspecified).AddTicks(9732), new TimeSpan(0, 0, 0, 0, 0)), "09f06911-9bfd-494d-81c8-7dd389d2f316", "Mami", false, "etsukomami@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "73e377b2-ff33-4160-8466-810b2c11732e", "f5d5eb44-307a-4b16-9942-d3d828d3bc72" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "816fa023-3a86-42f5-8b2f-42a00374f6d4", "0440a2c3-b4c8-42e5-9efb-f6cfad0a41f6" });
        }
    }
}
