namespace SpiritualHub.Data.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Models;

public class EventEntityConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder
            .HasOne(e => e.Category)
            .WithMany(c => c.Events)
            .HasForeignKey (e => e.CategoryID)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(e => e.Author)
            .WithMany(a => a.Events)
            .HasForeignKey(e => e.AuthorID)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(e => e.Publisher)
            .WithMany(u => u.Events)
            .HasForeignKey(e => e.PublisherID)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .Property(e => e.LocationName)
            .IsRequired(false);

        builder
            .Property(e => e.LocationUrl)
            .IsRequired(false);
    }
}
