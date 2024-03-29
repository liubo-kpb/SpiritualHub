namespace SpiritualHub.Tests.Service.BusinessService.CategoryService;

using Microsoft.EntityFrameworkCore;

using AutoMapper;

using Services;
using Data.Models;
using Extensions.Repository;

using static Extensions.Common.TestErrorMessagesConstants;

public class CategoryCRUDTests : TestBaseSetup<CategoryService>
{
    private TestDeletableRepository<Category> _categoryRepository = null!;

    protected override CategoryService InitializeServices(IMapper mapper)
    {
        _categoryRepository = new TestDeletableRepository<Category>(DbContext);

        return new CategoryService(_categoryRepository, mapper);
    }

    protected override void SeedTestData()
    {
        var categories = new List<Category>()
        {
            new Category() { Id = 1, Name = "Esoteric" },
            new Category() { Id = 2, Name = "Channeling" },
            new Category() { Id = 3, Name = "Scientific" },
            new Category() { Id = 4, Name = "Religious" },
            new Category() { Id = 5, Name = "Spiritual" },
            new Category() { Id = 6, Name = "Hindu" },
        };

        DbContext.Categories.AddRange(categories);
    }

    [Test]
    public async Task Test_AddMethod_WhenSuccess()
    {
        int categoriesCount = DbContext.Categories.Count();
        var newCategoryName = "NewCategory";
        var expected = new Category()
        {
            Id = categoriesCount + 1,
            Name = newCategoryName
        };

        await Service.AddAsync(newCategoryName);

        var categoriesInDb = DbContext.Categories;
        Assert.Multiple(() =>
        {
            Assert.That(categoriesInDb.Any(c => c.Id == expected.Id && c.Name == expected.Name), Is.True, "New Category not found. Category was not added to Database or there is an issue with the Id and/or Name of the test data.");
            Assert.That(categoriesInDb.Count() == categoriesCount + 1, Is.True, "Category count does not match new expectation. Category not added.");
            Assert.That(_categoryRepository.AddAsyncCounter, Is.EqualTo(1), "Add Counter does not match expected result. Check the call count of the method in the service method you are using.");
            Assert.That(_categoryRepository.SaveChangesAsyncCounter, Is.EqualTo(1), "Add Counter does not match expected result. Check the call count of the method in the service method you are using.");
        });
    }

    [Test]
    public void Test_AddMethod_WhenNullName()
    {
        Assert.ThrowsAsync<DbUpdateException>(async () => await Service.AddAsync(null!), NotRequiredPropertyErrorMessage);
    }
}
