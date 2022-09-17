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
                    { "8938cbe3-1fc1-4be7-8fbe-ca600bfaccd2", "8938cbe3-1fc1-4be7-8fbe-ca600bfaccd2", "Client", "CLIENT" },
                    { "84821894-ac58-40cf-aba9-1d1371364e80", "84821894-ac58-40cf-aba9-1d1371364e80", "Admin", "ADMIN" },
                    { "a8771bf8-ed2e-471d-a6e0-d992c46676a4", "a8771bf8-ed2e-471d-a6e0-d992c46676a4", "Courier", "COURIER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PhotoLink", "RegistrationDate", "SecurityStamp", "Surname", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "f85e6302-143d-4697-9507-1273fe4aeffe", 0, "09e923a9-f0dc-4777-905e-00f4d9b2f235", "User", "marylou@gmail.com", true, false, null, "Mary", "MARYLOU@GMAIL.COM", "MARYLOU@GMAIL.COM", "AQAAAAEAACcQAAAAECf4eXv5OTLiwL4xcfJDuGgxf6rxowNWxEROI6D9CAfhnsyyJyIl/KJuvdFFUYh83A==", "+380986734245", false, null, new DateTimeOffset(new DateTime(2022, 9, 17, 1, 19, 15, 813, DateTimeKind.Unspecified).AddTicks(971), new TimeSpan(0, 0, 0, 0, 0)), "c250e5d6-655b-4c69-81b7-9115d2c713c2", "Lou", false, "marylou@gmail.com" },
                    { "7993f3ea-8d6f-4b66-b030-399ed3b4d622", 0, "af0e1b61-af8b-4cca-b19e-01924a68c65d", "User", "etsukomami@gmail.com", true, false, null, "Etsuko", "ETSUKOMAMI@GMAIL.COM", "ETSUKOMAMI@GMAIL.COM", "AQAAAAEAACcQAAAAEP6lLp5s8qkL1L6MNlSvC77LViK8nKEGGKwTyR5EizXea7TnNmn2A/XHiSkleDI+Mg==", "+380988931245", false, null, new DateTimeOffset(new DateTime(2022, 9, 17, 1, 19, 15, 815, DateTimeKind.Unspecified).AddTicks(2985), new TimeSpan(0, 0, 0, 0, 0)), "3470b1cb-3b04-444a-be0f-2f5dca14c933", "Mami", false, "etsukomami@gmail.com" },
                    { "4562db3d-2b36-4729-9507-bd069d14b385", 0, "ad73c9c4-27eb-4928-8f35-0940eedc281a", "User", "yuurimorishita@gmail.com", true, false, null, "Yuuri", "YUURIMORISHITA@GMAIL.COM", "YUURIMORISHITA@GMAIL.COM", "AQAAAAEAACcQAAAAEJ96r6OtR891Iy++J7H+XP6HhHswEZ8DuDhHCRprA6lBQgA8xgQT4ZNeirtKvXRWww==", "+380988931245", false, null, new DateTimeOffset(new DateTime(2022, 9, 17, 1, 19, 15, 815, DateTimeKind.Unspecified).AddTicks(3236), new TimeSpan(0, 0, 0, 0, 0)), "2111399f-ada9-42cb-aba3-c0e1a55982cd", "Morishita", false, "yuurimorishita@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Title" },
                values: new object[] { 1, "Сategory has been deleted" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "8938cbe3-1fc1-4be7-8fbe-ca600bfaccd2", "7993f3ea-8d6f-4b66-b030-399ed3b4d622" },
                    { "84821894-ac58-40cf-aba9-1d1371364e80", "f85e6302-143d-4697-9507-1273fe4aeffe" },
                    { "a8771bf8-ed2e-471d-a6e0-d992c46676a4", "4562db3d-2b36-4729-9507-bd069d14b385" }
                });

            migrationBuilder.InsertData(
                table: "Carts",
                columns: new[] { "Id", "CreatorId", "OrderId" },
                values: new object[] { 1, "7993f3ea-8d6f-4b66-b030-399ed3b4d622", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "a8771bf8-ed2e-471d-a6e0-d992c46676a4", "4562db3d-2b36-4729-9507-bd069d14b385" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "8938cbe3-1fc1-4be7-8fbe-ca600bfaccd2", "7993f3ea-8d6f-4b66-b030-399ed3b4d622" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "84821894-ac58-40cf-aba9-1d1371364e80", "f85e6302-143d-4697-9507-1273fe4aeffe" });

            migrationBuilder.DeleteData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "84821894-ac58-40cf-aba9-1d1371364e80");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8938cbe3-1fc1-4be7-8fbe-ca600bfaccd2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a8771bf8-ed2e-471d-a6e0-d992c46676a4");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4562db3d-2b36-4729-9507-bd069d14b385");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7993f3ea-8d6f-4b66-b030-399ed3b4d622");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f85e6302-143d-4697-9507-1273fe4aeffe");
        }
    }
}
