using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpiritualHub.Data.Migrations
{
    public partial class NewDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    URL = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionTypes", x => x.Id);
                    table.CheckConstraint("CK__PossibleTypes", "Type = 'Monthly' OR Type = 'Quarterly' OR Type = 'Annual'");
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Publishers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publishers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Publishers_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Alias = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: false),
                    AvatarImageID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Authors_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Authors_Images_AvatarImageID",
                        column: x => x.AvatarImageID,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserAuthor",
                columns: table => new
                {
                    FollowedAuthorsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FollowersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserAuthor", x => new { x.FollowedAuthorsId, x.FollowersId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserAuthor_AspNetUsers_FollowersId",
                        column: x => x.FollowersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserAuthor_Authors_FollowedAuthorsId",
                        column: x => x.FollowedAuthorsId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuthorPublisher",
                columns: table => new
                {
                    AuthorsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PublishersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorPublisher", x => new { x.AuthorsId, x.PublishersId });
                    table.ForeignKey(
                        name: "FK_AuthorPublisher_Authors_AuthorsId",
                        column: x => x.AuthorsId,
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

            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthorID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: false),
                    PublisherID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blogs_Authors_AuthorID",
                        column: x => x.AuthorID,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Blogs_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Blogs_Publishers_PublisherID",
                        column: x => x.PublisherID,
                        principalTable: "Publishers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,5)", nullable: false),
                    AddedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    AuthorID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PublisherID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: false),
                    ApplicationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Books_Authors_AuthorID",
                        column: x => x.AuthorID,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Books_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Books_Images_ImageID",
                        column: x => x.ImageID,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Books_Publishers_PublisherID",
                        column: x => x.PublisherID,
                        principalTable: "Publishers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,5)", nullable: false),
                    AddedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    AuthorID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PublisherID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: false),
                    ImageID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Courses_Authors_AuthorID",
                        column: x => x.AuthorID,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Courses_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Courses_Images_ImageID",
                        column: x => x.ImageID,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Courses_Publishers_PublisherID",
                        column: x => x.PublisherID,
                        principalTable: "Publishers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,5)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LocationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocationUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsOnline = table.Column<bool>(type: "bit", nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: false),
                    AuthorID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PublisherID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_Authors_AuthorID",
                        column: x => x.AuthorID,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Events_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Events_Images_ImageID",
                        column: x => x.ImageID,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Events_Publishers_PublisherID",
                        column: x => x.PublisherID,
                        principalTable: "Publishers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,5)", nullable: false),
                    SubscriptionTypeID = table.Column<int>(type: "int", nullable: false),
                    AuthorID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscriptions_Authors_AuthorID",
                        column: x => x.AuthorID,
                        principalTable: "Authors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Subscriptions_SubscriptionTypes_SubscriptionTypeID",
                        column: x => x.SubscriptionTypeID,
                        principalTable: "SubscriptionTypes",
                        principalColumn: "Id");
                });

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

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    PostID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comments_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comments_Blogs_PostID",
                        column: x => x.PostID,
                        principalTable: "Blogs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserEvent",
                columns: table => new
                {
                    JoinedEventsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParticipantsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserEvent", x => new { x.JoinedEventsId, x.ParticipantsId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserEvent_AspNetUsers_ParticipantsId",
                        column: x => x.ParticipantsId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserEvent_Events_JoinedEventsId",
                        column: x => x.JoinedEventsId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Stars = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuthorID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ratings_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ratings_Authors_AuthorID",
                        column: x => x.AuthorID,
                        principalTable: "Authors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ratings_Books_BookID",
                        column: x => x.BookID,
                        principalTable: "Books",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ratings_Courses_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Courses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ratings_Events_EventID",
                        column: x => x.EventID,
                        principalTable: "Events",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserSubscription",
                columns: table => new
                {
                    SubscribersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubscriptionsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserSubscription", x => new { x.SubscribersId, x.SubscriptionsId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserSubscription_AspNetUsers_SubscribersId",
                        column: x => x.SubscribersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserSubscription_Subscriptions_SubscriptionsId",
                        column: x => x.SubscriptionsId,
                        principalTable: "Subscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("187e0540-5a90-419a-bf5b-f65ee213a0ca"), 0, "d17de0f2-70f8-4b6d-983c-ee1b60f1b64b", "noname@mail.com", false, null, null, false, null, "NONAME@MAIL.COM", "NONAME@MAIL.COM", "AQAAAAEAACcQAAAAEDIuGpCI2pJqxE9v4R9h2z7ZVDVUlJSQfMUlfCKy0tJxYAlPRHQp3jn58f9Upnvxvw==", null, false, "900f7d84-d92f-4674-8d77-f37296b9ee75", false, "noname@mail.com" },
                    { new Guid("194974cd-73f0-4946-ba85-710d4061472d"), 0, "d60dad2b-204a-4637-bda1-66e65d878bc4", "publisher@spirits.com", false, "Pablo", "Publish", false, null, "PUBLISHER@SPIRITS.COM", "PUBLISHER@SPIRITS.COM", "AQAAAAEAACcQAAAAEJd+WeTB17UOaGVVsUEJ2d8+DTbRv2VKtCiwG/C4g62BeLSSkHyaEACkXLoK0XpFSg==", null, false, "275de793-7938-4011-bd4b-9c38de417c47", false, "publisher@spirits.com" },
                    { new Guid("1fd95f69-4f9d-4671-b126-cefcf4b8a95e"), 0, "716dd339-39c4-4fe2-98f5-88fa86cc3390", "user@mail.com", false, "Martin", "User", false, null, "USER@MAIL.COM", "USER@MAIL.COM", "AQAAAAEAACcQAAAAEIj90D3UO8MbRRxKZkolYWxZqHcGg5Lmg37BhhMeU3RuPhmMduKwCWmHdZqknLd2wA==", null, false, "38474616-d00b-4898-adf5-af99e07c89f1", false, "user@mail.com" },
                    { new Guid("bcb4f072-ecca-43c9-ab26-c060c6f364e4"), 0, "33263793-7ed9-4a45-a208-deabd06ee2b6", "admin@mail.com", false, "Great", "Admin", false, null, "ADMIN@MAIL.COM", "ADMIN@MAIL.COM", "AQAAAAEAACcQAAAAEEkhhwzd5BBX0vl+CyaFUjKu2OA88MxaLA0MLuYcoGPDqvnYE3/8CDa6aKjRYHrjeQ==", null, false, "c0a20a5b-3d45-4cc1-9407-4abe92148206", false, "admin@mail.com" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Esoteric" },
                    { 2, "Channeling" },
                    { 3, "Scientific" },
                    { 4, "Religious" },
                    { 5, "Spiritual" },
                    { 6, "Hindu" }
                });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "Name", "URL" },
                values: new object[,]
                {
                    { new Guid("13e26f61-5a34-44e0-b9d4-d8ab04b8f342"), "An-Evening-with-Eckhart-Tolle-in-Stockholm", "https://eckharttolle.com/wp-content/uploads/2023/02/Waterfront_november_2019-2048x1460.jpg" },
                    { new Guid("26db05ea-2b5e-44dd-bdef-4e74b9ecaa5f"), "EckhartTolleAvatar", "https://eckharttolle.com/wp-content/uploads/2021/03/PHOTO-Eckhart_EDITEDIMG_5197-scaled.jpg" },
                    { new Guid("2a022e06-8c00-435f-93a9-9da816c1b483"), "BasharAvatar", "https://www.bashar.org/wp-content/uploads/2017/02/Bashar_purple2.jpg" },
                    { new Guid("327b0419-5ff9-4694-a4f8-151cb0a46e6b"), "PowerOfNow", "https://m.media-amazon.com/images/I/714FbKtXS+L._AC_UF1000,1000_QL80_.jpg" },
                    { new Guid("55dc2c91-c81b-40de-ac5b-f7474a7acfdc"), "MOL", "https://images-eu.ssl-images-amazon.com/images/I/61oU5+vqzwL._AC_UL750_SR750,750_.jpg" },
                    { new Guid("69630e42-a4de-4116-a1a4-38c43faa0b53"), "The-Three-Behaviors-of-Connection", "https://www.bashar.org/wp-content/uploads/2023/07/THREE-BEHAVIOURS_NEWSPAGE1-1024x576.jpg" },
                    { new Guid("7993bad4-df53-40dd-8921-15d4cdf5c252"), "LaoTzu", "https://www.newtraderu.com/wp-content/uploads/Lao-Tzu-Quotes-about-Life-That-Still-Ring-True-Today-.jpg" },
                    { new Guid("868aaede-674a-44a6-ae21-ec62bd2bec3b"), "CogitalityAvatar", "https://i.ytimg.com/vi/XvV8rllMh6c/maxresdefault.jpg" },
                    { new Guid("ab7cfc34-55f4-4ed8-9687-c48a747e9fb4"), "HealningSeminar", "https://kogitalnost.net/wp-content/uploads/2023/07/FINALL-Kogitalnost-2-3-09-2023-copy-1024x536.webp" },
                    { new Guid("c7b99bd1-8188-4277-b937-81ab367b4034"), "EC", "https://kogitalnost.net/wp-content/uploads/2023/06/3-te-knigi-1.webp" },
                    { new Guid("cbab4cbb-8f68-4445-8e5e-03b9503beb0a"), "Hermes", "https://8bccdf3481.clvaw-cdnwnd.com/fef11e181af7c99838320c3f6ce510d2/200003132-410794107c/hermes%20trismegisto.jpg?ph=8bccdf3481" }
                });

            migrationBuilder.InsertData(
                table: "SubscriptionTypes",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 1, "Monthly" },
                    { 2, "Quarterly" },
                    { 3, "Annual" }
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Alias", "AvatarImageID", "CategoryID", "Description", "IsActive", "Name" },
                values: new object[,]
                {
                    { new Guid("0fd425bd-bb0e-477e-ab19-a58ddad6fb27"), "Hermes Trismegistus", new Guid("cbab4cbb-8f68-4445-8e5e-03b9503beb0a"), 1, "Hermes Trismegistus (from Ancient Greek: Ἑρμῆς ὁ Τρισμέγιστος, \"Hermes the Thrice-Greatest\"; Classical Latin: Mercurius ter Maximus) is a legendary Hellenistic figure that originated as a syncretic combination of the Greek god Hermes and the Egyptian god Thoth. He is the purported author of the Hermetica, a widely diverse series of ancient and medieval pseudepigraphical texts that lay the basis of various philosophical systems known as Hermeticism.", false, "Hermes" },
                    { new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"), "Cogitality - Everything that IS!", new Guid("868aaede-674a-44a6-ae21-ec62bd2bec3b"), 3, "With its unique integration of scientific principles and spiritual insights, the Academy provides access to new horizons in understanding oneself and the surrounding world. The thesis that everything is interconnected is not just a theory or esoteric belief but a practical principle of Existence!\r\nIn the Academy, you pave the path towards your synchronized unfolding of thought, information, and energy, receiving tools and knowledge to consciously create your gracious and grateful world.\r\nCogitality Academy is the culmination of years of effort, exploration, practice, and mistakes, through which you now gain the fastest and easiest access to this extraordinary realm of wisdom and Life, beyond the confines of time!", false, "Cogitality Academy" },
                    { new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"), "Bashar", new Guid("2a022e06-8c00-435f-93a9-9da816c1b483"), 2, "Bashar is a physical E.T, a friend from the future who has spoken for the past 37 years through channel Darryl Anka.  He has brought through a wave of new information that clearly explains in detail how the universe works, and how each person creates the reality they experience. Over the years, thousands of individuals have had the opportunity to apply these principles, and see that they really work to change their lives and create the reality that they desire.", false, "Darryl Anka" },
                    { new Guid("68508613-d974-4237-5182-08dba58c19e0"), "Lao Tzu", new Guid("7993bad4-df53-40dd-8921-15d4cdf5c252"), 5, "Laozi (/ˈlaʊdzə/, Chinese: 老子), also romanized as Lao Tzu and various other ways, was a semi-legendary ancient Chinese Taoist philosopher, credited with writing the Tao Te Ching. Laozi is a Chinese honorific, generally translated as \"the Old Master\". Although modern scholarship generally regards him as a fictional person, traditional accounts say he was born as Li Er in the state of Chu in the 6th century BC during China's Spring and Autumn Period, served as the royal archivist for the Zhou court at Wangcheng (modern Luoyang), met and impressed Confucius on one occasion, and composed the Tao Te Ching in a single session before retiring into the western wilderness. And more...", false, "Laotzu" },
                    { new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"), "Eckhart Tolle", new Guid("26db05ea-2b5e-44dd-bdef-4e74b9ecaa5f"), 5, "Eckhart Tolle is widely recognized as one of the most inspiring and visionary spiritual teachers in the world today. With his international bestsellers, The Power of Now and A New Earth—translated into 52 languages—he has introduced millions to the joy and freedom of living life in the present moment. The New York Times has described him as “the most popular spiritual author in the United States”, and in 2011, Watkins Review named him “the most spiritually influential person in the world”.", false, "Eckhart Tolle" }
                });

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "PhoneNumber", "UserID" },
                values: new object[,]
                {
                    { new Guid("4779b556-cbb5-45d2-a16c-d8a83501198a"), "+359883588888", new Guid("bcb4f072-ecca-43c9-ab26-c060c6f364e4") },
                    { new Guid("d99242d9-3db2-4675-87e3-da7743c6b526"), "+359888888888", new Guid("194974cd-73f0-4946-ba85-710d4061472d") }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "ApplicationUserId", "AuthorID", "CategoryID", "Description", "ImageID", "Price", "PublisherID", "ShortDescription", "Title" },
                values: new object[,]
                {
                    { new Guid("12221379-d5c7-4688-8ad8-efbffcaf8d06"), null, new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"), 2, "\"The Masters of Limitation: An ET's Observations of Earth\" offers not only a unique perspective of human society and our place in the universe, but also gifts us with life-changing information that can profoundly alter our view of reality.", new Guid("55dc2c91-c81b-40de-ac5b-f7474a7acfdc"), 30m, new Guid("d99242d9-3db2-4675-87e3-da7743c6b526"), "\"The Masters of Limitation: An ET's Observations of Earth\" offers not only a unique perspective of human society and our place in the universe, but also gifts us with life-changing information that can profoundly alter our view of reality.\r\n", "The Masters of Limitation" },
                    { new Guid("641ae624-efd0-4eb6-87af-05f2cc17bbb7"), null, new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"), 5, "It's no wonder that The Power of Now has sold over 2 million copies worldwide and has been translated into over 30 foreign languages. Much more than simple principles and platitudes, the book takes readers on an inspiring spiritual journey to find their true and deepest self and reach the ultimate in personal growth and spirituality: the discovery of truth and light.\r\r\n\r\r\nIn the first chapter, Tolle introduces readers to enlightenment and its natural enemy, the mind. He awakens readers to their role as a creator of pain and shows them how to have a pain-free identity by living fully in the present. The journey is thrilling, and along the way, the author shows how to connect to the indestructible essence of our Being, \"the eternal, ever-present One Life beyond the myriad forms of life that are subject to birth and death.\"\r\r\n\r\r\nFeaturing a new preface by the author, this paperback shows that only after regaining awareness of Being, liberated from Mind and intensely in the Now, is there Enlightenment.", new Guid("327b0419-5ff9-4694-a4f8-151cb0a46e6b"), 30m, new Guid("d99242d9-3db2-4675-87e3-da7743c6b526"), "This book shows that only after regaining awareness of Being, liberated from Mind and intensely in the Now, is there Enlightenment", "The Power Of Now" },
                    { new Guid("e0aa1a89-c180-4ac0-935d-8efab304b274"), null, new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"), 3, "All the facts, in the beginning, were puzzle pieces, scattered in vastness - fragmented, incongruous, unordered. They arrived haphazardly in moments when you weren't seeking them and not expecting them... Flashes, illuminating the darkness, which it is fitting to capture in your hands like fireflies - to gather them with patience, inspiration, and dedication. Then, embracing the scattered chaos of your own ignorance, with faith in the Nothingness, you arrange the light of your own Life.", new Guid("c7b99bd1-8188-4277-b937-81ab367b4034"), 30m, new Guid("d99242d9-3db2-4675-87e3-da7743c6b526"), "Bundle of the books You - The Source, You - The Manifestation, You - The Life", "Encyclopedia Cogitality" }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "AuthorID", "CategoryID", "CreatedOn", "Description", "EndDateTime", "ImageID", "IsOnline", "LocationName", "LocationUrl", "Price", "PublisherID", "StartDateTime", "Title" },
                values: new object[,]
                {
                    { new Guid("15f326bc-f47f-487f-8764-5fb8fd5c448a"), new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"), 2, new DateTime(2023, 9, 15, 14, 2, 54, 703, DateTimeKind.Utc).AddTicks(2964), "What if there was one state of being we could adopt that would help us establish better, stronger connections not only with our families and friends on earth, but also with our friends from the stars?\r\r\n\r\r\nIn The Three Behaviors of Connection, Bashar will share how action, timing, and communication are vital concepts for making inroads and connection with the hybrid children that will eventually be living among us. He will expand in detail on these three behaviors and how we might apply them to our lives on Earth as well as to our quest for contact with our extraterrestrial family.", new DateTime(2023, 8, 26, 15, 30, 0, 0, DateTimeKind.Unspecified), new Guid("69630e42-a4de-4116-a1a4-38c43faa0b53"), true, null, null, 35m, new Guid("d99242d9-3db2-4675-87e3-da7743c6b526"), new DateTime(2023, 8, 26, 14, 30, 0, 0, DateTimeKind.Unspecified), "The 3 Behaviors of Connection" },
                    { new Guid("3db097df-7c7c-4c4e-b546-d4555c4c1521"), new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"), 3, new DateTime(2023, 9, 15, 14, 2, 54, 703, DateTimeKind.Utc).AddTicks(3011), "The Cogitality seminars are back - they have already started in the country, and now they are happening at the \"Healing\" campus too! They are pre-planned and organized by the team of cogitalists.\r\r\n\r\r\nThe first seminar at the \"Healing\" campus, which will take place on September 2-3, 2023, is already fully booked. Thank you for the sincere desire to share this experience together!", new DateTime(2023, 9, 3, 18, 0, 0, 0, DateTimeKind.Unspecified), new Guid("ab7cfc34-55f4-4ed8-9687-c48a747e9fb4"), true, "Campus \"Healing\"", "https://www.google.com/maps/place/%D0%9A%D0%B0%D0%BC%D0%BF%D1%83%D1%81+%D0%98%D0%B7%D1%86%D0%B5%D0%BB%D0%B5%D0%BD%D0%B8%D0%B5/@42.2625195,25.2288508,17z/data=!3m1!4b1!4m6!3m5!1s0x40a82595106658b3:0x4dc3df5ed0a4ca00!8m2!3d42.2625156!4d25.2314257!16s%2Fg%2F11ry_fh0ry?entry=ttu", 144m, new Guid("d99242d9-3db2-4675-87e3-da7743c6b526"), new DateTime(2023, 9, 2, 9, 0, 0, 0, DateTimeKind.Unspecified), "Seminar - Campus \"Healing\"" },
                    { new Guid("45bb1c09-b50d-4d47-8fdb-fbfb53086922"), new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"), 5, new DateTime(2023, 9, 15, 14, 2, 54, 703, DateTimeKind.Utc).AddTicks(2993), "Join us for this unique opportunity to sit with Eckhart Tolle as he points you to spiritual awakening and the transformation of consciousness. With his hallmark warmth, humour and compassion, this evening will connect you with the peace and serenity that arises from living in the moment.\r\r\n\r\r\nEckhart’s profound, yet simple teachings have helped countless people from around the globe awaken to a vibrantly alive inner peace in their daily lives. Eckhart Tolle’s writings and life-changing public events have touched millions of lives, garnering fans to the likes of Oprah, the Dalai Lama and Deepak Chopra. He is the best-selling author of The Power of Now and A New Earth that are widely regarded as the most transformational books of our time.", new DateTime(2023, 9, 26, 22, 0, 0, 0, DateTimeKind.Unspecified), new Guid("13e26f61-5a34-44e0-b9d4-d8ab04b8f342"), false, "Stockholm", "https://www.google.com/maps/place/Stockholm,+Sweden/@59.3262131,17.8172495,11z/data=!3m1!4b1!4m6!3m5!1s0x465f763119640bcb:0xa80d27d3679d7766!8m2!3d59.3293235!4d18.0685808!16zL20vMDZteHM?entry=ttu", 199m, new Guid("d99242d9-3db2-4675-87e3-da7743c6b526"), new DateTime(2023, 9, 26, 18, 30, 0, 0, DateTimeKind.Unspecified), "An Evening with Eckhart Tolle in Stockholm" }
                });

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
                name: "IX_ApplicationUserAuthor_FollowersId",
                table: "ApplicationUserAuthor",
                column: "FollowersId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserEvent_ParticipantsId",
                table: "ApplicationUserEvent",
                column: "ParticipantsId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserSubscription_SubscriptionsId",
                table: "ApplicationUserSubscription",
                column: "SubscriptionsId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorPublisher_PublishersId",
                table: "AuthorPublisher",
                column: "PublishersId");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_AvatarImageID",
                table: "Authors",
                column: "AvatarImageID");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_CategoryID",
                table: "Authors",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPostImage_BlogID",
                table: "BlogPostImage",
                column: "BlogID");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_AuthorID",
                table: "Blogs",
                column: "AuthorID");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_CategoryID",
                table: "Blogs",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_PublisherID",
                table: "Blogs",
                column: "PublisherID");

            migrationBuilder.CreateIndex(
                name: "IX_Books_ApplicationUserId",
                table: "Books",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorID",
                table: "Books",
                column: "AuthorID");

            migrationBuilder.CreateIndex(
                name: "IX_Books_CategoryID",
                table: "Books",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Books_ImageID",
                table: "Books",
                column: "ImageID");

            migrationBuilder.CreateIndex(
                name: "IX_Books_PublisherID",
                table: "Books",
                column: "PublisherID");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_AuthorId",
                table: "Comments",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostID",
                table: "Comments",
                column: "PostID");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserID",
                table: "Comments",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_ApplicationUserId",
                table: "Courses",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_AuthorID",
                table: "Courses",
                column: "AuthorID");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CategoryID",
                table: "Courses",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_ImageID",
                table: "Courses",
                column: "ImageID");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_PublisherID",
                table: "Courses",
                column: "PublisherID");

            migrationBuilder.CreateIndex(
                name: "IX_Events_AuthorID",
                table: "Events",
                column: "AuthorID");

            migrationBuilder.CreateIndex(
                name: "IX_Events_CategoryID",
                table: "Events",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Events_ImageID",
                table: "Events",
                column: "ImageID");

            migrationBuilder.CreateIndex(
                name: "IX_Events_PublisherID",
                table: "Events",
                column: "PublisherID");

            migrationBuilder.CreateIndex(
                name: "IX_Publishers_UserID",
                table: "Publishers",
                column: "UserID");

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

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_UserID",
                table: "Ratings",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_AuthorID",
                table: "Subscriptions",
                column: "AuthorID");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_SubscriptionTypeID",
                table: "Subscriptions",
                column: "SubscriptionTypeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserAuthor");

            migrationBuilder.DropTable(
                name: "ApplicationUserEvent");

            migrationBuilder.DropTable(
                name: "ApplicationUserSubscription");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AuthorPublisher");

            migrationBuilder.DropTable(
                name: "BlogPostImage");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "Subscriptions");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Blogs");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "SubscriptionTypes");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Publishers");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
