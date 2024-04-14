namespace SpiritualHub.Tests.Service.BusinessService.CategoryService;

using Microsoft.EntityFrameworkCore;
using Moq;
using AutoMapper;

using Data.Models;
using Data.Repository.Interfaces;
using Services;
using Services.Interfaces;

using static Extensions.Common.TestErrorMessagesConstants;

public class CategoryCRUDTests
{
    private ICategoryService _categoryService;
    private Mock<IDeletableRepository<Category>> _categoryRepositoryMock;
    private Mock<IMapper> _mapperMock;

    [SetUp]
    public void Setup()
    {
        _categoryRepositoryMock = new Mock<IDeletableRepository<Category>>();
        _mapperMock = new Mock<IMapper>();

        _categoryService = new CategoryService(_categoryRepositoryMock.Object, _mapperMock.Object);
    }

    [Test]
    public async Task AddAsync_WhenSuccess()
    {
        // Arrange
        string name = "Test";
        var category = new Category { Name = name };

        _categoryRepositoryMock.Setup(x => x.AddAsync(It.Is<Category>(x => x.Name == name))).Returns(Task.FromResult(true));
        _categoryRepositoryMock.Setup(x => x.SaveChangesAsync()).Returns(Task.FromResult(1));


        // Act
        await _categoryService.AddAsync(name);


        // Assert
        _categoryRepositoryMock.Verify(x => x.AddAsync(It.Is<Category>(x => x.Name == name)));
        _categoryRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Category>()), Times.Once);
        _categoryRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Test]
    public void AddAsync_WhenNullName_ThrowSuccess()
    {
        // Arrange
        string name = null!;
        _categoryRepositoryMock.Setup(x => x.SaveChangesAsync()).Throws<DbUpdateException>();


        // Act & Assert
        Assert.ThrowsAsync<DbUpdateException>(async () => await _categoryService.AddAsync(name), NotDbUpdateExceptionErrorMessage);
    }

    [Test]
    public async Task AddAsync_WhenNullName_MethodCallVerification()
    {
        // Arrange
        string name = null!;
        _categoryRepositoryMock.Setup(x => x.SaveChangesAsync()).Throws<DbUpdateException>();

        // Act
        try
        {
            await _categoryService.AddAsync(name);
        }
        // Assert
        catch (Exception)
        {
            _categoryRepositoryMock.Verify(x => x.AddAsync(It.Is<Category>(x => x.Name == name)), Times.Once);
            _categoryRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);

            return;
        }

        Assert.Fail(NotDbUpdateExceptionErrorMessage);
    }

    [Test]
    public void AddAsync_WithConcurrentName_ThrowSuccess()
    {
        // Arrange
        string name = "Hindu";
        _categoryRepositoryMock.Setup(x => x.AddAsync(It.Is<Category>(x => x.Name == name)));
        _categoryRepositoryMock.Setup(x => x.SaveChangesAsync()).Throws<DbUpdateException>();

        // Act & Assert
        Assert.ThrowsAsync<DbUpdateException>(async () => await _categoryService.AddAsync(name), NotDbUpdateExceptionErrorMessage);
    }

    [Test]
    public async Task AddAsync_WithConcurrentName_MethodCallVerification()
    {
        // Arrange
        string name = "Hindu";
        _categoryRepositoryMock.Setup(x => x.AddAsync(It.Is<Category>(x => x.Name == name)));
        _categoryRepositoryMock.Setup(x => x.SaveChangesAsync()).Throws<DbUpdateException>();

        // Act
        try
        {
            await _categoryService.AddAsync(name);

        }
        // Assert
        catch
        {
            _categoryRepositoryMock.Verify(x => x.AddAsync(It.Is<Category>(x => x.Name == name)), Times.Once);
            _categoryRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);

            return;
        }

        Assert.Fail(NotDbUpdateExceptionErrorMessage);
    }

    [Test]
    public async Task DeleteAsync_WhenSuccess()
    {
        // Arrange
        string id = "1";
        var category = new Category()
        {
            Id = 1,
            Name = "Test"
        };

        _categoryRepositoryMock.Setup(x => x.GetSingleByIdAsync(It.Is<string>(x => x == id))).Returns(Task.FromResult(category)!);

        // Act
        await _categoryService.DeleteAsync(id);

        // Assert
        _categoryRepositoryMock.Verify(x => x.Delete(It.Is<Category>(x => x.Id.ToString() == id)));
        _categoryRepositoryMock.Verify(x => x.Delete(It.IsAny<Category>()), Times.Once);
        _categoryRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    public async Task DeleteAsync_WithWrongId()
    {
        // Arrange


        // Act


        // Assert
    }
}
