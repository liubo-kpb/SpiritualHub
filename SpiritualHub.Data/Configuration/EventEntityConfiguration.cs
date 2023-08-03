namespace SpiritualHub.Data.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SpiritualHub.Data.Models;

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
            .HasForeignKey(e => e.AutorID)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(e => e.Organizer)
            .WithMany(u => u.Events)
            .HasForeignKey(e => e.OrganizerID)
            .OnDelete(DeleteBehavior.Restrict);

    }
}
