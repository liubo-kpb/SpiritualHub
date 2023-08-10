using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpiritualHub.Data.Migrations
{
    public partial class AddAuthorColumnIsActive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Authors",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("194974cd-73f0-4946-ba85-710d4061472d"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "cf9f3179-4559-40d6-88b6-1d2827c2362e", "AQAAAAEAACcQAAAAEDSOp34YBzveDPiwQT89zWm3+UK0OS+9dExgIxgdr/30h5DuEKLGpZxWsjKr0U/Smg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("1fd95f69-4f9d-4671-b126-cefcf4b8a95e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ea48d2c5-1ba3-4b5e-8723-93051e1fe833", "AQAAAAEAACcQAAAAEMs+0gLL84yPrEodnNZH/H+b1wy3sDZ2uJeU+glj+m/+cvcT7gqlFZhrsjSETLGDEg==" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("15f326bc-f47f-487f-8764-5fb8fd5c448a"),
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 17, 31, 52, 239, DateTimeKind.Utc).AddTicks(2645));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("3db097df-7c7c-4c4e-b546-d4555c4c1521"),
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 17, 31, 52, 239, DateTimeKind.Utc).AddTicks(2668));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("45bb1c09-b50d-4d47-8fdb-fbfb53086922"),
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 17, 31, 52, 239, DateTimeKind.Utc).AddTicks(2660));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Authors");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("194974cd-73f0-4946-ba85-710d4061472d"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "70fa9a14-07eb-45b1-883d-781f690a03f2", "AQAAAAEAACcQAAAAEKgAZ+rO6C0a2ZiUK1HgrN/HfmOm33mAp+6xDQngQaaKewod4QYI4seJkfNfzOVGUA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("1fd95f69-4f9d-4671-b126-cefcf4b8a95e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8810eba7-5c83-4729-8cdb-e2a914077e65", "AQAAAAEAACcQAAAAELdvi6QMQ7G53G/IozTk0mrS9BD4EEXrJCbffY8j8n018vsRSQdsGvqTyt2abIsfHA==" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("15f326bc-f47f-487f-8764-5fb8fd5c448a"),
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 16, 49, 18, 254, DateTimeKind.Utc).AddTicks(8854));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("3db097df-7c7c-4c4e-b546-d4555c4c1521"),
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 16, 49, 18, 254, DateTimeKind.Utc).AddTicks(8890));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("45bb1c09-b50d-4d47-8fdb-fbfb53086922"),
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 16, 49, 18, 254, DateTimeKind.Utc).AddTicks(8876));
        }
    }
}
