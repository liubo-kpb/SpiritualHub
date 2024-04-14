namespace SpiritualHub.Tests.Service.BusinessService.CategoryService;

using Moq;

using Data.Models;

public class DeleteTests : MockConfiguration
{
    [Test]
    public async Task WhenSuccess()
    {
        // Arrange
        string id = "1";
        var category = new Category()
        {
            Id = 1,
            Name = "Test"
        };

        _categoryRepositoryMock.Setup(x => x.GetSingleByIdAsync(It.Is<string>(x => x == id))).Returns(Task.FromResult(category)!);
        _categoryRepositoryMock.Setup(x => x.Delete(It.Is<Category>(x => x.Equals(category))));

        // Act
        await _categoryService.DeleteAsync(id);

        // Assert
        _categoryRepositoryMock.Verify(x => x.GetSingleByIdAsync(It.Is<string>(x => x == id)), Times.Once);
        _categoryRepositoryMock.Verify(x => x.Delete(It.Is<Category>(x => x.Equals(category))));
        _categoryRepositoryMock.Verify(x => x.Delete(It.IsAny<Category>()), Times.Once);
        _categoryRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Test]
    public async Task WithWrongId()
    {
        // Arrange
        string invalidId = "invalidId";

        _categoryRepositoryMock.Setup(x => x.GetSingleByIdAsync(It.Is<string>(x => x == invalidId))).ReturnsAsync((Category) null!);

        // Act
        await _categoryService.DeleteAsync(invalidId);

        // Assert
        _categoryRepositoryMock.Verify(x => x.GetSingleByIdAsync(It.Is<string>(x => x == invalidId)), Times.Once);
        _categoryRepositoryMock.Verify(x => x.Delete(It.IsAny<Category>()), Times.Once);
        _categoryRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);
    }
}
