namespace SpiritualHub.Data.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Models;

public class SubscriptionEntityConfiguration : IEntityTypeConfiguration<Subscription>
{
    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        builder
            .HasOne(s => s.SubscriptionType)
            .WithMany(st => st.Subscriptions)
            .HasForeignKey(s => s.SubscriptionTypeID)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(s => s.Author)
            .WithMany(a => a.Subscriptions)
            .HasForeignKey(s => s.AuthorID)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasMany(s => s.Subscribers)
            .WithMany(u => u.Subscriptions);
    }
}
