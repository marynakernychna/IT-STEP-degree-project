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
                    { "391a2601-78a6-4745-bcb8-f363d29b6e82", "391a2601-78a6-4745-bcb8-f363d29b6e82", "User", "USER" },
                    { "7ca763a9-5c97-4bc9-84d9-74fd52d41dd4", "7ca763a9-5c97-4bc9-84d9-74fd52d41dd4", "Admin", "ADMIN" },
                    { "f829a12f-3534-47ac-a5e0-806b9324a585", "f829a12f-3534-47ac-a5e0-806b9324a585", "Courier", "COURIER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PhotoLink", "RegistrationDate", "SecurityStamp", "Surname", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "abf9d23b-26d2-4e50-ab04-19e4cfabc736", 0, "fa5854c6-b085-4e4e-8645-7687033e0a1b", "User", "marylou@gmail.com", true, false, null, "Mary", "MARYLOU@GMAIL.COM", "MARYLOU@GMAIL.COM", "AQAAAAEAACcQAAAAEODuSGSgut58OoovNtnBGaXhVGM8mZgDXCSAxRYh1WWnutExSDZD6gQQy0EHHuZ38w==", "+380986734245", false, null, new DateTimeOffset(new DateTime(2022, 9, 7, 9, 47, 13, 366, DateTimeKind.Unspecified).AddTicks(3953), new TimeSpan(0, 0, 0, 0, 0)), "a65e6195-6c82-4f99-a388-1798b29a3622", "Lou", false, "marylou@gmail.com" },
                    { "02162c7f-ceaa-405d-b71d-bf397569d5fd", 0, "08ade8d3-ae08-42d0-b96c-8fa6e3b47c44", "User", "etsukomami@gmail.com", true, false, null, "Etsuko", "ETSUKOMAMI@GMAIL.COM", "ETSUKOMAMI@GMAIL.COM", "AQAAAAEAACcQAAAAEGh726G1dCHDAQgLPrx8wGW9hrmItYfwQgY4lHiMIGltsn2z7boO916WIl3mxNDEzg==", "+380988931245", false, null, new DateTimeOffset(new DateTime(2022, 9, 7, 9, 47, 13, 368, DateTimeKind.Unspecified).AddTicks(1001), new TimeSpan(0, 0, 0, 0, 0)), "fbb24125-753c-4f21-9c4b-3108bf9c84a6", "Mami", false, "etsukomami@gmail.com" },
                    { "8f7b5096-1a69-477e-852d-987bf9bf9fc9", 0, "bd670b4c-f25f-4656-9dad-129bb5ad6b52", "User", "yuurimorishita@gmail.com", true, false, null, "Yuuri", "YUURIMORISHITA@GMAIL.COM", "YUURIMORISHITA@GMAIL.COM", "AQAAAAEAACcQAAAAEFhba1A1dl7wTUuf0vna9AeIi+HYe96sRQSpDrlDBvYUitgrbOb/zyZzu9yfW6xuWw==", "+380988931245", false, null, new DateTimeOffset(new DateTime(2022, 9, 7, 9, 47, 13, 368, DateTimeKind.Unspecified).AddTicks(1086), new TimeSpan(0, 0, 0, 0, 0)), "9dead8a2-ea6c-4f5d-894f-6c3df1fc6934", "Morishita", false, "yuurimorishita@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "391a2601-78a6-4745-bcb8-f363d29b6e82", "02162c7f-ceaa-405d-b71d-bf397569d5fd" },
                    { "7ca763a9-5c97-4bc9-84d9-74fd52d41dd4", "abf9d23b-26d2-4e50-ab04-19e4cfabc736" },
                    { "f829a12f-3534-47ac-a5e0-806b9324a585", "8f7b5096-1a69-477e-852d-987bf9bf9fc9" }
                });

            migrationBuilder.InsertData(
                table: "Carts",
                columns: new[] { "Id", "CreatorId", "OrderId" },
                values: new object[] { 1, "02162c7f-ceaa-405d-b71d-bf397569d5fd", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "391a2601-78a6-4745-bcb8-f363d29b6e82", "02162c7f-ceaa-405d-b71d-bf397569d5fd" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "f829a12f-3534-47ac-a5e0-806b9324a585", "8f7b5096-1a69-477e-852d-987bf9bf9fc9" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "7ca763a9-5c97-4bc9-84d9-74fd52d41dd4", "abf9d23b-26d2-4e50-ab04-19e4cfabc736" });

            migrationBuilder.DeleteData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "391a2601-78a6-4745-bcb8-f363d29b6e82");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7ca763a9-5c97-4bc9-84d9-74fd52d41dd4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f829a12f-3534-47ac-a5e0-806b9324a585");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02162c7f-ceaa-405d-b71d-bf397569d5fd");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8f7b5096-1a69-477e-852d-987bf9bf9fc9");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "abf9d23b-26d2-4e50-ab04-19e4cfabc736");
        }
    }
}
