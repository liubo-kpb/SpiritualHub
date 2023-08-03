namespace SpiritualHub.Data.Configuration.Seed;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Configuration.Seed.Interface;
using Models;

public class SeedCategoryConfiguration : IEntitySeedConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasData(GenerateEntities());
    }

    public Category[] GenerateEntities()
    {
        ICollection<Category> categories = new HashSet<Category>();

        Category category;

        category = new Category()
        {
            Id = 1,
            Name = "Esoteric"
        };
        categories.Add(category);

        category = new Category()
        {
            Id = 2,
            Name = "Channeling"
        };
        categories.Add(category);

        category = new Category()
        {
            Id = 3,
            Name = "Scientific"
        };
        categories.Add(category);

        category = new Category()
        {
            Id = 4,
            Name = "Religious"
        };
        categories.Add(category);

        category = new Category()
        {
            Id = 5,
            Name = "Spiritual"
        };
        categories.Add(category);

        category = new Category()
        {
            Id = 6,
            Name = "Hindu"
        };
        categories.Add(category);

        return categories.ToArray();

    }
}
