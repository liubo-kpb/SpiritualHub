namespace SpiritualHub.Tests.Controller.BaseController.GetMethods;

using Microsoft.AspNetCore.Mvc;
using Moq;

using Client.ViewModels.BaseModels;
using Client.ViewModels.Publisher;
using Client.ViewModels.Category;
using Data.Models;

using static Common.ErrorMessagesConstants;
using static Common.ExceptionErrorMessagesConstants;
using static Common.NotificationMessagesConstants;
using static Extensions.Common.TestMessageConstants;
using static Extensions.Common.TestErrorMessagesConstants;

public class EditTests : MockConfiguration
{
    [Test]
    public async Task Edit_WhenSuccess()
    {
        // Arrange
        var id = "id";
        Controller.IsAdmin = true;
        IEnumerable<PublisherInfoViewModel> publishers = new List<PublisherInfoViewModel>
        {
            new PublisherInfoViewModel()
        };
        ICollection<CategoryServiceModel> categories = new List<CategoryServiceModel>
        {
            new CategoryServiceModel()
        };

        _publisherServiceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(publishers);
        _categoryServiceMock.Setup(x => x.GetAllAsync(It.IsAny<string>())).ReturnsAsync(categories);

        // Act
        var result = await Controller.Edit(id);

        // Assert
        Assert.Multiple(() =>
        {
            AssertCounters(1);
            Assert.That(result, Is.InstanceOf<ViewResult>());

            var modelResult = ((ViewResult) result).Model as BaseFormModel;
            Assert.That(modelResult, Is.InstanceOf<BaseFormModel>());
            Assert.That(modelResult!.Categories, Is.EqualTo(categories));
            Assert.That(modelResult.Publishers, Is.EqualTo(publishers));
        });
        _validationServiceMock.Verify(x => x.CheckModifyActionAsync(It.Is<string>(x => x == id), It.IsAny<string>()), Times.Once);
        _publisherServiceMock.Verify(x => x.GetAllAsync(), Times.Once);
        _categoryServiceMock.Verify(x => x.GetAllAsync(It.IsAny<string>()), Times.Once);
    }

    [Test]
    public async Task Edit_WhenUserIsNotPublisher()
    {
        // Arrange
        var id = "id";

        ICollection<CategoryServiceModel> categories = new List<CategoryServiceModel>
        {
            new CategoryServiceModel()
        };
        var redirectToAction = new RedirectToActionResult("Become", nameof(Publisher), null);
        _validationServiceMock.Setup(x => x.CheckModifyActionAsync(It.Is<string>(x => x == id), It.IsAny<string>())).ReturnsAsync(redirectToAction);

        // Act
        var result = await Controller.Edit(id);

        // Assert
        Assert.Multiple(() =>
        {
            AssertCounters(0);
            Assert.That(result, Is.InstanceOf<RedirectToActionResult>());

            var viewResult = (RedirectToActionResult) result;
            Assert.That(viewResult, Is.EqualTo(redirectToAction));
        });
        _validationServiceMock.Verify(x => x.CheckModifyActionAsync(It.Is<string>(x => x == id), It.IsAny<string>()), Times.Once);
        _publisherServiceMock.Verify(x => x.GetAllAsync(), Times.Never);
        _categoryServiceMock.Verify(x => x.GetAllAsync(It.IsAny<string>()), Times.Never);
    }

    [Test]
    public async Task Edit_WhenExceptionThrown_ReturnsRedirectToAction()
    {
        // Arrange
        var id = "id";
        Controller.ThrowExceptionFlag = true;

        // Act
        var result = await Controller.Edit(id);

        // Assert
        Assert.Multiple(() =>
        {
            AssertCounters(1);
            AssertTempData(string.Format(GeneralUnexpectedErrorMessage, $"load {EntityName}"));
            Assert.That(result, Is.InstanceOf<RedirectToActionResult>());
            Assert.That(((RedirectToActionResult) result).ActionName, Is.EqualTo("Details"));
        });
        _validationServiceMock.Verify(x => x.CheckModifyActionAsync(It.Is<string>(x => x == id), It.IsAny<string>()), Times.Once);
        _publisherServiceMock.Verify(x => x.GetAllAsync(), Times.Never);
        _categoryServiceMock.Verify(x => x.GetAllAsync(It.IsAny<string>()), Times.Never);
    }

    private void AssertCounters(int expectedCreateFormModelInstanceCounter)
    {
        Assert.That(Controller.GetEntityInfoAsyncCounter, Is.EqualTo(expectedCreateFormModelInstanceCounter), string.Format(WrongVariableValueErrorMessage, nameof(Controller.GetEntityInfoAsyncCounter)));
    }

    private void AssertTempData(string expectedMessage)
    {
        Assert.That(Controller.TempData[ErrorMessage], Is.EqualTo(expectedMessage), string.Format(WrongVariableValueErrorMessage, "TempData[ErrorMessage]"));
    }
}
