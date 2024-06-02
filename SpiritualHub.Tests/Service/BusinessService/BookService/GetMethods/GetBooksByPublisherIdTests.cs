namespace SpiritualHub.Tests.Service.BusinessService.BookService.GetMethods;

using MockQueryable.Moq;

using Client.ViewModels.Book;
using Services.Mappings;
using Data.Configuration.Seed;
using Data.Models;
using Moq;

public class GetBooksByPublisherIdTests : MockConfiguration
{
    [Test]
    public async Task WhenHasBooks()
    {
        // Arrange
        var publisher = new SeedPublisherConfiguration().GenerateEntities().First();
        _books[0].PublisherID = publisher.Id;
        _books[1].PublisherID = publisher.Id;

        _books[0].Readers.Add(_users.First(u => u.Id == publisher.UserID));

        var books = new List<Book>()
        {
            _books[0],
            _books[1],
        };

        var expected = new List<BookViewModel>();
        _mapper.MapListToViewModel(books, expected);

        _bookRepositoryMock.Setup(x => x.AllAsNoTracking()).Returns(books.AsQueryable().BuildMock());

        // Act
        var result = await _bookService.GetBooksByPublisherIdAsync(publisher.Id.ToString(), publisher.UserID.ToString());

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.EqualTo(expected));

            var bookWithReader = result.First(b => b.Id == _books[0].Id.ToString());
            Assert.That(bookWithReader.HasBook, Is.True);
        });
        _bookRepositoryMock.Verify(x => x.AllAsNoTracking(), Times.Once);
    }

    [Test]
    public async Task WhenHasNoBooks()
    {
        // Arrange
        var publisher = new SeedPublisherConfiguration().GenerateEntities().First();

        _bookRepositoryMock.Setup(x => x.AllAsNoTracking()).Returns(new List<Book>().AsQueryable().BuildMock());

        // Act
        var result = await _bookService.GetBooksByPublisherIdAsync(publisher.Id.ToString(), publisher.UserID.ToString());

        // Assert
        Assert.That(result.Count(), Is.EqualTo(0));
        _bookRepositoryMock.Verify(x => x.AllAsNoTracking(), Times.Once);
    }

    public override void Setup()
    {
        GenerateEntities = true;
        base.Setup();
    }
}
