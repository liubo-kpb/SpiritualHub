namespace SpiritualHub.Data.Configuration.Seed;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Configuration.Seed.Interface;
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
            AvatarImageID = Guid.Parse("2a022e06-8c00-435f-93a9-9da816c1b483")
        };
        authors.Add(author);

        author = new Author()
        {
            Id = Guid.Parse("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"),
            Alias = "Cogitality - Everything that IS!",
            Name = "Cogitality Academy",
            Description = "With its unique integration of scientific principles and spiritual insights, the Academy provides access to new horizons in understanding oneself and the surrounding world. The thesis that everything is interconnected is not just a theory or esoteric belief but a practical principle of Existence!\r\n\r\nIn the Academy, you pave the path towards your synchronized unfolding of thought, information, and energy, receiving tools and knowledge to consciously create your gracious and grateful world.\r\n\r\nCogitality Academy is the culmination of years of effort, exploration, practice, and mistakes, through which you now gain the fastest and easiest access to this extraordinary realm of wisdom and Life, beyond the confines of time!",
            CategoryID = 3,
            AvatarImageID = Guid.Parse("868aaede-674a-44a6-ae21-ec62bd2bec3b")
        };
        authors.Add(author);

        author = new Author()
        {
            Id = Guid.Parse("8c8bd426-2974-4bad-aa33-0e045ca86a54"),
            Alias = "Eckhart Tolle",
            Name = "Eckhart Tolle",
            Description = "Eckhart Tolle is widely recognized as one of the most inspiring and visionary spiritual teachers in the world today. With his international bestsellers, The Power of Now and A New Earth—translated into 52 languages—he has introduced millions to the joy and freedom of living life in the present moment. The New York Times has described him as “the most popular spiritual author in the United States”, and in 2011, Watkins Review named him “the most spiritually influential person in the world”.",
            CategoryID = 5,
            AvatarImageID = Guid.Parse("26db05ea-2b5e-44dd-bdef-4e74b9ecaa5f")
        };
        authors.Add(author);

        return authors.ToArray();
    }
}
