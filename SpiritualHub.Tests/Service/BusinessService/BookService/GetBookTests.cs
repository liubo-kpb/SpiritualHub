namespace SpiritualHub.Tests.Service.BusinessService.BookService;

using Moq;

using static Extensions.Common.TestErrorMessagesConstants;


public class GetBookTests : MockConfiguration
{
    [Test]
    public async Task WhenSuccess()
    {
        // Arrange
        var book = _books.First();
        var user = _users.First();

        var bookId = book.Id.ToString();
        var userId = user.Id.ToString();

        _userRepositoryMock.Setup(x => x.GetSingleByIdAsync(It.Is<string>(x => x == userId))).ReturnsAsync(user);
        _bookRepositoryMock.Setup(x => x.GetSingleByIdAsync(It.Is<string>(x => x == bookId))).ReturnsAsync(book);

        int expectedUserBooksCount = user.Books.Count + 1;

        // Act
        await _bookService.GetAsync(bookId, userId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(user.Books.Any(b => b.Id == book.Id));
            Assert.That(user.Books, Has.Count.EqualTo(expectedUserBooksCount));
        });
        _userRepositoryMock.Verify(x => x.GetSingleByIdAsync(It.Is<string>(x => x == userId)));
        _bookRepositoryMock.Verify(x => x.GetSingleByIdAsync(It.Is<string>(x => x == bookId)));
        _bookRepositoryMock.Verify(x => x.SaveChangesAsync());
    }

    [Test]
    public async Task WhenFail_WrongBookId()
    {
        // Arrange
        var user = _users.First();

        var bookId = "wrongId";
        var userId = user.Id.ToString();

        _userRepositoryMock.Setup(x => x.GetSingleByIdAsync(It.Is<string>(x => x == userId))).ReturnsAsync(user);

        int expectedUserBooksCount = user.Books.Count + 1;

        // Act
        await _bookService.GetAsync(bookId, userId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(user.Books.All(b => b != null && b.Id.ToString() != bookId), Is.False);
            Assert.That(user.Books, Has.Count.EqualTo(expectedUserBooksCount));
        });
        _userRepositoryMock.Verify(x => x.GetSingleByIdAsync(It.Is<string>(x => x == userId)));
        _bookRepositoryMock.Verify(x => x.GetSingleByIdAsync(It.Is<string>(x => x == bookId)));
        _bookRepositoryMock.Verify(x => x.SaveChangesAsync());
    }

    [Test]
    public void WhenFail_WrongUserId_ThrowTest()
    {
        // Arrange
        var book = _books.First();

        var bookId = book.Id.ToString();
        var userId = "wrongId";

        _bookRepositoryMock.Setup(x => x.GetSingleByIdAsync(It.Is<string>(x => x == bookId))).ReturnsAsync(book);

        // Act & Assert
        Assert.ThrowsAsync<NullReferenceException>(async () => await _bookService.GetAsync(bookId, userId));
    }

    [Test]
    public async Task WhenFail_WrongUserId_MethodCallTest()
    {
        // Arrange
        var book = _books.First();

        var bookId = book.Id.ToString();
        var userId = "wrongId";

        _bookRepositoryMock.Setup(x => x.GetSingleByIdAsync(It.Is<string>(x => x == bookId))).ReturnsAsync(book);

        // Act
        try
        {
            await _bookService.GetAsync(bookId, userId);
        }
        // Assert
        catch (NullReferenceException)
        {
            _userRepositoryMock.Verify(x => x.GetSingleByIdAsync(It.Is<string>(x => x == userId)));
            _bookRepositoryMock.Verify(x => x.GetSingleByIdAsync(It.Is<string>(x => x == bookId)));
            _bookRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Never);

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
