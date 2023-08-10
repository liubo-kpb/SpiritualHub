using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpiritualHub.Data.Migrations
{
    public partial class CourseAndBlogImageSolution : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ImageID",
                table: "Courses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "BlogPostImage",
                columns: table => new
                {
                    BlogID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPostImage", x => new { x.ImageID, x.BlogID });
                    table.ForeignKey(
                        name: "FK_BlogPostImage_Blogs_BlogID",
                        column: x => x.BlogID,
                        principalTable: "Blogs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BlogPostImage_Images_ImageID",
                        column: x => x.ImageID,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("194974cd-73f0-4946-ba85-710d4061472d"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8ad2d23d-7c63-4165-9961-a7aa94604bbd", "AQAAAAEAACcQAAAAENVnXn5EaOiEP+DSMciwms46PNpyLAjmlMh+ZkyzPDluFYkbBuIb9BVqEudwYMZdKQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("1fd95f69-4f9d-4671-b126-cefcf4b8a95e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3cffc673-f405-47f7-b44a-694cc4cd6f15", "AQAAAAEAACcQAAAAEAsH4y/dFAzbuUrqpbH9zAZwJ4aSOFBwOBfjDIKzpy1+YFmNxw2e8LKrqqkHYc3SCw==" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("15f326bc-f47f-487f-8764-5fb8fd5c448a"),
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 7, 6, 32, 528, DateTimeKind.Utc).AddTicks(465));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("3db097df-7c7c-4c4e-b546-d4555c4c1521"),
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 7, 6, 32, 528, DateTimeKind.Utc).AddTicks(490));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("45bb1c09-b50d-4d47-8fdb-fbfb53086922"),
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 7, 6, 32, 528, DateTimeKind.Utc).AddTicks(482));

            migrationBuilder.CreateIndex(
                name: "IX_Courses_ImageID",
                table: "Courses",
                column: "ImageID");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPostImage_BlogID",
                table: "BlogPostImage",
                column: "BlogID");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Images_ImageID",
                table: "Courses",
                column: "ImageID",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Images_ImageID",
                table: "Courses");

            migrationBuilder.DropTable(
                name: "BlogPostImage");

            migrationBuilder.DropIndex(
                name: "IX_Courses_ImageID",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "ImageID",
                table: "Courses");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("194974cd-73f0-4946-ba85-710d4061472d"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "fc21e028-6e4b-461f-9c32-9ec68f2f673d", "AQAAAAEAACcQAAAAEIL4AEEiADwVPojIM5PyhJEFXwRdtQ0qBWlh+x1Zbx0ShTOvOtHbE1HkKe93PfOZRw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("1fd95f69-4f9d-4671-b126-cefcf4b8a95e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "30e036f4-ffe6-40ad-83ef-480491843207", "AQAAAAEAACcQAAAAEP0BPL1YhJ1JWQIMtL86h4q83ZNOPJur1ONYN7o6PRC+R2MJJesrQlktv6TaOYasrA==" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("15f326bc-f47f-487f-8764-5fb8fd5c448a"),
                column: "CreatedOn",
                value: new DateTime(2023, 8, 9, 22, 51, 34, 677, DateTimeKind.Utc).AddTicks(166));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("3db097df-7c7c-4c4e-b546-d4555c4c1521"),
                column: "CreatedOn",
                value: new DateTime(2023, 8, 9, 22, 51, 34, 677, DateTimeKind.Utc).AddTicks(264));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("45bb1c09-b50d-4d47-8fdb-fbfb53086922"),
                column: "CreatedOn",
                value: new DateTime(2023, 8, 9, 22, 51, 34, 677, DateTimeKind.Utc).AddTicks(251));
        }
    }
}
