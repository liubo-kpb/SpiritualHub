using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpiritualHub.Data.Migrations
{
    public partial class IsActiveCourseChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Courses",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("187e0540-5a90-419a-bf5b-f65ee213a0ca"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d58882d2-7c08-4393-9739-8d49444c1bab", "AQAAAAEAACcQAAAAEFh2Y/vqIKJ0LOYFJvo0bDFIp4qFI7BQlKvtLdAEkcOYjTXvwBwKalrmx5MjSbCmZw==", "9eb06ccf-b694-474e-8946-4dbe43fe931e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("194974cd-73f0-4946-ba85-710d4061472d"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2b2c9b38-fc84-4367-b625-cbbd25d16e92", "AQAAAAEAACcQAAAAEHt6SruL1bXO4xq6RauUkWWDz73ItGg8kNCtlQn+w93wYhylAnnFPhQTfofEQ/xkEA==", "cb008526-0008-4606-8f6f-88e3e68f2ddd" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("1fd95f69-4f9d-4671-b126-cefcf4b8a95e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7fb77624-8389-431d-8fa3-cf09c4155a38", "AQAAAAEAACcQAAAAEJFu2xkIm8+NOfqFpKe57TmYwXyN+1yydNc+eYAYWxJ29swzhyt/tTQrd70zJ4CfSA==", "e5ceb697-be4d-464d-b395-4ca5faace707" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("bcb4f072-ecca-43c9-ab26-c060c6f364e4"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "860430e0-5bf3-4d0e-b15f-e7c1ed6f652f", "AQAAAAEAACcQAAAAEGtHVSRGUmlH5W3MfK1aM91cSet8vIfWQqamUiBueC5mcHFGgmfnEOn8tYJml5SoxA==", "add4d2fc-f208-470c-b6d8-e5097bf4c9a4" });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"),
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("2ac53aa9-a9a1-4517-a2e6-e8a311f3b6c9"),
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("2c85de5f-00f3-4cc3-8596-84571d342d28"),
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("5fec9e9c-cdfb-4037-8bc9-7b6e98267aac"),
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("fb3472d1-2259-4600-aa60-7ff29745f475"),
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("15f326bc-f47f-487f-8764-5fb8fd5c448a"),
                column: "CreatedOn",
                value: new DateTime(2024, 6, 6, 11, 0, 43, 694, DateTimeKind.Utc).AddTicks(7381));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("3db097df-7c7c-4c4e-b546-d4555c4c1521"),
                column: "CreatedOn",
                value: new DateTime(2024, 6, 6, 11, 0, 43, 694, DateTimeKind.Utc).AddTicks(7428));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("45bb1c09-b50d-4d47-8fdb-fbfb53086922"),
                column: "CreatedOn",
                value: new DateTime(2024, 6, 6, 11, 0, 43, 694, DateTimeKind.Utc).AddTicks(7415));

            migrationBuilder.InsertData(
                table: "Subscriptions",
                columns: new[] { "Id", "AuthorID", "Price", "SubscriptionTypeID" },
                values: new object[,]
                {
                    { new Guid("0615be2b-ac97-46db-a5b2-213bbe2b0efe"), new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"), 25m, 1 },
                    { new Guid("1e85e1be-dada-4f70-bdda-5204dec850f0"), new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"), 150m, 3 },
                    { new Guid("5cf4a7d0-5d4b-4f43-9fe5-991bcf9d78c2"), new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"), 55m, 2 },
                    { new Guid("6c8e4605-ec1c-4b24-b299-fe7c608de165"), new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"), 70m, 2 },
                    { new Guid("82dd59e6-0bfb-4744-9ffe-a199c026c246"), new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"), 65m, 2 },
                    { new Guid("c7122dc7-5766-493f-99b4-3d0b039f9ea0"), new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"), 21m, 1 },
                    { new Guid("d581b303-60f4-412d-8706-4fb9ead3d9a0"), new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"), 25m, 1 },
                    { new Guid("df96630a-e9ee-41ab-b7a9-7999be75c84c"), new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"), 200m, 3 },
                    { new Guid("edc07a23-e8c6-4dc8-8362-1139b691b816"), new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"), 189m, 3 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("0615be2b-ac97-46db-a5b2-213bbe2b0efe"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("1e85e1be-dada-4f70-bdda-5204dec850f0"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("5cf4a7d0-5d4b-4f43-9fe5-991bcf9d78c2"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("6c8e4605-ec1c-4b24-b299-fe7c608de165"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("82dd59e6-0bfb-4744-9ffe-a199c026c246"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("c7122dc7-5766-493f-99b4-3d0b039f9ea0"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("d581b303-60f4-412d-8706-4fb9ead3d9a0"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("df96630a-e9ee-41ab-b7a9-7999be75c84c"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("edc07a23-e8c6-4dc8-8362-1139b691b816"));

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Courses",
                type: "bit",
                nullable: true,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit");

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
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"),
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("2ac53aa9-a9a1-4517-a2e6-e8a311f3b6c9"),
                column: "IsActive",
                value: null);

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("2c85de5f-00f3-4cc3-8596-84571d342d28"),
                column: "IsActive",
                value: null);

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("5fec9e9c-cdfb-4037-8bc9-7b6e98267aac"),
                column: "IsActive",
                value: null);

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("fb3472d1-2259-4600-aa60-7ff29745f475"),
                column: "IsActive",
                value: null);

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
        }
    }
}
