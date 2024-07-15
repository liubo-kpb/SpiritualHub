namespace SpiritualHub.Tests.Controller.ProductController.GetMethods;

using Microsoft.AspNetCore.Mvc;
using Moq;

using static Common.NotificationMessagesConstants;
using static Common.ExceptionErrorMessagesConstants;
using static Extensions.Common.TestErrorMessagesConstants;

public class DeleteTests : MockConfiguration
{
    [Test]
    public async Task Delete_WhenSuccess_ReturnsRedirectToActionResult()
    {
        // Arrange
        var id = "id";

        // Act
        var result = await Controller.Delete(id);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.InstanceOf<ViewResult>());
            AssertCounters(1, id);
        });
    }

    [Test]
    public async Task Delete_WhenValidationFails_ReturnsRedirectToActionResult()
    {
        // Arrange
        var id = "id";

        var validationResult = new RedirectToActionResult("Test", null, null);
        _validationServiceMock.Setup(x => x.CheckModifyActionAsync(It.Is<string>(x => x == id), It.IsAny<string>())).ReturnsAsync(validationResult);

        // Act
        var result = await Controller.Delete(id) as RedirectToActionResult;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.ActionName, Is.EqualTo("Test"));
            AssertCounters(0, id);
        });
    }

    [Test]
    public async Task Delete_WhenDeleteAsyncThrowsException_ReturnsRedirectToActionResult()
    {
        // Arrange
        var id = "id";
        Controller.ThrowExceptionFlag = true;

        // Act
        var result = await Controller.Delete(id) as RedirectToActionResult;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.ActionName, Is.EqualTo(nameof(Controller.Details)));
            AssertCounters(1, id);
            AssertTempData(ErrorMessage, string.Format(GeneralUnexpectedErrorMessage, $"load the {EntityName}"));
        });
    }

    private void AssertTempData(string key, string expectedMessage)
    {
        Assert.That(Controller.TempData[key], Is.EqualTo(expectedMessage), string.Format(WrongVariableValueErrorMessage, $"{nameof(Controller.TempData)}[{key}]"));
    }

    private void AssertCounters(int expecetedGetEntitytCount, string id)
    {
        Assert.That(Controller.GetEntityInfoCounter, Is.EqualTo(expecetedGetEntitytCount));
        _validationServiceMock.Verify(x => x.CheckModifyActionAsync(It.Is<string>(x => x == id), It.IsAny<string>()), Times.Once);
    }
}
