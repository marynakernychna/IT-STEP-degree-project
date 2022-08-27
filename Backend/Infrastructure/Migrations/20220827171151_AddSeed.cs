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
                    { "b16b5844-4884-4af6-bbac-704265aaf6bf", "b16b5844-4884-4af6-bbac-704265aaf6bf", "User", "USER" },
                    { "1b5522fe-fc20-4a66-b4ff-ef8c926bf77a", "1b5522fe-fc20-4a66-b4ff-ef8c926bf77a", "Admin", "ADMIN" },
                    { "a61d36b0-03c3-4e83-8d8e-c29fab3e7ac0", "a61d36b0-03c3-4e83-8d8e-c29fab3e7ac0", "Courier", "COURIER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PhotoLink", "RegistrationDate", "SecurityStamp", "Surname", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "2587ca65-b446-4e30-8079-4c5ee9a85529", 0, "c119e170-83de-4dbb-9e53-97a8d637779a", "User", "marylou@gmail.com", true, false, null, "Mary", "MARYLOU@GMAIL.COM", "MARYLOU@GMAIL.COM", "AQAAAAEAACcQAAAAEGRNnWLsM/woLaxfB1kBVYkaSUUgwa/GAMqF7V9sG71RCijsn1xW18xV/mNq8v6wCQ==", "+380986734245", false, null, new DateTimeOffset(new DateTime(2022, 8, 27, 17, 11, 50, 40, DateTimeKind.Unspecified).AddTicks(6429), new TimeSpan(0, 0, 0, 0, 0)), "dfbe5c2b-4638-4612-93ab-59941a5c2a12", "Lou", false, "marylou@gmail.com" },
                    { "2d07b264-11fa-4ea3-a56b-670b79da948c", 0, "47e5e251-8dfd-41cb-a2ef-a3a8b7889e02", "User", "etsukomami@gmail.com", true, false, null, "Etsuko", "ETSUKOMAMI@GMAIL.COM", "ETSUKOMAMI@GMAIL.COM", "AQAAAAEAACcQAAAAEEpGIexngVAZdikJEdWZi5gJg1IG4a3nTyCQ+JMNw4CeZ/QHyVA5PPoGvVC/ZLNcWg==", "+380988931245", false, null, new DateTimeOffset(new DateTime(2022, 8, 27, 17, 11, 50, 61, DateTimeKind.Unspecified).AddTicks(3511), new TimeSpan(0, 0, 0, 0, 0)), "23299472-faa4-480b-9a26-dc7b905e4119", "Mami", false, "etsukomami@gmail.com" },
                    { "ac552d0f-2345-465c-a4f3-5b3c84724243", 0, "a7c32514-a535-4025-98a7-0101b8e56105", "User", "yuurimorishita@gmail.com", true, false, null, "Yuuri", "YUURIMORISHITA@GMAIL.COM", "YUURIMORISHITA@GMAIL.COM", "AQAAAAEAACcQAAAAEEjh9acmLhVEsV0bv28tmhok1PnkXH6jIbfc7jl2I/zkrhITqo9bhMx3IjpIRgVsqg==", "+380988931245", false, null, new DateTimeOffset(new DateTime(2022, 8, 27, 17, 11, 50, 61, DateTimeKind.Unspecified).AddTicks(3829), new TimeSpan(0, 0, 0, 0, 0)), "dad2384f-ca24-4b66-bec7-26cd9700e9f5", "Morishita", false, "yuurimorishita@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "b16b5844-4884-4af6-bbac-704265aaf6bf", "2d07b264-11fa-4ea3-a56b-670b79da948c" },
                    { "1b5522fe-fc20-4a66-b4ff-ef8c926bf77a", "2587ca65-b446-4e30-8079-4c5ee9a85529" },
                    { "a61d36b0-03c3-4e83-8d8e-c29fab3e7ac0", "ac552d0f-2345-465c-a4f3-5b3c84724243" }
                });

            migrationBuilder.InsertData(
                table: "Carts",
                columns: new[] { "Id", "CreatorId", "OrderId" },
                values: new object[] { 1, "2d07b264-11fa-4ea3-a56b-670b79da948c", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1b5522fe-fc20-4a66-b4ff-ef8c926bf77a", "2587ca65-b446-4e30-8079-4c5ee9a85529" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "b16b5844-4884-4af6-bbac-704265aaf6bf", "2d07b264-11fa-4ea3-a56b-670b79da948c" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "a61d36b0-03c3-4e83-8d8e-c29fab3e7ac0", "ac552d0f-2345-465c-a4f3-5b3c84724243" });

            migrationBuilder.DeleteData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1b5522fe-fc20-4a66-b4ff-ef8c926bf77a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a61d36b0-03c3-4e83-8d8e-c29fab3e7ac0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b16b5844-4884-4af6-bbac-704265aaf6bf");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2587ca65-b446-4e30-8079-4c5ee9a85529");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2d07b264-11fa-4ea3-a56b-670b79da948c");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ac552d0f-2345-465c-a4f3-5b3c84724243");
        }
    }
}
