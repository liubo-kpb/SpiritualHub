namespace SpiritualHub.Tests.Service.BusinessService.CategoryService;

using Moq;
using MockQueryable.Moq;

using Data.Models;
using Client.ViewModels.Category;

public class GetAllTests : MockConfiguration
{
    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase("l")]
    [TestCase("Esoteric")]
    [TestCase("       ")]
    public async Task TestFiltering(string searchWord)
    {

        // Arrange
        var categories = GenerateTestCategories();
        var expected = new List<CategoryServiceModel>()
        {
            new() { Id = 1, Name = "Spiritual"},
            new() { Id = 2, Name = "Esoteric"},
            new() { Id = 3, Name = "Religion"}
        };

        if (!string.IsNullOrEmpty(searchWord))
        {
            expected = expected.Where(c => c.Name.ToLower().Contains(searchWord.ToLower())).ToList();
        }

        _categoryRepositoryMock.Setup(x => x.GetAll()).Returns(categories);

        // Act
        var result = await _categoryService.GetAllAsync(searchWord);

        // Assert
        Assert.That(result, Is.Not.Null, "Collection returned null when it shouldn't have been.");
        Assert.That(result.Count, Is.EqualTo(expected.Count), "Collection count expectations didn't match.");
        _categoryRepositoryMock.Verify(x => x.GetAll(), Times.Once);

        if (!string.IsNullOrEmpty(searchWord))
        {
            foreach (var item in result)
            {
                Assert.That(item.Name.ToLower().Contains(searchWord.ToLower()), "Not all elements in collection contain the search word.");
            }
        }
    }

    private IQueryable<Category> GenerateTestCategories()
    {
        var list = new List<Category>()
        {
            new() { Id = 1, Name = "Spiritual"},
            new() { Id = 2, Name = "Esoteric"},
            new() { Id = 3, Name = "Religion"}
        };

        return list.AsQueryable().BuildMock();
    }
}
