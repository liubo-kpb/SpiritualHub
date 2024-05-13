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
        int existsCallCount = 0;
        string actualId = string.Empty;
        _validationService.ExistsAsyncFunc = (id) =>
        {
            existsCallCount++;
            actualId = id;
            return Task.FromResult(true);
        };

        int adminCheckCallCount = 0;
        _validationService.IsUserAdminFunc = () =>
        {
            adminCheckCallCount++;
            return false;
        };

        int getUserIdCallCount = 0;
        _validationService.GetUserIdFunc = () =>
        {
            getUserIdCallCount++;
            return userId;
        };

        var result = await _validationService.CheckSubscribeActionAsync(authorId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Null);
            Assert.That(_validationService.RouteValue, Is.Null);
            Assert.That(_validationService.ActionUrl, Is.Null);

            Assert.That(actualId, Is.EqualTo(authorId));
            Assert.That(existsCallCount, Is.EqualTo(expectedExistsCallCount));
            Assert.That(adminCheckCallCount, Is.EqualTo(expectedAdminCheckCallCount));
            Assert.That(getUserIdCallCount, Is.EqualTo(expectedGetUserIdCallCount));
        });
        _publisherServiceMock.Verify(x => x.ExistsByUserIdAsync(It.Is<string>(x => x == userId)));
    }

    [Test]
    public async Task WhenUserIsAdmin()
    {
        // Arrange
        string authorId = "authorId";
        string userId = "userId";

        int expectedExistsCallCount = 1;
        int expectedAdminCheckCallCount = 1;
        int expectedGetUserIdCallCount = 0;

        // Act
        int existsCallCount = 0;
        string actualId = string.Empty;
        _validationService.ExistsAsyncFunc = (id) =>
        {
            existsCallCount++;
            actualId = id;
            return Task.FromResult(true);
        };

        int adminCheckCallCount = 0;
        _validationService.IsUserAdminFunc = () =>
        {
            adminCheckCallCount++;
            return true;
        };

        int getUserIdCallCount = 0;
        _validationService.GetUserIdFunc = () =>
        {
            getUserIdCallCount++;
            return userId;
        };

        var result = await _validationService.CheckSubscribeActionAsync(authorId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Null);
            Assert.That(_validationService.RouteValue, Is.Null);
            Assert.That(_validationService.ActionUrl, Is.Null);

            Assert.That(actualId, Is.EqualTo(authorId));
            Assert.That(existsCallCount, Is.EqualTo(expectedExistsCallCount));
            Assert.That(adminCheckCallCount, Is.EqualTo(expectedAdminCheckCallCount));
            Assert.That(getUserIdCallCount, Is.EqualTo(expectedGetUserIdCallCount));
        });
        _publisherServiceMock.Verify(x => x.ExistsByUserIdAsync(It.IsAny<string>()), Times.Never);
    }

    [Test]
    public async Task WhenAuthorDoesNotExist()
    {
        // Arrange
        string authorId = "authorId";
        string userId = "userId";

        int expectedExistsCallCount = 1;
        int expectedAdminCheckCallCount = 0;
        int expectedGetUserIdCallCount = 0;

        string expectedErrorMessage = string.Format(NoEntityFoundErrorMessage, _validationService.EntityName);
        var expectedNotificationType = NotificationType.ErrorMessage;

        string expectedUrl = string.Format(_url, ControllerName, "All");

        // Act
        int existsCallCount = 0;
        string actualId = string.Empty;
        _validationService.ExistsAsyncFunc = (id) =>
        {
            existsCallCount++;
            actualId = id;
            return Task.FromResult(false);
        };

        int adminCheckCallCount = 0;
        _validationService.IsUserAdminFunc = () =>
        {
            adminCheckCallCount++;
            return false;
        };

        int getUserIdCallCount = 0;
        _validationService.GetUserIdFunc = () =>
        {
            getUserIdCallCount++;
            return userId;
        };

        string actualMessage = string.Empty;
        var actualType = NotificationType.Null;
        _validationService.SetTempDataMessageAction = (type, message) =>
        {
            actualMessage = message;
            actualType = type;
        };

        var result = await _validationService.CheckSubscribeActionAsync(authorId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(_validationService.RouteValue, Is.Null);
            Assert.That(_validationService.ActionUrl, Is.EqualTo(expectedUrl));

            Assert.That(actualId, Is.EqualTo(authorId));
            Assert.That(existsCallCount, Is.EqualTo(expectedExistsCallCount));
            Assert.That(adminCheckCallCount, Is.EqualTo(expectedAdminCheckCallCount));
            Assert.That(getUserIdCallCount, Is.EqualTo(expectedGetUserIdCallCount));
            Assert.That(actualType, Is.EqualTo(expectedNotificationType));
            Assert.That(actualMessage, Is.EqualTo(expectedErrorMessage));
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
        int existsCallCount = 0;
        string actualId = string.Empty;
        _validationService.ExistsAsyncFunc = (id) =>
        {
            existsCallCount++;
            actualId = id;
            return Task.FromResult(true);
        };

        int adminCheckCallCount = 0;
        _validationService.IsUserAdminFunc = () =>
        {
            adminCheckCallCount++;
            return false;
        };

        int getUserIdCallCount = 0;
        _validationService.GetUserIdFunc = () =>
        {
            getUserIdCallCount++;
            return userId;
        };

        string actualMessage = string.Empty;
        var actualType = NotificationType.Null;
        _validationService.SetTempDataMessageAction = (type, message) =>
        {
            actualMessage = message;
            actualType = type;
        };

        var result = await _validationService.CheckSubscribeActionAsync(authorId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(_validationService.RouteValue, Is.Not.Null);
            Assert.That(_validationService.ActionUrl, Is.EqualTo(expectedUrl));

            Assert.That(actualId, Is.EqualTo(authorId));
            Assert.That(existsCallCount, Is.EqualTo(expectedExistsCallCount));
            Assert.That(adminCheckCallCount, Is.EqualTo(expectedAdminCheckCallCount));
            Assert.That(getUserIdCallCount, Is.EqualTo(expectedGetUserIdCallCount));
            Assert.That(actualType, Is.EqualTo(expectedNotificationType));
            Assert.That(actualMessage, Is.EqualTo(expectedErrorMessage));
        });
        _publisherServiceMock.Verify(x => x.ExistsByUserIdAsync(It.Is<string>(x => x == userId)));
    }
}
