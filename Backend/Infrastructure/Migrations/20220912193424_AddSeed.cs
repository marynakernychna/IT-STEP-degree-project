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
                    { "846af0cc-1ef6-410d-9756-f8ed75736ffa", "846af0cc-1ef6-410d-9756-f8ed75736ffa", "Client", "CLIENT" },
                    { "d2982855-ef46-4149-8d4f-0210fda45664", "d2982855-ef46-4149-8d4f-0210fda45664", "Admin", "ADMIN" },
                    { "ebf9e6bc-4768-401a-82ba-11072cc08969", "ebf9e6bc-4768-401a-82ba-11072cc08969", "Courier", "COURIER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PhotoLink", "RegistrationDate", "SecurityStamp", "Surname", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "546ebf8d-19d6-486f-8461-f80fc6e0718a", 0, "21d096cc-6d00-42e7-91de-bffe78d08c5d", "User", "marylou@gmail.com", true, false, null, "Mary", "MARYLOU@GMAIL.COM", "MARYLOU@GMAIL.COM", "AQAAAAEAACcQAAAAEL1NrfsP3FhJi34wM7BgpSeP2h5Kspmw7vP0WhFXh2iwOqePWkusjPH3CGhA36PSJg==", "+380986734245", false, null, new DateTimeOffset(new DateTime(2022, 9, 12, 19, 34, 22, 603, DateTimeKind.Unspecified).AddTicks(9568), new TimeSpan(0, 0, 0, 0, 0)), "f496bfc6-7fd1-419a-a548-fefbd8b83764", "Lou", false, "marylou@gmail.com" },
                    { "fa6bc988-be66-43bd-8b03-f9af7f08a99d", 0, "36318d7e-f06e-426d-aa09-3ce81973c9dd", "User", "etsukomami@gmail.com", true, false, null, "Etsuko", "ETSUKOMAMI@GMAIL.COM", "ETSUKOMAMI@GMAIL.COM", "AQAAAAEAACcQAAAAEEQb69fmlYSy1daoVq37ruq0yFUo6Pt/KhkTKWKUDfBB5gImGdyNr3ReZgo4txvsUA==", "+380988931245", false, null, new DateTimeOffset(new DateTime(2022, 9, 12, 19, 34, 22, 606, DateTimeKind.Unspecified).AddTicks(5763), new TimeSpan(0, 0, 0, 0, 0)), "dd42597b-2875-4a29-91d0-e1a59deddda9", "Mami", false, "etsukomami@gmail.com" },
                    { "54edeb5a-c9fc-41de-bef6-644f3b85a67b", 0, "5d55c7aa-ee72-43b6-9618-941e36fb1a02", "User", "yuurimorishita@gmail.com", true, false, null, "Yuuri", "YUURIMORISHITA@GMAIL.COM", "YUURIMORISHITA@GMAIL.COM", "AQAAAAEAACcQAAAAEIE2IaQC7F/09Beia/I6w7PyT9go87lm5cusu/toGgBPStymba5VwZEjkgdXJHlvdg==", "+380988931245", false, null, new DateTimeOffset(new DateTime(2022, 9, 12, 19, 34, 22, 606, DateTimeKind.Unspecified).AddTicks(6074), new TimeSpan(0, 0, 0, 0, 0)), "7ad1e465-d94a-4ed4-90dd-55019e1a26c8", "Morishita", false, "yuurimorishita@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "846af0cc-1ef6-410d-9756-f8ed75736ffa", "fa6bc988-be66-43bd-8b03-f9af7f08a99d" },
                    { "d2982855-ef46-4149-8d4f-0210fda45664", "546ebf8d-19d6-486f-8461-f80fc6e0718a" },
                    { "ebf9e6bc-4768-401a-82ba-11072cc08969", "54edeb5a-c9fc-41de-bef6-644f3b85a67b" }
                });

            migrationBuilder.InsertData(
                table: "Carts",
                columns: new[] { "Id", "CreatorId", "OrderId" },
                values: new object[] { 1, "fa6bc988-be66-43bd-8b03-f9af7f08a99d", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "d2982855-ef46-4149-8d4f-0210fda45664", "546ebf8d-19d6-486f-8461-f80fc6e0718a" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "ebf9e6bc-4768-401a-82ba-11072cc08969", "54edeb5a-c9fc-41de-bef6-644f3b85a67b" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "846af0cc-1ef6-410d-9756-f8ed75736ffa", "fa6bc988-be66-43bd-8b03-f9af7f08a99d" });

            migrationBuilder.DeleteData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "846af0cc-1ef6-410d-9756-f8ed75736ffa");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d2982855-ef46-4149-8d4f-0210fda45664");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ebf9e6bc-4768-401a-82ba-11072cc08969");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "546ebf8d-19d6-486f-8461-f80fc6e0718a");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "54edeb5a-c9fc-41de-bef6-644f3b85a67b");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fa6bc988-be66-43bd-8b03-f9af7f08a99d");
        }
    }
}
