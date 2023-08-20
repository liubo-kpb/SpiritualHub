namespace SpiritualHub.Data.Configuration;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Microsoft.EntityFrameworkCore;

using Models;

internal class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder
            .Property(e => e.FirstName)
            .IsRequired(false);

        builder
            .Property(e => e.LastName)
            .IsRequired(false);
    }
}
