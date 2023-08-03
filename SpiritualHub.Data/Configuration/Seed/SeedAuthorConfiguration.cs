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
            CategoryID = 2,
            AvatarImageID = Guid.Parse("2a022e06-8c00-435f-93a9-9da816c1b483")
        };
        authors.Add(author);

        author = new Author()
        {
            Id = Guid.Parse("240ae09a-7f04-45e5-ac42-bf5311e1c4a8"),
            Alias = "Cogitality",
            Name = "Cogitality Academy",
            CategoryID = 3,
            AvatarImageID = Guid.Parse("868aaede-674a-44a6-ae21-ec62bd2bec3b")
        };
        authors.Add(author);

        author = new Author()
        {
            Id = Guid.Parse("8c8bd426-2974-4bad-aa33-0e045ca86a54"),
            Alias = "Eckhart Tolle",
            Name = "Eckhart Tolle",
            CategoryID = 5,
            AvatarImageID = Guid.Parse("26db05ea-2b5e-44dd-bdef-4e74b9ecaa5f")
        };
        authors.Add(author);

        return authors.ToArray();
    }
}
