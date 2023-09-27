using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpiritualHub.Data.Migrations
{
    public partial class CourseAndModuleSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "AuthorID", "CategoryID", "Description", "ImageID", "Name", "Price", "PublisherID", "ShortDescription" },
                values: new object[,]
                {
                    { new Guid("2ac53aa9-a9a1-4517-a2e6-e8a311f3b6c9"), new Guid("47383fe7-f3e1-4d22-8180-5bfaa76955f5"), 2, "Bashar’s follow your excitement formula is the key to being your True Self and living your dreams! Here are the basic steps:\r\n\r\n#1. Act on your excitement, your passion, whatever is most exciting to you, in the moment. Do this every moment that you can.\r\n#2. Do this to the best of your ability. Take it as far as you can go until you cannot take it any further.\r\n#3. Act on your excitement/passion with absolutely no insistence, assumption or expectation of what the outcome should be.\r\n#4. Choose to remain in a positive state regardless of what happens.\r\n#5. Constantly investigate your belief systems. Release & replace the un-preferred beliefs: fear-based beliefs, and the beliefs not in alignment with who you prefer to be.\r\n\r\nReady to take the plunge? Bashar takes us deep into the three critical parts of the “Follow Your Excitement Formula” necessary for you to receive the full life changing benefit of acting on your excitement. “The Formula” is available now in multiple formats and includes a holotope meditation to further guide you on your path.", new Guid("01457f31-bc09-4d77-8d5d-6c334ff3347b"), "Channel Your Passion", 250m, new Guid("af62ed49-898a-46ed-8aa4-336257ae6443"), "Ready to take the plunge? Bashar takes us deep into the three critical parts of the “Follow Your Excitement Formula” necessary for you to receive the full life changing benefit of acting on your excitement." },
                    { new Guid("2c85de5f-00f3-4cc3-8596-84571d342d28"), new Guid("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"), 3, "This is Cogitality Academy - information and practice for Everything that Is!\r\n\r\n9 Cogitality Modules - guides through various areas of knowledge!\r\n\r\nFor the first time, you can immerse yourself in unique techniques for uncovering your true inner potential! An exciting journey into the world of creativity and limitless possibilities, through which you will learn how to express yourself in a new, different, noble, and grateful way.\r\n\r\nExpand your boundaries and discover new horizons. Your adventure begins here - 9 Cogitality Modules!", new Guid("1dbf6044-b493-4373-b650-c5c00c967086"), "Cogitality Academy", 360m, new Guid("d99242d9-3db2-4675-87e3-da7743c6b526"), "The place where science and spirituality meet and create an inspiring journey towards boundless knowledge!" },
                    { new Guid("5fec9e9c-cdfb-4037-8bc9-7b6e98267aac"), new Guid("8c8bd426-2974-4bad-aa33-0e045ca86a54"), 5, "Leading a very troubled and problematic life, coined by many periods of serious depression, Eckhart Tolle found peace overnight, quite literally.\r\nPlagued by depressing late-night thoughts, he started questioning what it is that made his life so unbearable and found the answer in his “I” – the self-generated from the power of his thoughts in his mind. The next morning he woke up and felt very much at peace because he’d somehow managed to lose his worrier-self and live entirely in the now, the present moment.\r\n\r\nAfter spending several years doing nothing but enjoying his new-found peace, eventually people started asking him questions – so he answered. Eckhart started teaching and published The Power of Now in 1997, which eventually went on to become a New York Times bestseller in 2000 after Oprah Winfrey fell in love with it and recommended it.", new Guid("d3d4a16d-0050-4947-90a9-9133a2b129b9"), "Experience Now", 450m, new Guid("af62ed49-898a-46ed-8aa4-336257ae6443"), "The Power of Now shows you that every minute you spend worrying about the future or regretting the past is a minute lost, because the only place you can truly live in is the present, the now." },
                    { new Guid("fb3472d1-2259-4600-aa60-7ff29745f475"), new Guid("0fd425bd-bb0e-477e-ab19-a58ddad6fb27"), 1, "During the Middle Ages and the Renaissance, the Hermetica enjoyed great prestige and were popular among alchemists. Hermes was also strongly associated with astrology, for example by the influential Islamic astrologer Abu Ma'shar al-Balkhi (787–886). The \"Hermetic tradition\" consequently refers to alchemy, magic, astrology, and related subjects. The texts are usually divided into two categories: the philosophical and the technical hermetica. The former deals mainly with philosophy, and the latter with practical magic, potions, and alchemy. The expression \"hermetically sealed\" comes from the alchemical procedure to make the Philosopher's Stone. This required a mixture of materials to be placed in a glass vessel which was sealed by fusing the neck closed, a procedure known as the Seal of Hermes. The vessel was then heated for 30 to 40 days.", new Guid("251f1b7a-4aec-46b5-8cde-1740103cde1f"), "Ancient Alchemy", 123m, new Guid("d99242d9-3db2-4675-87e3-da7743c6b526"), "The \"Hermetic tradition\" consequently refers to alchemy, magic, astrology, and related subjects. The texts are usually divided into two categories: the philosophical and the technical hermetica." }
                });

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

            migrationBuilder.InsertData(
                table: "Module",
                columns: new[] { "Id", "CourseID", "Name", "Number", "ShortDescription", "Text", "VideoUrl" },
                values: new object[,]
                {
                    { new Guid("291f7df8-4953-4e97-bbc8-26a594d222c2"), new Guid("2c85de5f-00f3-4cc3-8596-84571d342d28"), "The Laws of Existence", 3, "The new reality – the part contains the whole, and the whole is contained in the part.", "Often, the new reality is not what we expected, and fear and doubt may arise, making us wonder if we've made a mistake. To prevent your old frequencies from surfacing, seek things that make you feel good without reverting to old ways of thinking. Celebrate everything – what you have achieved and what you haven't, because you have already seen it and have the chance to discover the possibility of success there. To be impartial observers of yourselves at all times, ask yourself frequently: \"Why am I doing (saying) this now? What frequencies of vibration does what I do or say evoke in me?\" After all, you are in the new reality, and there everything is much better and easier for you. You've created it for yourself, haven't you? So, it vibrates according to your own standards. But if you vibrate at your old frequencies, how will you remain in your new reality?", "https://www.youtube.com/watch?v=tadk0FfCy_w&ab_channel=Cogitality-EverythingthatIs%21" },
                    { new Guid("39209dc8-df5b-4a15-8d6b-c1917e55f429"), new Guid("fb3472d1-2259-4600-aa60-7ff29745f475"), "Spirit", 1, "A force within a human being thought to give the body life, energy, and power.", "The Bible tells us that God is spirit (John 4:24) and angels are spirits (Hebrews 1:13-14). By analogy, we can assume that everything in the spirit realm is composed of spirit, just as all things in the material realm are composed of matter.\r\n\r\nTherefore, God’s throne (Ezekiel 10:1) and the “furnishings” at God’s throne would be composed of spirit. The mercy seat on the Ark of the Covenant was a representation of the throne of God in heaven (Exodus 25:17, 22). From Hebrews 9:23-24 we see that the physical temple was patterned after the design of the heavenly sanctuary where God dwells.\r\n\r\nGetting to know the God of the Bible.So, again, we can conclude all that exists where God dwells is composed of spirit.\r\nDoes the spirit realm have anything analogous to the energy of the material realm? One way to describe the Holy Spirit is that it is the power God uses to do whatever He does. This is a biblical analogy (Acts 1:8; 2 Timothy 1:7).", "https://www.youtube.com/watch?v=BchgsTANO-k&pp=ygUHYWxjaGVteQ%3D%3D&ab_channel=TheGeneralistPapers" },
                    { new Guid("3c9b6e99-8108-4c3a-b027-6109669220f1"), new Guid("2ac53aa9-a9a1-4517-a2e6-e8a311f3b6c9"), "Information Grounding", 3, "The practice of professedly entering a meditative or trancelike state in order to convey messages from Source.", "Once you identify your highest passion, it is essential to take action on it to the best of your ability.\r\n\r\nBashar suggests exploring every possible path and opportunity related to your passion until you have exhausted all options.\r\n\r\nThis step encourages proactive engagement and a willingness to invest effort and energy into the pursuit of your passion. By acting on your passion, you create a strong connection to your higher mind and open yourself up to receiving further guidance and opportunities aligned with your true path.", "https://www.youtube.com/watch?v=y4aVXqUnQM0&pp=ygURYmFzaGFyIGNoYW5uZWxpbmc%3D&ab_channel=HigherJourneyswithAlexisBrooks" },
                    { new Guid("5ccdd241-9cc5-4b8a-9373-e8416e0b1890"), new Guid("5fec9e9c-cdfb-4037-8bc9-7b6e98267aac"), "Sick Body", 2, "It is an unprecedented time, and it is extremely important to remain as conscious as possible. The ego—on both the individual and the collective level—loves “the drama” that times like this can create.", "The physical body is always susceptible to all kinds of influences. If something goes wrong with the body, then it becomes doubly important not to judge yourself or to say that you created it. If you are ill, whatever illness it may be, the most effective thing you can do is to surrender to what is, which does not mean surrender to what you call the illness. Surrender means acceptance. Acceptance initiates healing. The foundation for healing is to accept this moment as it is. In this moment, the so-called illness may manifest either as pain, discomfort, or some kind of disability. This is what you surrender to.\r\n\r\nYou never surrender to the idea of illness. You don’t say, “I must surrender to the fact that I have COVID-19,” or, even worse, “I must surrender to the fact that I have this incurable condition.” All you surrender to is the present moment, whatever the body manifests in the present moment. That is what is; that is what you accept. With that kind of surrender, a doorway opens into the transcendent dimension, and that’s where the power of manifestation really comes through.", "https://www.youtube.com/watch?v=P78YZZaQfSg&pp=ygUWZWtoYXJ0IHRvbGxlIHNpY2sgYm9keQ%3D%3D&ab_channel=EckhartTolle" },
                    { new Guid("5cd93182-8506-4bdb-9078-1ec1753129e8"), new Guid("2c85de5f-00f3-4cc3-8596-84571d342d28"), "All Paradoxes Reconcile", 2, "Expanding the information, concepts, and unfolding to the potential.", "At the entrance of Module 2, it’s good to ask ourselves – having now spent at least a month pondering and reflecting on the nature of the world and matter, have I come to any conclusion about who I really am? Am I both Nothing and its opposite, because one does not exist without the other? I cannot be the one if the other is missing.\r\n\r\nI am the Whole – a collection of the joined vessels of knowledge and ignorance with a channel between them through which the energy of the manifested yet unemerged probability of the manifestation flows… When we believe in the information field, we believe in the Source of energy, which is ourselves. It’s time to venture down the path of this exploration…", "https://www.youtube.com/watch?v=0Nj9PPImgj8s&pp=ygUPY29naXRhbGl0eSBmaWxt&ab_channel=Cogitality-EverythingthatIs%21" },
                    { new Guid("612c4206-142a-4463-8fd1-463b9823b69c"), new Guid("5fec9e9c-cdfb-4037-8bc9-7b6e98267aac"), "Inner Peace", 1, "The Power of Now shows you that every minute you spend worrying about the future or regretting the past is a minute lost.", "Finding Inner Peace: 7 Days of Teachings and Meditations with Eckhart Tolle and Kim Eng consists of morning teachings and evening meditations. Consistency strengthens the pathway to Presence, so we will send a brief email each morning with a link to the daily practice. Each practice is less than 15 minutes in length. We recommend setting aside uninterrupted time in the morning and evening each day to support your practice.\r\n\r\n \r\n\r\nThe seven-day Presence practice offers many essential teachings from Eckhart and Kim, arranged here to help you:\r\n\r\nStep out of the stream of thinking and make the mind your ally\r\nBring Presence to your relationships—including the most challenging ones\r\nConnect deeply with nature\r\nFind the balance of Being and doing in your daily life (the key to transforming stress and contributing to a saner society)\r\n \r\nWe hope you will join us for this free seven-day Presence practice.", "https://www.youtube.com/watch?v=WWFkl8IOLTA&pp=ygUYZWtoYXJ0IHRvbGxlIGlubmVyIHBlYWNl&ab_channel=EckhartTolle" },
                    { new Guid("73a33310-756f-468d-8dae-ed96769dac81"), new Guid("2c85de5f-00f3-4cc3-8596-84571d342d28"), "Everything Is Energy", 1, "The basic concepts, resources, and structure of the Academy; stepping into the world of possibilities!", "You have just entered Module 1. By now you’re probably hoping that you’ll be able to levitate, read minds, or perform channeling with aliens and talk to them. All of this is possible and achievable, but before we get there, training is required, which should be carried out according to one’s own program, which each person builds individually. To be comfortable for everyone. This program is yours alone – individual and unique. We will give examples, but they are only an outline – a model, and the program itself remains your own creation.\r\nYou reap what you sow…", "https://www.youtube.com/watch?v=-BYgnO9h9ns&t=1s&ab_channel=Cogitality-EverythingthatIs%21" },
                    { new Guid("78d418cc-6713-4f12-8dd7-ede28da7bc19"), new Guid("5fec9e9c-cdfb-4037-8bc9-7b6e98267aac"), "Inner Space", 3, "But don’t look for it as if you were looking for something. You cannot pin it down and say, “Now I have it,” or grasp it mentally and define it in some way.", "Many poets and sages throughout the ages have observed that true happiness — I call it the joy of Being — is found in simple, seemingly unremarkable things. Most people, in their restless search for something significant to happen to them, continuously miss the insignificant, which may not be insignificant at all. The philosopher Nietzsche, in a rare moment of deep stillness, wrote, “For happiness, how little suffices for happiness! . . . the least thing precisely, the gentlest thing, the lightest thing, a lizard’s rustling, a breath, a wisk, an eye glance — little maketh up the best happiness. Be still.” Why is it the “least thing” that makes up “the best happiness”? Because true happiness is not caused by the thing or event, although this is how it first appears. The thing or event is so subtle, so unobtrusive, that it takes up only a small part of your consciousness—and the rest is inner space, consciousness itself unobstructed by form.", "https://www.youtube.com/watch?v=HQ0y_gh0Qho&pp=ygUYZWtoYXJ0IHRvbGxlIGlubmVyIHNwYWNl&ab_channel=EckhartTolle" },
                    { new Guid("8fe13950-5be0-451d-8539-f83d6bf37d09"), new Guid("2ac53aa9-a9a1-4517-a2e6-e8a311f3b6c9"), "Frequencies And Synchronization", 2, "You have a core vibrational frequency.\r\nIt's a beacon. It's like a lighthouse. It shines. It radiates purely that signature frequency of your unique being. It never stops radiating that light, that frequency, that energy - never stops.", "Here is the true secret of the Law of Attraction:\r\n\r\nYou have a core vibrational frequency.\r\nIt is purely uniquely you.\r\nIt's a beacon. It's like a lighthouse. It shines. It radiates purely that signature frequency of your unique being. It never stops radiating that light, that frequency, that energy - never stops.\r\n\r\nEverything that is in alignment with that frequency is doing its utmost to come to you.\r\n\r\nEverything that is not aligned with that frequency is doing its utmost to get as far away from you as it possibly can.\r\n\r\nIf the things that are aligned with that beacon aren't reaching you, it's not because \"you're not vibrating at the resonance that you need to attract it\". It's because your definitions and beliefs are holding it away.\r\n\r\nIf the things that are trying to get away from you can't get away from you, it's not because they're not trying - it's because you're holding onto them.\r\n\r\nSo the true Secret of the Law of Attraction is not \"how to learn to attract what you prefer\", it's how to learn to let go of what you don't, so that you can let in what is trying to get to you automatically - by definition.\r\n\r\nThat's the true Secret and that's why it's effortless.\r\nIt's just about letting go and letting in.\r\nIt's not about having to learn to do something you're not already doing.", "https://www.youtube.com/watch?v=1ooKqYA-rmk&pp=ygUcYmFzaGFyIHJhaXNlIHlvdXIgZnJlcXVlbmN5IA%3D%3D&ab_channel=TOWARDJOY" },
                    { new Guid("cff6f4e7-ea53-438c-ae1d-12ed9ece7838"), new Guid("fb3472d1-2259-4600-aa60-7ff29745f475"), "Matter", 3, "Matter is anything that takes up space and can be weighed. In other words, matter has volume and mass. There are many different substances, or types of matter, in the universe.", "Matter explained: Atoms, molecules, elements and compounds\r\nFundamentally, matter is composed of elementary particles called quarks and leptons, both of which are considered elementary particles in that they aren't made up of smaller units of matter. Quarks -- groups of subatomic particles that interact by means of a strong force -- combine into protons and neutrons. Leptons -- groups of subatomic particles that respond to weaker forces -- belong to a class of elementary particles that includes electrons.\r\n\r\nAtoms are the building blocks of matter. A combination of atoms forms a molecule. Large groups of atoms and molecules form the bulk matter of day-to-day life in the physical world. There are more than 100 different kinds of atoms listed in the periodic table, with each kind constituting a unique chemical element.", "https://www.youtube.com/watch?v=AhE_6qpu3UU&pp=ygUHYWxjaGVteQ%3D%3D&ab_channel=TheAlchemist" },
                    { new Guid("ead18201-dc40-4b3d-824f-3f8df867fe69"), new Guid("2ac53aa9-a9a1-4517-a2e6-e8a311f3b6c9"), "Follow Your Passion", 1, "All kinds of things will suddenly start to happen consistently when you apply this formula in your life. It's just like everything starts to fall into place. Everything shows you that's it's interconnected, that we're not really isolated", "Bashar’s follow your excitement formula is the key to being your True Self and living your dreams! Here are the basic steps:\r\n\r\n#1. Act on your excitement, your passion, whatever is most exciting to you, in the moment. Do this every moment that you can.\r\n#2. Do this to the best of your ability. Take it as far as you can go until you cannot take it any further.\r\n#3. Act on your excitement/passion with absolutely no insistence, assumption or expectation of what the outcome should be.\r\n#4. Choose to remain in a positive state regardless of what happens.\r\n#5. Constantly investigate your belief systems. Release & replace the un-preferred beliefs: fear-based beliefs, and the beliefs not in alignment with who you prefer to be.\r\n\r\nReady to take the plunge? Bashar takes us deep into the three critical parts of the “Follow Your Excitement Formula” necessary for you to receive the full life changing benefit of acting on your excitement. “The Formula” is available now in multiple formats and includes a holotope meditation to further guide you on your path.", "https://www.youtube.com/watch?v=fNHXJhkOxUw&ab_channel=FromEssassaniWithLove" },
                    { new Guid("f6fcbe2f-8a00-4126-809b-15c7fe189e6f"), new Guid("fb3472d1-2259-4600-aa60-7ff29745f475"), "Energy", 2, "Scientists define energy as the ability to do work. Modern civilization is possible because people have learned how to change energy from one form to another", "Scientists define energy as the ability to do work. Modern civilization is possible because people have learned how to change energy from one form to another and then use it to do work. People use energy for a variety of things, such as to walk and bicycle, to move cars along roads and boats through water, to cook food on stoves, to make ice in freezers, to light our homes and offices, to manufacture products, and to send astronauts into space.\r\n\r\nThere are many forms of energy:\r\n\r\nHeat\r\nLight\r\nMotion\r\nElectrical\r\nChemical\r\nGravitational\r\nThese forms of energy can be grouped into two general types of energy for doing work:\r\n\r\nPotential, or stored, energy\r\nKinetic, or working, energy\r\nEnergy can be converted from one form to another. For example, the food you eat contains chemical energy, and your body stores this energy until you use it as kinetic energy during work or play. The stored chemical energy in coal or natural gas and the kinetic energy of water flowing in rivers can be converted to electrical energy, which can be converted to light and heat.", "https://www.youtube.com/watch?v=DtKs3EI_2po&pp=ygUHYWxjaGVteQ%3D%3D&ab_channel=Sehnend" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Module",
                keyColumn: "Id",
                keyValue: new Guid("291f7df8-4953-4e97-bbc8-26a594d222c2"));

            migrationBuilder.DeleteData(
                table: "Module",
                keyColumn: "Id",
                keyValue: new Guid("39209dc8-df5b-4a15-8d6b-c1917e55f429"));

            migrationBuilder.DeleteData(
                table: "Module",
                keyColumn: "Id",
                keyValue: new Guid("3c9b6e99-8108-4c3a-b027-6109669220f1"));

            migrationBuilder.DeleteData(
                table: "Module",
                keyColumn: "Id",
                keyValue: new Guid("5ccdd241-9cc5-4b8a-9373-e8416e0b1890"));

            migrationBuilder.DeleteData(
                table: "Module",
                keyColumn: "Id",
                keyValue: new Guid("5cd93182-8506-4bdb-9078-1ec1753129e8"));

            migrationBuilder.DeleteData(
                table: "Module",
                keyColumn: "Id",
                keyValue: new Guid("612c4206-142a-4463-8fd1-463b9823b69c"));

            migrationBuilder.DeleteData(
                table: "Module",
                keyColumn: "Id",
                keyValue: new Guid("73a33310-756f-468d-8dae-ed96769dac81"));

            migrationBuilder.DeleteData(
                table: "Module",
                keyColumn: "Id",
                keyValue: new Guid("78d418cc-6713-4f12-8dd7-ede28da7bc19"));

            migrationBuilder.DeleteData(
                table: "Module",
                keyColumn: "Id",
                keyValue: new Guid("8fe13950-5be0-451d-8539-f83d6bf37d09"));

            migrationBuilder.DeleteData(
                table: "Module",
                keyColumn: "Id",
                keyValue: new Guid("cff6f4e7-ea53-438c-ae1d-12ed9ece7838"));

            migrationBuilder.DeleteData(
                table: "Module",
                keyColumn: "Id",
                keyValue: new Guid("ead18201-dc40-4b3d-824f-3f8df867fe69"));

            migrationBuilder.DeleteData(
                table: "Module",
                keyColumn: "Id",
                keyValue: new Guid("f6fcbe2f-8a00-4126-809b-15c7fe189e6f"));

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

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("2ac53aa9-a9a1-4517-a2e6-e8a311f3b6c9"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("2c85de5f-00f3-4cc3-8596-84571d342d28"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("5fec9e9c-cdfb-4037-8bc9-7b6e98267aac"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("fb3472d1-2259-4600-aa60-7ff29745f475"));

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
        }
    }
}
