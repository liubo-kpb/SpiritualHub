namespace SpiritualHub.Data.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Models;

public class CommentEntityConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder
            .HasOne(c => c.Post)
            .WithMany(b => b.Comments)
            .HasForeignKey(c => c.PostID)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(r => r.User)
            .WithMany(u => u.Comments)
            .HasForeignKey(r => r.UserID)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
