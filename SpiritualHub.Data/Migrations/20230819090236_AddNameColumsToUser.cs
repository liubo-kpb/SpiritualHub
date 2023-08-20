using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpiritualHub.Data.Migrations
{
    public partial class AddNameColumsToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("16c2c70f-57fb-4249-8d85-42f7d8a30a3e"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("3a10b313-8b50-4293-81b5-d124d0aab4f4"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("3be95343-27a0-441f-98e5-7babe08e4cbe"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("56aac85a-74df-4917-99db-0d2f2c881488"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("8c4c0fa4-8f2c-4144-9826-9d5b3fd9bc21"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("a3f7111b-5cc3-4143-a389-72a6ad5da4a8"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("bea5249f-f2ee-40b0-b0d3-83d5b7161c19"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("ed069b01-922e-444f-96b5-3ac28cf26fa3"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("fa8ff5d0-8aac-4829-9378-186fc8f23977"));

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(12)",
                maxLength: 12,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("194974cd-73f0-4946-ba85-710d4061472d"),
                columns: new[] { "ConcurrencyStamp", "FirstName", "LastName", "PasswordHash" },
                values: new object[] { "020dda7f-bed4-47ba-9c76-472915556522", "Pablo", "Publish", "AQAAAAEAACcQAAAAELBkPxzcvkRr3p6V68W0ovYkaCRaCUxUf5/Fwf3FboyUtZgAgDem7i74Vh4K5GRi6w==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("1fd95f69-4f9d-4671-b126-cefcf4b8a95e"),
                columns: new[] { "ConcurrencyStamp", "FirstName", "LastName", "PasswordHash" },
                values: new object[] { "2a39414f-eb4b-4f5d-b47a-799d68d4bb5e", "Martin", "User", "AQAAAAEAACcQAAAAENM9lK7q2Fc0KRnNDzfSPZZbJ/m1dvdcYRZNjftuakdjNQ1lYwAbWwG1ju2XIlPVzg==" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("15f326bc-f47f-487f-8764-5fb8fd5c448a"),
                column: "CreatedOn",
                value: new DateTime(2023, 8, 19, 9, 2, 35, 930, DateTimeKind.Utc).AddTicks(6204));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("3db097df-7c7c-4c4e-b546-d4555c4c1521"),
                column: "CreatedOn",
                value: new DateTime(2023, 8, 19, 9, 2, 35, 930, DateTimeKind.Utc).AddTicks(6235));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("45bb1c09-b50d-4d47-8fdb-fbfb53086922"),
                column: "CreatedOn",
                value: new DateTime(2023, 8, 19, 9, 2, 35, 930, DateTimeKind.Utc).AddTicks(6224));

            migrationBuilder.InsertData(
                table: "Subscriptions",
                columns: new[] { "Id", "AuthorID", "Price", "SubscriptionTypeID" },
                values: new object[,]
                {
                    { new Guid("03422802-9236-447e-ab41-facd15b37f0d"), new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"), 55m, 2 },
                    { new Guid("0a96bebf-0f1a-42bd-b0af-a27371c2f63e"), new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"), 200m, 3 },
                    { new Guid("375b057a-f95e-40cc-8a1d-faff54615a12"), new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"), 189m, 3 },
                    { new Guid("3980ffe4-5541-4a2a-84dd-0e4312f08188"), new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"), 25m, 1 },
                    { new Guid("58b090ab-23c9-4771-8077-96455ad7be69"), new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"), 70m, 2 },
                    { new Guid("830666b5-1a9d-4a43-a5d7-1ce00c86771c"), new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"), 21m, 1 },
                    { new Guid("b40e462f-60b9-4855-a1ba-661ed90d1880"), new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"), 65m, 2 },
                    { new Guid("ce7ea0db-7487-48fd-97a2-325dca81db14"), new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"), 25m, 1 },
                    { new Guid("e57d97aa-042b-4722-891d-443c868d1357"), new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"), 150m, 3 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("03422802-9236-447e-ab41-facd15b37f0d"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("0a96bebf-0f1a-42bd-b0af-a27371c2f63e"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("375b057a-f95e-40cc-8a1d-faff54615a12"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("3980ffe4-5541-4a2a-84dd-0e4312f08188"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("58b090ab-23c9-4771-8077-96455ad7be69"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("830666b5-1a9d-4a43-a5d7-1ce00c86771c"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("b40e462f-60b9-4855-a1ba-661ed90d1880"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("ce7ea0db-7487-48fd-97a2-325dca81db14"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("e57d97aa-042b-4722-891d-443c868d1357"));

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("194974cd-73f0-4946-ba85-710d4061472d"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ece2c2cc-a4a7-47ec-8e21-7657253bc3b8", "AQAAAAEAACcQAAAAEOoPNjfzyK3tYJfEp6SHRKIorMgDZWPXksQ/AMctd6r4MwQKy/wJqgQWcBNYDoZeNg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("1fd95f69-4f9d-4671-b126-cefcf4b8a95e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "61ad940d-3cac-4912-910d-bfffe6324b95", "AQAAAAEAACcQAAAAELbQ3BjMM/Krn6i9baTvjhaB32pZwCzdNRZ3xEsIpwhm6PQPFfYOXOkCn5Z8TeSpLA==" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("15f326bc-f47f-487f-8764-5fb8fd5c448a"),
                column: "CreatedOn",
                value: new DateTime(2023, 8, 11, 16, 19, 11, 153, DateTimeKind.Utc).AddTicks(2225));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("3db097df-7c7c-4c4e-b546-d4555c4c1521"),
                column: "CreatedOn",
                value: new DateTime(2023, 8, 11, 16, 19, 11, 153, DateTimeKind.Utc).AddTicks(2279));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("45bb1c09-b50d-4d47-8fdb-fbfb53086922"),
                column: "CreatedOn",
                value: new DateTime(2023, 8, 11, 16, 19, 11, 153, DateTimeKind.Utc).AddTicks(2266));

            migrationBuilder.InsertData(
                table: "Subscriptions",
                columns: new[] { "Id", "AuthorID", "Price", "SubscriptionTypeID" },
                values: new object[,]
                {
                    { new Guid("16c2c70f-57fb-4249-8d85-42f7d8a30a3e"), new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"), 70m, 2 },
                    { new Guid("3a10b313-8b50-4293-81b5-d124d0aab4f4"), new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"), 55m, 2 },
                    { new Guid("3be95343-27a0-441f-98e5-7babe08e4cbe"), new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"), 65m, 2 },
                    { new Guid("56aac85a-74df-4917-99db-0d2f2c881488"), new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"), 200m, 1 },
                    { new Guid("8c4c0fa4-8f2c-4144-9826-9d5b3fd9bc21"), new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"), 150m, 1 },
                    { new Guid("a3f7111b-5cc3-4143-a389-72a6ad5da4a8"), new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"), 21m, 1 },
                    { new Guid("bea5249f-f2ee-40b0-b0d3-83d5b7161c19"), new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"), 25m, 1 },
                    { new Guid("ed069b01-922e-444f-96b5-3ac28cf26fa3"), new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"), 25m, 1 },
                    { new Guid("fa8ff5d0-8aac-4829-9378-186fc8f23977"), new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"), 189m, 1 }
                });
        }
    }
}
