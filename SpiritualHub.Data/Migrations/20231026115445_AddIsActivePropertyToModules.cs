using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpiritualHub.Data.Migrations
{
    public partial class AddIsActivePropertyToModules : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("16c4da09-1f43-4836-ae8d-889659c64c30"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("3b339be3-d41d-4f5b-bdaf-3aa8d057db55"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("58989c4d-92cf-49fa-8d6e-05a226bc3e92"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("7e5cdd70-9c89-4886-9522-36e35ace299e"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("9243f93a-2321-4d23-ae59-3dd34ed5ef14"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("b3bf8b54-3e03-401e-8f6f-08f01662f8d7"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("c2d88a7f-2fc0-4263-b8c1-cd06072177d1"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("d2ffd82d-333c-4cc6-bf0c-9d9a8b604f96"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("f41bdc75-2f68-43da-9934-c6e022919d3d"));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Module",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Module");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("187e0540-5a90-419a-bf5b-f65ee213a0ca"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2b4f59ae-d7de-4941-9be9-f3be6a673c31", "AQAAAAEAACcQAAAAEPhvDYhT31iN4EtuAulbhylTN0z9QO0kDZLgrTQDrGWIZqhE9PZ1gUfaBVptP/HYLA==", "4dbd464f-f021-4224-b406-739fc7b2c507" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("194974cd-73f0-4946-ba85-710d4061472d"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f0965b13-9c60-4ddb-8b54-a42a47b97fc7", "AQAAAAEAACcQAAAAENv0/Jhq5Q+cwBvghhX/f+SaimVQp2w5xxeJkdFEE5fXFOcwD1iDeB9FjaTt8FtRUg==", "171a4210-b73e-42b4-8836-09dc9193ceae" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("1fd95f69-4f9d-4671-b126-cefcf4b8a95e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "55bb8577-63e1-459f-9ff6-45919778ae09", "AQAAAAEAACcQAAAAEEa29sISGNc2QxgZWKkJm2NOan96AySC9YGCL0GdxaPqHKf/jWNfYDDRhlkz20kWcg==", "3dc5ed17-6055-4525-9c61-7c7a107e51a8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("bcb4f072-ecca-43c9-ab26-c060c6f364e4"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9bd140e9-9845-4b53-8ca9-38a1039b2611", "AQAAAAEAACcQAAAAEHwzTBge60ylAJArBOiZwG8wihPF3+hlMCGU19rg1gTrXorHOhSrZxcUD2H2GfD2Iw==", "7e5deb11-9f17-440d-b834-4192c810b1f4" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("15f326bc-f47f-487f-8764-5fb8fd5c448a"),
                column: "CreatedOn",
                value: new DateTime(2023, 10, 2, 13, 15, 57, 626, DateTimeKind.Utc).AddTicks(6567));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("3db097df-7c7c-4c4e-b546-d4555c4c1521"),
                column: "CreatedOn",
                value: new DateTime(2023, 10, 2, 13, 15, 57, 626, DateTimeKind.Utc).AddTicks(6700));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("45bb1c09-b50d-4d47-8fdb-fbfb53086922"),
                column: "CreatedOn",
                value: new DateTime(2023, 10, 2, 13, 15, 57, 626, DateTimeKind.Utc).AddTicks(6672));

            migrationBuilder.InsertData(
                table: "Subscriptions",
                columns: new[] { "Id", "AuthorID", "Price", "SubscriptionTypeID" },
                values: new object[,]
                {
                    { new Guid("16c4da09-1f43-4836-ae8d-889659c64c30"), new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"), 189m, 3 },
                    { new Guid("3b339be3-d41d-4f5b-bdaf-3aa8d057db55"), new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"), 55m, 2 },
                    { new Guid("58989c4d-92cf-49fa-8d6e-05a226bc3e92"), new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"), 25m, 1 },
                    { new Guid("7e5cdd70-9c89-4886-9522-36e35ace299e"), new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"), 200m, 3 },
                    { new Guid("9243f93a-2321-4d23-ae59-3dd34ed5ef14"), new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"), 70m, 2 },
                    { new Guid("b3bf8b54-3e03-401e-8f6f-08f01662f8d7"), new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"), 21m, 1 },
                    { new Guid("c2d88a7f-2fc0-4263-b8c1-cd06072177d1"), new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"), 25m, 1 },
                    { new Guid("d2ffd82d-333c-4cc6-bf0c-9d9a8b604f96"), new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"), 150m, 3 },
                    { new Guid("f41bdc75-2f68-43da-9934-c6e022919d3d"), new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"), 65m, 2 }
                });
        }
    }
}
