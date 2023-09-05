using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpiritualHub.Data.Migrations
{
    public partial class SeedSubscriptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropIndex(
            //    name: "IX_Authors_PublisherID",
            //    table: "Authors");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Authors_Publishers_PublisherID",
            //    table: "Authors");

            //migrationBuilder.DropColumn(
            //    name: "PublisherId",
            //    table: "Authors");

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
                    { new Guid("56aac85a-74df-4917-99db-0d2f2c881488"), new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"), 200m, 3 },
                    { new Guid("8c4c0fa4-8f2c-4144-9826-9d5b3fd9bc21"), new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"), 150m, 3 },
                    { new Guid("a3f7111b-5cc3-4143-a389-72a6ad5da4a8"), new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"), 21m, 1 },
                    { new Guid("bea5249f-f2ee-40b0-b0d3-83d5b7161c19"), new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"), 25m, 1 },
                    { new Guid("ed069b01-922e-444f-96b5-3ac28cf26fa3"), new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"), 25m, 1 },
                    { new Guid("fa8ff5d0-8aac-4829-9378-186fc8f23977"), new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"), 189m, 3 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PublisherID",
                table: "Authors",
                type: "uniqueidentifier",
                nullable: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_Authors_PublisherID",
            //    table: "Authors",
            //    column: "PublisherID");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_Publishers_PublisherID",
                table: "Authors",
                column: "PublisherID",
                principalTable: "Publishers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
    }
}
