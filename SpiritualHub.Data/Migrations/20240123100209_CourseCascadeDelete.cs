using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpiritualHub.Data.Migrations
{
    public partial class CourseCascadeDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Module_Courses_CourseID",
                table: "Module");

            migrationBuilder.DropForeignKey(
                name: "FK_ModuleRating_Module_ModulesId",
                table: "ModuleRating");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Module",
                table: "Module");

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("55bf87c0-20df-46b7-8892-b6f65aff7ec9"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("5ee4ee40-48aa-4415-a029-06619595bb77"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("91b6fa21-aa86-41b9-892d-542d2d9a4e80"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("a0da970d-66f4-49f8-b3c3-813dd131c4da"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("a63a2e95-8112-44e8-9ba6-bbb1b79383bb"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("c463cf9f-0a53-4edc-9feb-3657aa2b4d25"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("ce290603-3519-4f96-85f5-b629d562cc7c"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("d2bebfa4-f78e-44bf-8e58-289c27b72828"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("d8f3c7cb-f65e-4a86-9bcc-156f3d8a3038"));

            migrationBuilder.RenameTable(
                name: "Module",
                newName: "Modules");

            migrationBuilder.RenameIndex(
                name: "IX_Module_CourseID",
                table: "Modules",
                newName: "IX_Modules_CourseID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Modules",
                table: "Modules",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("187e0540-5a90-419a-bf5b-f65ee213a0ca"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "524c6926-f0b3-4fba-8d8e-e323f429a832", "AQAAAAEAACcQAAAAEP/mveH2hI6Fcv6igLb7EmyKpdsRwIv80XAwNPnlqP/vqmjLIhM1F1Qb4NM2KhVJcA==", "f127b72a-da95-4d5e-acb9-36c35f4e586d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("194974cd-73f0-4946-ba85-710d4061472d"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "19773ba4-4411-422f-9e84-216397c0cf3f", "AQAAAAEAACcQAAAAEB2liX7noG99akIGbgOEKbgghqIK2I6UynK3wPLesTRuLmjdxez1NHgIE/gslBXpXA==", "6d0a2804-d0ef-4ecb-bb9e-039b3c0c5e8b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("1fd95f69-4f9d-4671-b126-cefcf4b8a95e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b1ab5bea-2038-4c71-9fbe-83b417f264db", "AQAAAAEAACcQAAAAEN6WanQb0U4FTV2BIn9afcsE/ew5OnvVpWgNKRigkpNtp8akLB8ElQ2kzhhxo09xWg==", "bd19f4a5-211d-42df-aab8-5598b8b054e0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("bcb4f072-ecca-43c9-ab26-c060c6f364e4"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f58f929c-5d16-4daf-a612-76f423fc5423", "AQAAAAEAACcQAAAAEG7yukZA0TQic7mHKD4X8o4r96e+r9T+4mx1iikuE3BU8ZQsjZKC3NRZFv/ARERKQA==", "58a75689-e87d-4863-8bbb-8cd445efed67" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("15f326bc-f47f-487f-8764-5fb8fd5c448a"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 23, 10, 2, 7, 916, DateTimeKind.Utc).AddTicks(3893));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("3db097df-7c7c-4c4e-b546-d4555c4c1521"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 23, 10, 2, 7, 916, DateTimeKind.Utc).AddTicks(3921));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("45bb1c09-b50d-4d47-8fdb-fbfb53086922"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 23, 10, 2, 7, 916, DateTimeKind.Utc).AddTicks(3910));

            migrationBuilder.InsertData(
                table: "Subscriptions",
                columns: new[] { "Id", "AuthorID", "Price", "SubscriptionTypeID" },
                values: new object[,]
                {
                    { new Guid("3eb29595-943c-48ed-81d1-807718b2c5e9"), new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"), 65m, 2 },
                    { new Guid("5140a397-578b-43db-9cc5-f633851ca343"), new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"), 189m, 3 },
                    { new Guid("77f2b024-abb5-440f-bc86-99d2a947377e"), new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"), 70m, 2 },
                    { new Guid("7d825d9e-7dbb-43e4-86ad-0a4a07b85459"), new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"), 25m, 1 },
                    { new Guid("8ecc9a24-2464-4ad9-882f-c0baa6e0baf1"), new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"), 150m, 3 },
                    { new Guid("c6d9cafc-534b-47bf-a311-4532d23de97f"), new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"), 25m, 1 },
                    { new Guid("d822e50e-be71-49b2-8d31-febebab3af73"), new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"), 21m, 1 },
                    { new Guid("e270bf12-dabb-4870-9ff5-a83334944ecd"), new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"), 200m, 3 },
                    { new Guid("e47fcfab-64c0-499b-8fc9-3723ca4c6bd0"), new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"), 55m, 2 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ModuleRating_Modules_ModulesId",
                table: "ModuleRating",
                column: "ModulesId",
                principalTable: "Modules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Modules_Courses_CourseID",
                table: "Modules",
                column: "CourseID",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ModuleRating_Modules_ModulesId",
                table: "ModuleRating");

            migrationBuilder.DropForeignKey(
                name: "FK_Modules_Courses_CourseID",
                table: "Modules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Modules",
                table: "Modules");

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("3eb29595-943c-48ed-81d1-807718b2c5e9"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("5140a397-578b-43db-9cc5-f633851ca343"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("77f2b024-abb5-440f-bc86-99d2a947377e"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("7d825d9e-7dbb-43e4-86ad-0a4a07b85459"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("8ecc9a24-2464-4ad9-882f-c0baa6e0baf1"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("c6d9cafc-534b-47bf-a311-4532d23de97f"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("d822e50e-be71-49b2-8d31-febebab3af73"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("e270bf12-dabb-4870-9ff5-a83334944ecd"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("e47fcfab-64c0-499b-8fc9-3723ca4c6bd0"));

            migrationBuilder.RenameTable(
                name: "Modules",
                newName: "Module");

            migrationBuilder.RenameIndex(
                name: "IX_Modules_CourseID",
                table: "Module",
                newName: "IX_Module_CourseID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Module",
                table: "Module",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("187e0540-5a90-419a-bf5b-f65ee213a0ca"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "60ee7fb8-f89c-402b-9c56-9780e809e1e0", "AQAAAAEAACcQAAAAEMXYv5q45bfo2q0sqMXEDqyymgMqqjrEgP3TnH05xP1smt+BTKeDUo0yl8NUkJmEgQ==", "b57c5705-5660-4d1f-9497-06e5a15d2251" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("194974cd-73f0-4946-ba85-710d4061472d"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "54efc2f2-9480-4c4e-a78c-f24b3cba6625", "AQAAAAEAACcQAAAAEFsRUVnuwwu2LAnUZ5+lCZ113h4BdKuH2gvnXwbZJxZzUVDkJYmcfJCzQAToUmhyEA==", "31e36d61-3b7c-424c-baff-985021f83924" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("1fd95f69-4f9d-4671-b126-cefcf4b8a95e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d9f3a0b1-f18b-42e0-aea8-4f4042019d83", "AQAAAAEAACcQAAAAEKRUw3qfzMeIZqINyA5Sypq7YZMHq3oJM1ha9OvAocCaA+ITIc8hcsOXujtCUmV/0w==", "0d3e4906-a7f4-4db8-b43b-75814d2f4e02" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("bcb4f072-ecca-43c9-ab26-c060c6f364e4"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bbbfd3be-9b0c-4155-a16e-7e5a195ececc", "AQAAAAEAACcQAAAAEOzyi+QskayGKTHeu45N7CrLx7ddf3t/Xnv+9x9kLoWw60+xhz0WFEbpup/5xXA0wg==", "dc59b7d6-4bb5-4745-881d-16de3d46d625" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("15f326bc-f47f-487f-8764-5fb8fd5c448a"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 18, 17, 19, 44, 617, DateTimeKind.Utc).AddTicks(9212));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("3db097df-7c7c-4c4e-b546-d4555c4c1521"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 18, 17, 19, 44, 617, DateTimeKind.Utc).AddTicks(9248));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("45bb1c09-b50d-4d47-8fdb-fbfb53086922"),
                column: "CreatedOn",
                value: new DateTime(2024, 1, 18, 17, 19, 44, 617, DateTimeKind.Utc).AddTicks(9234));

            migrationBuilder.InsertData(
                table: "Subscriptions",
                columns: new[] { "Id", "AuthorID", "Price", "SubscriptionTypeID" },
                values: new object[,]
                {
                    { new Guid("55bf87c0-20df-46b7-8892-b6f65aff7ec9"), new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"), 70m, 2 },
                    { new Guid("5ee4ee40-48aa-4415-a029-06619595bb77"), new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"), 21m, 1 },
                    { new Guid("91b6fa21-aa86-41b9-892d-542d2d9a4e80"), new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"), 25m, 1 },
                    { new Guid("a0da970d-66f4-49f8-b3c3-813dd131c4da"), new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"), 55m, 2 },
                    { new Guid("a63a2e95-8112-44e8-9ba6-bbb1b79383bb"), new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"), 150m, 3 },
                    { new Guid("c463cf9f-0a53-4edc-9feb-3657aa2b4d25"), new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"), 65m, 2 },
                    { new Guid("ce290603-3519-4f96-85f5-b629d562cc7c"), new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"), 25m, 1 },
                    { new Guid("d2bebfa4-f78e-44bf-8e58-289c27b72828"), new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"), 189m, 3 },
                    { new Guid("d8f3c7cb-f65e-4a86-9bcc-156f3d8a3038"), new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"), 200m, 3 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Module_Courses_CourseID",
                table: "Module",
                column: "CourseID",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ModuleRating_Module_ModulesId",
                table: "ModuleRating",
                column: "ModulesId",
                principalTable: "Module",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
