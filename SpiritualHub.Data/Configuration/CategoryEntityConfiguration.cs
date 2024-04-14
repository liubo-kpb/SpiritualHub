namespace SpiritualHub.Data.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Models;

public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder
            .HasIndex(c => c.Name)
            .IsUnique();
    }
}
