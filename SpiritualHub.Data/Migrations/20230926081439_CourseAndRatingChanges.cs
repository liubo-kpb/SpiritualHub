using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpiritualHub.Data.Migrations
{
    public partial class CourseAndRatingChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Authors_AuthorID",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Books_BookID",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Courses_CourseID",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Events_EventID",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_AuthorID",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_BookID",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_CourseID",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_EventID",
                table: "Ratings");

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
                name: "AuthorID",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "BookID",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "CourseID",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "EventID",
                table: "Ratings");

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Ratings",
                type: "nvarchar(600)",
                maxLength: 600,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Headline",
                table: "Ratings",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ShortDescription",
                table: "Courses",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Courses",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Courses",
                type: "bit",
                nullable: true,
                defaultValue: true);

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Comments",
                type: "nvarchar(600)",
                maxLength: 600,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Blogs",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "ShortDescription",
                table: "Blogs",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.CreateTable(
                name: "AuthorRating",
                columns: table => new
                {
                    AuthorsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RatingsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorRating", x => new { x.AuthorsId, x.RatingsId });
                    table.ForeignKey(
                        name: "FK_AuthorRating_Authors_AuthorsId",
                        column: x => x.AuthorsId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorRating_Ratings_RatingsId",
                        column: x => x.RatingsId,
                        principalTable: "Ratings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookRating",
                columns: table => new
                {
                    BooksId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RatingsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookRating", x => new { x.BooksId, x.RatingsId });
                    table.ForeignKey(
                        name: "FK_BookRating_Books_BooksId",
                        column: x => x.BooksId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookRating_Ratings_RatingsId",
                        column: x => x.RatingsId,
                        principalTable: "Ratings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseRating",
                columns: table => new
                {
                    CoursesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RatingsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseRating", x => new { x.CoursesId, x.RatingsId });
                    table.ForeignKey(
                        name: "FK_CourseRating_Courses_CoursesId",
                        column: x => x.CoursesId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseRating_Ratings_RatingsId",
                        column: x => x.RatingsId,
                        principalTable: "Ratings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventRating",
                columns: table => new
                {
                    EventsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RatingsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventRating", x => new { x.EventsId, x.RatingsId });
                    table.ForeignKey(
                        name: "FK_EventRating_Events_EventsId",
                        column: x => x.EventsId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventRating_Ratings_RatingsId",
                        column: x => x.RatingsId,
                        principalTable: "Ratings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Module",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    VideoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CourseID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Module", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Module_Courses_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ModuleRating",
                columns: table => new
                {
                    ModulesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RatingsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuleRating", x => new { x.ModulesId, x.RatingsId });
                    table.ForeignKey(
                        name: "FK_ModuleRating_Module_ModulesId",
                        column: x => x.ModulesId,
                        principalTable: "Module",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModuleRating_Ratings_RatingsId",
                        column: x => x.RatingsId,
                        principalTable: "Ratings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("187e0540-5a90-419a-bf5b-f65ee213a0ca"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b032432e-5d24-48c5-86d2-12c088f1f2bb", "AQAAAAEAACcQAAAAEBNg90h7kHCRitjcNTVItzrg6zmwiA5R4kF3n+zg6RCLQ0XvTqMqpQtqW+5UOnLkpA==", "368a5128-fa0a-40a1-ba32-eba30ac8c6d5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("194974cd-73f0-4946-ba85-710d4061472d"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7b11c4fd-83d3-4d93-b086-01348cfc0349", "AQAAAAEAACcQAAAAELvxxlnnruGo6yRNnSQ73PpOsSgZyHeQWD/4XK6VEsYUsgOhqO80TvzYI4YnIMnOlQ==", "3cce809a-1f6f-4246-b75a-f741ff8b4fba" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("1fd95f69-4f9d-4671-b126-cefcf4b8a95e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "791c5c82-50ce-4731-bbc8-1ba29e0780ce", "AQAAAAEAACcQAAAAEMwwyqUVX92RfAxp+7EdVu+Ke54BeXPqODpihEMwMgxMWDKZ68sWPbUJinpHcaJEsg==", "bd3f252d-55c8-4815-8b7a-2c4c6d15b9d9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("bcb4f072-ecca-43c9-ab26-c060c6f364e4"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6351c2f2-c76d-40af-8fff-0e6caed63788", "AQAAAAEAACcQAAAAEA9AcQg++7pn8NK7JK8qjMIuA0wAt8gdXQ7dTkGiIQbTk9GUoT+NlfCM3CITzYnruQ==", "803e32b6-a95d-4ae6-8cca-24d4512c3488" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("15f326bc-f47f-487f-8764-5fb8fd5c448a"),
                column: "CreatedOn",
                value: new DateTime(2023, 9, 26, 8, 14, 37, 677, DateTimeKind.Utc).AddTicks(7863));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("3db097df-7c7c-4c4e-b546-d4555c4c1521"),
                column: "CreatedOn",
                value: new DateTime(2023, 9, 26, 8, 14, 37, 677, DateTimeKind.Utc).AddTicks(7931));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("45bb1c09-b50d-4d47-8fdb-fbfb53086922"),
                column: "CreatedOn",
                value: new DateTime(2023, 9, 26, 8, 14, 37, 677, DateTimeKind.Utc).AddTicks(7908));

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "Name", "URL" },
                values: new object[,]
                {
                    { new Guid("01457f31-bc09-4d77-8d5d-6c334ff3347b"), "ChannelYourPassion", "https://www.strategyblocks.com/wp-content/uploads/2017/09/mountains.jpg" },
                    { new Guid("1dbf6044-b493-4373-b650-c5c00c967086"), "CogitalityAcademy", "https://academy.cogitality.net/wp-content/uploads/elementor/thumbs/logo-ca-e1687374679913-q8ax1pqb2zkk37wfo9eeytezs45hegaqpfefkzqldy.png" },
                    { new Guid("251f1b7a-4aec-46b5-8cde-1740103cde1f"), "AncientAlchemy", "https://cdn.hswstatic.com/gif/alchemy.jpg" },
                    { new Guid("d3d4a16d-0050-4947-90a9-9133a2b129b9"), "ExperienceNow", "https://cdn-fkmoj.nitrocdn.com/xvpOGZRTxJUhXKufpOYIruQcRqtvAAQX/assets/images/optimized/rev-4e1f421/s3.amazonaws.com/media.briantracy.com/blog/wp-content/uploads/2021/09/03073828/motivational-inspirational-quotes.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Subscriptions",
                columns: new[] { "Id", "AuthorID", "Price", "SubscriptionTypeID" },
                values: new object[,]
                {
                    { new Guid("0c8ee481-5397-4337-9684-aba4e5feb540"), new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"), 65m, 2 },
                    { new Guid("0fdf7f50-f77e-464b-ad75-96e4cfe5218c"), new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"), 189m, 3 },
                    { new Guid("15c33224-9ae1-4a47-99f3-aed6edb5921f"), new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"), 70m, 2 },
                    { new Guid("210980f2-78b5-4a0e-9d65-77506cb36de1"), new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"), 21m, 1 },
                    { new Guid("83af078e-8078-4729-98fc-851074eeae29"), new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"), 25m, 1 },
                    { new Guid("8834990d-0060-4f92-9db1-f8207ed86d5c"), new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"), 200m, 3 },
                    { new Guid("d6d84e60-afd3-47cd-94b1-337d884f5c0a"), new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"), 150m, 3 },
                    { new Guid("f8f28e33-c224-4f81-b0e4-890fa4ddb38b"), new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"), 25m, 1 },
                    { new Guid("fc6236a7-5089-400f-b31f-45e9f4c7578d"), new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"), 55m, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorRating_RatingsId",
                table: "AuthorRating",
                column: "RatingsId");

            migrationBuilder.CreateIndex(
                name: "IX_BookRating_RatingsId",
                table: "BookRating",
                column: "RatingsId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseRating_RatingsId",
                table: "CourseRating",
                column: "RatingsId");

            migrationBuilder.CreateIndex(
                name: "IX_EventRating_RatingsId",
                table: "EventRating",
                column: "RatingsId");

            migrationBuilder.CreateIndex(
                name: "IX_Module_CourseID",
                table: "Module",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_ModuleRating_RatingsId",
                table: "ModuleRating",
                column: "RatingsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorRating");

            migrationBuilder.DropTable(
                name: "BookRating");

            migrationBuilder.DropTable(
                name: "CourseRating");

            migrationBuilder.DropTable(
                name: "EventRating");

            migrationBuilder.DropTable(
                name: "ModuleRating");

            migrationBuilder.DropTable(
                name: "Module");

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("01457f31-bc09-4d77-8d5d-6c334ff3347b"));

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("1dbf6044-b493-4373-b650-c5c00c967086"));

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("251f1b7a-4aec-46b5-8cde-1740103cde1f"));

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("d3d4a16d-0050-4947-90a9-9133a2b129b9"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("0c8ee481-5397-4337-9684-aba4e5feb540"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("0fdf7f50-f77e-464b-ad75-96e4cfe5218c"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("15c33224-9ae1-4a47-99f3-aed6edb5921f"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("210980f2-78b5-4a0e-9d65-77506cb36de1"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("83af078e-8078-4729-98fc-851074eeae29"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("8834990d-0060-4f92-9db1-f8207ed86d5c"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("d6d84e60-afd3-47cd-94b1-337d884f5c0a"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("f8f28e33-c224-4f81-b0e4-890fa4ddb38b"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("fc6236a7-5089-400f-b31f-45e9f4c7578d"));

            migrationBuilder.DropColumn(
                name: "Headline",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Courses");

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Ratings",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(600)",
                oldMaxLength: 600);

            migrationBuilder.AddColumn<Guid>(
                name: "AuthorID",
                table: "Ratings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BookID",
                table: "Ratings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CourseID",
                table: "Ratings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "EventID",
                table: "Ratings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "ShortDescription",
                table: "Courses",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Courses",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(80)",
                oldMaxLength: 80);

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Comments",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(600)",
                oldMaxLength: 600);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Blogs",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "ShortDescription",
                table: "Blogs",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

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
                name: "IX_Ratings_AuthorID",
                table: "Ratings",
                column: "AuthorID");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_BookID",
                table: "Ratings",
                column: "BookID");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_CourseID",
                table: "Ratings",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_EventID",
                table: "Ratings",
                column: "EventID");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Authors_AuthorID",
                table: "Ratings",
                column: "AuthorID",
                principalTable: "Authors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Books_BookID",
                table: "Ratings",
                column: "BookID",
                principalTable: "Books",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Courses_CourseID",
                table: "Ratings",
                column: "CourseID",
                principalTable: "Courses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Events_EventID",
                table: "Ratings",
                column: "EventID",
                principalTable: "Events",
                principalColumn: "Id");
        }
    }
}
