using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpiritualHub.Data.Migrations
{
    public partial class EventsUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("1b3e3885-bd76-4150-b795-0e9b3556800b"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("1eca08e4-c66b-4267-ba47-8927dcd5a1d1"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("2b9403b8-588d-471a-93b9-1fd982bc025a"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("4d50e6e9-d3d1-434c-a9ad-dcc94769af04"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("66415ffc-1c98-4a51-a97e-eec4295934e8"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("68d37433-1a66-46de-8e35-c40c6b0b6acf"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("7fcfa18f-b3d0-4afd-877b-3575d933186b"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("84829035-74cd-492f-ada4-93ca643f8b7b"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("955914ca-259c-4d4d-8804-957cb8c1ed88"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("194974cd-73f0-4946-ba85-710d4061472d"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a1dc8436-64bb-4e64-be67-dfd8c350d769", "AQAAAAEAACcQAAAAEOesKCAL+m50U5Ds1MbPW6GmI5tXyNNs0WW2qZREWRix4x7kNFYd37YKi79Unfnnkw==", "d8a68382-4cb8-4584-b53f-abc2f2719454" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("1fd95f69-4f9d-4671-b126-cefcf4b8a95e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "adeabec1-5944-4405-b18e-ca34e372fddc", "AQAAAAEAACcQAAAAEBorEgrmm0Eg1S3bP46poSr+1vysUpNc0eAHjp7Whb464WtJiG1NiKTmYnXF4uP33Q==", "b8c7aab9-c408-4f1b-bb10-712d7bea47d6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("bcb4f072-ecca-43c9-ab26-c060c6f364e4"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5f327ff6-0284-4084-b052-5109d53d5802", "AQAAAAEAACcQAAAAEFDVHR9Ffwq335ozbNVXdICrVrtjWMmQW/Q8ezICO32RuRXXBm/jOSOKis1vtKHplw==", "f0016e66-f2c3-4dc4-8404-cd60f167dde9" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("15f326bc-f47f-487f-8764-5fb8fd5c448a"),
                columns: new[] { "CreatedOn", "PublisherID" },
                values: new object[] { new DateTime(2023, 8, 30, 13, 38, 3, 287, DateTimeKind.Utc).AddTicks(9170), new Guid("4779b556-cbb5-45d2-a16c-d8a83501198a") });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("3db097df-7c7c-4c4e-b546-d4555c4c1521"),
                columns: new[] { "CreatedOn", "LocationName", "LocationUrl", "PublisherID" },
                values: new object[] { new DateTime(2023, 8, 30, 13, 38, 3, 287, DateTimeKind.Utc).AddTicks(9202), "Campus \"Healing\"", "https://www.google.com/maps/place/%D0%9A%D0%B0%D0%BC%D0%BF%D1%83%D1%81+%D0%98%D0%B7%D1%86%D0%B5%D0%BB%D0%B5%D0%BD%D0%B8%D0%B5/@42.2625195,25.2288508,17z/data=!3m1!4b1!4m6!3m5!1s0x40a82595106658b3:0x4dc3df5ed0a4ca00!8m2!3d42.2625156!4d25.2314257!16s%2Fg%2F11ry_fh0ry?entry=ttu", new Guid("4779b556-cbb5-45d2-a16c-d8a83501198a") });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("45bb1c09-b50d-4d47-8fdb-fbfb53086922"),
                columns: new[] { "CreatedOn", "LocationName", "LocationUrl", "PublisherID" },
                values: new object[] { new DateTime(2023, 8, 30, 13, 38, 3, 287, DateTimeKind.Utc).AddTicks(9190), "Stockholm", "https://www.google.com/maps/place/Stockholm,+Sweden/@59.3262131,17.8172495,11z/data=!3m1!4b1!4m6!3m5!1s0x465f763119640bcb:0xa80d27d3679d7766!8m2!3d59.3293235!4d18.0685808!16zL20vMDZteHM?entry=ttu", new Guid("4779b556-cbb5-45d2-a16c-d8a83501198a") });

            migrationBuilder.InsertData(
                table: "Subscriptions",
                columns: new[] { "Id", "AuthorID", "Price", "SubscriptionTypeID" },
                values: new object[,]
                {
                    { new Guid("1be042e3-6b50-4d17-b9b9-f5a5d7f144a3"), new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"), 21m, 1 },
                    { new Guid("26fac86e-c18b-49f0-883a-27e9caf952fb"), new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"), 150m, 3 },
                    { new Guid("28fbdb15-a187-4e2b-8ca8-307f0f905c3e"), new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"), 200m, 3 },
                    { new Guid("520ca37d-98cc-4d1c-b052-0dae71c8c1dd"), new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"), 25m, 1 },
                    { new Guid("6c433b0d-87b5-45c9-9ce5-9e9f0d210b0d"), new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"), 65m, 2 },
                    { new Guid("790489e3-fbd4-4d32-ada1-061d93334a66"), new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"), 55m, 2 },
                    { new Guid("a24a8894-ed9e-43fe-8fa4-c42d034376f2"), new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"), 189m, 3 },
                    { new Guid("c9d0e00e-106d-4039-9287-1f066040f347"), new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"), 25m, 1 },
                    { new Guid("d465c46b-d5c4-4f75-9d0c-062dfbbd4532"), new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"), 70m, 2 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("1be042e3-6b50-4d17-b9b9-f5a5d7f144a3"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("26fac86e-c18b-49f0-883a-27e9caf952fb"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("28fbdb15-a187-4e2b-8ca8-307f0f905c3e"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("520ca37d-98cc-4d1c-b052-0dae71c8c1dd"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("6c433b0d-87b5-45c9-9ce5-9e9f0d210b0d"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("790489e3-fbd4-4d32-ada1-061d93334a66"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("a24a8894-ed9e-43fe-8fa4-c42d034376f2"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("c9d0e00e-106d-4039-9287-1f066040f347"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("d465c46b-d5c4-4f75-9d0c-062dfbbd4532"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("194974cd-73f0-4946-ba85-710d4061472d"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ac44fa44-3ed4-4e32-b879-4b8811a99b82", "AQAAAAEAACcQAAAAEEABLTELUjeXh2welylWuOjatrAxtjjwkEZJj8lFS1RTrJcQ173F7ESoOOrFgUAo4w==", "053a3aa6-7177-4e11-9a08-005b3d887acd" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("1fd95f69-4f9d-4671-b126-cefcf4b8a95e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5dd21831-1bbb-4921-a310-a59811ffa319", "AQAAAAEAACcQAAAAENxwbsUQqLFjOoN0aDZ6/ls2SsuAKlilRWfqD5LlouWOJxC+gZ9NMlFBfXvxfTlnLA==", "26e7452e-b760-4585-be69-6ec3459b7457" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("bcb4f072-ecca-43c9-ab26-c060c6f364e4"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "41c7d6bd-1719-43cc-8fa2-2f71e4de07c3", "AQAAAAEAACcQAAAAEIrhD5v+ikTidh6zM+h04oKhfvvzEp3bKCcWPvnmchA+aUWI7EPGZhV82xSiO6+Kpg==", "bbade6b1-3ea6-49de-a481-4fdf684793d2" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("15f326bc-f47f-487f-8764-5fb8fd5c448a"),
                columns: new[] { "CreatedOn", "PublisherID" },
                values: new object[] { new DateTime(2023, 8, 28, 8, 47, 50, 850, DateTimeKind.Utc).AddTicks(6), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("3db097df-7c7c-4c4e-b546-d4555c4c1521"),
                columns: new[] { "CreatedOn", "LocationName", "LocationUrl", "PublisherID" },
                values: new object[] { new DateTime(2023, 8, 28, 8, 47, 50, 850, DateTimeKind.Utc).AddTicks(33), null, null, new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("45bb1c09-b50d-4d47-8fdb-fbfb53086922"),
                columns: new[] { "CreatedOn", "LocationName", "LocationUrl", "PublisherID" },
                values: new object[] { new DateTime(2023, 8, 28, 8, 47, 50, 850, DateTimeKind.Utc).AddTicks(24), null, null, new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.InsertData(
                table: "Subscriptions",
                columns: new[] { "Id", "AuthorID", "Price", "SubscriptionTypeID" },
                values: new object[,]
                {
                    { new Guid("1b3e3885-bd76-4150-b795-0e9b3556800b"), new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"), 55m, 2 },
                    { new Guid("1eca08e4-c66b-4267-ba47-8927dcd5a1d1"), new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"), 200m, 3 },
                    { new Guid("2b9403b8-588d-471a-93b9-1fd982bc025a"), new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"), 65m, 2 },
                    { new Guid("4d50e6e9-d3d1-434c-a9ad-dcc94769af04"), new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"), 25m, 1 },
                    { new Guid("66415ffc-1c98-4a51-a97e-eec4295934e8"), new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"), 25m, 1 },
                    { new Guid("68d37433-1a66-46de-8e35-c40c6b0b6acf"), new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"), 150m, 3 },
                    { new Guid("7fcfa18f-b3d0-4afd-877b-3575d933186b"), new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"), 21m, 1 },
                    { new Guid("84829035-74cd-492f-ada4-93ca643f8b7b"), new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"), 189m, 3 },
                    { new Guid("955914ca-259c-4d4d-8804-957cb8c1ed88"), new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"), 70m, 2 }
                });
        }
    }
}
