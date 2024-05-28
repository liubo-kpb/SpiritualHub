namespace SpiritualHub.Tests.Service.BusinessService.AuthorService.CRUDMethods;

using Microsoft.EntityFrameworkCore;
using Moq;
using AutoMapper;

using Data.Models;
using Client.ViewModels.Author;

using static Extensions.Common.TestErrorMessagesConstants;

public class CreateTests : MockConfiguration
{
    private Mock<IMapper> _mapperMock;

    [Test]
    public async Task WhenSuccess()
    {
        // Arrange
        var testPublisher = _publishers.First();
        var newAuthor = GetAuthorFormModel(testPublisher);

        var authorEntity = new Author()
        {
            Alias = newAuthor.Alias,
            Name = newAuthor.Name,
            Description = newAuthor.Description,
            AddedOn = DateTime.Now,
        };

        _authorRepositoryMock.Setup(x => x.AddAsync(It.Is<Author>(x => x.Equals(authorEntity)))).ReturnsAsync(true);
        _mapperMock.Setup(x => x.Map<Author>(It.Is<AuthorFormModel>(x => x.Equals(newAuthor)))).Returns(authorEntity);

        string expected = authorEntity.Id.ToString();

        // Act
        var result = await _authorService.CreateAsync(newAuthor, testPublisher);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
        _authorRepositoryMock.Verify(x => x.AddAsync(It.Is<Author>(x => x.Equals(authorEntity))), Times.Once);
        _authorRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        _mapperMock.Verify(x => x.Map<Author>(It.Is<AuthorFormModel>(x => x.Equals(newAuthor))), Times.Once);
    }

    [Test]
    public void WhenFail_NullPublisher_ThrowTest()
    {
        // Arrange
        var testAuthor = GetAuthorFormModel();
        testAuthor.PublisherId = null!;

        var authorEntity = new Author()
        {
            Alias = testAuthor.Alias,
            Name = testAuthor.Name,
            Description = testAuthor.Description,
            AddedOn = DateTime.Now,
        };

        _mapperMock.Setup(x => x.Map<Author>(It.Is<AuthorFormModel>(x => x.Equals(testAuthor)))).Returns(authorEntity);
        _authorRepositoryMock.Setup(x => x.SaveChangesAsync()).Throws<DbUpdateException>();

        // Act & Assert
        Assert.ThrowsAsync<DbUpdateException>(async () => await _authorService.CreateAsync(testAuthor, null!));
    }

    [Test]
    public async Task WhenFail_NullPublisher_MethodCallTest()
    {
        // Arrange
        var testAuthor = GetAuthorFormModel();

        var authorEntity = new Author()
        {
            Alias = testAuthor.Alias,
            Name = testAuthor.Name,
            Description = testAuthor.Description,
            AddedOn = DateTime.Now,
        };

        _mapperMock.Setup(x => x.Map<Author>(It.Is<AuthorFormModel>(x => x.Equals(testAuthor)))).Returns(authorEntity);
        _authorRepositoryMock.Setup(x => x.SaveChangesAsync()).Throws<DbUpdateException>();

        // Act
        try
        {
            await _authorService.CreateAsync(testAuthor, null!);
        }
        // Assert
        catch (Exception)
        {
            _mapperMock.Verify(x => x.Map<Author>(It.Is<AuthorFormModel>(x => x.Equals(testAuthor))), Times.Once);
            _authorRepositoryMock.Verify(x => x.AddAsync(It.Is<Author>(x => x.Equals(authorEntity))), Times.Once);
            _authorRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);

            return;
        }

        Assert.Fail(NotDbUpdateExceptionErrorMessage);
    }

    private AuthorFormModel GetAuthorFormModel(Publisher publisher = null!)
    {
        return new AuthorFormModel()
        {
            Alias = "Test Alias",
            Name = "Test Name",
            Description = "Description",
            PublisherId = publisher?.Id.ToString() ?? null,
            CategoryId = 1,
        };
    }

    public override void OneTimeSetup()
    {
        _mapperMock = new Mock<IMapper>();
        _mapper = _mapperMock.Object;
    }
}
