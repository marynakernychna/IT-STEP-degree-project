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
                    { "34ef1daf-d38b-4ef7-87cf-6f7a6dae0dd5", "34ef1daf-d38b-4ef7-87cf-6f7a6dae0dd5", "User", "USER" },
                    { "17d369ee-11e3-40dd-af63-30b9eef221b6", "17d369ee-11e3-40dd-af63-30b9eef221b6", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PhotoLink", "RegistrationDate", "SecurityStamp", "Surname", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "08067daf-ed09-493f-a929-32b18ab898b0", 0, "e508e50e-705d-4164-8464-af01a6109507", "User", "marylou@gmail.com", true, false, null, "Mary", "MARYLOU@GMAIL.COM", "MARYLOU@GMAIL.COM", "AQAAAAEAACcQAAAAEDMRU7WisylSrOcwEiB1qKKSQR2akTjx7eCx5P7qCJZA2q//ob/AW8ftYEf3dXXOVg==", "+380986734245", false, null, new DateTimeOffset(new DateTime(2022, 7, 25, 16, 59, 55, 297, DateTimeKind.Unspecified).AddTicks(1508), new TimeSpan(0, 0, 0, 0, 0)), "689ca18a-dde7-4aa6-8abf-cf74a8b48773", "Lou", false, "marylou@gmail.com" },
                    { "c9fff39d-9cb5-4143-af00-4237b2f5253c", 0, "aac5b2c4-c9aa-417b-94eb-38c7c12bcc7b", "User", "etsukomami@gmail.com", true, false, null, "Etsuko", "ETSUKOMAMI@GMAIL.COM", "ETSUKOMAMI@GMAIL.COM", "AQAAAAEAACcQAAAAEE8G6ExAwpdQoXykI77C8zvr0y2mT5DW/G2A/DDCzRsvOPGQ5nkS0LvDP/s5SvNpFA==", "+380988931245", false, null, new DateTimeOffset(new DateTime(2022, 7, 25, 16, 59, 55, 299, DateTimeKind.Unspecified).AddTicks(5066), new TimeSpan(0, 0, 0, 0, 0)), "4c9683a5-706a-4fa5-827d-e57dd5ad176c", "Mami", false, "etsukomami@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "34ef1daf-d38b-4ef7-87cf-6f7a6dae0dd5", "c9fff39d-9cb5-4143-af00-4237b2f5253c" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "17d369ee-11e3-40dd-af63-30b9eef221b6", "08067daf-ed09-493f-a929-32b18ab898b0" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "17d369ee-11e3-40dd-af63-30b9eef221b6", "08067daf-ed09-493f-a929-32b18ab898b0" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "34ef1daf-d38b-4ef7-87cf-6f7a6dae0dd5", "c9fff39d-9cb5-4143-af00-4237b2f5253c" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "17d369ee-11e3-40dd-af63-30b9eef221b6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "34ef1daf-d38b-4ef7-87cf-6f7a6dae0dd5");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "08067daf-ed09-493f-a929-32b18ab898b0");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c9fff39d-9cb5-4143-af00-4237b2f5253c");
        }
    }
}
