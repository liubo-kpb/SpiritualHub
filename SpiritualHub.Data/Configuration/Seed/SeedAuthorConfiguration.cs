namespace SpiritualHub.Data.Configuration.Seed;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Interfaces;
using Models;

public class SeedAuthorConfiguration : IEntitySeedConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.HasData(this.GenerateEntities());
    }

    public Author[] GenerateEntities()
    {
        ICollection<Author> authors = new HashSet<Author>();

        Author author;

        author = new Author()
        {
            Id = Guid.Parse("47383fe7-f3e1-4d22-8180-5bfaa76955f5"),
            Alias = "Bashar",
            Name = "Darryl Anka",
            Description = "Bashar is a physical E.T, a friend from the future who has spoken for the past 37 years through channel Darryl Anka.  He has brought through a wave of new information that clearly explains in detail how the universe works, and how each person creates the reality they experience. Over the years, thousands of individuals have had the opportunity to apply these principles, and see that they really work to change their lives and create the reality that they desire.",
            CategoryID = 2,
            AvatarImageID = Guid.Parse("2a022e06-8c00-435f-93a9-9da816c1b483"),
            IsActive = true,
        };
        authors.Add(author);

        author = new Author()
        {
            Id = Guid.Parse("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"),
            Alias = "Cogitality - Everything that IS!",
            Name = "Cogitality Academy",
            Description = $"With its unique integration of scientific principles and spiritual insights, the Academy provides access to new horizons in understanding oneself and the surrounding world. The thesis that everything is interconnected is not just a theory or esoteric belief but a practical principle of Existence!{Environment.NewLine}In the Academy, you pave the path towards your synchronized unfolding of thought, information, and energy, receiving tools and knowledge to consciously create your gracious and grateful world.{Environment.NewLine}Cogitality Academy is the culmination of years of effort, exploration, practice, and mistakes, through which you now gain the fastest and easiest access to this extraordinary realm of wisdom and Life, beyond the confines of time!",
            CategoryID = 3,
            AvatarImageID = Guid.Parse("868aaede-674a-44a6-ae21-ec62bd2bec3b"),
            IsActive = true,
        };
        authors.Add(author);

        author = new Author()
        {
            Id = Guid.Parse("68508613-D974-4237-5182-08DBA58C19E0"),
            Alias = "Lao Tzu",
            Name = "Laotzu",
            Description = "Laozi (/ˈlaʊdzə/, Chinese: 老子), also romanized as Lao Tzu and various other ways, was a semi-legendary ancient Chinese Taoist philosopher, credited with writing the Tao Te Ching. Laozi is a Chinese honorific, generally translated as \"the Old Master\". Although modern scholarship generally regards him as a fictional person, traditional accounts say he was born as Li Er in the state of Chu in the 6th century BC during China's Spring and Autumn Period, served as the royal archivist for the Zhou court at Wangcheng (modern Luoyang), met and impressed Confucius on one occasion, and composed the Tao Te Ching in a single session before retiring into the western wilderness. And more...",
            CategoryID = 5,
            AvatarImageID = Guid.Parse("7993BAD4-DF53-40DD-8921-15D4CDF5C252"),
            IsActive = true,
        };
        authors.Add(author);

        author = new Author()
        {
            Id = Guid.Parse("8c8bd426-2974-4bad-aa33-0e045ca86a54"),
            Alias = "Eckhart Tolle",
            Name = "Eckhart Tolle",
            Description = "Eckhart Tolle is widely recognized as one of the most inspiring and visionary spiritual teachers in the world today. With his international bestsellers, The Power of Now and A New Earth—translated into 52 languages—he has introduced millions to the joy and freedom of living life in the present moment. The New York Times has described him as “the most popular spiritual author in the United States”, and in 2011, Watkins Review named him “the most spiritually influential person in the world”.",
            CategoryID = 5,
            AvatarImageID = Guid.Parse("26db05ea-2b5e-44dd-bdef-4e74b9ecaa5f"),
            IsActive = true,
        };
        authors.Add(author);

        author = new Author()
        {
            Id = Guid.Parse("0FD425BD-BB0E-477E-AB19-A58DDAD6FB27"),
            Alias = "Hermes Trismegistus",
            Name = "Hermes",
            Description = "Hermes Trismegistus (from Ancient Greek: Ἑρμῆς ὁ Τρισμέγιστος, \"Hermes the Thrice-Greatest\"; Classical Latin: Mercurius ter Maximus) is a legendary Hellenistic figure that originated as a syncretic combination of the Greek god Hermes and the Egyptian god Thoth. He is the purported author of the Hermetica, a widely diverse series of ancient and medieval pseudepigraphical texts that lay the basis of various philosophical systems known as Hermeticism.",
            CategoryID = 1,
            AvatarImageID = Guid.Parse("CBAB4CBB-8F68-4445-8E5E-03B9503BEB0A"),
            IsActive = true,
        };
        authors.Add(author);

        return authors.ToArray();
    }
}
