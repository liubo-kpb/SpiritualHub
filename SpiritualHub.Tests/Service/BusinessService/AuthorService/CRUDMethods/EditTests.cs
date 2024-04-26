namespace SpiritualHub.Tests.Service.BusinessService.AuthorService.CRUDMethods;

using Moq;
using Client.ViewModels.Author;
using Data.Models;

public class EditTests : MockConfiguration
{
    [Test]
    public async Task WhenSuccess()
    {
        // Arrange
        var author = new Author()
        {
            Id = Guid.NewGuid(),
            Alias = "Test a",
            Name = "Test n",
            Description = "Test d",
            IsActive = false,
            CategoryID = 2,
            AddedOn = DateTime.Now,
            AvatarImage = new Image() { Name = "name.png", URL = "url" }
        };

        var editedAuthor = new AuthorFormModel()
        {
            Id = author.Id.ToString(),
            Alias = "Test A",
            Name = "Test N",
            Description = "Test D",
            IsActive = true,
            CategoryId = 1,
            AvatarImageUrl = "URL"
        };

        var expected = _mapper.Map<Author>(editedAuthor);
        expected.AddedOn = author.AddedOn;

        _authorRepositoryMock.Setup(x => x.GetAuthorDetailsByIdAsync(It.Is<string>(x => x == editedAuthor.Id))).ReturnsAsync(author);

        // Act
        await _authorService.EditAsync(editedAuthor);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(author, Is.EqualTo(expected), "Author was not updated correctly. Some or all elements did not match.");
            Assert.That(author.CategoryID, Is.EqualTo(expected.CategoryID), "CategoryID was not updated.");
            Assert.That(author.AvatarImage.URL, Is.EqualTo(expected.AvatarImage.URL), "Image URL was not updated.");
        });
        _authorRepositoryMock.Verify(x => x.GetAuthorDetailsByIdAsync(It.Is<string>(x => x == editedAuthor.Id)), Times.Once);
        _authorRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

}
