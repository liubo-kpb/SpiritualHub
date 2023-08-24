using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpiritualHub.Data.Migrations
{
    public partial class SeedAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("15770521-ffc1-42eb-9111-9bb378a119a7"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("407a6d7c-32ba-42d2-98b3-662f66f142e0"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("449f3772-8c64-448e-a637-78b3da1058fa"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("6d50b6a2-bf88-40eb-80c2-81943f4933d9"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("7336f957-e89c-4b53-9a46-d50e4fd7620c"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("7f3999e0-d890-45af-854b-5c077748a898"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("83c655d9-9b2a-42ce-ab1b-81bd67a31f10"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("8581b9a3-6e82-4f99-851a-3d388ec28679"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("8735a14e-8c11-4b92-8979-cbc5fb90c152"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("194974cd-73f0-4946-ba85-710d4061472d"),
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "b006360a-5652-4476-9631-69ecb559e7b1", "PUBLISHER@SPIRITS.COM", "AQAAAAEAACcQAAAAEL1QtpZdql9PqPeGVCIOTdL/v2GBy3aTMN75QM2lhBJN6GJOsz5fTaS5hFQ7hPydwA==", "9b3c2066-fe40-4458-aa45-7add1ed8eb1b", "publisher@spirits.com" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("1fd95f69-4f9d-4671-b126-cefcf4b8a95e"),
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "07a62c31-f623-4ea4-8fc3-7e563b7d3ce4", "USER@MAIL.COM", "AQAAAAEAACcQAAAAEIErNcGRnjCVQMIX/disHNlRimlGqGF15oNG7m4XsDfwzIrIbpBn8MW21whhqPRIpg==", "7e04ad78-b0c1-42df-ab68-acab872b4e06", "user@mail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("bcb4f072-ecca-43c9-ab26-c060c6f364e4"), 0, "10429995-c696-41fd-a05e-255e2b5fa0ec", "admin@mail.com", false, "Great", "Admin", false, null, "ADMIN@MAIL.COM", "ADMIN@MAIL.COM", "AQAAAAEAACcQAAAAEDqBvFOogyDTqh9TWvpB0Quykj8MY4CKNBMc2Lr2CK+lON8epBs84nj94XU4H92Bzw==", null, false, "89b4836e-940d-456d-9233-25bb516e76ef", false, "admin@mail.com" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("15f326bc-f47f-487f-8764-5fb8fd5c448a"),
                column: "CreatedOn",
                value: new DateTime(2023, 8, 24, 1, 25, 22, 137, DateTimeKind.Utc).AddTicks(6881));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("3db097df-7c7c-4c4e-b546-d4555c4c1521"),
                column: "CreatedOn",
                value: new DateTime(2023, 8, 24, 1, 25, 22, 137, DateTimeKind.Utc).AddTicks(6902));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("45bb1c09-b50d-4d47-8fdb-fbfb53086922"),
                column: "CreatedOn",
                value: new DateTime(2023, 8, 24, 1, 25, 22, 137, DateTimeKind.Utc).AddTicks(6894));

            migrationBuilder.InsertData(
                table: "Subscriptions",
                columns: new[] { "Id", "AuthorID", "Price", "SubscriptionTypeID" },
                values: new object[,]
                {
                    { new Guid("048bff7c-4c39-43b1-8765-d276e4b4b4ab"), new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"), 200m, 3 },
                    { new Guid("1de5bc3f-ff9e-48c8-a65c-0b4f01f9a4e5"), new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"), 70m, 2 },
                    { new Guid("2b87c2c7-1889-4ca0-97d9-0200dc07cf11"), new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"), 65m, 2 },
                    { new Guid("6e1eeec9-72d9-48f9-8023-a44dc3435ad3"), new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"), 25m, 1 },
                    { new Guid("7ec4d45d-0d7b-4be9-8793-af7e66a8f44c"), new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"), 189m, 3 },
                    { new Guid("a2c29123-be00-4658-b132-05c019569efa"), new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"), 25m, 1 },
                    { new Guid("a3e2d8ac-d38c-4abf-b0d9-237dad108456"), new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"), 150m, 3 },
                    { new Guid("b31a05ce-0af3-4490-8ae5-ac8c6686afa7"), new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"), 21m, 1 },
                    { new Guid("d5dafb2e-2c58-449a-9ffa-c6985049eb28"), new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"), 55m, 2 }
                });

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "PhoneNumber", "UserID" },
                values: new object[] { new Guid("4779b556-cbb5-45d2-a16c-d8a83501198a"), "+359883588888", new Guid("bcb4f072-ecca-43c9-ab26-c060c6f364e4") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("4779b556-cbb5-45d2-a16c-d8a83501198a"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("048bff7c-4c39-43b1-8765-d276e4b4b4ab"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("1de5bc3f-ff9e-48c8-a65c-0b4f01f9a4e5"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("2b87c2c7-1889-4ca0-97d9-0200dc07cf11"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("6e1eeec9-72d9-48f9-8023-a44dc3435ad3"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("7ec4d45d-0d7b-4be9-8793-af7e66a8f44c"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("a2c29123-be00-4658-b132-05c019569efa"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("a3e2d8ac-d38c-4abf-b0d9-237dad108456"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("b31a05ce-0af3-4490-8ae5-ac8c6686afa7"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("d5dafb2e-2c58-449a-9ffa-c6985049eb28"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("bcb4f072-ecca-43c9-ab26-c060c6f364e4"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("194974cd-73f0-4946-ba85-710d4061472d"),
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "9e570b57-be0a-4b8e-9d62-28c338082152", "PUBLISHER", "AQAAAAEAACcQAAAAEO3hIGKJNck51rrys1y/NSso++p8dzEeVA05YIB6rn8AuRZvGWa0CiHlSwJQ5CSomA==", null, "publisher" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("1fd95f69-4f9d-4671-b126-cefcf4b8a95e"),
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "e73d63bf-de52-424d-81ae-4477bb789396", "USER", "AQAAAAEAACcQAAAAEOw8erVJB2RD+Rrf17S3yXXM2IaIkV9wjMf/A1FhN9VDTyMyFCL9z577In68Z8+SFg==", null, "user" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("15f326bc-f47f-487f-8764-5fb8fd5c448a"),
                column: "CreatedOn",
                value: new DateTime(2023, 8, 20, 18, 24, 10, 852, DateTimeKind.Utc).AddTicks(1577));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("3db097df-7c7c-4c4e-b546-d4555c4c1521"),
                column: "CreatedOn",
                value: new DateTime(2023, 8, 20, 18, 24, 10, 852, DateTimeKind.Utc).AddTicks(2046));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("45bb1c09-b50d-4d47-8fdb-fbfb53086922"),
                column: "CreatedOn",
                value: new DateTime(2023, 8, 20, 18, 24, 10, 852, DateTimeKind.Utc).AddTicks(1599));

            migrationBuilder.InsertData(
                table: "Subscriptions",
                columns: new[] { "Id", "AuthorID", "Price", "SubscriptionTypeID" },
                values: new object[,]
                {
                    { new Guid("15770521-ffc1-42eb-9111-9bb378a119a7"), new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"), 70m, 2 },
                    { new Guid("407a6d7c-32ba-42d2-98b3-662f66f142e0"), new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"), 150m, 3 },
                    { new Guid("449f3772-8c64-448e-a637-78b3da1058fa"), new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"), 55m, 2 },
                    { new Guid("6d50b6a2-bf88-40eb-80c2-81943f4933d9"), new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"), 200m, 3 },
                    { new Guid("7336f957-e89c-4b53-9a46-d50e4fd7620c"), new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"), 189m, 3 },
                    { new Guid("7f3999e0-d890-45af-854b-5c077748a898"), new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"), 21m, 1 },
                    { new Guid("83c655d9-9b2a-42ce-ab1b-81bd67a31f10"), new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"), 25m, 1 },
                    { new Guid("8581b9a3-6e82-4f99-851a-3d388ec28679"), new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"), 65m, 2 },
                    { new Guid("8735a14e-8c11-4b92-8979-cbc5fb90c152"), new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"), 25m, 1 }
                });
        }
    }
}
