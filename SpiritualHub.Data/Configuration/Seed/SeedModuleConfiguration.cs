namespace SpiritualHub.Data.Configuration.Seed;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Interface;
using Models;

public class SeedModuleConfiguration : IEntitySeedConfiguration<Module>
{
    public void Configure(EntityTypeBuilder<Module> builder)
    {
        builder.HasData(GenerateEntities());
    }

    public Module[] GenerateEntities()
    {
        ICollection<Module> modules = new HashSet<Module>();

        Module module;

        //CogitalityAcademy
        module = new Module()
        {
            Id = Guid.Parse("73a33310-756f-468d-8dae-ed96769dac81"),
            Name = "Everything Is Energy",
            ShortDescription = "The basic concepts, resources, and structure of the Academy; stepping into the world of possibilities!",
            VideoUrl = "https://www.youtube.com/watch?v=-BYgnO9h9ns&t=1s&ab_channel=Cogitality-EverythingthatIs%21",
            Text = "You have just entered Module 1. By now you’re probably hoping that you’ll be able to levitate, read minds, or perform channeling with aliens and talk to them. All of this is possible and achievable, but before we get there, training is required, which should be carried out according to one’s own program, which each person builds individually. To be comfortable for everyone. This program is yours alone – individual and unique. We will give examples, but they are only an outline – a model, and the program itself remains your own creation.\r\nYou reap what you sow…",
            Number = 1,
            CourseID = Guid.Parse("2c85de5f-00f3-4cc3-8596-84571d342d28"),
        };
        modules.Add(module);

        module = new Module()
        {
            Id = Guid.Parse("5cd93182-8506-4bdb-9078-1ec1753129e8"),
            Name = "All Paradoxes Reconcile",
            ShortDescription = "Expanding the information, concepts, and unfolding to the potential.",
            VideoUrl = "https://www.youtube.com/watch?v=0Nj9PPImgj8s&pp=ygUPY29naXRhbGl0eSBmaWxt&ab_channel=Cogitality-EverythingthatIs%21",
            Text = "At the entrance of Module 2, it’s good to ask ourselves – having now spent at least a month pondering and reflecting on the nature of the world and matter, have I come to any conclusion about who I really am? Am I both Nothing and its opposite, because one does not exist without the other? I cannot be the one if the other is missing.\r\n\r\nI am the Whole – a collection of the joined vessels of knowledge and ignorance with a channel between them through which the energy of the manifested yet unemerged probability of the manifestation flows… When we believe in the information field, we believe in the Source of energy, which is ourselves. It’s time to venture down the path of this exploration…",
            Number = 2,
            CourseID = Guid.Parse("2c85de5f-00f3-4cc3-8596-84571d342d28"),
        };
        modules.Add(module);

        module = new Module()
        {
            Id = Guid.Parse("291f7df8-4953-4e97-bbc8-26a594d222c2"),
            Name = "The Laws of Existence",
            ShortDescription = "The new reality – the part contains the whole, and the whole is contained in the part.",
            VideoUrl = "https://www.youtube.com/watch?v=tadk0FfCy_w&ab_channel=Cogitality-EverythingthatIs%21",
            Text = "Often, the new reality is not what we expected, and fear and doubt may arise, making us wonder if we've made a mistake. To prevent your old frequencies from surfacing, seek things that make you feel good without reverting to old ways of thinking. Celebrate everything – what you have achieved and what you haven't, because you have already seen it and have the chance to discover the possibility of success there. To be impartial observers of yourselves at all times, ask yourself frequently: \"Why am I doing (saying) this now? What frequencies of vibration does what I do or say evoke in me?\" After all, you are in the new reality, and there everything is much better and easier for you. You've created it for yourself, haven't you? So, it vibrates according to your own standards. But if you vibrate at your old frequencies, how will you remain in your new reality?",
            Number = 3,
            CourseID = Guid.Parse("2c85de5f-00f3-4cc3-8596-84571d342d28"),
        };
        modules.Add(module);

        //ExperienceNow
        module = new Module()
        {
            Id = Guid.Parse("612c4206-142a-4463-8fd1-463b9823b69c"),
            Name = "Inner Peace",
            ShortDescription = "The Power of Now shows you that every minute you spend worrying about the future or regretting the past is a minute lost.",
            VideoUrl = "https://www.youtube.com/watch?v=WWFkl8IOLTA&pp=ygUYZWtoYXJ0IHRvbGxlIGlubmVyIHBlYWNl&ab_channel=EckhartTolle",
            Text = "Finding Inner Peace: 7 Days of Teachings and Meditations with Eckhart Tolle and Kim Eng consists of morning teachings and evening meditations. Consistency strengthens the pathway to Presence, so we will send a brief email each morning with a link to the daily practice. Each practice is less than 15 minutes in length. We recommend setting aside uninterrupted time in the morning and evening each day to support your practice.\r\n\r\n \r\n\r\nThe seven-day Presence practice offers many essential teachings from Eckhart and Kim, arranged here to help you:\r\n\r\nStep out of the stream of thinking and make the mind your ally\r\nBring Presence to your relationships—including the most challenging ones\r\nConnect deeply with nature\r\nFind the balance of Being and doing in your daily life (the key to transforming stress and contributing to a saner society)\r\n \r\nWe hope you will join us for this free seven-day Presence practice.",
            Number = 1,
            CourseID = Guid.Parse("5fec9e9c-cdfb-4037-8bc9-7b6e98267aac"),
        };
        modules.Add(module);

        module = new Module()
        {
            Id = Guid.Parse("5ccdd241-9cc5-4b8a-9373-e8416e0b1890"),
            Name = "Sick Body",
            ShortDescription = "It is an unprecedented time, and it is extremely important to remain as conscious as possible. The ego—on both the individual and the collective level—loves “the drama” that times like this can create.",
            VideoUrl = "https://www.youtube.com/watch?v=P78YZZaQfSg&pp=ygUWZWtoYXJ0IHRvbGxlIHNpY2sgYm9keQ%3D%3D&ab_channel=EckhartTolle",
            Text = "The physical body is always susceptible to all kinds of influences. If something goes wrong with the body, then it becomes doubly important not to judge yourself or to say that you created it. If you are ill, whatever illness it may be, the most effective thing you can do is to surrender to what is, which does not mean surrender to what you call the illness. Surrender means acceptance. Acceptance initiates healing. The foundation for healing is to accept this moment as it is. In this moment, the so-called illness may manifest either as pain, discomfort, or some kind of disability. This is what you surrender to.\r\n\r\nYou never surrender to the idea of illness. You don’t say, “I must surrender to the fact that I have COVID-19,” or, even worse, “I must surrender to the fact that I have this incurable condition.” All you surrender to is the present moment, whatever the body manifests in the present moment. That is what is; that is what you accept. With that kind of surrender, a doorway opens into the transcendent dimension, and that’s where the power of manifestation really comes through.",
            Number = 2,
            CourseID = Guid.Parse("5fec9e9c-cdfb-4037-8bc9-7b6e98267aac"),
        };
        modules.Add(module);

        module = new Module()
        {
            Id = Guid.Parse("78d418cc-6713-4f12-8dd7-ede28da7bc19"),
            Name = "Inner Space",
            ShortDescription = "But don’t look for it as if you were looking for something. You cannot pin it down and say, “Now I have it,” or grasp it mentally and define it in some way.",
            VideoUrl = "https://www.youtube.com/watch?v=HQ0y_gh0Qho&pp=ygUYZWtoYXJ0IHRvbGxlIGlubmVyIHNwYWNl&ab_channel=EckhartTolle",
            Text = "Many poets and sages throughout the ages have observed that true happiness — I call it the joy of Being — is found in simple, seemingly unremarkable things. Most people, in their restless search for something significant to happen to them, continuously miss the insignificant, which may not be insignificant at all. The philosopher Nietzsche, in a rare moment of deep stillness, wrote, “For happiness, how little suffices for happiness! . . . the least thing precisely, the gentlest thing, the lightest thing, a lizard’s rustling, a breath, a wisk, an eye glance — little maketh up the best happiness. Be still.” Why is it the “least thing” that makes up “the best happiness”? Because true happiness is not caused by the thing or event, although this is how it first appears. The thing or event is so subtle, so unobtrusive, that it takes up only a small part of your consciousness—and the rest is inner space, consciousness itself unobstructed by form.",
            Number = 3,
            CourseID = Guid.Parse("5fec9e9c-cdfb-4037-8bc9-7b6e98267aac"),
        };
        modules.Add(module);

        //ChannelYourPassion
        module = new Module()
        {
            Id = Guid.Parse("ead18201-dc40-4b3d-824f-3f8df867fe69"),
            Name = "Follow Your Passion",
            ShortDescription = "All kinds of things will suddenly start to happen consistently when you apply this formula in your life. It's just like everything starts to fall into place. Everything shows you that's it's interconnected, that we're not really isolated",
            VideoUrl = "https://www.youtube.com/watch?v=fNHXJhkOxUw&ab_channel=FromEssassaniWithLove",
            Text = "Bashar’s follow your excitement formula is the key to being your True Self and living your dreams! Here are the basic steps:\r\n\r\n#1. Act on your excitement, your passion, whatever is most exciting to you, in the moment. Do this every moment that you can.\r\n#2. Do this to the best of your ability. Take it as far as you can go until you cannot take it any further.\r\n#3. Act on your excitement/passion with absolutely no insistence, assumption or expectation of what the outcome should be.\r\n#4. Choose to remain in a positive state regardless of what happens.\r\n#5. Constantly investigate your belief systems. Release & replace the un-preferred beliefs: fear-based beliefs, and the beliefs not in alignment with who you prefer to be.\r\n\r\nReady to take the plunge? Bashar takes us deep into the three critical parts of the “Follow Your Excitement Formula” necessary for you to receive the full life changing benefit of acting on your excitement. “The Formula” is available now in multiple formats and includes a holotope meditation to further guide you on your path.",
            Number = 1,
            CourseID = Guid.Parse("2ac53aa9-a9a1-4517-a2e6-e8a311f3b6c9"),
        };
        modules.Add(module);

        module = new Module()
        {
            Id = Guid.Parse("8fe13950-5be0-451d-8539-f83d6bf37d09"),
            Name = "Frequencies And Synchronization",
            ShortDescription = "You have a core vibrational frequency.\r\nIt's a beacon. It's like a lighthouse. It shines. It radiates purely that signature frequency of your unique being. It never stops radiating that light, that frequency, that energy - never stops.",
            VideoUrl = "https://www.youtube.com/watch?v=1ooKqYA-rmk&pp=ygUcYmFzaGFyIHJhaXNlIHlvdXIgZnJlcXVlbmN5IA%3D%3D&ab_channel=TOWARDJOY",
            Text = "Here is the true secret of the Law of Attraction:\r\n\r\nYou have a core vibrational frequency.\r\nIt is purely uniquely you.\r\nIt's a beacon. It's like a lighthouse. It shines. It radiates purely that signature frequency of your unique being. It never stops radiating that light, that frequency, that energy - never stops.\r\n\r\nEverything that is in alignment with that frequency is doing its utmost to come to you.\r\n\r\nEverything that is not aligned with that frequency is doing its utmost to get as far away from you as it possibly can.\r\n\r\nIf the things that are aligned with that beacon aren't reaching you, it's not because \"you're not vibrating at the resonance that you need to attract it\". It's because your definitions and beliefs are holding it away.\r\n\r\nIf the things that are trying to get away from you can't get away from you, it's not because they're not trying - it's because you're holding onto them.\r\n\r\nSo the true Secret of the Law of Attraction is not \"how to learn to attract what you prefer\", it's how to learn to let go of what you don't, so that you can let in what is trying to get to you automatically - by definition.\r\n\r\nThat's the true Secret and that's why it's effortless.\r\nIt's just about letting go and letting in.\r\nIt's not about having to learn to do something you're not already doing.",
            Number = 2,
            CourseID = Guid.Parse("2ac53aa9-a9a1-4517-a2e6-e8a311f3b6c9"),
        };
        modules.Add(module);

        module = new Module()
        {
            Id = Guid.Parse("3c9b6e99-8108-4c3a-b027-6109669220f1"),
            Name = "Information Grounding",
            ShortDescription = "The practice of professedly entering a meditative or trancelike state in order to convey messages from Source.",
            VideoUrl = "https://www.youtube.com/watch?v=y4aVXqUnQM0&pp=ygURYmFzaGFyIGNoYW5uZWxpbmc%3D&ab_channel=HigherJourneyswithAlexisBrooks",
            Text = "Once you identify your highest passion, it is essential to take action on it to the best of your ability.\r\n\r\nBashar suggests exploring every possible path and opportunity related to your passion until you have exhausted all options.\r\n\r\nThis step encourages proactive engagement and a willingness to invest effort and energy into the pursuit of your passion. By acting on your passion, you create a strong connection to your higher mind and open yourself up to receiving further guidance and opportunities aligned with your true path.",
            Number = 3,
            CourseID = Guid.Parse("2ac53aa9-a9a1-4517-a2e6-e8a311f3b6c9"),
        };
        modules.Add(module);

        //AncientAlchemy
        module = new Module()
        {
            Id = Guid.Parse("39209dc8-df5b-4a15-8d6b-c1917e55f429"),
            Name = "Spirit",
            ShortDescription = "A force within a human being thought to give the body life, energy, and power.",
            VideoUrl = "https://www.youtube.com/watch?v=BchgsTANO-k&pp=ygUHYWxjaGVteQ%3D%3D&ab_channel=TheGeneralistPapers",
            Text = "The Bible tells us that God is spirit (John 4:24) and angels are spirits (Hebrews 1:13-14). By analogy, we can assume that everything in the spirit realm is composed of spirit, just as all things in the material realm are composed of matter.\r\n\r\nTherefore, God’s throne (Ezekiel 10:1) and the “furnishings” at God’s throne would be composed of spirit. The mercy seat on the Ark of the Covenant was a representation of the throne of God in heaven (Exodus 25:17, 22). From Hebrews 9:23-24 we see that the physical temple was patterned after the design of the heavenly sanctuary where God dwells.\r\n\r\nGetting to know the God of the Bible.So, again, we can conclude all that exists where God dwells is composed of spirit.\r\nDoes the spirit realm have anything analogous to the energy of the material realm? One way to describe the Holy Spirit is that it is the power God uses to do whatever He does. This is a biblical analogy (Acts 1:8; 2 Timothy 1:7).",
            Number = 1,
            CourseID = Guid.Parse("fb3472d1-2259-4600-aa60-7ff29745f475"),
        };
        modules.Add(module);

        module = new Module()
        {
            Id = Guid.Parse("f6fcbe2f-8a00-4126-809b-15c7fe189e6f"),
            Name = "Energy",
            ShortDescription = "Scientists define energy as the ability to do work. Modern civilization is possible because people have learned how to change energy from one form to another",
            VideoUrl = "https://www.youtube.com/watch?v=DtKs3EI_2po&pp=ygUHYWxjaGVteQ%3D%3D&ab_channel=Sehnend",
            Text = "Scientists define energy as the ability to do work. Modern civilization is possible because people have learned how to change energy from one form to another and then use it to do work. People use energy for a variety of things, such as to walk and bicycle, to move cars along roads and boats through water, to cook food on stoves, to make ice in freezers, to light our homes and offices, to manufacture products, and to send astronauts into space.\r\n\r\nThere are many forms of energy:\r\n\r\nHeat\r\nLight\r\nMotion\r\nElectrical\r\nChemical\r\nGravitational\r\nThese forms of energy can be grouped into two general types of energy for doing work:\r\n\r\nPotential, or stored, energy\r\nKinetic, or working, energy\r\nEnergy can be converted from one form to another. For example, the food you eat contains chemical energy, and your body stores this energy until you use it as kinetic energy during work or play. The stored chemical energy in coal or natural gas and the kinetic energy of water flowing in rivers can be converted to electrical energy, which can be converted to light and heat.",
            Number = 2,
            CourseID = Guid.Parse("fb3472d1-2259-4600-aa60-7ff29745f475"),
        };
        modules.Add(module);

        module = new Module()
        {
            Id = Guid.Parse("cff6f4e7-ea53-438c-ae1d-12ed9ece7838"),
            Name = "Matter",
            ShortDescription = "Matter is anything that takes up space and can be weighed. In other words, matter has volume and mass. There are many different substances, or types of matter, in the universe.",
            VideoUrl = "https://www.youtube.com/watch?v=AhE_6qpu3UU&pp=ygUHYWxjaGVteQ%3D%3D&ab_channel=TheAlchemist",
            Text = "Matter explained: Atoms, molecules, elements and compounds\r\nFundamentally, matter is composed of elementary particles called quarks and leptons, both of which are considered elementary particles in that they aren't made up of smaller units of matter. Quarks -- groups of subatomic particles that interact by means of a strong force -- combine into protons and neutrons. Leptons -- groups of subatomic particles that respond to weaker forces -- belong to a class of elementary particles that includes electrons.\r\n\r\nAtoms are the building blocks of matter. A combination of atoms forms a molecule. Large groups of atoms and molecules form the bulk matter of day-to-day life in the physical world. There are more than 100 different kinds of atoms listed in the periodic table, with each kind constituting a unique chemical element.",
            Number = 3,
            CourseID = Guid.Parse("fb3472d1-2259-4600-aa60-7ff29745f475"),
        };
        modules.Add(module);

        return modules.ToArray();
    }
}
