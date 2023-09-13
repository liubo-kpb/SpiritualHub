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
            URL = "https://www.bashar.org/wp-content/uploads/2017/02/Bashar_purple2.jpg"
        };
        images.Add(image);

        image = new Image()
        {
            Id = Guid.Parse("868aaede-674a-44a6-ae21-ec62bd2bec3b"),
            Name = "CogitalityAvatar",
            URL = "https://i.ytimg.com/vi/XvV8rllMh6c/maxresdefault.jpg"
        };
        images.Add(image);

        image = new Image()
        {
            Id = Guid.Parse("26db05ea-2b5e-44dd-bdef-4e74b9ecaa5f"),
            Name = "EckhartTolleAvatar",
            URL = "https://eckharttolle.com/wp-content/uploads/2021/03/PHOTO-Eckhart_EDITEDIMG_5197-scaled.jpg"
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
            URL = "https://m.media-amazon.com/images/I/714FbKtXS+L._AC_UF1000,1000_QL80_.jpg"
        };
        images.Add(image);

        image = new Image()
        {
            Id = Guid.Parse("55dc2c91-c81b-40de-ac5b-f7474a7acfdc"),
            Name = "MOL",
            URL = "https://images-eu.ssl-images-amazon.com/images/I/61oU5+vqzwL._AC_UL750_SR750,750_.jpg"
        };
        images.Add(image);

        return images.ToArray();
    }
}
