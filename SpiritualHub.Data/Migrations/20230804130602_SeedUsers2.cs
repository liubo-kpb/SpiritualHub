using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpiritualHub.Data.Migrations
{
    public partial class SeedUsers2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("194974cd-73f0-4946-ba85-710d4061472d"),
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash" },
                values: new object[] { "af9913b5-109b-499d-bdb4-0981705be496", "PUBLISHER@SPIRITS.COM", "PUBLISHER", "AQAAAAEAACcQAAAAEL7h3j7nFECR4WekgIBEC+w05iKTZ3or2eFP2XgyB8x4dB27UCquLqoVVq79B7UDMA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("1fd95f69-4f9d-4671-b126-cefcf4b8a95e"),
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash" },
                values: new object[] { "55a09262-14b2-412f-86ef-d1d7e45e9883", "USER@MAIL.COM", "USER", "AQAAAAEAACcQAAAAEAJP8HFi8ajrlunSo9CsXQy3OnEil18Ijc7LPg+aoEHbdGUcL1Eig7lq89P5I0WN+A==" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("15f326bc-f47f-487f-8764-5fb8fd5c448a"),
                column: "CreatedOn",
                value: new DateTime(2023, 8, 4, 13, 6, 1, 751, DateTimeKind.Utc).AddTicks(7749));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("3db097df-7c7c-4c4e-b546-d4555c4c1521"),
                column: "CreatedOn",
                value: new DateTime(2023, 8, 4, 13, 6, 1, 751, DateTimeKind.Utc).AddTicks(7806));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("45bb1c09-b50d-4d47-8fdb-fbfb53086922"),
                column: "CreatedOn",
                value: new DateTime(2023, 8, 4, 13, 6, 1, 751, DateTimeKind.Utc).AddTicks(7787));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("194974cd-73f0-4946-ba85-710d4061472d"),
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash" },
                values: new object[] { "d07c8583-8399-4d14-8495-4fc3fbf5e6c7", null, null, "AQAAAAEAACcQAAAAEC0TLDGDWdISET2q/c1f811TYR+18s8qgQjVEtEx34jFISs/xjzq1JFsWRZ5rrVlGg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("1fd95f69-4f9d-4671-b126-cefcf4b8a95e"),
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash" },
                values: new object[] { "b75137a8-f94b-491f-ae5a-77596e7ae7aa", null, null, "AQAAAAEAACcQAAAAEEZNQ9+0Evp20LWX0y0VfLV0jp8LVMHm1VliOIa92sQF7Ga2DiOEYTqLlOONe1DApg==" });

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
        }
    }
}
