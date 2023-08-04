using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpiritualHub.Data.Migrations
{
    public partial class AddPublisher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "PhoneNumber", "UserID" },
                values: new object[] { new Guid("d99242d9-3db2-4675-87e3-da7743c6b526"), "+359888888888", new Guid("194974cd-73f0-4946-ba85-710d4061472d") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("d99242d9-3db2-4675-87e3-da7743c6b526"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("194974cd-73f0-4946-ba85-710d4061472d"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "af9913b5-109b-499d-bdb4-0981705be496", "AQAAAAEAACcQAAAAEL7h3j7nFECR4WekgIBEC+w05iKTZ3or2eFP2XgyB8x4dB27UCquLqoVVq79B7UDMA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("1fd95f69-4f9d-4671-b126-cefcf4b8a95e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "55a09262-14b2-412f-86ef-d1d7e45e9883", "AQAAAAEAACcQAAAAEAJP8HFi8ajrlunSo9CsXQy3OnEil18Ijc7LPg+aoEHbdGUcL1Eig7lq89P5I0WN+A==" });

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
    }
}
