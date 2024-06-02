namespace SpiritualHub.Tests.Service.BusinessService.BookService.GetMethods;

using Moq;

using Client.ViewModels.Book;

public class GetBookDetailsTests : MockConfiguration
{
    [Test]
    public async Task WhenSuccess_HasBook()
    {
        // Arrange
        var book = GetBookWithReader();
        var expected = _mapper.Map<BookDetailsViewModel>(book);
        expected.HasBook = true;

        var bookId = book.Id.ToString();
        var userId = _users.First().Id.ToString();

        _bookRepositoryMock.Setup(x => x.GetFullBookDetailsAsync(It.Is<string>(x => x == bookId))).ReturnsAsync(book);

        // Act
        var result = await _bookService.GetBookDetailsAsync(bookId, userId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.EqualTo(expected));
            Assert.That(result.HasBook, Is.EqualTo(expected.HasBook));
        });
        _bookRepositoryMock.Verify(x => x.GetFullBookDetailsAsync(It.Is<string>(x => x == bookId)));
    }

    [Test]
    public async Task WhenSuccess_DoesntHaveBook()
    {
        // Arrange
        var book = _books.First();
        var expected = _mapper.Map<BookDetailsViewModel>(book);

        var bookId = book.Id.ToString();
        var userId = _users.First().Id.ToString();

        _bookRepositoryMock.Setup(x => x.GetFullBookDetailsAsync(It.Is<string>(x => x == bookId))).ReturnsAsync(book);

        // Act
        var result = await _bookService.GetBookDetailsAsync(bookId, userId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.EqualTo(expected));
            Assert.That(result.HasBook, Is.EqualTo(expected.HasBook));
        });
        _bookRepositoryMock.Verify(x => x.GetFullBookDetailsAsync(It.Is<string>(x => x == bookId)));
    }

    public override void Setup()
    {
        GenerateEntities = true;
        base.Setup();
    }
}
