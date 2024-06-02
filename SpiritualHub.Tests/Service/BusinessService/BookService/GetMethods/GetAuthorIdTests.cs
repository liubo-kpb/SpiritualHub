namespace SpiritualHub.Tests.Service.BusinessService.BookService.GetMethods;

using Moq;

public class GetAuthorIdTests : MockConfiguration
{
    [Test]
    public async Task WhenSuccess()
    {
        // Arrange
        var bookId = _books.First().Id.ToString();
        var authorId = _books.First().AuthorID.ToString();

        _bookRepositoryMock.Setup(x => x.GetBookAuthorId(It.Is<string>(x => x == bookId))).ReturnsAsync(authorId);

        // Act
        var result = await _bookService.GetAuthorIdAsync(bookId);

        // Assert
        Assert.That(result, Is.EqualTo(authorId));
        _bookRepositoryMock.Verify(x => x.GetBookAuthorId(It.Is<string>(x => x == bookId)));
    }

    [Test]
    public async Task WhenWrongId()
    {
        // Arrange
        var bookId = "wrongId";

        // Act
        var result = await _bookService.GetAuthorIdAsync(bookId);

        // Assert
        Assert.That(result, Is.Null);
        _bookRepositoryMock.Verify(x => x.GetBookAuthorId(It.Is<string>(x => x == bookId)));
    }


    public override void Setup()
    {
        GenerateEntities = true;
        base.Setup();
    }
}
