using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class AddSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "f008ba91-7336-4a6d-8217-5030db458fa5", "f008ba91-7336-4a6d-8217-5030db458fa5", "User", "USER" },
                    { "81af2d7e-9df2-4f09-8131-72ffdab373b2", "81af2d7e-9df2-4f09-8131-72ffdab373b2", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PhotoLink", "RegistrationDate", "SecurityStamp", "Surname", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "24e4da3e-931b-4918-b259-85d174d8fcd3", 0, "e5a60e6a-9efc-4593-a916-f2c07fcafca0", "User", "marylou@gmail.com", true, false, null, "Mary", "MARYLOU@GMAIL.COM", "MARYLOU@GMAIL.COM", "AQAAAAEAACcQAAAAENvUrKeZM4WnQBC/azrdjuOIhu1B1SVOTRyypWv6YwjmfnLjR4d9zIZfe/A+/eyPcg==", "+380986734245", false, null, new DateTimeOffset(new DateTime(2022, 8, 12, 16, 49, 57, 716, DateTimeKind.Unspecified).AddTicks(5664), new TimeSpan(0, 0, 0, 0, 0)), "634fcc81-9e99-425e-b0d2-b101ebe3a3ba", "Lou", false, "marylou@gmail.com" },
                    { "22195e96-32e9-4740-bab9-6434e481f4f9", 0, "e14138cc-0197-46d6-a314-f4e02403ac52", "User", "etsukomami@gmail.com", true, false, null, "Etsuko", "ETSUKOMAMI@GMAIL.COM", "ETSUKOMAMI@GMAIL.COM", "AQAAAAEAACcQAAAAEPr9VxhZba26pGJwHMDWcXrEIE1/zPhE7OhFAzbQfYoVLMDaDpbKKAHlvZi9f48yTw==", "+380988931245", false, null, new DateTimeOffset(new DateTime(2022, 8, 12, 16, 49, 57, 719, DateTimeKind.Unspecified).AddTicks(9342), new TimeSpan(0, 0, 0, 0, 0)), "cd8ff034-f309-470c-8895-1abe8a552771", "Mami", false, "etsukomami@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "f008ba91-7336-4a6d-8217-5030db458fa5", "22195e96-32e9-4740-bab9-6434e481f4f9" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "81af2d7e-9df2-4f09-8131-72ffdab373b2", "24e4da3e-931b-4918-b259-85d174d8fcd3" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "f008ba91-7336-4a6d-8217-5030db458fa5", "22195e96-32e9-4740-bab9-6434e481f4f9" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "81af2d7e-9df2-4f09-8131-72ffdab373b2", "24e4da3e-931b-4918-b259-85d174d8fcd3" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "81af2d7e-9df2-4f09-8131-72ffdab373b2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f008ba91-7336-4a6d-8217-5030db458fa5");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "22195e96-32e9-4740-bab9-6434e481f4f9");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "24e4da3e-931b-4918-b259-85d174d8fcd3");
        }
    }
}
