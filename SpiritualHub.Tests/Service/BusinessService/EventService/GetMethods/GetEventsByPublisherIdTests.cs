namespace SpiritualHub.Tests.Service.BusinessService.EventService.GetMethods;

using MockQueryable.Moq;

using Client.ViewModels.Event;
using Services.Mappings;
using Data.Configuration.Seed;
using Data.Models;
using Moq;

public class GetEventsByPublisherIdTests : MockConfiguration
{
    [Test]
    public async Task WhenHasEvents()
    {
        // Arrange
        var publisher = new SeedPublisherConfiguration().GenerateEntities().First();
        _events[0].PublisherID = Guid.NewGuid();

        var events = new List<Event>()
        {
            _events[1],
            _events[2],
        };

        var expected = new List<EventViewModel>();
        _mapper.MapListToViewModel(events, expected);

        _eventRepositoryMock.Setup(x => x.AllAsNoTracking()).Returns(_events.AsQueryable().BuildMock());

        // Act
        var result = await _eventService.GetEventsByPublisherIdAsync(publisher.Id.ToString());

        // Assert
        Assert.That(result, Is.EqualTo(expected));
        _eventRepositoryMock.Verify(x => x.AllAsNoTracking(), Times.Once);
    }

    [Test]
    public async Task WhenHasNoEvents()
    {
        // Arrange
        var publisher = new SeedPublisherConfiguration().GenerateEntities().First();

        _eventRepositoryMock.Setup(x => x.AllAsNoTracking()).Returns(new List<Event>().AsQueryable().BuildMock());

        // Act
        var result = await _eventService.GetEventsByPublisherIdAsync(publisher.Id.ToString());

        // Assert
        Assert.That(result.Count(), Is.EqualTo(0));
        _eventRepositoryMock.Verify(x => x.AllAsNoTracking(), Times.Once);
    }
}
