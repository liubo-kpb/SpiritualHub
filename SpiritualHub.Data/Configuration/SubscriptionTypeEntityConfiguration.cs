namespace SpiritualHub.Data.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Models;

public class SubscriptionTypeEntityConfiguration : IEntityTypeConfiguration<SubscriptionType>
{
    public void Configure(EntityTypeBuilder<SubscriptionType> builder)
    {
        builder
            .HasCheckConstraint("CK__PossibleTypes", "Type = 'Monthly' OR " +
                                                     "Type = 'Quarterly' OR " +
                                                     "Type = 'Annual'");
    }
}
