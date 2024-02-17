namespace SpiritualHub.Data.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Models;

public class RatingEntityConfiguration : IEntityTypeConfiguration<Rating>
{
    public void Configure(EntityTypeBuilder<Rating> builder)
    {
        builder
            .HasOne(r => r.User)
            .WithMany(u => u.Ratings)
            .HasForeignKey(r => r.UserID)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasMany(r => r.Authors)
            .WithMany(a => a.Ratings);

        builder
            .HasMany(r => r.Courses)
            .WithMany(c => c.Ratings);

        builder
            .HasMany(r => r.Modules)
            .WithMany(m => m.Ratings);

        builder
            .HasMany(r => r.Events)
            .WithMany(e => e.Ratings);

        builder
            .HasMany(r => r.Books)
            .WithMany(b => b.Ratings);
    }
}
