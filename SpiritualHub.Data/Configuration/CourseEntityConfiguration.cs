namespace SpiritualHub.Data.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Models;

public class CourseEntityConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder
                .Property(h => h.AddedOn)
                .HasDefaultValueSql("GETDATE()");

        builder
            .HasOne(c => c.Author)
            .WithMany(a => a.Courses)
            .HasForeignKey(c => c.AuthorID)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(c => c.Publisher)
            .WithMany(p => p.Courses)
            .HasForeignKey(c => c.PublisherID)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(c => c.Category)
            .WithMany(c => c.Courses)
            .HasForeignKey(c => c.CategoryID)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasMany(c => c.Students)
            .WithMany(u => u.Courses);
    }
}
