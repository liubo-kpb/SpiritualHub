using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpiritualHub.Data.Migrations
{
    public partial class PublisherColumnNameChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("050ca3d0-7087-45af-a68d-5d814f73f604"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("108f6076-da3c-4085-828e-e297713a0a97"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("3009ac6f-6fcb-477d-9516-205f325b9244"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("30104344-d78c-49a7-8985-1d5ad55069be"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("43b40f97-7b8f-4aa4-8e4b-34f5231db710"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("67a9f2d4-f513-4af2-a09b-93ef70a7dca9"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("b094f677-3b9b-4c98-b825-879048418178"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("b4547b31-354e-453f-8684-f66bfb23fab9"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("dad6db19-d97a-41c5-84d6-fc518a5a873f"));

            migrationBuilder.RenameColumn(
                name: "PublishedAuthorsId",
                table: "AuthorPublisher",
                newName: "AuthorsId");

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
                column: "CreatedOn",
                value: new DateTime(2023, 8, 28, 8, 47, 50, 850, DateTimeKind.Utc).AddTicks(6));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("3db097df-7c7c-4c4e-b546-d4555c4c1521"),
                column: "CreatedOn",
                value: new DateTime(2023, 8, 28, 8, 47, 50, 850, DateTimeKind.Utc).AddTicks(33));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("45bb1c09-b50d-4d47-8fdb-fbfb53086922"),
                column: "CreatedOn",
                value: new DateTime(2023, 8, 28, 8, 47, 50, 850, DateTimeKind.Utc).AddTicks(24));

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

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorPublisher_Authors_AuthorsId",
                table: "AuthorPublisher",
                column: "AuthorsId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorPublisher_Authors_AuthorsId",
                table: "AuthorPublisher");

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

            migrationBuilder.RenameColumn(
                name: "AuthorsId",
                table: "AuthorPublisher",
                newName: "PublishedAuthorsId");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("194974cd-73f0-4946-ba85-710d4061472d"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "22cb25b4-9b34-4fd0-89b7-0fefba8a2948", "AQAAAAEAACcQAAAAELi1fgndhs1kzMy3Pue3O1TUjsXlbPHhcBKka9dOI4o/ktpcbaARFYE+3+5DNyc4Fg==", "629609d8-be76-47e3-912a-10531fed64d5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("1fd95f69-4f9d-4671-b126-cefcf4b8a95e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ede91845-add6-4351-a528-75d0551cb56b", "AQAAAAEAACcQAAAAEFe1jeDB3raRG26+sT690b9bhWGXosg25GFPuZdnzcjinmHXtEToKXbOOgBPPWFYJQ==", "90ae48ca-ffa4-4f81-b453-824ad9c33a29" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("bcb4f072-ecca-43c9-ab26-c060c6f364e4"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "caa9ad93-8289-46f5-ba22-56efd4abb410", "AQAAAAEAACcQAAAAEJ+Lp3t9g/iiY/OPUO/k+3nNB0QpCpGuYz4S3RAB+olKNvm3+xRRUs48wdcKToKbFg==", "1a0113cb-8971-4cd0-acb9-766a8eb96ee5" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("15f326bc-f47f-487f-8764-5fb8fd5c448a"),
                column: "CreatedOn",
                value: new DateTime(2023, 8, 27, 19, 36, 39, 424, DateTimeKind.Utc).AddTicks(4434));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("3db097df-7c7c-4c4e-b546-d4555c4c1521"),
                column: "CreatedOn",
                value: new DateTime(2023, 8, 27, 19, 36, 39, 424, DateTimeKind.Utc).AddTicks(4463));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("45bb1c09-b50d-4d47-8fdb-fbfb53086922"),
                column: "CreatedOn",
                value: new DateTime(2023, 8, 27, 19, 36, 39, 424, DateTimeKind.Utc).AddTicks(4455));

            migrationBuilder.InsertData(
                table: "Subscriptions",
                columns: new[] { "Id", "AuthorID", "Price", "SubscriptionTypeID" },
                values: new object[,]
                {
                    { new Guid("050ca3d0-7087-45af-a68d-5d814f73f604"), new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"), 150m, 3 },
                    { new Guid("108f6076-da3c-4085-828e-e297713a0a97"), new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"), 21m, 1 },
                    { new Guid("3009ac6f-6fcb-477d-9516-205f325b9244"), new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"), 65m, 2 },
                    { new Guid("30104344-d78c-49a7-8985-1d5ad55069be"), new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"), 70m, 2 },
                    { new Guid("43b40f97-7b8f-4aa4-8e4b-34f5231db710"), new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"), 55m, 2 },
                    { new Guid("67a9f2d4-f513-4af2-a09b-93ef70a7dca9"), new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"), 189m, 3 },
                    { new Guid("b094f677-3b9b-4c98-b825-879048418178"), new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"), 25m, 1 },
                    { new Guid("b4547b31-354e-453f-8684-f66bfb23fab9"), new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"), 200m, 3 },
                    { new Guid("dad6db19-d97a-41c5-84d6-fc518a5a873f"), new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"), 25m, 1 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorPublisher_Authors_PublishedAuthorsId",
                table: "AuthorPublisher",
                column: "PublishedAuthorsId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
