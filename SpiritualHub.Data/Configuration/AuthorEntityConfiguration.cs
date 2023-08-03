namespace SpiritualHub.Data.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Models;

public class AuthorEntityConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder
            .HasOne(a => a.Category)
            .WithMany(c => c.Authors)
            .HasForeignKey(a => a.CategoryID)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(a => a.Publisher)
            .WithMany(p => p.PublishedAuthors)
            .HasForeignKey(a => a.PublisherID)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
