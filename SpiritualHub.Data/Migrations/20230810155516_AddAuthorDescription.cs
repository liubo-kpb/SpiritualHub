using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpiritualHub.Data.Migrations
{
    public partial class AddAuthorDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Authors",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("194974cd-73f0-4946-ba85-710d4061472d"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ffbb345c-8d73-43a3-89e3-0da4b0d88e7c", "AQAAAAEAACcQAAAAEBcLz5ushKFyuSXn+g1rTrMtEJrT7vyIYkUWrbCs93a53tji8om+YwsUyGxBRugeYw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("1fd95f69-4f9d-4671-b126-cefcf4b8a95e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "70bb4f67-01b4-41d8-9581-c988347c4e72", "AQAAAAEAACcQAAAAEHLGfwYdIOoc4ezRi6UcZcLffxAdRFWKWirj9bbPgmJSUx5btcH8HwhVHoquaEJMQQ==" });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"),
                column: "Description",
                value: "With its unique integration of scientific principles and spiritual insights, the Academy provides access to new horizons in understanding oneself and the surrounding world. The thesis that everything is interconnected is not just a theory or esoteric belief but a practical principle of Existence!\r\n\r\nIn the Academy, you pave the path towards your synchronized unfolding of thought, information, and energy, receiving tools and knowledge to consciously create your gracious and grateful world.\r\n\r\nCogitality Academy is the culmination of years of effort, exploration, practice, and mistakes, through which you now gain the fastest and easiest access to this extraordinary realm of wisdom and Life, beyond the confines of time!");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"),
                column: "Description",
                value: "Bashar is a physical E.T, a friend from the future who has spoken for the past 37 years through channel Darryl Anka.  He has brought through a wave of new information that clearly explains in detail how the universe works, and how each person creates the reality they experience. Over the years, thousands of individuals have had the opportunity to apply these principles, and see that they really work to change their lives and create the reality that they desire.");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"),
                column: "Description",
                value: "Eckhart Tolle is widely recognized as one of the most inspiring and visionary spiritual teachers in the world today. With his international bestsellers, The Power of Now and A New Earth—translated into 52 languages—he has introduced millions to the joy and freedom of living life in the present moment. The New York Times has described him as “the most popular spiritual author in the United States”, and in 2011, Watkins Review named him “the most spiritually influential person in the world”.");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("15f326bc-f47f-487f-8764-5fb8fd5c448a"),
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 15, 55, 15, 960, DateTimeKind.Utc).AddTicks(7744));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("3db097df-7c7c-4c4e-b546-d4555c4c1521"),
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 15, 55, 15, 960, DateTimeKind.Utc).AddTicks(7810));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("45bb1c09-b50d-4d47-8fdb-fbfb53086922"),
                column: "CreatedOn",
                value: new DateTime(2023, 8, 10, 15, 55, 15, 960, DateTimeKind.Utc).AddTicks(7802));

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("26db05ea-2b5e-44dd-bdef-4e74b9ecaa5f"),
                column: "URL",
                value: "https://eckharttolle.com/wp-content/uploads/2021/03/PHOTO-Eckhart_EDITEDIMG_5197-scaled.jpg");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("2a022e06-8c00-435f-93a9-9da816c1b483"),
                column: "URL",
                value: "https://www.bashar.org/wp-content/uploads/2017/02/Bashar_purple2.jpg");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("327b0419-5ff9-4694-a4f8-151cb0a46e6b"),
                column: "URL",
                value: "https://m.media-amazon.com/images/I/714FbKtXS+L._AC_UF1000,1000_QL80_.jpg");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("55dc2c91-c81b-40de-ac5b-f7474a7acfdc"),
                column: "URL",
                value: "https://images-na.ssl-images-amazon.com/images/I/61oU5+vqzwL._AC_UL750_SR750,750_.jpg");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("868aaede-674a-44a6-ae21-ec62bd2bec3b"),
                column: "URL",
                value: "https://i.ytimg.com/vi/XvV8rllMh6c/maxresdefault.jpg");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Authors");

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

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("26db05ea-2b5e-44dd-bdef-4e74b9ecaa5f"),
                column: "URL",
                value: "https://1drv.ms/i/s!AtAU7bartlmmhpxwLMTJsLUEHxnZSQ?e=sln5JF");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("2a022e06-8c00-435f-93a9-9da816c1b483"),
                column: "URL",
                value: "https://1drv.ms/i/s!AtAU7bartlmmgYQEO5c530QekMydnA?e=65X6RK");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("327b0419-5ff9-4694-a4f8-151cb0a46e6b"),
                column: "URL",
                value: "https://1drv.ms/i/s!AtAU7bartlmmhp15puF4kOZMXXn-9w?e=fZECnk");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("55dc2c91-c81b-40de-ac5b-f7474a7acfdc"),
                column: "URL",
                value: "https://1drv.ms/i/s!AtAU7bartlmmhp17o6bOmIyxERpgkQ?e=bWhmAO");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("868aaede-674a-44a6-ae21-ec62bd2bec3b"),
                column: "URL",
                value: "https://1drv.ms/i/s!AtAU7bartlmmgYRw6O57eiKsf9iNBQ?e=OsdWqo");
        }
    }
}
