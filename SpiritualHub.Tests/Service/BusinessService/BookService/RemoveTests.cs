namespace SpiritualHub.Tests.Service.BusinessService.BookService;

using Moq;

using Data.Models;

using static Extensions.Common.TestErrorMessagesConstants;

public class RemoveTests : MockConfiguration
{
    [Test]
    public async Task WhenSuccess()
    {
        // Arrange
        var user = _users.First();
        var book = GetBookWithReader(user);

        var bookId = book.Id.ToString();
        var userId = book.Readers.First().Id.ToString();

        int expectedReadersCount = book.Readers.Count - 1;

        _bookRepositoryMock.Setup(x => x.GetBookWithReaders(It.Is<string>(x => x == bookId))).ReturnsAsync(book);
        _userRepositoryMock.Setup(x => x.GetSingleByIdAsync(It.Is<string>(x => x == userId))).ReturnsAsync(user);

        // Act
        await _bookService.RemoveAsync(bookId, userId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(book.Readers, Has.Count.EqualTo(expectedReadersCount));
            Assert.That(book.Readers.Any(u => u.Id == user.Id), Is.False);
        });
        _bookRepositoryMock.Verify(x => x.GetBookWithReaders(It.Is<string>(x => x == bookId)));
        _userRepositoryMock.Verify(x => x.GetSingleByIdAsync(It.Is<string>(x => x == userId)));
        _userRepositoryMock.Verify(x => x.SaveChangesAsync());
    }

    [Test]
    public async Task WhenUserDoesNotExist()
    {
        // Arrange
        var user = _users.First();
        var book = GetBookWithReader(user);

        var bookId = book.Id.ToString();
        var userId = "wrongId";

        int expectedReadersCount = book.Readers.Count;

        _bookRepositoryMock.Setup(x => x.GetBookWithReaders(It.Is<string>(x => x == bookId))).ReturnsAsync(book);

        // Act
        await _bookService.RemoveAsync(bookId, userId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(book.Readers, Has.Count.EqualTo(expectedReadersCount));
        });
        _bookRepositoryMock.Verify(x => x.GetBookWithReaders(It.Is<string>(x => x == bookId)));
        _userRepositoryMock.Verify(x => x.GetSingleByIdAsync(It.Is<string>(x => x == userId)));
        _userRepositoryMock.Verify(x => x.SaveChangesAsync());
    }

    [Test]
    public void WhenBookDoesNotExist_ThrowTest()
    {
        // Arrange
        var user = new ApplicationUser();

        var bookId = "wrongId";
        var userId = user.Id.ToString();

        _userRepositoryMock.Setup(x => x.GetSingleByIdAsync(It.Is<string>(x => x == userId))).ReturnsAsync(user);

        // Act & Assert
        Assert.ThrowsAsync<NullReferenceException>(async () => await _bookService.RemoveAsync(bookId, userId), NoNullReferenceExceptionErrorMessage);
    }

    [Test]
    public async Task WhenBookDoesNotExist_MethodCallTest()
    {
        // Arrange
        var user = new ApplicationUser();

        var bookId = "wrongId";
        var userId = user.Id.ToString();

        _userRepositoryMock.Setup(x => x.GetSingleByIdAsync(It.Is<string>(x => x == userId))).ReturnsAsync(user);

        // Act
        try
        {
            await _bookService.RemoveAsync(bookId, userId);
        }
        catch (NullReferenceException)
        {
            _bookRepositoryMock.Verify(x => x.GetBookWithReaders(It.Is<string>(x => x == bookId)));
            _userRepositoryMock.Verify(x => x.GetSingleByIdAsync(It.Is<string>(x => x == userId)));
            _userRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Never);

            return;
        }
        catch (Exception)
        {

        }

        Assert.Fail(NoNullReferenceExceptionErrorMessage);
    }

    public override void Setup()
    {
        GenerateEntities = true;
        base.Setup();
    }
}
