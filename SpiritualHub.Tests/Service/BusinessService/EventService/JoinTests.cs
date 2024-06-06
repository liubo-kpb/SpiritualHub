namespace SpiritualHub.Tests.Service.BusinessService.EventService;

using Moq;

using static Extensions.Common.TestErrorMessagesConstants;


public class JoinTests : MockConfiguration
{
    [Test]
    public async Task WhenSuccess()
    {
        // Arrange
        var eventEntity = _events.First();
        var user = _users.First();

        var eventId = eventEntity.Id.ToString();
        var userId = user.Id.ToString();

        _userRepositoryMock.Setup(x => x.GetSingleByIdAsync(It.Is<string>(x => x == userId))).ReturnsAsync(user);
        _eventRepositoryMock.Setup(x => x.GetSingleByIdAsync(It.Is<string>(x => x == eventId))).ReturnsAsync(eventEntity);

        int expectedUserEventsCount = eventEntity.Participants.Count + 1;

        // Act
        await _eventService.JoinAsync(eventId, userId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventEntity.Participants.Any(p => p.Id == user.Id));
            Assert.That(eventEntity.Participants, Has.Count.EqualTo(expectedUserEventsCount));
        });
        _userRepositoryMock.Verify(x => x.GetSingleByIdAsync(It.Is<string>(x => x == userId)));
        _eventRepositoryMock.Verify(x => x.GetSingleByIdAsync(It.Is<string>(x => x == eventId)));
        _eventRepositoryMock.Verify(x => x.SaveChangesAsync());
    }

    [Test]
    public async Task WhenFail_WrongUserId()
    {
        // Arrange
        var eventEntity = _events.First();
        var eventId = eventEntity.Id.ToString();
        var userId = "wrongId";

        _eventRepositoryMock.Setup(x => x.GetSingleByIdAsync(It.Is<string>(x => x == eventId))).ReturnsAsync(eventEntity);

        // Act
        await _eventService.JoinAsync(eventId, userId);

        // Assert
        Assert.That(eventEntity.Participants.Any(s => s != null && s.Id.ToString() == userId), Is.False);
        _eventRepositoryMock.Verify(x => x.GetSingleByIdAsync(It.Is<string>(x => x == eventId)), Times.Once);
        _userRepositoryMock.Verify(x => x.GetSingleByIdAsync(It.Is<string>(x => x == userId)), Times.Once);
        _eventRepositoryMock.Verify(x => x.SaveChangesAsync());
    }

    [Test]
    public async Task WhenFail_WrongCourseId_ThrowTest()
    {
        // Arrange
        var eventId = "wrongId";
        var userId = "userId";

        // Act & Assert
        Assert.ThrowsAsync<NullReferenceException>(async () => await _eventService.JoinAsync(eventId, userId));
    }

    [Test]
    public async Task WhenFaile_WrongCourseId_MethodCallTest()
    {
        // Arrange
        var eventId = "wrongId";
        var user = _users.First();
        var userId = user.Id.ToString();

        _userRepositoryMock.Setup(x => x.GetSingleByIdAsync(It.Is<string>(x => x == userId))).ReturnsAsync(user);

        // Act
        try
        {
            await _eventService.JoinAsync(eventId, userId);
        }
        // Assert
        catch (NullReferenceException)
        {
            _eventRepositoryMock.Verify(x => x.GetSingleByIdAsync(It.Is<string>(x => x == eventId)), Times.Once);
            _userRepositoryMock.Verify(x => x.GetSingleByIdAsync(It.Is<string>(x => x == userId)), Times.Once);
            _eventRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Never);

            return;
        }
        catch (Exception)
        {
        }

        Assert.Fail(NoNullReferenceExceptionErrorMessage);
    }
}
