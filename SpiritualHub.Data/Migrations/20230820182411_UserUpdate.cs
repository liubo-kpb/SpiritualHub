using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpiritualHub.Data.Migrations
{
    public partial class UserUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(12)",
                maxLength: 12,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(12)",
                oldMaxLength: 12);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("194974cd-73f0-4946-ba85-710d4061472d"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "9e570b57-be0a-4b8e-9d62-28c338082152", "AQAAAAEAACcQAAAAEO3hIGKJNck51rrys1y/NSso++p8dzEeVA05YIB6rn8AuRZvGWa0CiHlSwJQ5CSomA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("1fd95f69-4f9d-4671-b126-cefcf4b8a95e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e73d63bf-de52-424d-81ae-4477bb789396", "AQAAAAEAACcQAAAAEOw8erVJB2RD+Rrf17S3yXXM2IaIkV9wjMf/A1FhN9VDTyMyFCL9z577In68Z8+SFg==" });

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(12)",
                maxLength: 12,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(12)",
                oldMaxLength: 12,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("194974cd-73f0-4946-ba85-710d4061472d"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "020dda7f-bed4-47ba-9c76-472915556522", "AQAAAAEAACcQAAAAELBkPxzcvkRr3p6V68W0ovYkaCRaCUxUf5/Fwf3FboyUtZgAgDem7i74Vh4K5GRi6w==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("1fd95f69-4f9d-4671-b126-cefcf4b8a95e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2a39414f-eb4b-4f5d-b47a-799d68d4bb5e", "AQAAAAEAACcQAAAAENM9lK7q2Fc0KRnNDzfSPZZbJ/m1dvdcYRZNjftuakdjNQ1lYwAbWwG1ju2XIlPVzg==" });

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
    }
}
