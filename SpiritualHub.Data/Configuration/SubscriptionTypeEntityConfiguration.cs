namespace SpiritualHub.Data.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Models;

public class SubscriptionTypeEntityConfiguration : IEntityTypeConfiguration<Subscription>
{
    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        builder
            .HasCheckConstraint("CK__PossibleTypes", "Type = 'Monthly' OR " +
                                                     "Type = 'Quarterly' OR " +
                                                     "Type = 'Annual'");
    }
}
