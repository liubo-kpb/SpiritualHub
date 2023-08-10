using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpiritualHub.Data.Migrations
{
    public partial class AddEntityCreationDates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK__PossibleTypes",
                table: "SubscriptionTypes");

            migrationBuilder.AddColumn<DateTime>(
                name: "AddedOn",
                table: "Courses",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "AddedOn",
                table: "Books",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "AddedOn",
                table: "Authors",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("194974cd-73f0-4946-ba85-710d4061472d"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5894e9d6-2eab-4ddc-84ab-54a54759ad2b", "AQAAAAEAACcQAAAAENGWTdXdFmeftrWNQtHm00gO96JUB1O31UslysaBHEPswk3T4Tx7BKwVrTIuncMmRA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("1fd95f69-4f9d-4671-b126-cefcf4b8a95e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4fa82f13-2243-462e-be0b-409388661bdc", "AQAAAAEAACcQAAAAEESd5QKm1Vhq0G1HJgdyymmMnaBmJncW6gysQoChhCsSLx6TfAkySVulT6J1exeWOw==" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("15f326bc-f47f-487f-8764-5fb8fd5c448a"),
                column: "CreatedOn",
                value: new DateTime(2023, 8, 8, 17, 45, 34, 735, DateTimeKind.Utc).AddTicks(5872));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("3db097df-7c7c-4c4e-b546-d4555c4c1521"),
                column: "CreatedOn",
                value: new DateTime(2023, 8, 8, 17, 45, 34, 735, DateTimeKind.Utc).AddTicks(5902));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("45bb1c09-b50d-4d47-8fdb-fbfb53086922"),
                column: "CreatedOn",
                value: new DateTime(2023, 8, 8, 17, 45, 34, 735, DateTimeKind.Utc).AddTicks(5894));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
