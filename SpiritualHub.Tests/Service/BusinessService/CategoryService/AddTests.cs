﻿namespace SpiritualHub.Tests.Service.BusinessService.CategoryService;

using Microsoft.EntityFrameworkCore;
using Moq;

using Data.Models;

using System.Linq.Expressions;

public class AddTests : MockConfiguration
{
    [Test]
    public async Task WhenSuccess()
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
    [TestCase(null)]
    [TestCase("")]
    [TestCase("      ")]
    public async Task WhenInvalidName(string name)
    {
        // Arrange
        _categoryRepositoryMock.Setup(x => x.SaveChangesAsync()).Throws<DbUpdateException>();

        // Act
        await _categoryService.AddAsync(name);

        // Assert
        _categoryRepositoryMock.Verify(x => x.AddAsync(It.Is<Category>(x => x.Name == name)), Times.Never);
        _categoryRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Never);
    }

    [Test]
    public async Task WithConcurrentName()
    {
        // Arrange
        string name = "Hindu";
        _categoryRepositoryMock.Setup(x => x.AddAsync(It.Is<Category>(x => x.Name == name)));
        _categoryRepositoryMock
            .Setup(x => x.AnyAsync(It.IsAny<Expression<Func<Category, bool>>>()))
            .Returns((Expression<Func<Category, bool>> predicate) => Task.FromResult(true));
        _categoryRepositoryMock.Setup(x => x.SaveChangesAsync()).Throws<DbUpdateException>();

        // Act
        await _categoryService.AddAsync(name);

        // Assert
        _categoryRepositoryMock.Verify(x => x.AddAsync(It.Is<Category>(x => x.Name == name)), Times.Never);
        _categoryRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Never);
    }
}
