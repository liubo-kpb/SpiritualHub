namespace SpiritualHub.Tests.Service.BusinessService.BookService.CRUDMethods;

using Moq;

using Data.Models;

using static Extensions.Common.TestErrorMessagesConstants;

public class DeleteTests : MockConfiguration
{
    [Test]
    public async Task WhenSuccess()
    {
        // Arrange
        var book = new Book()
        {
            Image = new Image(),
            Ratings = new List<Rating>()
        };

        var bookId = book.Id.ToString();

        _bookRepositoryMock.Setup(x => x.GetBookWithImageAndRatingsAsync(It.Is<string>(x => x == bookId))).ReturnsAsync(book);

        // Act
        await _bookService.DeleteAsync(bookId);

        // Assert
        _bookRepositoryMock.Verify(x => x.GetBookWithImageAndRatingsAsync(It.Is<string>(x => x == bookId)));

        _bookRepositoryMock.Verify(x => x.Delete(It.Is<Book>(x => x.Equals(book))));
        _imageRepositoryMock.Verify(x => x.Delete(It.Is<Image>(x => x.Equals(book.Image))));
        _ratingRepositoryMock.Verify(x => x.DeleteMultiple(It.Is<IEnumerable<Rating>>(x => x.Equals(book.Ratings))));

        _bookRepositoryMock.Verify(x => x.SaveChangesAsync());
    }

    [Test]
    public void WhenWrongId()
    {
        // Arrange
        var bookId = "wrongId";

        // Act & Assert
        Assert.ThrowsAsync<NullReferenceException>(async () => await _bookService.DeleteAsync(bookId));
    }

    [Test]
    public async Task WhenWrongId_MethodCallTest()
    {
        // Arrange
        var bookId = "wrongId";

        // Act
        try
        {
            await _bookService.DeleteAsync(bookId);
        }
        // Assert
        catch
        {
            _bookRepositoryMock.Verify(x => x.GetBookWithImageAndRatingsAsync(It.Is<string>(x => x == bookId)));

            _bookRepositoryMock.Verify(x => x.Delete(It.IsAny<Book>()));
            _imageRepositoryMock.Verify(x => x.Delete(It.IsAny<Image>()), Times.Never);
            _ratingRepositoryMock.Verify(x => x.DeleteMultiple(It.IsAny<IEnumerable<Rating>>()), Times.Never);

            _bookRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Never);

            return;
        }

        Assert.Fail(NoNullReferenceExceptionErrorMessage);
    }
}
