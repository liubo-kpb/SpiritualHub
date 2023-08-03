namespace SpiritualHub.Data.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SpiritualHub.Data.Models;

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
            .HasOne(r => r.Author)
            .WithMany(a => a.Ratings)
            .HasForeignKey(r => r.AuthorID)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(r => r.Course)
            .WithMany(c => c.Ratings)
            .HasForeignKey(r => r.CourseID)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(r => r.Event)
            .WithMany(e => e.Ratings)
            .HasForeignKey(r => r.EventID)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(r => r.Book)
            .WithMany(b => b.Ratings)
            .HasForeignKey(r => r.BookID)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
