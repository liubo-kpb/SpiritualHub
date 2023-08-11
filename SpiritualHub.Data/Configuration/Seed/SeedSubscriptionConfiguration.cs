namespace SpiritualHub.Data.Configuration.Seed;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Configuration.Seed.Interface;
using Models;

public class SeedSubscriptionConfiguration : IEntitySeedConfiguration<Subscription>
{
    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        builder.HasData(GenerateEntities());
    }

    public Subscription[] GenerateEntities()
    {
        ICollection<Subscription> subscriptoins = new HashSet<Subscription>();

        Subscription subscription = null;


        // Tolle Subscriptions
        subscription = new Subscription()
        {
            SubscriptionTypeID = 1,
            Price = 25m,
            AuthorID = Guid.Parse("8C8BD426-2974-4BAD-AA33-0E045CA86A54")
        };
        subscriptoins.Add(subscription);

        subscription = new Subscription()
        {
            SubscriptionTypeID = 2,
            Price = 65m,
            AuthorID = Guid.Parse("8C8BD426-2974-4BAD-AA33-0E045CA86A54")
        };
        subscriptoins.Add(subscription);

        subscription = new Subscription()
        {
            SubscriptionTypeID = 3,
            Price = 150m,
            AuthorID = Guid.Parse("8C8BD426-2974-4BAD-AA33-0E045CA86A54")
        };
        subscriptoins.Add(subscription);

        // Bashar Subscriptions
        subscription = new Subscription()
        {
            SubscriptionTypeID = 1,
            Price = 25m,
            AuthorID = Guid.Parse("47383FE7-F3E1-4D22-8180-5BFAA76955F5")
        };
        subscriptoins.Add(subscription);

        subscription = new Subscription()
        {
            SubscriptionTypeID = 2,
            Price = 70m,
            AuthorID = Guid.Parse("47383FE7-F3E1-4D22-8180-5BFAA76955F5")
        };
        subscriptoins.Add(subscription);

        subscription = new Subscription()
        {
            SubscriptionTypeID = 3,
            Price = 200m,
            AuthorID = Guid.Parse("47383FE7-F3E1-4D22-8180-5BFAA76955F5")
        };
        subscriptoins.Add(subscription);

        // Cogitality Subscriptions
        subscription = new Subscription()
        {
            SubscriptionTypeID = 1,
            Price = 21m,
            AuthorID = Guid.Parse("240AE09A-7F04-45E5-AC42-BF5311E1C4A8")
        };
        subscriptoins.Add(subscription);

        subscription = new Subscription()
        {
            SubscriptionTypeID = 2,
            Price = 55m,
            AuthorID = Guid.Parse("240AE09A-7F04-45E5-AC42-BF5311E1C4A8")
        };
        subscriptoins.Add(subscription);

        subscription = new Subscription()
        {
            SubscriptionTypeID = 3,
            Price = 189m,
            AuthorID = Guid.Parse("240AE09A-7F04-45E5-AC42-BF5311E1C4A8")
        };
        subscriptoins.Add(subscription);

        return subscriptoins.ToArray();
    }
}
