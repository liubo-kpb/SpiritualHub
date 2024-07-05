namespace SpiritualHub.Tests.Controller.BaseController.GetMethods;

using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Moq;

using Client.ViewModels.BaseModels;
using Client.ViewModels.Publisher;
using Client.ViewModels.Category;

using static Common.ErrorMessagesConstants;
using static Common.ExceptionErrorMessagesConstants;
using static Common.NotificationMessagesConstants;
using static Extensions.Common.TestMessageConstants;
using static Extensions.Common.TestErrorMessagesConstants;

public class AddTests : MockConfiguration
{
    [Test]
    public async Task Add_WhenSuccess_UserIsAdmin()
    {
        // Arrange
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
        var result = await Controller.Add();

        // Assert
        Assert.Multiple(() =>
        {
            AssertCounters(1);
            Assert.That(result, Is.InstanceOf<ViewResult>());

            var modeulResult = ((ViewResult) result).Model as BaseFormModel;
            Assert.That(modeulResult!.Publishers, Is.EqualTo(publishers));
            Assert.That(modeulResult!.Categories, Is.EqualTo(categories));
        });
        _publisherServiceMock.Verify(x => x.ExistsByUserIdAsync(It.IsAny<string>()), Times.Never);
        _publisherServiceMock.Verify(x => x.GetAllAsync(), Times.Once);
        _categoryServiceMock.Verify(x => x.GetAllAsync(It.IsAny<string>()), Times.Once);
    }

    [Test]
    public async Task Add_WhenSuccess_UserIsPublisher()
    {
        // Arrange
        ICollection<CategoryServiceModel> categories = new List<CategoryServiceModel>
        {
            new CategoryServiceModel()
        };

        _publisherServiceMock.Setup(x => x.ExistsByUserIdAsync(It.Is<string>(x => x == Controller.UserId))).ReturnsAsync(true);
        _categoryServiceMock.Setup(x => x.GetAllAsync(It.IsAny<string>())).ReturnsAsync(categories);

        // Act
        var result = await Controller.Add();

        // Assert
        Assert.Multiple(() =>
        {
            AssertCounters(1);
            Assert.That(result, Is.InstanceOf<ViewResult>());

            var modeulResult = ((ViewResult) result).Model as BaseFormModel;
            Assert.That(modeulResult!.Categories, Is.EqualTo(categories));
        });
        _publisherServiceMock.Verify(x => x.ExistsByUserIdAsync(It.Is<string>(x => x == Controller.UserId)), Times.Once);
        _publisherServiceMock.Verify(x => x.GetAllAsync(), Times.Never);
        _categoryServiceMock.Verify(x => x.GetAllAsync(It.IsAny<string>()), Times.Once);
    }

    [Test]
    public async Task Add_WhenUserIsNotAPublisher_ReturnsRedirectToAction()
    {
        // Arrange
        _publisherServiceMock.Setup(p => p.ExistsByUserIdAsync(It.Is<string>(x => x == Controller.UserId))).ReturnsAsync(false);

        // Act
        var result = await Controller.Add();

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
    public async Task Add_WhenNotImplementedExceptionThrown_ReturnsRedirectToAction()
    {
        // Arrange
        Controller.IsAdmin = true;
        Controller.ThrowNotImplementedExceptionFlag = true;

        // Act
        var result = await Controller.Add();

        // Assert
        Assert.Multiple(() =>
        {
            AssertCounters(1);
            AssertTempData(TestErrorMessageForExceptions);
            Assert.That(result, Is.InstanceOf<RedirectToActionResult>());
            Assert.That(((RedirectToActionResult) result).ActionName, Is.EqualTo("Index"));
            Assert.That(((RedirectToActionResult) result).ControllerName, Is.EqualTo("Home"));
        });
    }

    [Test]
    public async Task Add_WhenExceptionThrown_ReturnsRedirectToAction()
    {
        // Arrange
        Controller.IsAdmin = true;
        Controller.ThrowExceptionFlag = true;

        // Act
        var result = await Controller.Add();

        // Assert
        Assert.Multiple(() =>
        {
            AssertCounters(1);
            AssertTempData(string.Format(GeneralUnexpectedErrorMessage, "load page"));
            Assert.That(result, Is.InstanceOf<RedirectToActionResult>());
            Assert.That(((RedirectToActionResult) result).ActionName, Is.EqualTo("Index"));
            Assert.That(((RedirectToActionResult) result).ControllerName, Is.EqualTo("Home"));
        });
    }

    private void AssertCounters(int expectedCreateFormModelInstanceCounter)
    {
        Assert.That(Controller.CreateFormModelCounter, Is.EqualTo(expectedCreateFormModelInstanceCounter), string.Format(WrongVariableValueErrorMessage, "CreateFormModelCounter"));
    }

    private void AssertTempData(string expectedMessage)
    {
        Assert.That(Controller.TempData[ErrorMessage], Is.EqualTo(expectedMessage), string.Format(WrongVariableValueErrorMessage, "TempData[ErrorMessage]"));
    }
}
