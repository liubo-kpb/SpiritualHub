namespace SpiritualHub.Tests.Service.BusinessService.EventService.GetMethods;

using MockQueryable.Moq;

using Client.ViewModels.Event;

public class AllEventsByUserIdTests : MockConfiguration
{
    [Test]
    public async Task WhenHasJoinedEvent()
    {
        // Arrange
        var eventWithReader = GetEventWithParticipant();
        var userId = eventWithReader.Participants.First().Id.ToString();

        _eventRepositoryMock.Setup(x => x.AllAsNoTracking()).Returns(_events.AsQueryable().BuildMock());

        var eventViewModel = _mapper.Map<EventViewModel>(eventWithReader);

        ICollection<EventViewModel> expected = new List<EventViewModel>()
        {
            eventViewModel,
        };
        eventWithReader = _events[2];
        eventWithReader.Participants.Add(_users.First());

        eventViewModel = _mapper.Map<EventViewModel>(eventWithReader);

        expected.Add(eventViewModel);

        // Act
        var result = await _eventService.AllEventsByUserIdAsync(userId);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
        _eventRepositoryMock.Verify(x => x.AllAsNoTracking());
    }

    [Test]
    public async Task WhenHasNoEvents()
    {
        // Arrange
        var userId = "userId";
        var expected = new List<EventViewModel>();

        _eventRepositoryMock.Setup(x => x.AllAsNoTracking()).Returns(_events.AsQueryable().BuildMock());

        // Act
        var result = await _eventService.AllEventsByUserIdAsync(userId);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
        _eventRepositoryMock.Verify(x => x.AllAsNoTracking());
    }

    public override void Setup()
    {
        GenerateEntities = true;
        base.Setup();
    }
}
