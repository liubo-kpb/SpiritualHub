namespace SpiritualHub.Data.Configuration.Seed;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Configuration.Seed.Interface;
using Models;

public class SeedImageConfiguration : IEntitySeedConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder)
    {
        builder.HasData(GenerateEntities());
    }

    public Image[] GenerateEntities()
    {
        ICollection<Image> images = new HashSet<Image>();

        Image image;

        //Authors
        image = new Image()
        {
            Id = Guid.Parse("2a022e06-8c00-435f-93a9-9da816c1b483"),
            Name = "BasharAvatar",
            URL = "https://1drv.ms/i/s!AtAU7bartlmmgYQEO5c530QekMydnA?e=65X6RK"
        };
        images.Add(image);

        image = new Image()
        {
            Id = Guid.Parse("868aaede-674a-44a6-ae21-ec62bd2bec3b"),
            Name = "CogitalityAvatar",
            URL = "https://1drv.ms/i/s!AtAU7bartlmmgYRw6O57eiKsf9iNBQ?e=OsdWqo"
        };
        images.Add(image);

        image = new Image()
        {
            Id = Guid.Parse("26db05ea-2b5e-44dd-bdef-4e74b9ecaa5f"),
            Name = "EckhartTolleAvatar",
            URL = "https://1drv.ms/i/s!AtAU7bartlmmhpxwLMTJsLUEHxnZSQ?e=sln5JF"
        };
        images.Add(image);

        //Events
        image = new Image()
        {
            Id = Guid.Parse("ab7cfc34-55f4-4ed8-9687-c48a747e9fb4"),
            Name = "HealningSeminar",
            URL = "https://kogitalnost.net/wp-content/uploads/2023/07/FINALL-Kogitalnost-2-3-09-2023-copy-1024x536.webp"
        };
        images.Add(image);

        image = new Image()
        {
            Id = Guid.Parse("13e26f61-5a34-44e0-b9d4-d8ab04b8f342"),
            Name = "An-Evening-with-Eckhart-Tolle-in-Stockholm",
            URL = "https://eckharttolle.com/wp-content/uploads/2023/02/Waterfront_november_2019-2048x1460.jpg"
        };
        images.Add(image);

        image = new Image()
        {
            Id = Guid.Parse("69630e42-a4de-4116-a1a4-38c43faa0b53"),
            Name = "The-Three-Behaviors-of-Connection",
            URL = "https://www.bashar.org/wp-content/uploads/2023/07/THREE-BEHAVIOURS_NEWSPAGE1-1024x576.jpg"
        };
        images.Add(image);


        //Books
        image = new Image()
        {
            Id = Guid.Parse("c7b99bd1-8188-4277-b937-81ab367b4034"),
            Name = "EC",
            URL = "https://kogitalnost.net/wp-content/uploads/2023/06/3-te-knigi-1.webp"
        };
        images.Add(image);

        image = new Image()
        {
            Id = Guid.Parse("327b0419-5ff9-4694-a4f8-151cb0a46e6b"),
            Name = "PowerOfNow",
            URL = "https://1drv.ms/i/s!AtAU7bartlmmhp15puF4kOZMXXn-9w?e=fZECnk"
        };
        images.Add(image);

        image = new Image()
        {
            Id = Guid.Parse("55dc2c91-c81b-40de-ac5b-f7474a7acfdc"),
            Name = "MOL",
            URL = "https://1drv.ms/i/s!AtAU7bartlmmhp17o6bOmIyxERpgkQ?e=bWhmAO"
        };
        images.Add(image);

        return images.ToArray();
    }
}
