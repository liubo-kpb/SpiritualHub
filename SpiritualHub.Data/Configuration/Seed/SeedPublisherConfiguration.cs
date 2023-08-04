namespace SpiritualHub.Data.Configuration.Seed;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Configuration.Seed.Interface;
using Models;

public class SeedPublisherConfiguration : IEntitySeedConfiguration<Publisher>
{
    public void Configure(EntityTypeBuilder<Publisher> builder)
    {
        builder.HasData(GenerateEntities());
    }

    public Publisher[] GenerateEntities()
    {
        ICollection<Publisher> publishers = new HashSet<Publisher>();

        Publisher publisher;

        publisher = new Publisher()
        {
            Id = Guid.Parse("d99242d9-3db2-4675-87e3-da7743c6b526"),
            PhoneNumber = "+359888888888",
            UserID = Guid.Parse("194974cd-73f0-4946-ba85-710d4061472d")
        };
        publishers.Add(publisher);

        return publishers.ToArray();
    }
}
