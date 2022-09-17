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
                    { "6cbc16ff-d1e1-4780-a7c4-0f382f3fcc94", "6cbc16ff-d1e1-4780-a7c4-0f382f3fcc94", "Client", "CLIENT" },
                    { "dcf03835-34c0-4a96-b296-cafb4d2b34c3", "dcf03835-34c0-4a96-b296-cafb4d2b34c3", "Admin", "ADMIN" },
                    { "879e1ab2-8ff2-45a9-917e-ef646376a045", "879e1ab2-8ff2-45a9-917e-ef646376a045", "Courier", "COURIER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PhotoLink", "RegistrationDate", "SecurityStamp", "Surname", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "aac58995-671c-4cac-ae5c-48042350dbeb", 0, "35726dc0-4fd5-41f3-9511-4bdfcf507ec7", "User", "marylou@gmail.com", true, false, null, "Mary", "MARYLOU@GMAIL.COM", "MARYLOU@GMAIL.COM", "AQAAAAEAACcQAAAAED/FsUEo/0VIMBgSFHHJyuTrGY4P2XfgrvofRYRAydG/KkTww2PP/F7HbbocG9F6lw==", "+380986734245", false, null, new DateTimeOffset(new DateTime(2022, 9, 17, 7, 1, 55, 708, DateTimeKind.Unspecified).AddTicks(7925), new TimeSpan(0, 0, 0, 0, 0)), "4fa3a724-3f12-494d-ac6a-6fbb19a3dbd3", "Lou", false, "marylou@gmail.com" },
                    { "3ccd2d5a-2006-4ad5-b3c1-023f885c1b0a", 0, "e32a3cb4-94f9-462e-9d25-4bdd37395db1", "User", "etsukomami@gmail.com", true, false, null, "Etsuko", "ETSUKOMAMI@GMAIL.COM", "ETSUKOMAMI@GMAIL.COM", "AQAAAAEAACcQAAAAEEZt44JF11xjGZ5O4luQAad0E90/PGsXgSJcLpaPWzxbsQvyswr05zKPCIioQWw6rg==", "+380988931245", false, null, new DateTimeOffset(new DateTime(2022, 9, 17, 7, 1, 55, 710, DateTimeKind.Unspecified).AddTicks(2693), new TimeSpan(0, 0, 0, 0, 0)), "2184aece-4edc-4634-864c-fef1842401f3", "Mami", false, "etsukomami@gmail.com" },
                    { "7fe48240-c8a2-4995-ac12-7d5b9f33c04e", 0, "7b647e02-18a3-44ee-b4af-bae87b19417f", "User", "yuurimorishita@gmail.com", true, false, null, "Yuuri", "YUURIMORISHITA@GMAIL.COM", "YUURIMORISHITA@GMAIL.COM", "AQAAAAEAACcQAAAAEGKRW0jrfDWzgj7HdBT6t8ulhtqT8oZFlbvS0tpPFT24fpxQ59wWkaGNOhvRvh+dYQ==", "+380988931245", false, null, new DateTimeOffset(new DateTime(2022, 9, 17, 7, 1, 55, 710, DateTimeKind.Unspecified).AddTicks(3021), new TimeSpan(0, 0, 0, 0, 0)), "b2750684-fa01-42eb-917c-7fb57382f75a", "Morishita", false, "yuurimorishita@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { 1, "Сategory has been deleted" },
                    { 2, "Laptops" },
                    { 3, "Books" },
                    { 4, "Footwear" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "6cbc16ff-d1e1-4780-a7c4-0f382f3fcc94", "3ccd2d5a-2006-4ad5-b3c1-023f885c1b0a" },
                    { "dcf03835-34c0-4a96-b296-cafb4d2b34c3", "aac58995-671c-4cac-ae5c-48042350dbeb" },
                    { "879e1ab2-8ff2-45a9-917e-ef646376a045", "7fe48240-c8a2-4995-ac12-7d5b9f33c04e" }
                });

            migrationBuilder.InsertData(
                table: "Carts",
                columns: new[] { "Id", "CreatorId", "OrderId" },
                values: new object[] { 1, "3ccd2d5a-2006-4ad5-b3c1-023f885c1b0a", null });

            migrationBuilder.InsertData(
                table: "Wares",
                columns: new[] { "Id", "AvailableCount", "CategoryId", "Cost", "CreationDate", "CreatorId", "Description", "PhotoLink", "Title" },
                values: new object[,]
                {
                    { 1, 5, 4, 40450.0, new DateTimeOffset(new DateTime(2022, 9, 17, 7, 1, 55, 746, DateTimeKind.Unspecified).AddTicks(3229), new TimeSpan(0, 0, 0, 0, 0)), "3ccd2d5a-2006-4ad5-b3c1-023f885c1b0a", "GIANVITO ROSSI", "3d67cfc9-341f-4c3d-8f4c-8da09f2ce953.jpeg", "Ankle Boots" },
                    { 2, 3, 4, 17362.0, new DateTimeOffset(new DateTime(2022, 9, 17, 7, 1, 55, 746, DateTimeKind.Unspecified).AddTicks(4963), new TimeSpan(0, 0, 0, 0, 0)), "3ccd2d5a-2006-4ad5-b3c1-023f885c1b0a", "EYTYS", "f80bde70-20a4-45f2-b755-5282a46308e1.jpeg", "Halo Leather Sneaker" },
                    { 3, 15, 4, 2770.0, new DateTimeOffset(new DateTime(2022, 9, 17, 7, 1, 55, 746, DateTimeKind.Unspecified).AddTicks(4968), new TimeSpan(0, 0, 0, 0, 0)), "3ccd2d5a-2006-4ad5-b3c1-023f885c1b0a", "CONVERSE", "9c2e3e64-07f5-44fe-97f8-ffc4fb02d52e.jpeg", "Chuck Taylor All Star" },
                    { 4, 38, 3, 257.0, new DateTimeOffset(new DateTime(2022, 9, 17, 7, 1, 55, 746, DateTimeKind.Unspecified).AddTicks(4971), new TimeSpan(0, 0, 0, 0, 0)), "3ccd2d5a-2006-4ad5-b3c1-023f885c1b0a", "Whose truth is the lie? Stay up all night readingthe sensational psychological thriller that has readers obsessed,from the #1 New York Times bestselling author of It Ends With Us.", "0901295c-e213-4a9c-9463-5744499c839a.jpeg", "Verity" },
                    { 5, 13, 3, 841.0, new DateTimeOffset(new DateTime(2022, 9, 17, 7, 1, 55, 746, DateTimeKind.Unspecified).AddTicks(4975), new TimeSpan(0, 0, 0, 0, 0)), "3ccd2d5a-2006-4ad5-b3c1-023f885c1b0a", "The Myth of Normal: Trauma, Illness, and Healing in a Toxic Culture", "8a16df30-8159-4499-ab51-456d87c9d113.jpeg", "The Myth of Normal" },
                    { 6, 11, 3, 701.0, new DateTimeOffset(new DateTime(2022, 9, 17, 7, 1, 55, 746, DateTimeKind.Unspecified).AddTicks(4978), new TimeSpan(0, 0, 0, 0, 0)), "3ccd2d5a-2006-4ad5-b3c1-023f885c1b0a", "What If? 2: Additional Serious Scientific Answers toAbsurd Hypothetical Questions.", "d4a9e5ea-ae19-49b5-a406-55faf60ba19a.jpeg", "What If? 2" },
                    { 7, 17, 2, 59999.0, new DateTimeOffset(new DateTime(2022, 9, 17, 7, 1, 55, 746, DateTimeKind.Unspecified).AddTicks(4981), new TimeSpan(0, 0, 0, 0, 0)), "3ccd2d5a-2006-4ad5-b3c1-023f885c1b0a", "OLED UP5401EA-KN094W (90NB0V41-M004V0)", "4e8967e8-c1d1-45e9-9a6e-758e980e0614.jpeg", "ASUS ZenBook 14 Flip OLED" },
                    { 8, 19, 2, 114999.0, new DateTimeOffset(new DateTime(2022, 9, 17, 7, 1, 55, 746, DateTimeKind.Unspecified).AddTicks(4985), new TimeSpan(0, 0, 0, 0, 0)), "3ccd2d5a-2006-4ad5-b3c1-023f885c1b0a", "MK183UA/A", "91dd7d98-b00f-424e-8b2a-960eecf4ca2e.jpeg", "Apple MacBook Pro" },
                    { 9, 19, 2, 42999.0, new DateTimeOffset(new DateTime(2022, 9, 17, 7, 1, 55, 746, DateTimeKind.Unspecified).AddTicks(4988), new TimeSpan(0, 0, 0, 0, 0)), "3ccd2d5a-2006-4ad5-b3c1-023f885c1b0a", "Chip Gold (MGND3)", "e8bb3505-7ca2-4d5d-b2df-9232767abae8.jpeg", "Apple MacBook Air" }
                });

            migrationBuilder.InsertData(
                table: "Characteristics",
                columns: new[] { "Id", "Name", "Value", "WareId" },
                values: new object[,]
                {
                    { 1, "Color", "Pine Grey", 7 },
                    { 2, "Processor", "Intel Core i7-1165G7", 7 },
                    { 3, "RAM", "16 GB", 7 },
                    { 4, "SSD", "1 TB", 7 },
                    { 5, "Weight", "2.1 kg", 8 },
                    { 6, "Color", "Gray", 8 },
                    { 7, "Diagonal", "16.2", 8 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "6cbc16ff-d1e1-4780-a7c4-0f382f3fcc94", "3ccd2d5a-2006-4ad5-b3c1-023f885c1b0a" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "879e1ab2-8ff2-45a9-917e-ef646376a045", "7fe48240-c8a2-4995-ac12-7d5b9f33c04e" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "dcf03835-34c0-4a96-b296-cafb4d2b34c3", "aac58995-671c-4cac-ae5c-48042350dbeb" });

            migrationBuilder.DeleteData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Characteristics",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Characteristics",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Characteristics",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Characteristics",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Characteristics",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Characteristics",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Characteristics",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Wares",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Wares",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Wares",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Wares",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Wares",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Wares",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Wares",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6cbc16ff-d1e1-4780-a7c4-0f382f3fcc94");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "879e1ab2-8ff2-45a9-917e-ef646376a045");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dcf03835-34c0-4a96-b296-cafb4d2b34c3");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7fe48240-c8a2-4995-ac12-7d5b9f33c04e");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aac58995-671c-4cac-ae5c-48042350dbeb");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Wares",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Wares",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3ccd2d5a-2006-4ad5-b3c1-023f885c1b0a");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
