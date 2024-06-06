namespace SpiritualHub.Tests.Service.BusinessService.EventService.GetMethods;

using Moq;

using Client.ViewModels.Event;

public class GetEventDetailsTests : MockConfiguration
{
    [Test]
    public async Task WhenSuccess_HasEvent()
    {
        // Arrange
        var eventEntity = GetEventWithParticipant();
        var expected = _mapper.Map<EventDetailsViewModel>(eventEntity);
        expected.IsUserJoined = true;

        var eventId = eventEntity.Id.ToString();
        var userId = _users.First().Id.ToString();

        _eventRepositoryMock.Setup(x => x.GetFullEventDetailsAsync(It.Is<string>(x => x == eventId))).ReturnsAsync(eventEntity);

        // Act
        var result = await _eventService.GetEventDetailsAsync(eventId, userId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.EqualTo(expected));
            Assert.That(result.IsUserJoined, Is.EqualTo(expected.IsUserJoined));
        });
        _eventRepositoryMock.Verify(x => x.GetFullEventDetailsAsync(It.Is<string>(x => x == eventId)));
    }

    [Test]
    public async Task WhenSuccess_DoesntHaveEvent()
    {
        // Arrange
        var eventEntity = _events.First();
        var expected = _mapper.Map<EventDetailsViewModel>(eventEntity);

        var eventId = eventEntity.Id.ToString();
        var userId = _users.First().Id.ToString();

        _eventRepositoryMock.Setup(x => x.GetFullEventDetailsAsync(It.Is<string>(x => x == eventId))).ReturnsAsync(eventEntity);

        // Act
        var result = await _eventService.GetEventDetailsAsync(eventId, userId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.EqualTo(expected));
            Assert.That(result.IsUserJoined, Is.EqualTo(expected.IsUserJoined));
        });
        _eventRepositoryMock.Verify(x => x.GetFullEventDetailsAsync(It.Is<string>(x => x == eventId)));
    }

    public override void Setup()
    {
        GenerateEntities = true;
        base.Setup();
    }
}
