using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpiritualHub.Data.Migrations
{
    public partial class AddEntityCreationDates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AddedOn",
                table: "Courses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, 0,0,0,0, DateTimeKind.Utc));

            migrationBuilder.AddColumn<DateTime>(
                name: "AddedOn",
                table: "Books",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.AddColumn<DateTime>(
                name: "AddedOn",
                table: "Authors",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("194974cd-73f0-4946-ba85-710d4061472d"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "644cef2f-ed7e-4f8a-b21b-e00c4cfb2da0", "AQAAAAEAACcQAAAAEB5jsi+09LSZUQBjghjvUbeCbkRw4JJcYgdGsafhLVduj3mdy8sOpupveSnSfSuz4Q==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("1fd95f69-4f9d-4671-b126-cefcf4b8a95e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "bab93638-5a3e-486f-a0ee-78f2ef8164ba", "AQAAAAEAACcQAAAAEJnjieSk6r1uj08uD+dBIx2aaKkwGjRPENzMCvQujBub15hcloYhn3avu4ZLI3oZQQ==" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("15f326bc-f47f-487f-8764-5fb8fd5c448a"),
                column: "CreatedOn",
                value: new DateTime(2023, 8, 7, 21, 6, 17, 237, DateTimeKind.Utc).AddTicks(6248));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("3db097df-7c7c-4c4e-b546-d4555c4c1521"),
                column: "CreatedOn",
                value: new DateTime(2023, 8, 7, 21, 6, 17, 237, DateTimeKind.Utc).AddTicks(6296));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("45bb1c09-b50d-4d47-8fdb-fbfb53086922"),
                column: "CreatedOn",
                value: new DateTime(2023, 8, 7, 21, 6, 17, 237, DateTimeKind.Utc).AddTicks(6278));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK__PossibleTypes",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "AddedOn",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "AddedOn",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "AddedOn",
                table: "Authors");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("194974cd-73f0-4946-ba85-710d4061472d"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "06ca63e5-fba7-40ee-bddd-d82eca9e4ee6", "AQAAAAEAACcQAAAAEKqKNt/Y3SJd5fbAiWrWk1gz5IrawQ4nvHZ23mVoECOXXvjsh/neD/6P3Py2FBoxTg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("1fd95f69-4f9d-4671-b126-cefcf4b8a95e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "10490887-f13a-4ee7-99a2-687a12bbd68b", "AQAAAAEAACcQAAAAEH8DjtzPbVnCGPiNXahJAMuZzHnfxu1f+LT3eJIjLMkcFe2Ml6X2pZV36W1dZSFLBw==" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("15f326bc-f47f-487f-8764-5fb8fd5c448a"),
                column: "CreatedOn",
                value: new DateTime(2023, 8, 4, 13, 7, 52, 752, DateTimeKind.Utc).AddTicks(2990));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("3db097df-7c7c-4c4e-b546-d4555c4c1521"),
                column: "CreatedOn",
                value: new DateTime(2023, 8, 4, 13, 7, 52, 752, DateTimeKind.Utc).AddTicks(3059));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("45bb1c09-b50d-4d47-8fdb-fbfb53086922"),
                column: "CreatedOn",
                value: new DateTime(2023, 8, 4, 13, 7, 52, 752, DateTimeKind.Utc).AddTicks(3006));

            migrationBuilder.AddCheckConstraint(
                name: "CK__PossibleTypes",
                table: "SubscriptionTypes",
                sql: "Type = 'Monthly' OR Type = 'Quarterly' OR Type = 'Annual'");
        }
    }
}
