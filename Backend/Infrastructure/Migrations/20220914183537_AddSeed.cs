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
                    { "3548abec-ea91-4049-8415-7bda3675362d", "3548abec-ea91-4049-8415-7bda3675362d", "Client", "CLIENT" },
                    { "2c8e9e26-4d0e-4f30-9645-934b39a8bd34", "2c8e9e26-4d0e-4f30-9645-934b39a8bd34", "Admin", "ADMIN" },
                    { "a29695b3-1320-4005-a266-6e045cf0a93d", "a29695b3-1320-4005-a266-6e045cf0a93d", "Courier", "COURIER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PhotoLink", "RegistrationDate", "SecurityStamp", "Surname", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "6a6788fd-a2c8-46a7-8562-184aa7291426", 0, "ef5ce1eb-a4d3-4f30-8c09-41183cd6b89f", "User", "marylou@gmail.com", true, false, null, "Mary", "MARYLOU@GMAIL.COM", "MARYLOU@GMAIL.COM", "AQAAAAEAACcQAAAAEHD4iwGeJo0g2UJfsDC1zd1nN1BacWRFKJ9I4yBfrFKvSrZOrzpgtqeAjg77i85pvQ==", "+380986734245", false, null, new DateTimeOffset(new DateTime(2022, 9, 14, 18, 35, 36, 37, DateTimeKind.Unspecified).AddTicks(2095), new TimeSpan(0, 0, 0, 0, 0)), "de936898-d7e9-4da5-831e-fba05b2c4201", "Lou", false, "marylou@gmail.com" },
                    { "4ae86c5f-694a-4bbc-8bc7-7b6ac22f9d25", 0, "99d0f983-2372-4603-b533-aa93c94b6403", "User", "etsukomami@gmail.com", true, false, null, "Etsuko", "ETSUKOMAMI@GMAIL.COM", "ETSUKOMAMI@GMAIL.COM", "AQAAAAEAACcQAAAAEGDC60MHt+B7a1ziHQZdB7iGV6s79XyXooKJ6PLbAn2D2NL85IY654MfsbEIMRznUA==", "+380988931245", false, null, new DateTimeOffset(new DateTime(2022, 9, 14, 18, 35, 36, 46, DateTimeKind.Unspecified).AddTicks(3310), new TimeSpan(0, 0, 0, 0, 0)), "95322c48-642b-4147-94f9-f3984bb24002", "Mami", false, "etsukomami@gmail.com" },
                    { "05771bff-38e5-47eb-8c58-8774e319ae80", 0, "ac666496-f8e3-4728-a5ee-e8fc43b36d7b", "User", "yuurimorishita@gmail.com", true, false, null, "Yuuri", "YUURIMORISHITA@GMAIL.COM", "YUURIMORISHITA@GMAIL.COM", "AQAAAAEAACcQAAAAECfHBEcqAR6w0nw7VXf8I4JUu6K0QuUIk/DjR8wdB25W5A3qTEJfV/yptP5l6Aqwcg==", "+380988931245", false, null, new DateTimeOffset(new DateTime(2022, 9, 14, 18, 35, 36, 46, DateTimeKind.Unspecified).AddTicks(3710), new TimeSpan(0, 0, 0, 0, 0)), "bb22ba08-21bb-42a6-b810-063023e7ae2d", "Morishita", false, "yuurimorishita@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "3548abec-ea91-4049-8415-7bda3675362d", "4ae86c5f-694a-4bbc-8bc7-7b6ac22f9d25" },
                    { "2c8e9e26-4d0e-4f30-9645-934b39a8bd34", "6a6788fd-a2c8-46a7-8562-184aa7291426" },
                    { "a29695b3-1320-4005-a266-6e045cf0a93d", "05771bff-38e5-47eb-8c58-8774e319ae80" }
                });

            migrationBuilder.InsertData(
                table: "Carts",
                columns: new[] { "Id", "CreatorId", "OrderId" },
                values: new object[] { 1, "4ae86c5f-694a-4bbc-8bc7-7b6ac22f9d25", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "a29695b3-1320-4005-a266-6e045cf0a93d", "05771bff-38e5-47eb-8c58-8774e319ae80" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3548abec-ea91-4049-8415-7bda3675362d", "4ae86c5f-694a-4bbc-8bc7-7b6ac22f9d25" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2c8e9e26-4d0e-4f30-9645-934b39a8bd34", "6a6788fd-a2c8-46a7-8562-184aa7291426" });

            migrationBuilder.DeleteData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c8e9e26-4d0e-4f30-9645-934b39a8bd34");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3548abec-ea91-4049-8415-7bda3675362d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a29695b3-1320-4005-a266-6e045cf0a93d");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "05771bff-38e5-47eb-8c58-8774e319ae80");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4ae86c5f-694a-4bbc-8bc7-7b6ac22f9d25");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6a6788fd-a2c8-46a7-8562-184aa7291426");
        }
    }
}
