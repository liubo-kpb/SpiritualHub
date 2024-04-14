using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpiritualHub.Data.Migrations
{
    public partial class MakeCategoryNameUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("187e0540-5a90-419a-bf5b-f65ee213a0ca"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "82bb431d-6d35-4dc3-8f16-f95438d3223c", "AQAAAAEAACcQAAAAELLtEeVsCoGu1F3QDrfDG+kV4usXIAQ3pyA4ff2KTq74J2aVLR+aZ5LJTsHg+55NPw==", "79c83312-668a-4cdc-a6ce-ccc480406f9a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("194974cd-73f0-4946-ba85-710d4061472d"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9d629b1c-75ed-4bf0-af63-52e1030615ab", "AQAAAAEAACcQAAAAELrWdCP9DQqp3xZGOx2UsTV1cSBMmrnRPw5zeYAobPJb5KL94iLLT6PMX4oEFiqQOQ==", "37ed351c-baa4-4a1f-8f00-a6f02cff489f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("1fd95f69-4f9d-4671-b126-cefcf4b8a95e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "574a7936-1000-4b4b-a291-19e3f5335430", "AQAAAAEAACcQAAAAEKvisZLHsxpc/+mJSHUtXU7fVJxZauWV1x73Ge1CeiPDKRVjpNHc2cOVmVFnwA/nHQ==", "3973b927-bc17-4d09-a8a8-fe994ea071ff" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("bcb4f072-ecca-43c9-ab26-c060c6f364e4"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f277fb73-6932-4e3e-9f2f-4599a30aa046", "AQAAAAEAACcQAAAAEJoRt8Dt5CXX7WDBskL3fzB2yjZ3sDjem6AoOwAlGbN+lqh+02VEk44PpoxzG6vJ9A==", "755b1c26-fb5e-45f3-a7f4-01b1cf869892" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("15f326bc-f47f-487f-8764-5fb8fd5c448a"),
                column: "CreatedOn",
                value: new DateTime(2024, 3, 29, 8, 50, 35, 927, DateTimeKind.Utc).AddTicks(4662));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("3db097df-7c7c-4c4e-b546-d4555c4c1521"),
                column: "CreatedOn",
                value: new DateTime(2024, 3, 29, 8, 50, 35, 927, DateTimeKind.Utc).AddTicks(4689));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("45bb1c09-b50d-4d47-8fdb-fbfb53086922"),
                column: "CreatedOn",
                value: new DateTime(2024, 3, 29, 8, 50, 35, 927, DateTimeKind.Utc).AddTicks(4680));

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("291f7df8-4953-4e97-bbc8-26a594d222c2"),
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("39209dc8-df5b-4a15-8d6b-c1917e55f429"),
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("3c9b6e99-8108-4c3a-b027-6109669220f1"),
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("5ccdd241-9cc5-4b8a-9373-e8416e0b1890"),
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("5cd93182-8506-4bdb-9078-1ec1753129e8"),
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("612c4206-142a-4463-8fd1-463b9823b69c"),
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("73a33310-756f-468d-8dae-ed96769dac81"),
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("78d418cc-6713-4f12-8dd7-ede28da7bc19"),
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("8fe13950-5be0-451d-8539-f83d6bf37d09"),
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("cff6f4e7-ea53-438c-ae1d-12ed9ece7838"),
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("ead18201-dc40-4b3d-824f-3f8df867fe69"),
                column: "IsActive",
                value: true);

            migrationBuilder.InsertData(
                table: "Subscriptions",
                columns: new[] { "Id", "AuthorID", "Price", "SubscriptionTypeID" },
                values: new object[,]
                {
                    { new Guid("422473cf-dd87-4b52-ac36-8d4fd513fcba"), new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"), 55m, 2 },
                    { new Guid("5b8051b9-58e2-4344-b50e-7243dd89ecf0"), new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"), 25m, 1 },
                    { new Guid("8a97c09e-6c42-4cf9-8395-7915fa8fde95"), new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"), 25m, 1 },
                    { new Guid("bbd5a400-86cb-4143-8500-a2ebf93dc03a"), new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"), 65m, 2 },
                    { new Guid("c079de4a-fb5c-488b-9a27-a097c7a65727"), new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"), 70m, 2 },
                    { new Guid("c191aa8b-2d7c-46e7-8101-7ea4525a7451"), new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"), 21m, 1 },
                    { new Guid("c1eff871-bcde-4138-ab35-71804fd0c562"), new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"), 200m, 3 },
                    { new Guid("d590817a-9df8-472a-9446-8b034e93aecc"), new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"), 150m, 3 },
                    { new Guid("ef2469ec-958c-4343-adb6-9b543942204c"), new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"), 189m, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Categories_Name",
                table: "Categories");

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("422473cf-dd87-4b52-ac36-8d4fd513fcba"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("5b8051b9-58e2-4344-b50e-7243dd89ecf0"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("8a97c09e-6c42-4cf9-8395-7915fa8fde95"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("bbd5a400-86cb-4143-8500-a2ebf93dc03a"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("c079de4a-fb5c-488b-9a27-a097c7a65727"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("c191aa8b-2d7c-46e7-8101-7ea4525a7451"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("c1eff871-bcde-4138-ab35-71804fd0c562"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("d590817a-9df8-472a-9446-8b034e93aecc"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("ef2469ec-958c-4343-adb6-9b543942204c"));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

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

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("291f7df8-4953-4e97-bbc8-26a594d222c2"),
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("39209dc8-df5b-4a15-8d6b-c1917e55f429"),
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("3c9b6e99-8108-4c3a-b027-6109669220f1"),
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("5ccdd241-9cc5-4b8a-9373-e8416e0b1890"),
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("5cd93182-8506-4bdb-9078-1ec1753129e8"),
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("612c4206-142a-4463-8fd1-463b9823b69c"),
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("73a33310-756f-468d-8dae-ed96769dac81"),
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("78d418cc-6713-4f12-8dd7-ede28da7bc19"),
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("8fe13950-5be0-451d-8539-f83d6bf37d09"),
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("cff6f4e7-ea53-438c-ae1d-12ed9ece7838"),
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("ead18201-dc40-4b3d-824f-3f8df867fe69"),
                column: "IsActive",
                value: false);

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
        }
    }
}
