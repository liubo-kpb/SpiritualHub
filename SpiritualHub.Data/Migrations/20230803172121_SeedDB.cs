using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpiritualHub.Data.Migrations
{
    public partial class SeedDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    { new Guid("26db05ea-2b5e-44dd-bdef-4e74b9ecaa5f"), "EckhartTolleAvatar", "https://1drv.ms/i/s!AtAU7bartlmmhpxwLMTJsLUEHxnZSQ?e=sln5JF" },
                    { new Guid("2a022e06-8c00-435f-93a9-9da816c1b483"), "BasharAvatar", "https://1drv.ms/i/s!AtAU7bartlmmgYQEO5c530QekMydnA?e=65X6RK" },
                    { new Guid("327b0419-5ff9-4694-a4f8-151cb0a46e6b"), "PowerOfNow", "https://1drv.ms/i/s!AtAU7bartlmmhp15puF4kOZMXXn-9w?e=fZECnk" },
                    { new Guid("55dc2c91-c81b-40de-ac5b-f7474a7acfdc"), "MOL", "https://1drv.ms/i/s!AtAU7bartlmmhp17o6bOmIyxERpgkQ?e=bWhmAO" },
                    { new Guid("69630e42-a4de-4116-a1a4-38c43faa0b53"), "The-Three-Behaviors-of-Connection", "https://www.bashar.org/wp-content/uploads/2023/07/THREE-BEHAVIOURS_NEWSPAGE1-1024x576.jpg" },
                    { new Guid("868aaede-674a-44a6-ae21-ec62bd2bec3b"), "CogitalityAvatar", "https://1drv.ms/i/s!AtAU7bartlmmgYRw6O57eiKsf9iNBQ?e=OsdWqo" },
                    { new Guid("ab7cfc34-55f4-4ed8-9687-c48a747e9fb4"), "HealningSeminar", "https://kogitalnost.net/wp-content/uploads/2023/07/FINALL-Kogitalnost-2-3-09-2023-copy-1024x536.webp" },
                    { new Guid("c7b99bd1-8188-4277-b937-81ab367b4034"), "EC", "https://kogitalnost.net/wp-content/uploads/2023/06/3-te-knigi-1.webp" }
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
                columns: new[] { "Id", "Alias", "AvatarImageID", "CategoryID", "Name" },
                values: new object[] { new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"), "Cogitality", new Guid("868aaede-674a-44a6-ae21-ec62bd2bec3b"), 3, "Cogitality Academy" });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Alias", "AvatarImageID", "CategoryID", "Name" },
                values: new object[] { new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"), "Bashar", new Guid("2a022e06-8c00-435f-93a9-9da816c1b483"), 2, "Darryl Anka" });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Alias", "AvatarImageID", "CategoryID", "Name" },
                values: new object[] { new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"), "Eckhart Tolle", new Guid("26db05ea-2b5e-44dd-bdef-4e74b9ecaa5f"), 5, "Eckhart Tolle" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorID", "CategoryID", "Description", "ImageID", "Price", "PublisherID", "ShortDescription", "Title" },
                values: new object[,]
                {
                    { new Guid("12221379-d5c7-4688-8ad8-efbffcaf8d06"), new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"), 2, "All the facts, in the beginning, were puzzle pieces, scattered in vastness - fragmented, incongruous, unordered. They arrived haphazardly in moments when you weren't seeking them and not expecting them... Flashes, illuminating the darkness, which it is fitting to capture in your hands like fireflies - to gather them with patience, inspiration, and dedication. Then, embracing the scattered chaos of your own ignorance, with faith in the Nothingness, you arrange the light of your own Life.", new Guid("c7b99bd1-8188-4277-b937-81ab367b4034"), 30m, null, "Bundle of the books You - The Source, You - The Manifestation, You - The Life", "The Masters of Limitation" },
                    { new Guid("641ae624-efd0-4eb6-87af-05f2cc17bbb7"), new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"), 5, "It's no wonder that The Power of Now has sold over 2 million copies worldwide and has been translated into over 30 foreign languages. Much more than simple principles and platitudes, the book takes readers on an inspiring spiritual journey to find their true and deepest self and reach the ultimate in personal growth and spirituality: the discovery of truth and light.\r\r\n\r\r\nIn the first chapter, Tolle introduces readers to enlightenment and its natural enemy, the mind. He awakens readers to their role as a creator of pain and shows them how to have a pain-free identity by living fully in the present. The journey is thrilling, and along the way, the author shows how to connect to the indestructible essence of our Being, \"the eternal, ever-present One Life beyond the myriad forms of life that are subject to birth and death.\"\r\r\n\r\r\nFeaturing a new preface by the author, this paperback shows that only after regaining awareness of Being, liberated from Mind and intensely in the Now, is there Enlightenment.", new Guid("327b0419-5ff9-4694-a4f8-151cb0a46e6b"), 30m, null, "This book shows that only after regaining awareness of Being, liberated from Mind and intensely in the Now, is there Enlightenment", "The Power Of Now" },
                    { new Guid("e0aa1a89-c180-4ac0-935d-8efab304b274"), new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"), 3, "All the facts, in the beginning, were puzzle pieces, scattered in vastness - fragmented, incongruous, unordered. They arrived haphazardly in moments when you weren't seeking them and not expecting them... Flashes, illuminating the darkness, which it is fitting to capture in your hands like fireflies - to gather them with patience, inspiration, and dedication. Then, embracing the scattered chaos of your own ignorance, with faith in the Nothingness, you arrange the light of your own Life.", new Guid("c7b99bd1-8188-4277-b937-81ab367b4034"), 30m, null, "Bundle of the books You - The Source, You - The Manifestation, You - The Life", "Encyclopedia Cogitality" }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "AutorID", "CategoryID", "CreatedOn", "Description", "EndDateTime", "ImageID", "IsOnline", "OrganizerID", "Price", "StartDateTime", "Title" },
                values: new object[,]
                {
                    { new Guid("15f326bc-f47f-487f-8764-5fb8fd5c448a"), new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"), 2, new DateTime(2023, 8, 3, 17, 21, 21, 325, DateTimeKind.Utc).AddTicks(507), "What if there was one state of being we could adopt that would help us establish better, stronger connections not only with our families and friends on earth, but also with our friends from the stars?\r\r\n\r\r\nIn The Three Behaviors of Connection, Bashar will share how action, timing, and communication are vital concepts for making inroads and connection with the hybrid children that will eventually be living among us. He will expand in detail on these three behaviors and how we might apply them to our lives on Earth as well as to our quest for contact with our extraterrestrial family.", new DateTime(2023, 8, 26, 15, 30, 0, 0, DateTimeKind.Unspecified), new Guid("69630e42-a4de-4116-a1a4-38c43faa0b53"), true, null, 35m, new DateTime(2023, 8, 26, 14, 30, 0, 0, DateTimeKind.Unspecified), "The 3 Behaviors of Connection" },
                    { new Guid("3db097df-7c7c-4c4e-b546-d4555c4c1521"), new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"), 3, new DateTime(2023, 8, 3, 17, 21, 21, 325, DateTimeKind.Utc).AddTicks(569), "The Cogitality seminars are back - they have already started in the country, and now they are happening at the \"Healing\" campus too! They are pre-planned and organized by the team of cogitalists.\r\r\n\r\r\nThe first seminar at the \"Healing\" campus, which will take place on September 2-3, 2023, is already fully booked. Thank you for the sincere desire to share this experience together!", new DateTime(2023, 9, 3, 18, 0, 0, 0, DateTimeKind.Unspecified), new Guid("ab7cfc34-55f4-4ed8-9687-c48a747e9fb4"), true, null, 144m, new DateTime(2023, 9, 2, 9, 0, 0, 0, DateTimeKind.Unspecified), "Seminar - Campus \"Healing\"" },
                    { new Guid("45bb1c09-b50d-4d47-8fdb-fbfb53086922"), new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"), 5, new DateTime(2023, 8, 3, 17, 21, 21, 325, DateTimeKind.Utc).AddTicks(521), "Join us for this unique opportunity to sit with Eckhart Tolle as he points you to spiritual awakening and the transformation of consciousness. With his hallmark warmth, humour and compassion, this evening will connect you with the peace and serenity that arises from living in the moment.\r\r\n\r\r\nEckhart’s profound, yet simple teachings have helped countless people from around the globe awaken to a vibrantly alive inner peace in their daily lives. Eckhart Tolle’s writings and life-changing public events have touched millions of lives, garnering fans to the likes of Oprah, the Dalai Lama and Deepak Chopra. He is the best-selling author of The Power of Now and A New Earth that are widely regarded as the most transformational books of our time.", new DateTime(2023, 9, 26, 22, 0, 0, 0, DateTimeKind.Unspecified), new Guid("13e26f61-5a34-44e0-b9d4-d8ab04b8f342"), false, null, 199m, new DateTime(2023, 9, 26, 18, 30, 0, 0, DateTimeKind.Unspecified), "An Evening with Eckhart Tolle in Stockholm" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("12221379-d5c7-4688-8ad8-efbffcaf8d06"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("641ae624-efd0-4eb6-87af-05f2cc17bbb7"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("e0aa1a89-c180-4ac0-935d-8efab304b274"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("15f326bc-f47f-487f-8764-5fb8fd5c448a"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("3db097df-7c7c-4c4e-b546-d4555c4c1521"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("45bb1c09-b50d-4d47-8fdb-fbfb53086922"));

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("55dc2c91-c81b-40de-ac5b-f7474a7acfdc"));

            migrationBuilder.DeleteData(
                table: "SubscriptionTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SubscriptionTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SubscriptionTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"));

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("13e26f61-5a34-44e0-b9d4-d8ab04b8f342"));

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("327b0419-5ff9-4694-a4f8-151cb0a46e6b"));

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("69630e42-a4de-4116-a1a4-38c43faa0b53"));

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("ab7cfc34-55f4-4ed8-9687-c48a747e9fb4"));

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("c7b99bd1-8188-4277-b937-81ab367b4034"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("26db05ea-2b5e-44dd-bdef-4e74b9ecaa5f"));

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("2a022e06-8c00-435f-93a9-9da816c1b483"));

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("868aaede-674a-44a6-ae21-ec62bd2bec3b"));
        }
    }
}
