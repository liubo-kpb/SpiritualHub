namespace SpiritualHub.Data.Configuration.Seed;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Configuration.Seed.Interface;
using Models;


internal class SeedSubscriptionTypeConfiguration : IEntitySeedConfiguration<SubscriptionType>
{
    public void Configure(EntityTypeBuilder<SubscriptionType> builder)
    {
        builder.HasData(GenerateEntities());
    }

    public SubscriptionType[] GenerateEntities()
    {
        ICollection<SubscriptionType> subTypes = new HashSet<SubscriptionType>();

        SubscriptionType subType;

        subType = new SubscriptionType()
        {
            Id = 1,
            Type = "Monthly",
        };
        subTypes.Add(subType);

        subType = new SubscriptionType()
        {
            Id = 2,
            Type = "Quarterly",
        };
        subTypes.Add(subType);

        subType = new SubscriptionType()
        {
            Id = 3,
            Type = "Annual",
        };
        subTypes.Add(subType);

        return subTypes.ToArray();
    }
}
