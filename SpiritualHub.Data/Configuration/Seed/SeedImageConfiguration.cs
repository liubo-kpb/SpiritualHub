namespace SpiritualHub.Data.Configuration.Seed;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpiritualHub.Data.Configuration.Seed.Interface;
using SpiritualHub.Data.Models;

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
            URL = "https://1drv.ms/i/s!AtAU7bartlmmgYQEO5c530QekMydnA?e=65X6RK" //TODO: change!
        };
        images.Add(image);

        return images.ToArray();
    }
}
