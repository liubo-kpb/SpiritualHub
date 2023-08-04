using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpiritualHub.Data.Migrations
{
    public partial class SeedUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Publishers",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("194974cd-73f0-4946-ba85-710d4061472d"), 0, "d07c8583-8399-4d14-8495-4fc3fbf5e6c7", "publisher@spirits.com", false, false, null, null, null, "AQAAAAEAACcQAAAAEC0TLDGDWdISET2q/c1f811TYR+18s8qgQjVEtEx34jFISs/xjzq1JFsWRZ5rrVlGg==", null, false, null, false, "publisher" },
                    { new Guid("1fd95f69-4f9d-4671-b126-cefcf4b8a95e"), 0, "b75137a8-f94b-491f-ae5a-77596e7ae7aa", "user@mail.com", false, false, null, null, null, "AQAAAAEAACcQAAAAEEZNQ9+0Evp20LWX0y0VfLV0jp8LVMHm1VliOIa92sQF7Ga2DiOEYTqLlOONe1DApg==", null, false, null, false, "user" }
                });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("15f326bc-f47f-487f-8764-5fb8fd5c448a"),
                column: "CreatedOn",
                value: new DateTime(2023, 8, 4, 13, 2, 0, 474, DateTimeKind.Utc).AddTicks(8614));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("3db097df-7c7c-4c4e-b546-d4555c4c1521"),
                column: "CreatedOn",
                value: new DateTime(2023, 8, 4, 13, 2, 0, 474, DateTimeKind.Utc).AddTicks(8644));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("45bb1c09-b50d-4d47-8fdb-fbfb53086922"),
                column: "CreatedOn",
                value: new DateTime(2023, 8, 4, 13, 2, 0, 474, DateTimeKind.Utc).AddTicks(8636));

            migrationBuilder.UpdateData(
                table: "SubscriptionTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Type",
                value: "Monthly");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("194974cd-73f0-4946-ba85-710d4061472d"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("1fd95f69-4f9d-4671-b126-cefcf4b8a95e"));

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Publishers");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("15f326bc-f47f-487f-8764-5fb8fd5c448a"),
                column: "CreatedOn",
                value: new DateTime(2023, 8, 3, 17, 21, 21, 325, DateTimeKind.Utc).AddTicks(507));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("3db097df-7c7c-4c4e-b546-d4555c4c1521"),
                column: "CreatedOn",
                value: new DateTime(2023, 8, 3, 17, 21, 21, 325, DateTimeKind.Utc).AddTicks(569));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("45bb1c09-b50d-4d47-8fdb-fbfb53086922"),
                column: "CreatedOn",
                value: new DateTime(2023, 8, 3, 17, 21, 21, 325, DateTimeKind.Utc).AddTicks(521));

            migrationBuilder.UpdateData(
                table: "SubscriptionTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Type",
                value: "Montly");
        }
    }
}
