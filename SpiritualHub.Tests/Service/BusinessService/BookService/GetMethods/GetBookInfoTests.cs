namespace SpiritualHub.Tests.Service.BusinessService.BookService.GetMethods;

using Moq;

using Client.ViewModels.Book;

public class GetBookInfoTests : MockConfiguration
{
    [Test]
    public async Task WhenSuccess()
    {
        // Arrange
        var book = _books.First();
        var bookId = book.Id.ToString();

        var expected = _mapper.Map<BookFormModel>(book);

        _bookRepositoryMock.Setup(x => x.GetBookInfoAsync(It.Is<string>(x => x == bookId))).ReturnsAsync(book);

        // Act
        var result = await _bookService.GetBookInfoAsync(bookId);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
        _bookRepositoryMock.Verify(x => x.GetBookInfoAsync(It.Is<string>(x => x == bookId)));
    }

    [Test]
    public async Task WhenFail_WithWrongId()
    {
        // Arrange
        var bookId = "wrongId";

        // Act
        var result = await _bookService.GetBookInfoAsync(bookId);

        // Assert
        Assert.That(result, Is.Null);
        _bookRepositoryMock.Verify(x => x.GetBookInfoAsync(It.Is<string>(x => x == bookId)));
    }

    public override void Setup()
    {
        GenerateEntities = true;
        base.Setup();
    }
}
