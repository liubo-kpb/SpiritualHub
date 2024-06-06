namespace SpiritualHub.Tests.Service.BusinessService.EventService.CRUDMethods;

using Moq;

using Data.Models;

using static Extensions.Common.TestErrorMessagesConstants;

public class DeleteTests : MockConfiguration
{
    [Test]
    public async Task WhenSuccess()
    {
        // Arrange
        var eventEntity = new Event()
        {
            Image = new Image()
        };
        var eventId = eventEntity.Id.ToString();

        _eventRepositoryMock.Setup(x => x.GetEventWithImageAndRatingsAsync(It.Is<string>(x => x == eventId))).ReturnsAsync(eventEntity);

        // Act
        await _eventService.DeleteAsync(eventId);

        // Assert
        _eventRepositoryMock.Verify(x => x.GetEventWithImageAndRatingsAsync(It.Is<string>(x => x == eventId)), Times.Once);
        _eventRepositoryMock.Verify(x => x.Delete(It.Is<Event>(x => x.Equals(eventEntity))));

        _imageRepositoryMock.Verify(x => x.Delete(It.Is<Image>(x => x.Equals(eventEntity.Image))));
        _ratingRepositoryMock.Verify(x => x.DeleteMultiple(It.Is<ICollection<Rating>>(x => x.Equals(eventEntity.Ratings))));


        _eventRepositoryMock.Verify(x => x.SaveChangesAsync());
    }

    [Test]
    public void WithWrongId_ThrowTest()
    {
        // Arrange
        var eventId = "wrongId";

        // Act & Assert
        Assert.ThrowsAsync<NullReferenceException>(async () => await _eventService.DeleteAsync(eventId), NoNullReferenceExceptionErrorMessage);
    }

    [Test]
    public async Task WithWrongId_MethodCallTest()
    {
        // Arrange
        var eventId = "wrongId";

        // Act
        try
        {
            await _eventService.DeleteAsync(eventId);
        }
        // Assert
        catch (NullReferenceException)
        {
            _eventRepositoryMock.Verify(x => x.GetEventWithImageAndRatingsAsync(It.Is<string>(x => x == eventId)), Times.Once);
            _eventRepositoryMock.Verify(x => x.Delete(It.IsAny<Event>()));

            _imageRepositoryMock.Verify(x => x.Delete(It.IsAny<Image>()), Times.Never);
            _ratingRepositoryMock.Verify(x => x.DeleteMultiple(It.IsAny<ICollection<Rating>>()), Times.Never);


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
}
