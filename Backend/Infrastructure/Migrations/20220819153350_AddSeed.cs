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
                    { "46eb935e-0dd8-406c-bf6c-bd0c6c2d5358", "46eb935e-0dd8-406c-bf6c-bd0c6c2d5358", "User", "USER" },
                    { "a81cba07-81a2-4df3-b17c-ebe3d27758ff", "a81cba07-81a2-4df3-b17c-ebe3d27758ff", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PhotoLink", "RegistrationDate", "SecurityStamp", "Surname", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "5d52f215-96eb-495d-97ca-a7ba810a4a9a", 0, "3280676b-3789-4bdb-991e-b139b501c727", "User", "marylou@gmail.com", true, false, null, "Mary", "MARYLOU@GMAIL.COM", "MARYLOU@GMAIL.COM", "AQAAAAEAACcQAAAAEJ7S42vVrxTDVSqB9kvwjE/Z+adXvbDJ3a4R8x1NjWNRL+PhcdtLqR9deL6D7BJ7ZQ==", "+380986734245", false, null, new DateTimeOffset(new DateTime(2022, 8, 19, 15, 33, 49, 158, DateTimeKind.Unspecified).AddTicks(3991), new TimeSpan(0, 0, 0, 0, 0)), "80f1e68f-1138-4357-aaf5-3bff768be85d", "Lou", false, "marylou@gmail.com" },
                    { "3cb616b5-deff-4176-b794-88d6fda8e5de", 0, "93cd0e0e-d025-4cf5-9bd7-d4ca030811ef", "User", "etsukomami@gmail.com", true, false, null, "Etsuko", "ETSUKOMAMI@GMAIL.COM", "ETSUKOMAMI@GMAIL.COM", "AQAAAAEAACcQAAAAEMIgFO2TZLPpkQjlYASmPNZSE8N6CymHOsCgAhN9V4Th28AzlGENc6uODOKI0Ih6HA==", "+380988931245", false, null, new DateTimeOffset(new DateTime(2022, 8, 19, 15, 33, 49, 160, DateTimeKind.Unspecified).AddTicks(7903), new TimeSpan(0, 0, 0, 0, 0)), "65ef944c-5452-4320-bd75-7bedbd4ceaf8", "Mami", false, "etsukomami@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "46eb935e-0dd8-406c-bf6c-bd0c6c2d5358", "3cb616b5-deff-4176-b794-88d6fda8e5de" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "a81cba07-81a2-4df3-b17c-ebe3d27758ff", "5d52f215-96eb-495d-97ca-a7ba810a4a9a" });

            migrationBuilder.InsertData(
                table: "Carts",
                columns: new[] { "Id", "CreatorId", "OrderId" },
                values: new object[] { 1, "3cb616b5-deff-4176-b794-88d6fda8e5de", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "46eb935e-0dd8-406c-bf6c-bd0c6c2d5358", "3cb616b5-deff-4176-b794-88d6fda8e5de" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "a81cba07-81a2-4df3-b17c-ebe3d27758ff", "5d52f215-96eb-495d-97ca-a7ba810a4a9a" });

            migrationBuilder.DeleteData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "46eb935e-0dd8-406c-bf6c-bd0c6c2d5358");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a81cba07-81a2-4df3-b17c-ebe3d27758ff");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3cb616b5-deff-4176-b794-88d6fda8e5de");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5d52f215-96eb-495d-97ca-a7ba810a4a9a");
        }
    }
}
