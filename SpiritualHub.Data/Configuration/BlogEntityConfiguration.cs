namespace SpiritualHub.Data.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Models;

public class BlogEntityConfiguration : IEntityTypeConfiguration<Blog>
{
    public void Configure(EntityTypeBuilder<Blog> builder)
    {
        builder
            .HasOne(b => b.Author)
            .WithMany(a => a.Blogs)
            .HasForeignKey(b => b.AuthorID)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(b => b.Category)
            .WithMany(c => c.Blogs)
            .HasForeignKey(b => b.CategoryID)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(b => b.Publisher)
            .WithMany(u => u.Blogs)
            .HasForeignKey(b => b.PublisherID)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
