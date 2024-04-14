namespace SpiritualHub.Tests.Service.BusinessService.CategoryService;

using System;

using Moq;

using Data.Models;

using static Extensions.Common.TestErrorMessagesConstants;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

public class EditTests : MockConfiguration
{
    [Test]
    public async Task WhenSuccess()
    {
        // Arrange
        int id = 1;
        var category = new Category { Id = id, Name = "Old Name" };
        string newName = "New name";

        _categoryRepositoryMock.Setup(x => x.GetSingleByIdAsync(It.Is<string>(x => x == id.ToString()))).Returns(Task.FromResult(category)!);

        // Act
        await _categoryService.EditAsync(id, newName);

        // Assert
        Assert.That(category.Name, Is.EqualTo(newName));
        _categoryRepositoryMock.Verify(x => x.GetSingleByIdAsync(It.Is<string>(x => x == id.ToString())), Times.Once);
        _categoryRepositoryMock.Verify(x => x.Update(It.Is<Category>(x => x.Equals(category))), Times.Once);
        _categoryRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Test]
    public void WithWrongId_ThrowsNullReferenceException()
    {
        // Arrange
        int invalidId = 0;
        string newName = "New name";

        _categoryRepositoryMock.Setup(x => x.GetSingleByIdAsync(It.Is<string>(x => x == invalidId.ToString()))).ReturnsAsync((Category) null!);

        // Act & Assert
        Assert.ThrowsAsync<NullReferenceException>(async () => await _categoryService.EditAsync(invalidId, newName), NoNullReferenceExceptionErrorMessage);
    }

    [Test]
    public async Task WithWrongId_MethodCallTest()
    {
        // Arrange
        int invalidId = 0;
        string newName = "New name";

        _categoryRepositoryMock.Setup(x => x.GetSingleByIdAsync(It.Is<string>(x => x == invalidId.ToString()))).ReturnsAsync((Category) null!);

        // Act
        try
        {
            await _categoryService.EditAsync(invalidId, newName);
        }
        // Assert
        catch (Exception)
        {
            _categoryRepositoryMock.Verify(x => x.GetSingleByIdAsync(It.Is<string>(x => x == invalidId.ToString())), Times.Once);
            _categoryRepositoryMock.Verify(x => x.Update(It.IsAny<Category>()), Times.Never);
            _categoryRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Never);

            return;
        }

        Assert.Fail(NoNullReferenceExceptionErrorMessage);
    }

    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase("      ")]
    public async Task WithInvalidName(string newName)
    {
        // Arrange
        int id = 1;
        var category = new Category { Id = id, Name = "Old Name" };
        _categoryRepositoryMock.Setup(x => x.GetSingleByIdAsync(It.Is<string>(x => x == id.ToString()))).Returns(Task.FromResult(category)!);

        // Act
        await _categoryService.EditAsync(id, newName);

        // Assert
        Assert.That(category.Name, !Is.EqualTo(newName), EntityWasUpdatedErrorMessage);
        _categoryRepositoryMock.Verify(x => x.GetSingleByIdAsync(It.Is<string>(x => x == id.ToString())), Times.Never);
        _categoryRepositoryMock.Verify(x => x.Update(It.IsAny<Category>()), Times.Never);
        _categoryRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Never);
    }

    [Test]
    public async Task WithConcurrentName()
    {
        // Arrange
        int id = 1;
        string concurrentName = "Hindu";
        var category = new Category()
        {
            Id = id,
            Name = "old name"
        };

        _categoryRepositoryMock.Setup(x => x.GetSingleByIdAsync(It.Is<string>(x => x == id.ToString()))).Returns(Task.FromResult(category)!);
        _categoryRepositoryMock
            .Setup(x => x.AnyAsync(It.IsAny<Expression<Func<Category, bool>>>()))
            .Returns((Expression<Func<Category, bool>> predicate) => Task.FromResult(true));
        _categoryRepositoryMock.Setup(x => x.SaveChangesAsync()).Throws<DbUpdateException>();

        // Act
        await _categoryService.EditAsync(id, concurrentName);

        // Assert
        Assert.That(category.Name, !Is.EqualTo(concurrentName), EntityWasUpdatedErrorMessage);
        _categoryRepositoryMock.Verify(x => x.GetSingleByIdAsync(It.Is<string>(x => x == id.ToString())), Times.Never);
        _categoryRepositoryMock.Verify(x => x.Update(It.Is<Category>(x => x.Equals(category))), Times.Never);
        _categoryRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Never);
    }
}
