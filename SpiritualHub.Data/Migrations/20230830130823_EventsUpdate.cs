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
                values: new object[] { "57e0c5ad-893f-4017-90db-3cab36b8dd96", "AQAAAAEAACcQAAAAEEsockSELBhN4J2Xo0yfPEvNsttMSFulUQSYhBhQIY/G3QmCS5189k+pHOzod+F0sg==", "a671f7ea-6361-4702-8198-dbd33a64146d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("1fd95f69-4f9d-4671-b126-cefcf4b8a95e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "58d52dba-be40-4144-950a-c38e7d1845ea", "AQAAAAEAACcQAAAAEJc79p/mYdyQQNHDFT/SenVjpPaBFIgvvNU3UvtzovPRbHu1qXTNsROK1o9C7TYKFg==", "965d05ac-6073-4a33-bb00-d212405e919c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("bcb4f072-ecca-43c9-ab26-c060c6f364e4"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b292e632-94f0-4a8c-ae65-05ce71d3f257", "AQAAAAEAACcQAAAAEIfbhW4Klo9lCYF580TuidvQZndHOO/lw9HmNkiEkXn8X9YJZDrYxnWq/+tRkiAcIQ==", "2c9bdf47-d41e-4f39-a1e9-d8a800e04f32" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("15f326bc-f47f-487f-8764-5fb8fd5c448a"),
                columns: new[] { "CreatedOn", "PublisherID" },
                values: new object[] { new DateTime(2023, 8, 30, 13, 8, 22, 932, DateTimeKind.Utc).AddTicks(6582), new Guid("4779b556-cbb5-45d2-a16c-d8a83501198a") });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("3db097df-7c7c-4c4e-b546-d4555c4c1521"),
                columns: new[] { "CreatedOn", "PublisherID" },
                values: new object[] { new DateTime(2023, 8, 30, 13, 8, 22, 932, DateTimeKind.Utc).AddTicks(6606), new Guid("4779b556-cbb5-45d2-a16c-d8a83501198a") });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("45bb1c09-b50d-4d47-8fdb-fbfb53086922"),
                columns: new[] { "CreatedOn", "PublisherID" },
                values: new object[] { new DateTime(2023, 8, 30, 13, 8, 22, 932, DateTimeKind.Utc).AddTicks(6597), new Guid("4779b556-cbb5-45d2-a16c-d8a83501198a") });

            migrationBuilder.InsertData(
                table: "Subscriptions",
                columns: new[] { "Id", "AuthorID", "Price", "SubscriptionTypeID" },
                values: new object[,]
                {
                    { new Guid("08453641-4fbb-4012-8cfc-07269d520ab9"), new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"), 55m, 2 },
                    { new Guid("23dca6f5-edc9-4f1e-a865-ace009b8c10f"), new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"), 65m, 2 },
                    { new Guid("3648c022-a59d-4523-bee7-45ab06f87239"), new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"), 189m, 3 },
                    { new Guid("38dddb59-80e4-44ed-8cac-15124a12a793"), new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"), 70m, 2 },
                    { new Guid("73d1d7ad-1fa5-44f7-8112-c4d36c9f27f8"), new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"), 21m, 1 },
                    { new Guid("a5067781-1299-48ab-9b01-3916cac7668a"), new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"), 200m, 3 },
                    { new Guid("b7ec74ba-030d-491b-a453-13567d1ce629"), new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"), 25m, 1 },
                    { new Guid("c72d9e64-2a03-4d4b-ab41-bef007661d22"), new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"), 150m, 3 },
                    { new Guid("de1a875f-c7d0-4121-9a95-98580f899417"), new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"), 25m, 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("08453641-4fbb-4012-8cfc-07269d520ab9"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("23dca6f5-edc9-4f1e-a865-ace009b8c10f"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("3648c022-a59d-4523-bee7-45ab06f87239"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("38dddb59-80e4-44ed-8cac-15124a12a793"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("73d1d7ad-1fa5-44f7-8112-c4d36c9f27f8"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("a5067781-1299-48ab-9b01-3916cac7668a"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("b7ec74ba-030d-491b-a453-13567d1ce629"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("c72d9e64-2a03-4d4b-ab41-bef007661d22"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("de1a875f-c7d0-4121-9a95-98580f899417"));

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
                columns: new[] { "CreatedOn", "PublisherID" },
                values: new object[] { new DateTime(2023, 8, 28, 8, 47, 50, 850, DateTimeKind.Utc).AddTicks(33), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("45bb1c09-b50d-4d47-8fdb-fbfb53086922"),
                columns: new[] { "CreatedOn", "PublisherID" },
                values: new object[] { new DateTime(2023, 8, 28, 8, 47, 50, 850, DateTimeKind.Utc).AddTicks(24), new Guid("00000000-0000-0000-0000-000000000000") });

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
