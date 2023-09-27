namespace SpiritualHub.Data.Configuration.Seed;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Interface;
using Models;

public class SeedCourseConfiguration : IEntitySeedConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.HasData(GenerateEntities());
    }

    public Course[] GenerateEntities()
    {
        ICollection<Course> courses = new HashSet<Course>();

        Course course;

        course = new Course()
        {
            Id = Guid.Parse("2c85de5f-00f3-4cc3-8596-84571d342d28"),
            Name = "Cogitality Academy",
            Description = "This is Cogitality Academy - information and practice for Everything that Is!\r\n\r\n9 Cogitality Modules - guides through various areas of knowledge!\r\n\r\nFor the first time, you can immerse yourself in unique techniques for uncovering your true inner potential! An exciting journey into the world of creativity and limitless possibilities, through which you will learn how to express yourself in a new, different, noble, and grateful way.\r\n\r\nExpand your boundaries and discover new horizons. Your adventure begins here - 9 Cogitality Modules!",
            ShortDescription = "The place where science and spirituality meet and create an inspiring journey towards boundless knowledge!",
            Price = 360m,
            ImageID = Guid.Parse("1dbf6044-b493-4373-b650-c5c00c967086"),
            CategoryID = 3,
            AuthorID = Guid.Parse("240AE09A-7F04-45E5-AC42-BF5311E1C4A8"),
            PublisherID = Guid.Parse("D99242D9-3DB2-4675-87E3-DA7743C6B526"),
        };
        courses.Add(course);

        course = new Course()
        {
            Id = Guid.Parse("2ac53aa9-a9a1-4517-a2e6-e8a311f3b6c9"),
            Name = "Channel Your Passion",
            Description = "Bashar’s follow your excitement formula is the key to being your True Self and living your dreams! Here are the basic steps:\r\n\r\n#1. Act on your excitement, your passion, whatever is most exciting to you, in the moment. Do this every moment that you can.\r\n#2. Do this to the best of your ability. Take it as far as you can go until you cannot take it any further.\r\n#3. Act on your excitement/passion with absolutely no insistence, assumption or expectation of what the outcome should be.\r\n#4. Choose to remain in a positive state regardless of what happens.\r\n#5. Constantly investigate your belief systems. Release & replace the un-preferred beliefs: fear-based beliefs, and the beliefs not in alignment with who you prefer to be.\r\n\r\nReady to take the plunge? Bashar takes us deep into the three critical parts of the “Follow Your Excitement Formula” necessary for you to receive the full life changing benefit of acting on your excitement. “The Formula” is available now in multiple formats and includes a holotope meditation to further guide you on your path.",
            ShortDescription = "Ready to take the plunge? Bashar takes us deep into the three critical parts of the “Follow Your Excitement Formula” necessary for you to receive the full life changing benefit of acting on your excitement.",
            Price = 250m,
            ImageID = Guid.Parse("01457f31-bc09-4d77-8d5d-6c334ff3347b"),
            CategoryID = 2,
            AuthorID = Guid.Parse("47383FE7-F3E1-4D22-8180-5BFAA76955F5"),
            PublisherID = Guid.Parse("AF62ED49-898A-46ED-8AA4-336257AE6443"),
        };
        courses.Add(course);

        course = new Course()
        {
            Id = Guid.Parse("5fec9e9c-cdfb-4037-8bc9-7b6e98267aac"),
            Name = "Experience Now",
            Description = "Leading a very troubled and problematic life, coined by many periods of serious depression, Eckhart Tolle found peace overnight, quite literally.\r\nPlagued by depressing late-night thoughts, he started questioning what it is that made his life so unbearable and found the answer in his “I” – the self-generated from the power of his thoughts in his mind. The next morning he woke up and felt very much at peace because he’d somehow managed to lose his worrier-self and live entirely in the now, the present moment.\r\n\r\nAfter spending several years doing nothing but enjoying his new-found peace, eventually people started asking him questions – so he answered. Eckhart started teaching and published The Power of Now in 1997, which eventually went on to become a New York Times bestseller in 2000 after Oprah Winfrey fell in love with it and recommended it.",
            ShortDescription = "The Power of Now shows you that every minute you spend worrying about the future or regretting the past is a minute lost, because the only place you can truly live in is the present, the now.",
            Price = 450m,
            ImageID = Guid.Parse("d3d4a16d-0050-4947-90a9-9133a2b129b9"),
            CategoryID = 5,
            AuthorID = Guid.Parse("8C8BD426-2974-4BAD-AA33-0E045CA86A54"),
            PublisherID = Guid.Parse("AF62ED49-898A-46ED-8AA4-336257AE6443"),
        };
        courses.Add(course);

        course = new Course()
        {
            Id = Guid.Parse("fb3472d1-2259-4600-aa60-7ff29745f475"),
            Name = "Ancient Alchemy",
            Description = "During the Middle Ages and the Renaissance, the Hermetica enjoyed great prestige and were popular among alchemists. Hermes was also strongly associated with astrology, for example by the influential Islamic astrologer Abu Ma'shar al-Balkhi (787–886). The \"Hermetic tradition\" consequently refers to alchemy, magic, astrology, and related subjects. The texts are usually divided into two categories: the philosophical and the technical hermetica. The former deals mainly with philosophy, and the latter with practical magic, potions, and alchemy. The expression \"hermetically sealed\" comes from the alchemical procedure to make the Philosopher's Stone. This required a mixture of materials to be placed in a glass vessel which was sealed by fusing the neck closed, a procedure known as the Seal of Hermes. The vessel was then heated for 30 to 40 days.",
            ShortDescription = "The \"Hermetic tradition\" consequently refers to alchemy, magic, astrology, and related subjects. The texts are usually divided into two categories: the philosophical and the technical hermetica.",
            Price = 123m,
            ImageID = Guid.Parse("251f1b7a-4aec-46b5-8cde-1740103cde1f"),
            CategoryID = 1,
            AuthorID = Guid.Parse("0FD425BD-BB0E-477E-AB19-A58DDAD6FB27"),
            PublisherID = Guid.Parse("D99242D9-3DB2-4675-87E3-DA7743C6B526"),
        };
        courses.Add(course);

        return courses.ToArray();
    }
}
