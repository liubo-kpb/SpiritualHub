namespace SpiritualHub.Tests.Service.BusinessService.AuthorService;

using Moq;

public class AddRemovePublisherTests : MockConfiguration
{
    [Test]
    public async Task Add_WhenSuccess()
    {
        // Arrange
        var testAuthor = _authors.First();
        var publisher = _publishers.First();

        string authorId = testAuthor.Id.ToString();

        _authorRepositoryMock.Setup(x => x.GetAuthorWithPublishersAsync(It.Is<string>(x => x == authorId))).ReturnsAsync(testAuthor);

        int expectedPublisherCount = testAuthor.Publishers.Count + 1;

        // Act
        await _authorService.AddPublisherAsync(authorId, publisher);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(testAuthor.Publishers, Has.Count.EqualTo(expectedPublisherCount), "New publisher count doesn't match expected.");
            Assert.That(testAuthor.Publishers.Any(p => p.Id == publisher.Id), "Delegated publisher was not connected to author.");
        });
        _authorRepositoryMock.Verify(x => x.GetAuthorWithPublishersAsync(It.Is<string>(x => x == authorId)));
        _authorRepositoryMock.Verify(x => x.SaveChangesAsync());
    }

    [Test]
    public async Task Remove_WhenSuccess()
    {
        // Arrange
        var testAuthor = _authors.First();
        var testPublisher = _publishers.First();
        testAuthor.Publishers.Add(testPublisher);

        string authorId = testAuthor.Id.ToString();
        string publisherId = testPublisher.Id.ToString();

        _authorRepositoryMock.Setup(x => x.GetAuthorWithPublishersAsync(It.Is<string>(x => x == authorId))).ReturnsAsync(testAuthor);

        int expectedPublisherCount = testAuthor.Publishers.Count - 1;

        // Act
        await _authorService.RemovePublisherAsync(authorId, publisherId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(testAuthor.Publishers, Has.Count.EqualTo(expectedPublisherCount), "New publisher count doesn't match expected.");
            Assert.That(testAuthor.Publishers.All(p => p.Id != testPublisher.Id && !p.Equals(testPublisher)), "Delegated publisher was not removed from author.");
        });
        _authorRepositoryMock.Verify(x => x.GetAuthorWithPublishersAsync(It.Is<string>(x => x == authorId)));
        _authorRepositoryMock.Verify(x => x.SaveChangesAsync());
    }

    [Test]
    public async Task Remove_WithMorePublishers()
    {
        // Arrange
        var testAuthor = _authors.First();
        var testPublisher = _publishers.First();
        var testPublisher2 = _publishers[1];
        testAuthor.Publishers.Add(testPublisher);
        testAuthor.Publishers.Add(testPublisher2);

        string authorId = testAuthor.Id.ToString();
        string publisherId = testPublisher.Id.ToString();

        _authorRepositoryMock.Setup(x => x.GetAuthorWithPublishersAsync(It.Is<string>(x => x == authorId))).ReturnsAsync(testAuthor);

        int expectedPublisherCount = testAuthor.Publishers.Count - 1;

        // Act
        await _authorService.RemovePublisherAsync(authorId, publisherId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(testAuthor.Publishers, Has.Count.EqualTo(expectedPublisherCount), "New publisher count doesn't match expected.");
            Assert.That(testAuthor.Publishers.All(p => p.Id != testPublisher.Id && !p.Equals(testPublisher)), "Delegated publisher was not removed from author.");
            Assert.That(testAuthor.Publishers.Any(p => p.Id == testPublisher2.Id && p.Equals(testPublisher2)), "The wrong publisher was removed from author.");
        });
        _authorRepositoryMock.Verify(x => x.GetAuthorWithPublishersAsync(It.Is<string>(x => x == authorId)));
        _authorRepositoryMock.Verify(x => x.SaveChangesAsync());
    }

    [Test]
    public async Task Remove_WithInvalidPublisherId()
    {
        // Arrange
        var testAuthor = _authors.First();
        var testPublisher = _publishers.First();
        var testPublisher2 = _publishers[1];
        testAuthor.Publishers.Add(testPublisher);
        testAuthor.Publishers.Add(testPublisher2);

        string authorId = testAuthor.Id.ToString();
        string publisherId = "invalid Id";

        _authorRepositoryMock.Setup(x => x.GetAuthorWithPublishersAsync(It.Is<string>(x => x == authorId))).ReturnsAsync(testAuthor);

        int expectedPublisherCount = testAuthor.Publishers.Count;

        // Act
        await _authorService.RemovePublisherAsync(authorId, publisherId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(testAuthor.Publishers, Has.Count.EqualTo(expectedPublisherCount), "New publisher count doesn't match expected.");
            Assert.That(testAuthor.Publishers.Any(p => p.Id == testPublisher.Id && p.Equals(testPublisher)), "Delegated publisher was removed from author.");
            Assert.That(testAuthor.Publishers.Any(p => p.Id == testPublisher2.Id && p.Equals(testPublisher2)), "The wrong publisher was removed from author.");
        });
        _authorRepositoryMock.Verify(x => x.GetAuthorWithPublishersAsync(It.Is<string>(x => x == authorId)));
        _authorRepositoryMock.Verify(x => x.SaveChangesAsync());
    }
}
