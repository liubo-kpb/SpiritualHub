namespace SpiritualHub.Tests.Service.BusinessService.BookService;

using Moq;

using Data.Models;

using static Extensions.Common.TestErrorMessagesConstants;

public class HIdeTests : MockConfiguration
{
    [Test]
    public async Task WhenSuccess()
    {
        // Arrange
        var book = new Book()
        {
            IsHidden = false,
        };

        var bookId = book.Id.ToString();

        _bookRepositoryMock.Setup(x => x.GetSingleByIdAsync(It.Is<string>(x => x == bookId))).ReturnsAsync(book);

        // Act
        await _bookService.HideAsync(bookId);

        // Assert
        Assert.That(book.IsHidden, Is.True);
        _bookRepositoryMock.Verify(x => x.GetSingleByIdAsync(It.Is<string>(x => x == bookId)), Times.Once);
        _bookRepositoryMock.Verify(x => x.SaveChangesAsync());
    }

    [Test]
    public void WhenWrongId_ThrowTest()
    {
        // Arrange

        var bookId = "wrongId";

        // Act & Assert
        Assert.ThrowsAsync<NullReferenceException>(async () => await _bookService.HideAsync(bookId));
    }

    [Test]
    public async Task WhenWrongId_MethodCallTest()
    {
        // Arrange
        var bookId = "wrongId";

        // Act
        try
        {
            await _bookService.HideAsync(bookId);
        }
        // Assert
        catch (NullReferenceException)
        {
            _bookRepositoryMock.Verify(x => x.GetSingleByIdAsync(It.Is<string>(x => x == bookId)), Times.Once);
            _bookRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Never);
            return;
        }
        catch (Exception)
        {

        }

        Assert.Fail(NoNullReferenceExceptionErrorMessage);
    }
}
