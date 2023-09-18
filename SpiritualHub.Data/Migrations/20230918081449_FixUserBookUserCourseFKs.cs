using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpiritualHub.Data.Migrations
{
    public partial class FixUserBookUserCourseFKs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_AspNetUsers_ApplicationUserId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_AspNetUsers_ApplicationUserId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_ApplicationUserId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Books_ApplicationUserId",
                table: "Books");

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("0b38dbbb-1657-4347-8cc2-15c50e8fd167"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("0d530c57-2ec2-4617-a918-2f573a0d9317"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("3b204122-6918-4a1b-ad6f-f489a5035a3e"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("4ffe90fc-18a5-4982-9cbc-0199f7e14ad9"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("5d15b369-03e2-4e98-a1c5-17af15319974"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("beabb97b-a19b-48d0-a2bb-3a4f1f480254"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("e95fff8c-6454-4f70-8b88-96f957a2b5e0"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("eb524666-b14d-47b0-8a3c-0b7ce17fa138"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("f614d540-41d2-4972-a00e-5733c84d9549"));

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Books");

            migrationBuilder.AddColumn<bool>(
                name: "IsHidden",
                table: "Books",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "ApplicationUserBook",
                columns: table => new
                {
                    BooksId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReadersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserBook", x => new { x.BooksId, x.ReadersId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserBook_AspNetUsers_ReadersId",
                        column: x => x.ReadersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ApplicationUserBook_Books_BooksId",
                        column: x => x.BooksId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserCourse",
                columns: table => new
                {
                    CoursesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserCourse", x => new { x.CoursesId, x.StudentsId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserCourse_AspNetUsers_StudentsId",
                        column: x => x.StudentsId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ApplicationUserCourse_Courses_CoursesId",
                        column: x => x.CoursesId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("187e0540-5a90-419a-bf5b-f65ee213a0ca"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b55b0670-4d9e-4464-882d-988d0621d482", "AQAAAAEAACcQAAAAEPlF/S0mZhTC7XOaMUiiZdKTbZ+knSkdeLL5W34OhEomgjpa0gQJz9OXQpgE2M4lKw==", "bc07e234-dbaa-4483-9ebd-ce978d466b5c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("194974cd-73f0-4946-ba85-710d4061472d"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9618370a-69e8-4f5c-926e-5f5ee2df3dd7", "AQAAAAEAACcQAAAAELPRGOGCB+mH1O17I/p+REf5R43sSv3/egjaA31zbKzQXYxmL4Od0S19ngncI7sFQg==", "a1e87dfe-fd79-45dc-8a79-22251494801c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("1fd95f69-4f9d-4671-b126-cefcf4b8a95e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3f56b8f1-b02f-4560-ac24-66e2a3930820", "AQAAAAEAACcQAAAAEGvnNzde5g0wCoV3KChh99WZPtLBwy5fm1auOgN3nHWKTzTkW5QmWEbcBS7ISn9zOg==", "d4c079bc-881d-49e0-95bb-23c154128bac" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("bcb4f072-ecca-43c9-ab26-c060c6f364e4"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b006aaef-4a2f-4a30-8018-d52b1138c62b", "AQAAAAEAACcQAAAAEIbCdHOdfdAZLdHC0/KSjSXUBb3/Aq9hnTJI9SQkgS03rsWezquqxGd27S5eDm98zQ==", "83c5bcef-d1ca-43fa-bdc1-b1593d63a4c7" });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("0fd425bd-bb0e-477e-ab19-a58ddad6fb27"),
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"),
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"),
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("68508613-d974-4237-5182-08dba58c19e0"),
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"),
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("15f326bc-f47f-487f-8764-5fb8fd5c448a"),
                column: "CreatedOn",
                value: new DateTime(2023, 9, 18, 8, 14, 45, 550, DateTimeKind.Utc).AddTicks(1924));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("3db097df-7c7c-4c4e-b546-d4555c4c1521"),
                column: "CreatedOn",
                value: new DateTime(2023, 9, 18, 8, 14, 45, 550, DateTimeKind.Utc).AddTicks(2216));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("45bb1c09-b50d-4d47-8fdb-fbfb53086922"),
                column: "CreatedOn",
                value: new DateTime(2023, 9, 18, 8, 14, 45, 550, DateTimeKind.Utc).AddTicks(2010));

            migrationBuilder.InsertData(
                table: "Subscriptions",
                columns: new[] { "Id", "AuthorID", "Price", "SubscriptionTypeID" },
                values: new object[,]
                {
                    { new Guid("34fc3be5-8970-445a-8177-17869599276e"), new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"), 55m, 2 },
                    { new Guid("44d15e3c-3655-4dac-9351-adf22596297e"), new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"), 25m, 1 },
                    { new Guid("6eeb1155-7b95-4123-8b13-ec5f8a2d8c6d"), new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"), 65m, 2 },
                    { new Guid("71503ce1-a4bd-47cc-b9f4-aec5c6ee8b59"), new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"), 189m, 3 },
                    { new Guid("9ddb8474-ce6c-4375-99bd-fd3d0f2614e4"), new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"), 200m, 3 },
                    { new Guid("a45531de-8abf-431e-8834-af613f3c626a"), new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"), 25m, 1 },
                    { new Guid("b359ed3c-faca-465f-b274-a57b6054a422"), new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"), 21m, 1 },
                    { new Guid("d70eb15c-4179-48ca-8b85-6f194028b0c0"), new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"), 70m, 2 },
                    { new Guid("e9db03c7-3a1f-4169-b17b-c26d6798de4c"), new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"), 150m, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserBook_ReadersId",
                table: "ApplicationUserBook",
                column: "ReadersId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserCourse_StudentsId",
                table: "ApplicationUserCourse",
                column: "StudentsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserBook");

            migrationBuilder.DropTable(
                name: "ApplicationUserCourse");

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("34fc3be5-8970-445a-8177-17869599276e"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("44d15e3c-3655-4dac-9351-adf22596297e"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("6eeb1155-7b95-4123-8b13-ec5f8a2d8c6d"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("71503ce1-a4bd-47cc-b9f4-aec5c6ee8b59"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("9ddb8474-ce6c-4375-99bd-fd3d0f2614e4"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("a45531de-8abf-431e-8834-af613f3c626a"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("b359ed3c-faca-465f-b274-a57b6054a422"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("d70eb15c-4179-48ca-8b85-6f194028b0c0"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("e9db03c7-3a1f-4169-b17b-c26d6798de4c"));

            migrationBuilder.DropColumn(
                name: "IsHidden",
                table: "Books");

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserId",
                table: "Courses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserId",
                table: "Books",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("187e0540-5a90-419a-bf5b-f65ee213a0ca"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d17de0f2-70f8-4b6d-983c-ee1b60f1b64b", "AQAAAAEAACcQAAAAEDIuGpCI2pJqxE9v4R9h2z7ZVDVUlJSQfMUlfCKy0tJxYAlPRHQp3jn58f9Upnvxvw==", "900f7d84-d92f-4674-8d77-f37296b9ee75" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("194974cd-73f0-4946-ba85-710d4061472d"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d60dad2b-204a-4637-bda1-66e65d878bc4", "AQAAAAEAACcQAAAAEJd+WeTB17UOaGVVsUEJ2d8+DTbRv2VKtCiwG/C4g62BeLSSkHyaEACkXLoK0XpFSg==", "275de793-7938-4011-bd4b-9c38de417c47" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("1fd95f69-4f9d-4671-b126-cefcf4b8a95e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "716dd339-39c4-4fe2-98f5-88fa86cc3390", "AQAAAAEAACcQAAAAEIj90D3UO8MbRRxKZkolYWxZqHcGg5Lmg37BhhMeU3RuPhmMduKwCWmHdZqknLd2wA==", "38474616-d00b-4898-adf5-af99e07c89f1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("bcb4f072-ecca-43c9-ab26-c060c6f364e4"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "33263793-7ed9-4a45-a208-deabd06ee2b6", "AQAAAAEAACcQAAAAEEkhhwzd5BBX0vl+CyaFUjKu2OA88MxaLA0MLuYcoGPDqvnYE3/8CDa6aKjRYHrjeQ==", "c0a20a5b-3d45-4cc1-9407-4abe92148206" });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("0fd425bd-bb0e-477e-ab19-a58ddad6fb27"),
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"),
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"),
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("68508613-d974-4237-5182-08dba58c19e0"),
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"),
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("15f326bc-f47f-487f-8764-5fb8fd5c448a"),
                column: "CreatedOn",
                value: new DateTime(2023, 9, 15, 14, 2, 54, 703, DateTimeKind.Utc).AddTicks(2964));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("3db097df-7c7c-4c4e-b546-d4555c4c1521"),
                column: "CreatedOn",
                value: new DateTime(2023, 9, 15, 14, 2, 54, 703, DateTimeKind.Utc).AddTicks(3011));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("45bb1c09-b50d-4d47-8fdb-fbfb53086922"),
                column: "CreatedOn",
                value: new DateTime(2023, 9, 15, 14, 2, 54, 703, DateTimeKind.Utc).AddTicks(2993));

            migrationBuilder.InsertData(
                table: "Subscriptions",
                columns: new[] { "Id", "AuthorID", "Price", "SubscriptionTypeID" },
                values: new object[,]
                {
                    { new Guid("0b38dbbb-1657-4347-8cc2-15c50e8fd167"), new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"), 70m, 2 },
                    { new Guid("0d530c57-2ec2-4617-a918-2f573a0d9317"), new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"), 21m, 1 },
                    { new Guid("3b204122-6918-4a1b-ad6f-f489a5035a3e"), new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"), 25m, 1 },
                    { new Guid("4ffe90fc-18a5-4982-9cbc-0199f7e14ad9"), new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"), 200m, 3 },
                    { new Guid("5d15b369-03e2-4e98-a1c5-17af15319974"), new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"), 25m, 1 },
                    { new Guid("beabb97b-a19b-48d0-a2bb-3a4f1f480254"), new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"), 189m, 3 },
                    { new Guid("e95fff8c-6454-4f70-8b88-96f957a2b5e0"), new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"), 150m, 3 },
                    { new Guid("eb524666-b14d-47b0-8a3c-0b7ce17fa138"), new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"), 65m, 2 },
                    { new Guid("f614d540-41d2-4972-a00e-5733c84d9549"), new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"), 55m, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_ApplicationUserId",
                table: "Courses",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_ApplicationUserId",
                table: "Books",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_AspNetUsers_ApplicationUserId",
                table: "Books",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_AspNetUsers_ApplicationUserId",
                table: "Courses",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
