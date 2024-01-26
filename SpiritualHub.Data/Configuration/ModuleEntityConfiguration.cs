namespace SpiritualHub.Data.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Models;

public class ModuleEntityConfiguration : IEntityTypeConfiguration<Module>
{
    public void Configure(EntityTypeBuilder<Module> builder)
    {
        builder
            .HasOne(m => m.Course)
            .WithMany(c => c.Modules)
            .HasForeignKey(m => m.CourseID)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .Property(m => m.VideoUrl)
            .IsRequired(false);

        builder
            .Property(m => m.Text)
            .IsRequired(false);

        builder
            .Property(m => m.Description)
            .HasDefaultValue("Please add a description...");
    }
}
