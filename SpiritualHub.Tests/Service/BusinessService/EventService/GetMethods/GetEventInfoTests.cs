namespace SpiritualHub.Tests.Service.BusinessService.EventService.GetMethods;

using Moq;

using Client.ViewModels.Event;

public class GetEventInfoTests : MockConfiguration
{
    [Test]
    public async Task WhenSuccess()
    {
        // Arrange
        var eventEntity = _events.First();
        var eventId = eventEntity.Id.ToString();

        var expected = _mapper.Map<EventFormModel>(eventEntity);

        _eventRepositoryMock.Setup(x => x.GetEventInfoAsync(It.Is<string>(x => x == eventId))).ReturnsAsync(eventEntity);

        // Act
        var result = await _eventService.GetEventInfoAsync(eventId);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
        _eventRepositoryMock.Verify(x => x.GetEventInfoAsync(It.Is<string>(x => x == eventId)));
    }

    [Test]
    public async Task WhenFail_WithWrongId()
    {
        // Arrange
        var eventId = "wrongId";

        // Act
        var result = await _eventService.GetEventInfoAsync(eventId);

        // Assert
        Assert.That(result, Is.Null);
        _eventRepositoryMock.Verify(x => x.GetEventInfoAsync(It.Is<string>(x => x == eventId)));
    }
}
