namespace SpiritualHub.Tests.Controller.ProductController.PostMethods;

using Microsoft.AspNetCore.Mvc;
using Moq;

using static Common.NotificationMessagesConstants;
using static Common.ExceptionErrorMessagesConstants;
using static Common.SuccessMessageConstants;
using static Extensions.Common.TestMessageConstants;
using static Extensions.Common.TestErrorMessagesConstants;

public class ShowTests : MockConfiguration
{
    [Test]
    public async Task Show_WhenSuccess_ReturnsRedirectToActionResult()
    {
        // Arrange
        var id = "id";

        // Act
        var result = await Controller.Show(id) as RedirectToActionResult;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.ActionName, Is.EqualTo(nameof(Controller.MyPublishings)));
            AssertTempData(SuccessMessage, string.Format(ShowEntitySuccessMessage, EntityName));
            AssertCounters(1, id);
        });
    }

    [Test]
    public async Task Show_WhenValidationFails_ReturnsRedirectToActionResult()
    {
        // Arrange
        var id = "id";
        
        var validationResult = new RedirectToActionResult("Test", null, null);
        _validationServiceMock.Setup(x => x.CheckModifyActionAsync(It.Is<string>(x => x == id), It.IsAny<string>())).ReturnsAsync(validationResult);

        // Act
        var result = await Controller.Show(id) as RedirectToActionResult;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.ActionName, Is.EqualTo("Test"));
            AssertCounters(0, id);
        });
    }

    [Test]
    public async Task Show_WhenShowAsyncThrowsNotImplementedException_ReturnsRedirectToActionResult()
    {
        // Arrange
        var id = "id";
        Controller.ThrowNotImplementedExceptionFlag = true;

        // Act
        var result = await Controller.Show(id) as RedirectToActionResult;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.ActionName, Is.EqualTo("Index"));
            AssertTempData(ErrorMessage, TestErrorMessageForExceptions);
            AssertCounters(1, id);
        });
    }

    [Test]
    public async Task Show_WhenShowAsyncThrowsException_ReturnsRedirectToActionResult()
    {
        // Arrange
        var id = "id";
        Controller.ThrowExceptionFlag = true;

        // Act
        var result = await Controller.Show(id) as RedirectToActionResult;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.ActionName, Is.EqualTo(nameof(Controller.Details)));
            AssertTempData(ErrorMessage, string.Format(GeneralUnexpectedErrorMessage, $"show the {EntityName}"));
            AssertCounters(1, id);
        });
    }

    private void AssertTempData(string key, string expectedMessage)
    {
        Assert.That(Controller.TempData[key], Is.EqualTo(expectedMessage), string.Format(WrongVariableValueErrorMessage, $"{nameof(Controller.TempData)}[{key}]"));
    }

    private void AssertCounters(int expectedShowCount, string id)
    {
        Assert.That(Controller.ShowAsyncCounter, Is.EqualTo(expectedShowCount), string.Format(WrongVariableValueErrorMessage, nameof(Controller.ShowAsyncCounter)));
        _validationServiceMock.Verify(x => x.CheckModifyActionAsync(It.Is<string>(x => x == id), It.IsAny<string>()), Times.Once);
    }
}
