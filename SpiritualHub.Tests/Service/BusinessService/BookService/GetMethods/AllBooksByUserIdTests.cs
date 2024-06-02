namespace SpiritualHub.Tests.Service.BusinessService.BookService.GetMethods;

using MockQueryable.Moq;

using Client.ViewModels.Book;

public class AllBooksByUserIdTests : MockConfiguration
{
    [Test]
    public async Task WhenHasBook_OneIsHidden()
    {
        // Arrange
        var bookWithReader = GetBookWithReader();
        var userId = bookWithReader.Readers.First().Id.ToString();

        _bookRepositoryMock.Setup(x => x.AllAsNoTracking()).Returns(_books.AsQueryable().BuildMock());

        var bookViewModel = _mapper.Map<BookViewModel>(bookWithReader);

        ICollection<BookViewModel> expected = new List<BookViewModel>()
        {
            bookViewModel,
        };
        bookWithReader = _books[2];
        bookWithReader.Readers.Add(_users.First());
        bookWithReader.IsHidden = true;

        bookViewModel = _mapper.Map<BookViewModel>(bookWithReader);

        expected.Add(bookViewModel);

        // Act
        var result = await _bookService.AllBooksByUserIdAsync(userId);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
        _bookRepositoryMock.Verify(x => x.AllAsNoTracking());
    }

    [Test]
    public async Task WhenHasNoBooks()
    {
        // Arrange
        var userId = "userId";
        var expected = new List<BookViewModel>();

        _bookRepositoryMock.Setup(x => x.AllAsNoTracking()).Returns(_books.AsQueryable().BuildMock());


        // Act
        var result = await _bookService.AllBooksByUserIdAsync(userId);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
        _bookRepositoryMock.Verify(x => x.AllAsNoTracking());
    }

    public override void Setup()
    {
        GenerateEntities = true;
        base.Setup();
    }
}
