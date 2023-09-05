using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpiritualHub.Data.Migrations
{
    public partial class ColumnFixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Authors_Publishers_PublisherID",
            //    table: "Authors");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Blogs_BlogID",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Books_BookId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Courses_CourseID",
                table: "Images");

            migrationBuilder.DropCheckConstraint(
                name: "CK__PossibleTypes",
                table: "SubscriptionTypes");

            migrationBuilder.DropIndex(
                name: "IX_Images_BookId",
                table: "Images");

            //migrationBuilder.DropIndex(
            //    name: "IX_Authors_PublisherID",
            //    table: "Authors");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "PublisherID",
                table: "Authors");

            migrationBuilder.RenameColumn(
                name: "CourseID",
                table: "Images",
                newName: "CourseId");

            migrationBuilder.RenameColumn(
                name: "BlogID",
                table: "Images",
                newName: "BlogId");

            migrationBuilder.RenameIndex(
                name: "IX_Images_CourseID",
                table: "Images",
                newName: "IX_Images_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Images_BlogID",
                table: "Images",
                newName: "IX_Images_BlogId");

            migrationBuilder.AlterColumn<Guid>(
                name: "CourseId",
                table: "Images",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "BlogId",
                table: "Images",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "ImageID",
                table: "Events",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ImageID",
                table: "Books",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "AuthorPublisher",
                columns: table => new
                {
                    PublishedAuthorsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PublishersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorPublisher", x => new { x.PublishedAuthorsId, x.PublishersId });
                    table.ForeignKey(
                        name: "FK_AuthorPublisher_Authors_PublishedAuthorsId",
                        column: x => x.PublishedAuthorsId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorPublisher_Publishers_PublishersId",
                        column: x => x.PublishersId,
                        principalTable: "Publishers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddCheckConstraint(
                name: "CK__PossibleTypes",
                table: "SubscriptionTypes",
                sql: "Type = 'Monthly' OR Type = 'Quarterly' OR Type = 'Annual'");

            migrationBuilder.CreateIndex(
                name: "IX_Events_ImageID",
                table: "Events",
                column: "ImageID");

            migrationBuilder.CreateIndex(
                name: "IX_Books_ImageID",
                table: "Books",
                column: "ImageID");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorPublisher_PublishersId",
                table: "AuthorPublisher",
                column: "PublishersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Images_ImageID",
                table: "Books",
                column: "ImageID",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Images_ImageID",
                table: "Events",
                column: "ImageID",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Blogs_BlogId",
                table: "Images",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Courses_CourseId",
                table: "Images",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Images_ImageID",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Images_ImageID",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Blogs_BlogId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Courses_CourseId",
                table: "Images");

            migrationBuilder.DropTable(
                name: "AuthorPublisher");

            migrationBuilder.DropCheckConstraint(
                name: "CK__PossibleTypes",
                table: "SubscriptionTypes");

            migrationBuilder.DropIndex(
                name: "IX_Events_ImageID",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Books_ImageID",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "ImageID",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "ImageID",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "Images",
                newName: "CourseID");

            migrationBuilder.RenameColumn(
                name: "BlogId",
                table: "Images",
                newName: "BlogID");

            migrationBuilder.RenameIndex(
                name: "IX_Images_CourseId",
                table: "Images",
                newName: "IX_Images_CourseID");

            migrationBuilder.RenameIndex(
                name: "IX_Images_BlogId",
                table: "Images",
                newName: "IX_Images_BlogID");

            migrationBuilder.AlterColumn<Guid>(
                name: "CourseID",
                table: "Images",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "BlogID",
                table: "Images",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BookId",
                table: "Images",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PublisherID",
                table: "Authors",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddCheckConstraint(
                name: "CK__PossibleTypes",
                table: "SubscriptionTypes",
                sql: "Type = 'Monthly' ORType = 'Quarterly' ORType = 'Annual'");

            migrationBuilder.CreateIndex(
                name: "IX_Images_BookId",
                table: "Images",
                column: "BookId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Blogs_BlogID",
                table: "Images",
                column: "BlogID",
                principalTable: "Blogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Books_BookId",
                table: "Images",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Courses_CourseID",
                table: "Images",
                column: "CourseID",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
