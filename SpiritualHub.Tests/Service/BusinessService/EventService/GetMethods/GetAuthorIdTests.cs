namespace SpiritualHub.Tests.Service.BusinessService.EventService.GetMethods;

using Moq;

public class GetAuthorIdTests : MockConfiguration
{
    [Test]
    public async Task WhenSuccess()
    {
        // Arrange
        var eventId = _events.First().Id.ToString();
        var authorId = _events.First().AuthorID.ToString();

        _eventRepositoryMock.Setup(x => x.GetAuthorIdAsync(It.Is<string>(x => x == eventId))).ReturnsAsync(authorId);

        // Act
        var result = await _eventService.GetAuthorIdAsync(eventId);

        // Assert
        Assert.That(result, Is.EqualTo(authorId));
        _eventRepositoryMock.Verify(x => x.GetAuthorIdAsync(It.Is<string>(x => x == eventId)));
    }

    [Test]
    public async Task WhenWrongId()
    {
        // Arrange
        var eventId = "wrongId";

        // Act
        var result = await _eventService.GetAuthorIdAsync(eventId);

        // Assert
        Assert.That(result, Is.Null);
        _eventRepositoryMock.Verify(x => x.GetAuthorIdAsync(It.Is<string>(x => x == eventId)));
    }
}
