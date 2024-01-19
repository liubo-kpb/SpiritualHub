using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpiritualHub.Data.Migrations
{
    public partial class ModuleRequirementsChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("06d0c3c8-6cfc-44f4-9e98-0f094f69a946"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("265e3f18-1835-4de6-a1ac-057bf7dbcf73"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("77ea325f-7873-4561-b436-fa19a0538dd8"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("a4e16581-31bc-448d-bebc-f6c7618daf93"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("bd5f6d1c-106b-433c-b8d1-e84d554ad4cd"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("c62b2a69-eb46-4874-ae5b-db0f01320ebe"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("c95896f7-9a52-49af-a75d-9bdd41c9ed1f"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("d5a92e5e-e83c-4079-bd41-cb5b580b26c5"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("e39432aa-d5eb-42a8-a3c3-760f24ad3c78"));

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Module",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Module",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Please add a description...",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("187e0540-5a90-419a-bf5b-f65ee213a0ca"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "60ee7fb8-f89c-402b-9c56-9780e809e1e0", "AQAAAAEAACcQAAAAEMXYv5q45bfo2q0sqMXEDqyymgMqqjrEgP3TnH05xP1smt+BTKeDUo0yl8NUkJmEgQ==", "b57c5705-5660-4d1f-9497-06e5a15d2251" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("194974cd-73f0-4946-ba85-710d4061472d"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "54efc2f2-9480-4c4e-a78c-f24b3cba6625", "AQAAAAEAACcQAAAAEFsRUVnuwwu2LAnUZ5+lCZ113h4BdKuH2gvnXwbZJxZzUVDkJYmcfJCzQAToUmhyEA==", "31e36d61-3b7c-424c-baff-985021f83924" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("1fd95f69-4f9d-4671-b126-cefcf4b8a95e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d9f3a0b1-f18b-42e0-aea8-4f4042019d83", "AQAAAAEAACcQAAAAEKRUw3qfzMeIZqINyA5Sypq7YZMHq3oJM1ha9OvAocCaA+ITIc8hcsOXujtCUmV/0w==", "0d3e4906-a7f4-4db8-b43b-75814d2f4e02" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("bcb4f072-ecca-43c9-ab26-c060c6f364e4"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bbbfd3be-9b0c-4155-a16e-7e5a195ececc", "AQAAAAEAACcQAAAAEOzyi+QskayGKTHeu45N7CrLx7ddf3t/Xnv+9x9kLoWw60+xhz0WFEbpup/5xXA0wg==", "dc59b7d6-4bb5-4745-881d-16de3d46d625" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("15f326bc-f47f-487f-8764-5fb8fd5c448a"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 18, 17, 19, 44, 617, DateTimeKind.Utc).AddTicks(9212));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("3db097df-7c7c-4c4e-b546-d4555c4c1521"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 18, 17, 19, 44, 617, DateTimeKind.Utc).AddTicks(9248));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("45bb1c09-b50d-4d47-8fdb-fbfb53086922"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 18, 17, 19, 44, 617, DateTimeKind.Utc).AddTicks(9234));

            migrationBuilder.InsertData(
                table: "Subscriptions",
                columns: new[] { "Id", "AuthorID", "Price", "SubscriptionTypeID" },
                values: new object[,]
                {
                    { new Guid("55bf87c0-20df-46b7-8892-b6f65aff7ec9"), new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"), 70m, 2 },
                    { new Guid("5ee4ee40-48aa-4415-a029-06619595bb77"), new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"), 21m, 1 },
                    { new Guid("91b6fa21-aa86-41b9-892d-542d2d9a4e80"), new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"), 25m, 1 },
                    { new Guid("a0da970d-66f4-49f8-b3c3-813dd131c4da"), new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"), 55m, 2 },
                    { new Guid("a63a2e95-8112-44e8-9ba6-bbb1b79383bb"), new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"), 150m, 3 },
                    { new Guid("c463cf9f-0a53-4edc-9feb-3657aa2b4d25"), new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"), 65m, 2 },
                    { new Guid("ce290603-3519-4f96-85f5-b629d562cc7c"), new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"), 25m, 1 },
                    { new Guid("d2bebfa4-f78e-44bf-8e58-289c27b72828"), new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"), 189m, 3 },
                    { new Guid("d8f3c7cb-f65e-4a86-9bcc-156f3d8a3038"), new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"), 200m, 3 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("55bf87c0-20df-46b7-8892-b6f65aff7ec9"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("5ee4ee40-48aa-4415-a029-06619595bb77"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("91b6fa21-aa86-41b9-892d-542d2d9a4e80"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("a0da970d-66f4-49f8-b3c3-813dd131c4da"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("a63a2e95-8112-44e8-9ba6-bbb1b79383bb"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("c463cf9f-0a53-4edc-9feb-3657aa2b4d25"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("ce290603-3519-4f96-85f5-b629d562cc7c"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("d2bebfa4-f78e-44bf-8e58-289c27b72828"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("d8f3c7cb-f65e-4a86-9bcc-156f3d8a3038"));

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Module",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Module",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "Please add a description...");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("187e0540-5a90-419a-bf5b-f65ee213a0ca"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6788edcf-ee83-4a67-b547-35940cbb062d", "AQAAAAEAACcQAAAAECbB6dJvqZjK/aAwkloMmj0CkNRQWUX7U2vLT4pAUb4tQ1vfufdTsiXQfPAhyVV65w==", "c7c703e1-0f2e-4dce-8ae7-4443cb3a732f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("194974cd-73f0-4946-ba85-710d4061472d"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dfd6d13a-8c68-4ad9-bc0b-f4ffd309515b", "AQAAAAEAACcQAAAAEGrprPq0NXPLO7LDtgHEiTHqIe+2rGOULNVLp+qlFh1k6/iyTyN+vU0VfklsO+NKkA==", "c5f4d41b-9679-4e34-ba86-cca4b40f1beb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("1fd95f69-4f9d-4671-b126-cefcf4b8a95e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e1924684-568a-4f9e-8244-a475b6df48c4", "AQAAAAEAACcQAAAAEICD/gc2mi6csRUMxYnVBTICv2Cxq9HqVwOFhTBg0iUQr0lqpnjYycuGlIi/UbKmQA==", "5411ad4c-94a3-47e0-b39e-a586105b3e59" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("bcb4f072-ecca-43c9-ab26-c060c6f364e4"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "76a0e14f-6f70-4420-af45-ed6262c3a95b", "AQAAAAEAACcQAAAAELafktD+r9K6tvDJxPY/l5IGIWygQGmDHFPHeeoHrYxQJImwRKwV7BGiDtV2JQLhnA==", "12c9f568-d9ea-4f58-9232-387ed12b237b" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("15f326bc-f47f-487f-8764-5fb8fd5c448a"),
                column: "CreatedOn",
                value: new DateTime(2023, 10, 26, 11, 54, 43, 890, DateTimeKind.Utc).AddTicks(7881));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("3db097df-7c7c-4c4e-b546-d4555c4c1521"),
                column: "CreatedOn",
                value: new DateTime(2023, 10, 26, 11, 54, 43, 890, DateTimeKind.Utc).AddTicks(7958));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("45bb1c09-b50d-4d47-8fdb-fbfb53086922"),
                column: "CreatedOn",
                value: new DateTime(2023, 10, 26, 11, 54, 43, 890, DateTimeKind.Utc).AddTicks(7905));

            migrationBuilder.InsertData(
                table: "Subscriptions",
                columns: new[] { "Id", "AuthorID", "Price", "SubscriptionTypeID" },
                values: new object[,]
                {
                    { new Guid("06d0c3c8-6cfc-44f4-9e98-0f094f69a946"), new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"), 21m, 1 },
                    { new Guid("265e3f18-1835-4de6-a1ac-057bf7dbcf73"), new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"), 25m, 1 },
                    { new Guid("77ea325f-7873-4561-b436-fa19a0538dd8"), new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"), 55m, 2 },
                    { new Guid("a4e16581-31bc-448d-bebc-f6c7618daf93"), new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"), 150m, 3 },
                    { new Guid("bd5f6d1c-106b-433c-b8d1-e84d554ad4cd"), new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"), 189m, 3 },
                    { new Guid("c62b2a69-eb46-4874-ae5b-db0f01320ebe"), new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"), 25m, 1 },
                    { new Guid("c95896f7-9a52-49af-a75d-9bdd41c9ed1f"), new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"), 200m, 3 },
                    { new Guid("d5a92e5e-e83c-4079-bd41-cb5b580b26c5"), new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"), 65m, 2 },
                    { new Guid("e39432aa-d5eb-42a8-a3c3-760f24ad3c78"), new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"), 70m, 2 }
                });
        }
    }
}
