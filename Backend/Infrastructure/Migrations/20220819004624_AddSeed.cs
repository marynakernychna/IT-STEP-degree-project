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
                    { "f2f52a33-da83-429c-9ced-d21160ebe729", "f2f52a33-da83-429c-9ced-d21160ebe729", "User", "USER" },
                    { "b880f611-9ced-4ac2-94bb-c270678716ca", "b880f611-9ced-4ac2-94bb-c270678716ca", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PhotoLink", "RegistrationDate", "SecurityStamp", "Surname", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "7a1e3183-b12d-47ea-8cec-cf831bf96a16", 0, "4f4bcc14-1fa3-4df9-a276-3b97221c7940", "User", "marylou@gmail.com", true, false, null, "Mary", "MARYLOU@GMAIL.COM", "MARYLOU@GMAIL.COM", "AQAAAAEAACcQAAAAEKkms7Wjf6+Tp0szvHzmadnEOPacKVNnyi9/56hNS9IWcXi4ZhJRg0OIgkfXzc1m7A==", "+380986734245", false, null, new DateTimeOffset(new DateTime(2022, 8, 19, 0, 46, 22, 931, DateTimeKind.Unspecified).AddTicks(6102), new TimeSpan(0, 0, 0, 0, 0)), "85a897a1-05b3-4b5f-b5e9-bfedc74be993", "Lou", false, "marylou@gmail.com" },
                    { "15975170-335a-45b9-9281-781ae15aa976", 0, "20957e1c-2317-47ab-88fe-1ab34e03d673", "User", "etsukomami@gmail.com", true, false, null, "Etsuko", "ETSUKOMAMI@GMAIL.COM", "ETSUKOMAMI@GMAIL.COM", "AQAAAAEAACcQAAAAECs075FIX6jYSKnfEg0mPQB7JXxc/9wP8DC5HKOzjvrG5fyCkdlX6PvDHAFAtfugSQ==", "+380988931245", false, null, new DateTimeOffset(new DateTime(2022, 8, 19, 0, 46, 22, 934, DateTimeKind.Unspecified).AddTicks(6355), new TimeSpan(0, 0, 0, 0, 0)), "bed1b89a-f0f3-4542-aabc-cac8afc20390", "Mami", false, "etsukomami@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "f2f52a33-da83-429c-9ced-d21160ebe729", "15975170-335a-45b9-9281-781ae15aa976" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "b880f611-9ced-4ac2-94bb-c270678716ca", "7a1e3183-b12d-47ea-8cec-cf831bf96a16" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "f2f52a33-da83-429c-9ced-d21160ebe729", "15975170-335a-45b9-9281-781ae15aa976" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "b880f611-9ced-4ac2-94bb-c270678716ca", "7a1e3183-b12d-47ea-8cec-cf831bf96a16" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b880f611-9ced-4ac2-94bb-c270678716ca");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f2f52a33-da83-429c-9ced-d21160ebe729");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "15975170-335a-45b9-9281-781ae15aa976");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7a1e3183-b12d-47ea-8cec-cf831bf96a16");
        }
    }
}
