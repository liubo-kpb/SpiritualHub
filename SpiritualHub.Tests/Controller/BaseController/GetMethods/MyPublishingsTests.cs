namespace SpiritualHub.Tests.Controller.BaseController.GetMethods;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Client.ViewModels.BaseModels;

using static Common.ErrorMessagesConstants;
using static Common.ExceptionErrorMessagesConstants;
using static Common.NotificationMessagesConstants;

using static Extensions.Common.TestMessageConstants;
using static Extensions.Common.TestErrorMessagesConstants;

public class MyPublishingsTests : MockConfiguration
{
    [Test]
    public async Task MyPublishings_WhenSuccess()
    {
        // Arrange
        var viewModel = new List<EmptyViewModel>();
        _publisherServiceMock.Setup(x => x.ExistsByUserIdAsync(It.Is<string>(x => x == Controller.UserId))).ReturnsAsync(true);

        // Act
        var result = await Controller.MyPublishings();

        // Assert
        Assert.Multiple(() =>
        {
            AssertCounters(1);
            Assert.That(result, Is.InstanceOf<ViewResult>());
            Assert.That(((ViewResult) result).Model, Is.EqualTo(viewModel));
        });

        _publisherServiceMock.Verify(p => p.ExistsByUserIdAsync(It.Is<string>(x => x == Controller.UserId)), Times.Once);
    }

    [Test]
    public async Task MyPublishings_WhenNotAPublisher_ReturnsRedirectToAction()
    {
        // Arrange
        _publisherServiceMock.Setup(p => p.ExistsByUserIdAsync(It.IsAny<string>())).ReturnsAsync(false);

        // Act
        var result = await Controller.MyPublishings();

        // Assert
        Assert.Multiple(() =>
        {
            AssertCounters(0);
            AssertTempData(NotAPublisherErrorMessage);
            Assert.That(result, Is.InstanceOf<RedirectToActionResult>());
            Assert.That(((RedirectToActionResult) result).ActionName, Is.EqualTo("Become"));
            Assert.That(((RedirectToActionResult) result).ControllerName, Is.EqualTo("Publisher"));
        });

        _publisherServiceMock.Verify(p => p.ExistsByUserIdAsync(It.Is<string>(x => x == Controller.UserId)), Times.Once);
    }

    [Test]
    public async Task MyPublishings_WhenNotImplementedExceptionThrown_ReturnsRedirectToAction()
    {
        // Arrange
        Controller.ThrowNotImplementedExceptionFlag = true;
        _publisherServiceMock.Setup(x => x.ExistsByUserIdAsync(It.Is<string>(x => x == Controller.UserId))).ReturnsAsync(true);

        // Act
        var result = await Controller.MyPublishings();

        // Assert
        Assert.Multiple(() =>
        {
            AssertCounters(1);
            AssertTempData(TestErrorMessageForExceptions);
            Assert.That(result, Is.InstanceOf<RedirectToActionResult>());
            Assert.That(((RedirectToActionResult) result).ActionName, Is.EqualTo("Index"));
            Assert.That(((RedirectToActionResult) result).ControllerName, Is.EqualTo("Home"));
        });

        _publisherServiceMock.Verify(p => p.ExistsByUserIdAsync(It.Is<string>(x => x == Controller.UserId)), Times.Once);
    }

    [Test]
    public async Task MyPublishings_WhenExceptionThrown_ReturnsRedirectToAction()
    {
        // Arrange
        Controller.ThrowExceptionFlag = true;
        _publisherServiceMock.Setup(x => x.ExistsByUserIdAsync(It.Is<string>(x => x == Controller.UserId))).ReturnsAsync(true);

        // Act
        var result = await Controller.MyPublishings();

        // Assert
        Assert.Multiple(() =>
        {
            AssertCounters(1);
            AssertTempData(string.Format(GeneralUnexpectedErrorMessage, $"load your {EntityName}s"));
            Assert.That(result, Is.InstanceOf<RedirectToActionResult>());
            Assert.That(((RedirectToActionResult) result).ActionName, Is.EqualTo("All"));
        });

        _publisherServiceMock.Verify(p => p.ExistsByUserIdAsync(It.Is<string>(x => x == Controller.UserId)), Times.Once);
    }

    private void AssertCounters(int expectedGetEntitiesByPublisherIdCounter)
    {
        Assert.That(Controller.GetEntitiesByPublisherIdAsyncCounter, Is.EqualTo(expectedGetEntitiesByPublisherIdCounter), string.Format(WrongVariableValueErrorMessage, "GetEntitiesByPublisherIdAsyncCounter"));
    }

    private void AssertTempData(string expectedMessage)
    {
        Assert.That(Controller.TempData[ErrorMessage], Is.EqualTo(expectedMessage), string.Format(WrongVariableValueErrorMessage, "TempData[ErrorMessage]"));
    }
}
