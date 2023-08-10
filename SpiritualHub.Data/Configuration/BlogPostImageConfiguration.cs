namespace SpiritualHub.Data.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Data.Models;

public class BlogPostImageConfiguration : IEntityTypeConfiguration<BlogPostImage>
{
    public void Configure(EntityTypeBuilder<BlogPostImage> builder)
    {
        builder.HasKey(bp => new { bp.ImageID, bp.BlogID });

        builder.HasOne(bp => bp.Blog)
            .WithMany(b => b.Images)
            .HasForeignKey(bp => bp.BlogID)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
