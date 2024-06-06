namespace SpiritualHub.Tests.Service.BusinessService.EventService.CRUDMethods;

using Moq;
using AutoMapper;

using Data.Models;
using Client.ViewModels.Event;

public class CreateTests : MockConfiguration
{
    private Mock<IMapper> _mapperMock = null!;

    [Test]
    public async Task WhenSuccess()
    {
        // Arrange
        var newEvent = new EventFormModel()
        {
            Title = "Test",
            Description = "Test Description",
            Price = 123,
            StartDateTime = DateTime.Now,
            EndDateTime = DateTime.Now,
            LocationName = "location",
            LocationUrl = "location url",
            IsOnline = true,
            ImageUrl = "Test URL",
        };
        var eventEntity = new Event()
        {
            Title = newEvent.Title,
            Description = newEvent.Description,
            Price = newEvent.Price,
            StartDateTime = newEvent.StartDateTime,
            EndDateTime = newEvent.EndDateTime,
            LocationName = newEvent.LocationName,
            LocationUrl = newEvent.LocationUrl,
            CreatedOn = DateTime.Now,
            IsOnline = newEvent.IsOnline,
            Image = new Image() { URL = newEvent.ImageUrl },

        };

        _mapperMock.Setup(x => x.Map<Event>(It.Is<EventFormModel>(x => x.Equals(newEvent)))).Returns(eventEntity);

        // Act
        var result = await _eventService.CreateAsync(newEvent);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.EqualTo(eventEntity.Id.ToString()));
            Assert.That(eventEntity.Image.Name, Is.EqualTo(eventEntity.Title));
        });
        _mapperMock.Verify(x => x.Map<Event>(It.Is<EventFormModel>(x => x.Equals(newEvent))), Times.Once);
        _eventRepositoryMock.Verify(x => x.AddAsync(It.Is<Event>(x => x.Equals(eventEntity))), Times.Once);
        _eventRepositoryMock.Verify(x => x.SaveChangesAsync());
    }

    public override void OneTimeSetup()
    {
        _mapperMock = new Mock<IMapper>();
        _mapper = _mapperMock.Object;

        GenerateEntities = false;
    }
}
