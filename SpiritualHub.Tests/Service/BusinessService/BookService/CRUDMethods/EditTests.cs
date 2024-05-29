namespace SpiritualHub.Tests.Service.BusinessService.BookService.CRUDMethods;

using Moq;

using Client.ViewModels.Book;
using Data.Models;

public class EditTests : MockConfiguration
{
    [Test]
    public async Task WhenSuccess()
    {
        // Arrange
        var book = new Book()
        {
            Title = "Title",
            Description = "Description",
            ShortDescription = "Short Description",
            Price = 123,
            IsHidden = true,
            Image = new Image { URL = "URL" },
            AddedOn = DateTime.Now,
            AuthorID = Guid.NewGuid(),
            PublisherID = Guid.NewGuid(),
            CategoryID = 1,
        };

        var updatedBook = new BookFormModel()
        {
            Id = book.Id.ToString(),
            Title = "title",
            Description = "description",
            ShortDescription = "short description",
            Price = 123.123m,
            IsHidden = false,
            ImageUrl = "url",
            AuthorId = Guid.NewGuid().ToString(),
            PublisherId = Guid.NewGuid().ToString(),
            CategoryId = 2,
        };

        var expected = _mapper.Map<Book>(updatedBook);
        expected.AddedOn = book.AddedOn;

        _bookRepositoryMock.Setup(x => x.GetBookInfoAsync(It.Is<string>(x => x == updatedBook.Id))).ReturnsAsync(book);

        // Act
        await _bookService.EditAsync(updatedBook);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(book, Is.EqualTo(expected));
            Assert.That(book.AuthorID, Is.EqualTo(expected.AuthorID));
            Assert.That(book.PublisherID, Is.EqualTo(expected.PublisherID));
            Assert.That(book.CategoryID, Is.EqualTo(expected.CategoryID));
            Assert.That(book.Image.URL, Is.EqualTo(expected.Image.URL));
        });
        _bookRepositoryMock.Verify(x => x.GetBookInfoAsync(It.Is<string>(x => x == updatedBook.Id)));
    }
}
