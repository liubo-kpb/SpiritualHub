namespace SpiritualHub.Tests.Service.ValidationService.AuthorValidation;

using Moq;

using Client.Infrastructure.Enums;

using static Common.ErrorMessagesConstants;

public class CheckSubscribeActionTests : MockConfiguration
{
    [Test]
    public async Task WhenValidRequest()
    {
        // Arrange
        string authorId = "authorId";
        string userId = "userId";

        _publisherServiceMock.Setup(x => x.ExistsByUserIdAsync(It.Is<string>(x => x == userId))).ReturnsAsync(false);

        int expectedExistsCallCount = 1;
        int expectedAdminCheckCallCount = 1;
        int expectedGetUserIdCallCount = 1;

        // Act
        var result = await _validationService.CheckSubscribeActionAsync(authorId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Null);
            Assert.That(_validationService.RouteValue, Is.Null);
            Assert.That(_validationService.ActionUrl, Is.Null);

            Assert.That(_validationService.ActualEntityId, Is.EqualTo(authorId));
            Assert.That(_validationService.ExistsCallCount, Is.EqualTo(expectedExistsCallCount));
            Assert.That(_validationService.AdminCheckCallCount, Is.EqualTo(expectedAdminCheckCallCount));
            Assert.That(_validationService.GetUserIdCallCount, Is.EqualTo(expectedGetUserIdCallCount));
        });
        _publisherServiceMock.Verify(x => x.ExistsByUserIdAsync(It.Is<string>(x => x == userId)));
    }

    [Test]
    public async Task WhenUserIsAdmin()
    {
        // Arrange
        string authorId = "authorId";

        _validationService.IsAdmin = true;

        int expectedExistsCallCount = 1;
        int expectedAdminCheckCallCount = 1;
        int expectedGetUserIdCallCount = 0;

        // Act
        var result = await _validationService.CheckSubscribeActionAsync(authorId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Null);
            Assert.That(_validationService.RouteValue, Is.Null);
            Assert.That(_validationService.ActionUrl, Is.Null);

            Assert.That(_validationService.ActualEntityId, Is.EqualTo(authorId));
            Assert.That(_validationService.ExistsCallCount, Is.EqualTo(expectedExistsCallCount));
            Assert.That(_validationService.AdminCheckCallCount, Is.EqualTo(expectedAdminCheckCallCount));
            Assert.That(_validationService.GetUserIdCallCount, Is.EqualTo(expectedGetUserIdCallCount));
        });
        _publisherServiceMock.Verify(x => x.ExistsByUserIdAsync(It.IsAny<string>()), Times.Never);
    }

    [Test]
    public async Task WhenAuthorDoesNotExist()
    {
        // Arrange
        string authorId = "authorId";
        _validationService.Exists = false;

        int expectedExistsCallCount = 1;
        int expectedAdminCheckCallCount = 0;
        int expectedGetUserIdCallCount = 0;

        string expectedErrorMessage = string.Format(NoEntityFoundErrorMessage, _validationService.EntityName);
        var expectedNotificationType = NotificationType.ErrorMessage;

        string expectedUrl = string.Format(_url, ControllerName, "All");

        // Act
        var result = await _validationService.CheckSubscribeActionAsync(authorId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(_validationService.RouteValue, Is.Null);
            Assert.That(_validationService.ActionUrl, Is.EqualTo(expectedUrl));

            Assert.That(_validationService.ActualEntityId, Is.EqualTo(authorId));
            Assert.That(_validationService.ExistsCallCount, Is.EqualTo(expectedExistsCallCount));
            Assert.That(_validationService.AdminCheckCallCount, Is.EqualTo(expectedAdminCheckCallCount));
            Assert.That(_validationService.GetUserIdCallCount, Is.EqualTo(expectedGetUserIdCallCount));
            Assert.That(_validationService.ActualNotificationType, Is.EqualTo(expectedNotificationType));
            Assert.That(_validationService.ActualErrorMessage, Is.EqualTo(expectedErrorMessage));
        });
        _publisherServiceMock.Verify(x => x.ExistsByUserIdAsync(It.IsAny<string>()), Times.Never);
    }

    [Test]
    public async Task WhenUserIsPublisher()
    {
        // Arrange
        string authorId = "authorId";
        string userId = "userId";

        int expectedExistsCallCount = 1;
        int expectedAdminCheckCallCount = 1;
        int expectedGetUserIdCallCount = 1;

        string expectedErrorMessage = PublishersCannotSubscribeErrorMessage;
        var expectedNotificationType = NotificationType.ErrorMessage;

        string expectedUrl = string.Format(_url, ControllerName, "Details");

        _publisherServiceMock.Setup(x => x.ExistsByUserIdAsync(It.Is<string>(x => x == userId))).ReturnsAsync(true);

        // Act
        var result = await _validationService.CheckSubscribeActionAsync(authorId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(_validationService.RouteValue, Is.Not.Null);
            Assert.That(_validationService.ActionUrl, Is.EqualTo(expectedUrl));

            Assert.That(_validationService.ActualEntityId, Is.EqualTo(authorId));
            Assert.That(_validationService.ExistsCallCount, Is.EqualTo(expectedExistsCallCount));
            Assert.That(_validationService.AdminCheckCallCount, Is.EqualTo(expectedAdminCheckCallCount));
            Assert.That(_validationService.GetUserIdCallCount, Is.EqualTo(expectedGetUserIdCallCount));
            Assert.That(_validationService.ActualNotificationType, Is.EqualTo(expectedNotificationType));
            Assert.That(_validationService.ActualErrorMessage, Is.EqualTo(expectedErrorMessage));
        });
        _publisherServiceMock.Verify(x => x.ExistsByUserIdAsync(It.Is<string>(x => x == userId)));
    }
}
