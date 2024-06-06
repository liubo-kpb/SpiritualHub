namespace SpiritualHub.Tests.Service.BusinessService.EventService;

using Moq;

using Data.Models;

using static Extensions.Common.TestErrorMessagesConstants;

public class LeaveTests : MockConfiguration
{
    [Test]
    public async Task WhenSuccess()
    {
        // Arrange
        var user = _users.First();
        var eventEntity = GetEventWithParticipant(user);

        var eventId = eventEntity.Id.ToString();
        var userId = eventEntity.Participants.First().Id.ToString();

        int expectedReadersCount = eventEntity.Participants.Count - 1;

        _eventRepositoryMock.Setup(x => x.GetEventWithParticipantsAsync(It.Is<string>(x => x == eventId))).ReturnsAsync(eventEntity);
        _userRepositoryMock.Setup(x => x.GetSingleByIdAsync(It.Is<string>(x => x == userId))).ReturnsAsync(user);

        // Act
        await _eventService.LeaveAsync(eventId, userId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventEntity.Participants, Has.Count.EqualTo(expectedReadersCount));
            Assert.That(eventEntity.Participants.Any(u => u.Id == user.Id), Is.False);
        });
        _eventRepositoryMock.Verify(x => x.GetEventWithParticipantsAsync(It.Is<string>(x => x == eventId)));
        _userRepositoryMock.Verify(x => x.GetSingleByIdAsync(It.Is<string>(x => x == userId)));
        _eventRepositoryMock.Verify(x => x.SaveChangesAsync());
    }

    [Test]
    public async Task WhenUserDoesNotExist()
    {
        // Arrange
        var user = _users.First();
        var eventEntity = GetEventWithParticipant(user);

        var eventId = eventEntity.Id.ToString();
        var userId = "wrongId";

        int expectedReadersCount = eventEntity.Participants.Count;

        _eventRepositoryMock.Setup(x => x.GetEventWithParticipantsAsync(It.Is<string>(x => x == eventId))).ReturnsAsync(eventEntity);

        // Act
        await _eventService.LeaveAsync(eventId, userId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventEntity.Participants, Has.Count.EqualTo(expectedReadersCount));
        });
        _eventRepositoryMock.Verify(x => x.GetEventWithParticipantsAsync(It.Is<string>(x => x == eventId)));
        _userRepositoryMock.Verify(x => x.GetSingleByIdAsync(It.Is<string>(x => x == userId)));
        _eventRepositoryMock.Verify(x => x.SaveChangesAsync());
    }

    [Test]
    public void WhenEventDoesNotExist_ThrowTest()
    {
        // Arrange
        var user = new ApplicationUser();

        var eventId = "wrongId";
        var userId = user.Id.ToString();

        _userRepositoryMock.Setup(x => x.GetSingleByIdAsync(It.Is<string>(x => x == userId))).ReturnsAsync(user);

        // Act & Assert
        Assert.ThrowsAsync<NullReferenceException>(async () => await _eventService.LeaveAsync(eventId, userId), NoNullReferenceExceptionErrorMessage);
    }

    [Test]
    public async Task WhenEventDoesNotExist_MethodCallTest()
    {
        // Arrange
        var user = new ApplicationUser();

        var eventId = "wrongId";
        var userId = user.Id.ToString();

        _userRepositoryMock.Setup(x => x.GetSingleByIdAsync(It.Is<string>(x => x == userId))).ReturnsAsync(user);

        // Act
        try
        {
            await _eventService.LeaveAsync(eventId, userId);
        }
        catch (NullReferenceException)
        {
            _eventRepositoryMock.Verify(x => x.GetEventWithParticipantsAsync(It.Is<string>(x => x == eventId)));
            _userRepositoryMock.Verify(x => x.GetSingleByIdAsync(It.Is<string>(x => x == userId)));
            _userRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Never);

            return;
        }
        catch (Exception)
        {

        }

        Assert.Fail(NoNullReferenceExceptionErrorMessage);
    }
}
