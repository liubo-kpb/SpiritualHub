namespace SpiritualHub.Tests.Service.BusinessService.EventService.CRUDMethods;

using Moq;

using Client.ViewModels.Event;
using Data.Models;

using static Extensions.Common.TestErrorMessagesConstants;

public class EditTests : MockConfiguration
{
    [Test]
    public async Task WhenSuccess()
    {
        // Arrange
        var eventEntity = GetEventEntity();
        var eventFormModel = GetEventFormModel(eventEntity.Id);
        var expected = _mapper.Map<Event>(eventFormModel);

        _eventRepositoryMock.Setup(x => x.GetEventInfoAsync(It.Is<string>(x => x == eventFormModel.Id))).ReturnsAsync(eventEntity);

        // Act
        await _eventService.EditAsync(eventFormModel);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventEntity, Is.EqualTo(expected));
            Assert.That(eventEntity.Image.URL, Is.EqualTo(expected.Image.URL));
            Assert.That(eventEntity.CategoryID, Is.EqualTo(expected.CategoryID));
            Assert.That(eventEntity.AuthorID, Is.EqualTo(expected.AuthorID));
            Assert.That(eventEntity.PublisherID, Is.EqualTo(expected.PublisherID));
        });
        _eventRepositoryMock.Verify(x => x.GetEventInfoAsync(It.Is<string>(x => x == eventFormModel.Id)));
        _eventRepositoryMock.Verify(x => x.SaveChangesAsync());
    }

    [Test]
    public void WithWrongId_ThrowTest()
    {
        // Arrange
        var eventFormModel = new EventFormModel()
        {
            Id = "wrongId",
        };

        // Act & Assert
        Assert.ThrowsAsync<NullReferenceException>(async () => await _eventService.EditAsync(eventFormModel), NoNullReferenceExceptionErrorMessage);
    }

    [Test]
    public async Task WithWrongId_MethodCallTest()
    {
        // Arrange
        var eventFormModel = new EventFormModel()
        {
            Id = "wrongId",
        };

        // Act
        try
        {
            await _eventService.EditAsync(eventFormModel);
        }
        // Assert
        catch (NullReferenceException)
        {
            _eventRepositoryMock.Verify(x => x.GetEventInfoAsync(It.Is<string>(x => x == eventFormModel.Id)), Times.Once);
            _eventRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Never);

            return;
        }
        catch (Exception)
        {
        }

        Assert.Fail(NoNullReferenceExceptionErrorMessage);
    }

    public override void OneTimeSetup()
    {
        GenerateEntities = false;
        base.OneTimeSetup();
    }

    private static EventFormModel GetEventFormModel(Guid eventId)
    {
        return new EventFormModel()
        {
            Id = eventId.ToString(),
            Title = "Test Name",
            Description = "Test Description",
            Price = 123.4m,
            StartDateTime = DateTime.Now,
            EndDateTime = DateTime.Now,
            LocationName = "test location",
            LocationUrl = "test url",
            IsOnline = true,
            ImageUrl = "new url",
            CategoryId = 1,
            AuthorId = Guid.NewGuid().ToString(),
            PublisherId = Guid.NewGuid().ToString(),
        };
    }

    private Event GetEventEntity()
    {
        return new Event()
        {
            Title = "Old Name",
            Description = "Old Description",
            Price = 123,
            StartDateTime = DateTime.UtcNow,
            EndDateTime = DateTime.UtcNow,
            LocationName = "old location",
            LocationUrl = "old url",
            IsOnline = false,
            Image = new Image { URL = "old url" },
            CategoryID = 2,
            AuthorID = Guid.NewGuid(),
            PublisherID = Guid.NewGuid(),
        };
    }
}
