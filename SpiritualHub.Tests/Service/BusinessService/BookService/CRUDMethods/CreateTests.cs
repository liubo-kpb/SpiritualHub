namespace SpiritualHub.Tests.Service.BusinessService.BookService.CRUDMethods;

using Moq;
using AutoMapper;

using Client.ViewModels.Book;
using Data.Models;

public class CreateTests : MockConfiguration
{
    private Mock<IMapper> _mapperMock;

    [Test]
    public async Task WhenSuccess()
    {
        // Arrange
        var newBookForm = new BookFormModel()
        {
            Title = "Test Title",
            Description = "Test Description",
            ShortDescription = "Test Short Description",
            Price = 123,
            IsHidden = false,
            ImageUrl = "*url*",
        };

        var newBookEntity = new Book()
        {
            Title = newBookForm.Title,
            Description = newBookForm.Description,
            ShortDescription = newBookForm.ShortDescription,
            Price = newBookForm.Price,
            IsHidden = newBookForm.IsHidden,
            Image = new Image() { URL = newBookForm.ImageUrl },
            AddedOn = DateTime.Now,
        };

        _mapperMock.Setup(x => x.Map<Book>(It.Is<BookFormModel>(x => x.Equals(newBookForm)))).Returns(newBookEntity);

        var expectedId = newBookEntity.Id.ToString();

        // Act
        var result = await _bookService.CreateAsync(newBookForm);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.EqualTo(expectedId));
            Assert.That(newBookEntity.Image.Name, Is.EqualTo(newBookForm.Title));
        });
        _mapperMock.Verify(x => x.Map<Book>(It.Is<BookFormModel>(x => x.Equals(newBookForm))));
        _bookRepositoryMock.Verify(x => x.AddAsync(It.Is<Book>(x => x.Equals(newBookEntity))));
        _bookRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    public override void OneTimeSetup()
    {
        _mapperMock = new Mock<IMapper>();
        _mapper = _mapperMock.Object;
    }
}
