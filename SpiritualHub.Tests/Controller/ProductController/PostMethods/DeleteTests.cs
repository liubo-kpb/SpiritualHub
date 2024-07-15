namespace SpiritualHub.Tests.Controller.ProductController.PostMethods;

using Microsoft.AspNetCore.Mvc;
using Moq;

using Client.ViewModels.BaseModels;

using static Common.NotificationMessagesConstants;
using static Common.SuccessMessageConstants;
using static Common.ExceptionErrorMessagesConstants;
using static Extensions.Common.TestErrorMessagesConstants;

public class DeleteTests : MockConfiguration
{
    [Test]
    public async Task Delete_WhenSuccess_ReturnsRedirectToActionResult()
    {
        // Arrange
        var model = new BaseDetailsViewModel { Id = "id" };

        // Act
        var result = await Controller.Delete(model) as RedirectToActionResult;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.ActionName, Is.EqualTo(nameof(Controller.MyPublishings)));
            AssertTempData(SuccessMessage, string.Format(DeleteSuccessfulMessage, EntityName));
            AssertCounters(1, model.Id);
        });
    }

    [Test]
    public async Task Delete_WhenValidationFails_ReturnsRedirectToActionResult()
    {
        // Arrange
        var model = new BaseDetailsViewModel { Id = "id" };

        var validationResult = new RedirectToActionResult("Test", null, null);
        _validationServiceMock.Setup(x => x.CheckModifyActionAsync(It.Is<string>(x => x == model.Id), It.IsAny<string>())).ReturnsAsync(validationResult);

        // Act
        var result = await Controller.Delete(model) as RedirectToActionResult;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.ActionName, Is.EqualTo("Test"));
            AssertCounters(0, model.Id);
        });
    }

    [Test]
    public async Task Delete_WhenDeleteAsyncThrowsException_ReturnsRedirectToActionResult()
    {
        // Arrange
        var model = new BaseDetailsViewModel { Id = "id" };
        Controller.ThrowExceptionFlag = true;

        // Act
        var result = await Controller.Delete(model);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.InstanceOf<ViewResult>());
            AssertTempData(ErrorMessage, string.Format(GeneralUnexpectedErrorMessage, $"delete {EntityName}"));
            AssertCounters(1, model.Id);
        });
    }

    private void AssertTempData(string key, string expectedMessage)
    {
        Assert.That(Controller.TempData[key], Is.EqualTo(expectedMessage), string.Format(WrongVariableValueErrorMessage, $"{nameof(Controller.TempData)}[{key}]"));
    }

    private void AssertCounters(int expectedDeleteCount, string id)
    {
        Assert.That(Controller.DeleteAsyncCounter, Is.EqualTo(expectedDeleteCount));
        _validationServiceMock.Verify(x => x.CheckModifyActionAsync(It.Is<string>(x => x == id), It.IsAny<string>()));
    }
}
