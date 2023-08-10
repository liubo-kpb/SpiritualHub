using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpiritualHub.Data.Migrations
{
    public partial class DropImageColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("194974cd-73f0-4946-ba85-710d4061472d"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "fc21e028-6e4b-461f-9c32-9ec68f2f673d", "AQAAAAEAACcQAAAAEIL4AEEiADwVPojIM5PyhJEFXwRdtQ0qBWlh+x1Zbx0ShTOvOtHbE1HkKe93PfOZRw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("1fd95f69-4f9d-4671-b126-cefcf4b8a95e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "30e036f4-ffe6-40ad-83ef-480491843207", "AQAAAAEAACcQAAAAEP0BPL1YhJ1JWQIMtL86h4q83ZNOPJur1ONYN7o6PRC+R2MJJesrQlktv6TaOYasrA==" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("15f326bc-f47f-487f-8764-5fb8fd5c448a"),
                column: "CreatedOn",
                value: new DateTime(2023, 8, 9, 22, 51, 34, 677, DateTimeKind.Utc).AddTicks(166));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("3db097df-7c7c-4c4e-b546-d4555c4c1521"),
                column: "CreatedOn",
                value: new DateTime(2023, 8, 9, 22, 51, 34, 677, DateTimeKind.Utc).AddTicks(264));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("45bb1c09-b50d-4d47-8fdb-fbfb53086922"),
                column: "CreatedOn",
                value: new DateTime(2023, 8, 9, 22, 51, 34, 677, DateTimeKind.Utc).AddTicks(251));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
