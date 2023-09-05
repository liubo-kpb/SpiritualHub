using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpiritualHub.Data.Migrations
{
    public partial class EventEntityChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Events_Publishers_OrganizerID",
            //    table: "Events");

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

            migrationBuilder.RenameColumn(
                name: "OrganizerID",
                table: "Events",
                newName: "PublisherID");

            migrationBuilder.RenameIndex(
                name: "IX_Events_OrganizerID",
                table: "Events",
                newName: "IX_Events_PublisherID");

            migrationBuilder.AddColumn<string>(
                name: "LocationName",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LocationUrl",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true);

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
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"),
                column: "Description",
                value: "With its unique integration of scientific principles and spiritual insights, the Academy provides access to new horizons in understanding oneself and the surrounding world. The thesis that everything is interconnected is not just a theory or esoteric belief but a practical principle of Existence!\r\nIn the Academy, you pave the path towards your synchronized unfolding of thought, information, and energy, receiving tools and knowledge to consciously create your gracious and grateful world.\r\nCogitality Academy is the culmination of years of effort, exploration, practice, and mistakes, through which you now gain the fastest and easiest access to this extraordinary realm of wisdom and Life, beyond the confines of time!");

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
                name: "FK_Events_Publishers_PublisherID",
                table: "Events",
                column: "PublisherID",
                principalTable: "Publishers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Publishers_PublisherID",
                table: "Events");

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

            migrationBuilder.DropColumn(
                name: "LocationName",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "LocationUrl",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "PublisherID",
                table: "Events",
                newName: "OrganizerID");

            migrationBuilder.RenameIndex(
                name: "IX_Events_PublisherID",
                table: "Events",
                newName: "IX_Events_OrganizerID");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("194974cd-73f0-4946-ba85-710d4061472d"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b006360a-5652-4476-9631-69ecb559e7b1", "AQAAAAEAACcQAAAAEL1QtpZdql9PqPeGVCIOTdL/v2GBy3aTMN75QM2lhBJN6GJOsz5fTaS5hFQ7hPydwA==", "9b3c2066-fe40-4458-aa45-7add1ed8eb1b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("1fd95f69-4f9d-4671-b126-cefcf4b8a95e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "07a62c31-f623-4ea4-8fc3-7e563b7d3ce4", "AQAAAAEAACcQAAAAEIErNcGRnjCVQMIX/disHNlRimlGqGF15oNG7m4XsDfwzIrIbpBn8MW21whhqPRIpg==", "7e04ad78-b0c1-42df-ab68-acab872b4e06" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("bcb4f072-ecca-43c9-ab26-c060c6f364e4"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "10429995-c696-41fd-a05e-255e2b5fa0ec", "AQAAAAEAACcQAAAAEDqBvFOogyDTqh9TWvpB0Quykj8MY4CKNBMc2Lr2CK+lON8epBs84nj94XU4H92Bzw==", "89b4836e-940d-456d-9233-25bb516e76ef" });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"),
                column: "Description",
                value: "With its unique integration of scientific principles and spiritual insights, the Academy provides access to new horizons in understanding oneself and the surrounding world. The thesis that everything is interconnected is not just a theory or esoteric belief but a practical principle of Existence!\r\n\r\nIn the Academy, you pave the path towards your synchronized unfolding of thought, information, and energy, receiving tools and knowledge to consciously create your gracious and grateful world.\r\n\r\nCogitality Academy is the culmination of years of effort, exploration, practice, and mistakes, through which you now gain the fastest and easiest access to this extraordinary realm of wisdom and Life, beyond the confines of time!");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Publishers_OrganizerID",
                table: "Events",
                column: "OrganizerID",
                principalTable: "Publishers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
