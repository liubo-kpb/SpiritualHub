using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpiritualHub.Data.Migrations
{
    public partial class ChangeModuleDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("3feca357-4709-4daf-8d5a-3e407a009913"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("41b84daa-b16c-4a54-b3c6-b1635daf6792"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("55b25fea-96b4-415b-a0ba-941bfff19039"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("6d39c9b2-5ed8-4788-989d-278bd9a63cf2"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("c2781d0b-759b-4309-842a-866004baaaf8"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("ccd67ab4-5702-44b0-8eec-23f21a9e103e"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("ef64b371-97b1-4a59-991e-bcb09191c77f"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("efd44e86-b36f-4d70-aa1a-62d1ad2a1197"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("fcbf9dd7-2843-4c35-9964-72a32a910baf"));

            migrationBuilder.DropColumn(
                name: "ShortDescription",
                table: "Module");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Module",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("187e0540-5a90-419a-bf5b-f65ee213a0ca"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2b4f59ae-d7de-4941-9be9-f3be6a673c31", "AQAAAAEAACcQAAAAEPhvDYhT31iN4EtuAulbhylTN0z9QO0kDZLgrTQDrGWIZqhE9PZ1gUfaBVptP/HYLA==", "4dbd464f-f021-4224-b406-739fc7b2c507" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("194974cd-73f0-4946-ba85-710d4061472d"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f0965b13-9c60-4ddb-8b54-a42a47b97fc7", "AQAAAAEAACcQAAAAENv0/Jhq5Q+cwBvghhX/f+SaimVQp2w5xxeJkdFEE5fXFOcwD1iDeB9FjaTt8FtRUg==", "171a4210-b73e-42b4-8836-09dc9193ceae" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("1fd95f69-4f9d-4671-b126-cefcf4b8a95e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "55bb8577-63e1-459f-9ff6-45919778ae09", "AQAAAAEAACcQAAAAEEa29sISGNc2QxgZWKkJm2NOan96AySC9YGCL0GdxaPqHKf/jWNfYDDRhlkz20kWcg==", "3dc5ed17-6055-4525-9c61-7c7a107e51a8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("bcb4f072-ecca-43c9-ab26-c060c6f364e4"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9bd140e9-9845-4b53-8ca9-38a1039b2611", "AQAAAAEAACcQAAAAEHwzTBge60ylAJArBOiZwG8wihPF3+hlMCGU19rg1gTrXorHOhSrZxcUD2H2GfD2Iw==", "7e5deb11-9f17-440d-b834-4192c810b1f4" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("15f326bc-f47f-487f-8764-5fb8fd5c448a"),
                column: "CreatedOn",
                value: new DateTime(2023, 10, 2, 13, 15, 57, 626, DateTimeKind.Utc).AddTicks(6567));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("3db097df-7c7c-4c4e-b546-d4555c4c1521"),
                column: "CreatedOn",
                value: new DateTime(2023, 10, 2, 13, 15, 57, 626, DateTimeKind.Utc).AddTicks(6700));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("45bb1c09-b50d-4d47-8fdb-fbfb53086922"),
                column: "CreatedOn",
                value: new DateTime(2023, 10, 2, 13, 15, 57, 626, DateTimeKind.Utc).AddTicks(6672));

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "Id",
                keyValue: new Guid("291f7df8-4953-4e97-bbc8-26a594d222c2"),
                column: "Description",
                value: "The new reality – the part contains the whole, and the whole is contained in the part.");

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "Id",
                keyValue: new Guid("39209dc8-df5b-4a15-8d6b-c1917e55f429"),
                column: "Description",
                value: "A force within a human being thought to give the body life, energy, and power.");

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "Id",
                keyValue: new Guid("3c9b6e99-8108-4c3a-b027-6109669220f1"),
                column: "Description",
                value: "The practice of professedly entering a meditative or trancelike state in order to convey messages from Source.");

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "Id",
                keyValue: new Guid("5ccdd241-9cc5-4b8a-9373-e8416e0b1890"),
                column: "Description",
                value: "It is an unprecedented time, and it is extremely important to remain as conscious as possible. The ego—on both the individual and the collective level—loves “the drama” that times like this can create.");

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "Id",
                keyValue: new Guid("5cd93182-8506-4bdb-9078-1ec1753129e8"),
                column: "Description",
                value: "Expanding the information, concepts, and unfolding to the potential.");

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "Id",
                keyValue: new Guid("612c4206-142a-4463-8fd1-463b9823b69c"),
                column: "Description",
                value: "The Power of Now shows you that every minute you spend worrying about the future or regretting the past is a minute lost.");

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "Id",
                keyValue: new Guid("73a33310-756f-468d-8dae-ed96769dac81"),
                column: "Description",
                value: "The basic concepts, resources, and structure of the Academy; stepping into the world of possibilities!");

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "Id",
                keyValue: new Guid("78d418cc-6713-4f12-8dd7-ede28da7bc19"),
                column: "Description",
                value: "But don’t look for it as if you were looking for something. You cannot pin it down and say, “Now I have it,” or grasp it mentally and define it in some way.");

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "Id",
                keyValue: new Guid("8fe13950-5be0-451d-8539-f83d6bf37d09"),
                column: "Description",
                value: "You have a core vibrational frequency.\r\nIt's a beacon. It's like a lighthouse. It shines. It radiates purely that signature frequency of your unique being. It never stops radiating that light, that frequency, that energy - never stops.");

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "Id",
                keyValue: new Guid("cff6f4e7-ea53-438c-ae1d-12ed9ece7838"),
                column: "Description",
                value: "Matter is anything that takes up space and can be weighed. In other words, matter has volume and mass. There are many different substances, or types of matter, in the universe.");

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "Id",
                keyValue: new Guid("ead18201-dc40-4b3d-824f-3f8df867fe69"),
                column: "Description",
                value: "All kinds of things will suddenly start to happen consistently when you apply this formula in your life. It's just like everything starts to fall into place. Everything shows you that's it's interconnected, that we're not really isolated");

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "Id",
                keyValue: new Guid("f6fcbe2f-8a00-4126-809b-15c7fe189e6f"),
                column: "Description",
                value: "Scientists define energy as the ability to do work. Modern civilization is possible because people have learned how to change energy from one form to another");

            migrationBuilder.InsertData(
                table: "Subscriptions",
                columns: new[] { "Id", "AuthorID", "Price", "SubscriptionTypeID" },
                values: new object[,]
                {
                    { new Guid("16c4da09-1f43-4836-ae8d-889659c64c30"), new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"), 189m, 3 },
                    { new Guid("3b339be3-d41d-4f5b-bdaf-3aa8d057db55"), new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"), 55m, 2 },
                    { new Guid("58989c4d-92cf-49fa-8d6e-05a226bc3e92"), new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"), 25m, 1 },
                    { new Guid("7e5cdd70-9c89-4886-9522-36e35ace299e"), new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"), 200m, 3 },
                    { new Guid("9243f93a-2321-4d23-ae59-3dd34ed5ef14"), new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"), 70m, 2 },
                    { new Guid("b3bf8b54-3e03-401e-8f6f-08f01662f8d7"), new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"), 21m, 1 },
                    { new Guid("c2d88a7f-2fc0-4263-b8c1-cd06072177d1"), new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"), 25m, 1 },
                    { new Guid("d2ffd82d-333c-4cc6-bf0c-9d9a8b604f96"), new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"), 150m, 3 },
                    { new Guid("f41bdc75-2f68-43da-9934-c6e022919d3d"), new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"), 65m, 2 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("16c4da09-1f43-4836-ae8d-889659c64c30"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("3b339be3-d41d-4f5b-bdaf-3aa8d057db55"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("58989c4d-92cf-49fa-8d6e-05a226bc3e92"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("7e5cdd70-9c89-4886-9522-36e35ace299e"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("9243f93a-2321-4d23-ae59-3dd34ed5ef14"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("b3bf8b54-3e03-401e-8f6f-08f01662f8d7"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("c2d88a7f-2fc0-4263-b8c1-cd06072177d1"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("d2ffd82d-333c-4cc6-bf0c-9d9a8b604f96"));

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: new Guid("f41bdc75-2f68-43da-9934-c6e022919d3d"));

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Module");

            migrationBuilder.AddColumn<string>(
                name: "ShortDescription",
                table: "Module",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("187e0540-5a90-419a-bf5b-f65ee213a0ca"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f557f02f-9a87-42e1-95fc-4cc23abffa27", "AQAAAAEAACcQAAAAENL3I1YHLh6ZqqTkpuX63/45o8DDJESXvH7t21KhIxnxEZTLI8kd0eBeN6qfkY2ikA==", "2dc65754-cf1e-4207-9e10-a0429ac87d63" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("194974cd-73f0-4946-ba85-710d4061472d"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "624bef28-6684-49c9-8fa7-1963c895055a", "AQAAAAEAACcQAAAAEMKSmSJMAM9OISnDuv5jl9KIMsX+xTRoW5BBufmh8RAfu3zq2gV4hU+4bS5DG0JwaQ==", "6088e571-636d-4443-9f7c-a366bb41dd22" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("1fd95f69-4f9d-4671-b126-cefcf4b8a95e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ece57240-b6c9-4b15-b3a3-6f7735180ee9", "AQAAAAEAACcQAAAAENVwuLa4Ck5mxuQwz9goziQq5PAdJp8rdfQC4h1d26ggG2nV5WZDgTVeLs0zvrxr6Q==", "f6e89fb3-87e3-4a6d-a8ea-be7caca0ae13" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("bcb4f072-ecca-43c9-ab26-c060c6f364e4"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d8ee182d-dc23-4f29-b8e5-255499b7aea8", "AQAAAAEAACcQAAAAELvlew2O+BFAl1YvgtHi+GTz7hLyMN++c45o/SXemxJvGaEU7h0SK2C+xGQIjH7PYw==", "91252c59-afea-4065-bfc3-a5d9ec2c5d58" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("15f326bc-f47f-487f-8764-5fb8fd5c448a"),
                column: "CreatedOn",
                value: new DateTime(2023, 9, 27, 11, 55, 3, 813, DateTimeKind.Utc).AddTicks(5540));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("3db097df-7c7c-4c4e-b546-d4555c4c1521"),
                column: "CreatedOn",
                value: new DateTime(2023, 9, 27, 11, 55, 3, 813, DateTimeKind.Utc).AddTicks(5647));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("45bb1c09-b50d-4d47-8fdb-fbfb53086922"),
                column: "CreatedOn",
                value: new DateTime(2023, 9, 27, 11, 55, 3, 813, DateTimeKind.Utc).AddTicks(5624));

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "Id",
                keyValue: new Guid("291f7df8-4953-4e97-bbc8-26a594d222c2"),
                column: "ShortDescription",
                value: "The new reality – the part contains the whole, and the whole is contained in the part.");

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "Id",
                keyValue: new Guid("39209dc8-df5b-4a15-8d6b-c1917e55f429"),
                column: "ShortDescription",
                value: "A force within a human being thought to give the body life, energy, and power.");

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "Id",
                keyValue: new Guid("3c9b6e99-8108-4c3a-b027-6109669220f1"),
                column: "ShortDescription",
                value: "The practice of professedly entering a meditative or trancelike state in order to convey messages from Source.");

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "Id",
                keyValue: new Guid("5ccdd241-9cc5-4b8a-9373-e8416e0b1890"),
                column: "ShortDescription",
                value: "It is an unprecedented time, and it is extremely important to remain as conscious as possible. The ego—on both the individual and the collective level—loves “the drama” that times like this can create.");

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "Id",
                keyValue: new Guid("5cd93182-8506-4bdb-9078-1ec1753129e8"),
                column: "ShortDescription",
                value: "Expanding the information, concepts, and unfolding to the potential.");

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "Id",
                keyValue: new Guid("612c4206-142a-4463-8fd1-463b9823b69c"),
                column: "ShortDescription",
                value: "The Power of Now shows you that every minute you spend worrying about the future or regretting the past is a minute lost.");

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "Id",
                keyValue: new Guid("73a33310-756f-468d-8dae-ed96769dac81"),
                column: "ShortDescription",
                value: "The basic concepts, resources, and structure of the Academy; stepping into the world of possibilities!");

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "Id",
                keyValue: new Guid("78d418cc-6713-4f12-8dd7-ede28da7bc19"),
                column: "ShortDescription",
                value: "But don’t look for it as if you were looking for something. You cannot pin it down and say, “Now I have it,” or grasp it mentally and define it in some way.");

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "Id",
                keyValue: new Guid("8fe13950-5be0-451d-8539-f83d6bf37d09"),
                column: "ShortDescription",
                value: "You have a core vibrational frequency.\r\nIt's a beacon. It's like a lighthouse. It shines. It radiates purely that signature frequency of your unique being. It never stops radiating that light, that frequency, that energy - never stops.");

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "Id",
                keyValue: new Guid("cff6f4e7-ea53-438c-ae1d-12ed9ece7838"),
                column: "ShortDescription",
                value: "Matter is anything that takes up space and can be weighed. In other words, matter has volume and mass. There are many different substances, or types of matter, in the universe.");

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "Id",
                keyValue: new Guid("ead18201-dc40-4b3d-824f-3f8df867fe69"),
                column: "ShortDescription",
                value: "All kinds of things will suddenly start to happen consistently when you apply this formula in your life. It's just like everything starts to fall into place. Everything shows you that's it's interconnected, that we're not really isolated");

            migrationBuilder.UpdateData(
                table: "Module",
                keyColumn: "Id",
                keyValue: new Guid("f6fcbe2f-8a00-4126-809b-15c7fe189e6f"),
                column: "ShortDescription",
                value: "Scientists define energy as the ability to do work. Modern civilization is possible because people have learned how to change energy from one form to another");

            migrationBuilder.InsertData(
                table: "Subscriptions",
                columns: new[] { "Id", "AuthorID", "Price", "SubscriptionTypeID" },
                values: new object[,]
                {
                    { new Guid("3feca357-4709-4daf-8d5a-3e407a009913"), new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"), 21m, 1 },
                    { new Guid("41b84daa-b16c-4a54-b3c6-b1635daf6792"), new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"), 70m, 2 },
                    { new Guid("55b25fea-96b4-415b-a0ba-941bfff19039"), new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"), 65m, 2 },
                    { new Guid("6d39c9b2-5ed8-4788-989d-278bd9a63cf2"), new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"), 25m, 1 },
                    { new Guid("c2781d0b-759b-4309-842a-866004baaaf8"), new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"), 200m, 3 },
                    { new Guid("ccd67ab4-5702-44b0-8eec-23f21a9e103e"), new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"), 55m, 2 },
                    { new Guid("ef64b371-97b1-4a59-991e-bcb09191c77f"), new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"), 150m, 3 },
                    { new Guid("efd44e86-b36f-4d70-aa1a-62d1ad2a1197"), new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"), 25m, 1 },
                    { new Guid("fcbf9dd7-2843-4c35-9964-72a32a910baf"), new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"), 189m, 3 }
                });
        }
    }
}
