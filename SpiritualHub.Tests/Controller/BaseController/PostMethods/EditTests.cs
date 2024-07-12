namespace SpiritualHub.Tests.Controller.BaseController.PostMethods;

using Microsoft.AspNetCore.Mvc;
using Moq;

using Client.ViewModels.BaseModels;
using Client.ViewModels.Publisher;
using Client.ViewModels.Category;

using static Common.NotificationMessagesConstants;
using static Common.ErrorMessagesConstants;
using static Common.ExceptionErrorMessagesConstants;
using static Common.SuccessMessageConstants;

public class EditTests : MockConfiguration
{
    [Test]
    [TestCase(true)]
    [TestCase(false)]
    public async Task Edit_WhenSuccess(bool isAdmin)
    {
        // Arrange
        Controller.IsAdmin = isAdmin;
        string entityId = "entityId";
        var updatedEntityForm = new BaseFormModel()
        {
            Id = entityId,
            CategoryId = 1,
            PublisherId = isAdmin ? "publisherId" : null,
        };
        
        _categoryServiceMock.Setup(x => x.ExistsAsync(It.Is<int>(x => x == updatedEntityForm.CategoryId))).ReturnsAsync(true);

        if (isAdmin)
        {
            _publisherServiceMock.Setup(x => x.ExistsByIdAsync(It.Is<string>(x => x == updatedEntityForm.PublisherId))).ReturnsAsync(true);
        }
        else
        {
            _publisherServiceMock.Setup(x => x.GetPublisherIdAsync(It.Is<string>(x => x == Controller.UserId))).ReturnsAsync(updatedEntityForm.PublisherId);
        }

        // Act
        var result = await Controller.Edit(updatedEntityForm);

        // Assert
        Assert.Multiple(() =>
        {
            AssertCounter(1, 1);
            Assert.That(Controller.TempData[SuccessMessage]!, Is.EqualTo(string.Format(EditSuccessfulMessage, EntityName)));
            Assert.That(result, Is.InstanceOf<RedirectToActionResult>());

            var redirectToActionResult = (RedirectToActionResult) result;
            Assert.That(redirectToActionResult.ActionName!, Is.EqualTo("Details"));
            Assert.That(redirectToActionResult.RouteValues!["id"]!, Is.EqualTo(updatedEntityForm.Id));
        });
        _validationServiceMock.Verify(x => x.CheckModifyActionAsync(It.Is<string>(x => x == entityId), It.IsAny<string>()), Times.Once);
        _categoryServiceMock.Verify(x => x.ExistsAsync(It.Is<int>(x => x == updatedEntityForm.CategoryId)), Times.Once);
        _categoryServiceMock.Verify(x => x.GetAllAsync(It.IsAny<string>()), Times.Never);

        _publisherServiceMock.Verify(x => x.GetAllAsync(), Times.Never);
        if (isAdmin)
        {
            _publisherServiceMock.Verify(x => x.ExistsByIdAsync(It.Is<string>(x => x == updatedEntityForm.PublisherId)), Times.Once);
            _publisherServiceMock.Verify(x => x.GetPublisherIdAsync(It.IsAny<string>()), Times.Never);
        }
        else
        {
            _publisherServiceMock.Verify(x => x.ExistsByIdAsync(It.IsAny<string>()), Times.Never);
            _publisherServiceMock.Verify(x => x.GetPublisherIdAsync(It.Is<string>(x => x == Controller.UserId)), Times.Once);
        }
    }

    [Test]
    [TestCase(true)]
    [TestCase(false)]
    public async Task Edit_InvalidFormModel_ReturnsViewResult(bool isAdmin)
    {
        // Arrange
        Controller.IsAdmin = isAdmin;
        string entityId = "entityId";
        var updatedEntityForm = new BaseFormModel()
        {
            Id = entityId,
            CategoryId = -1,
            PublisherId = isAdmin ? "publisherId" : null,
        };

        IEnumerable<PublisherInfoViewModel> publishers = new List<PublisherInfoViewModel>()
        {
            new PublisherInfoViewModel(),
        };
        ICollection<CategoryServiceModel> categories = new List<CategoryServiceModel>
        {
            new CategoryServiceModel()
        };

        _categoryServiceMock.Setup(x => x.ExistsAsync(It.Is<int>(x => x == updatedEntityForm.CategoryId))).ReturnsAsync(false);
        _categoryServiceMock.Setup(x => x.GetAllAsync(It.IsAny<string>())).ReturnsAsync(categories);
        if (isAdmin)
        {
            _publisherServiceMock.Setup(x => x.ExistsByIdAsync(It.Is<string>(x => x == updatedEntityForm.PublisherId))).ReturnsAsync(false);
            _publisherServiceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(publishers);
        }

        // Act
        var result = await Controller.Edit(updatedEntityForm);

        // Assert
        Assert.Multiple(() =>
        {
            AssertCounter(0, 0);

            Assert.That(Controller.ModelState.ErrorCount, Is.EqualTo(isAdmin ? 2 : 1));
            Assert.That(Controller.ModelState[nameof(updatedEntityForm.CategoryId)]!.Errors, Has.Count.EqualTo(1));
            Assert.That(Controller.ModelState[nameof(updatedEntityForm.CategoryId)]!.Errors.First().ErrorMessage, Is.EqualTo(string.Format(NoEntityFoundErrorMessage, "category")));
            if (isAdmin)
            {
                Assert.That(Controller.ModelState[nameof(updatedEntityForm.PublisherId)]!.Errors.First().ErrorMessage, Is.EqualTo(string.Format(NoEntityFoundErrorMessage, "publisher")));
                Assert.That(Controller.ModelState[nameof(updatedEntityForm.PublisherId)]!.Errors, Has.Count.EqualTo(1));
            }
            else
            {
                Assert.That(Controller.ModelState.ContainsKey(nameof(updatedEntityForm.PublisherId)), Is.False);
            }

            Assert.That(result, Is.InstanceOf<ViewResult>());
            var resultModel = (BaseFormModel) ((ViewResult) result).Model!;
            Assert.That(resultModel.Categories, Is.EqualTo(categories));
            if (isAdmin)
            {
                Assert.That(resultModel.Publishers, Is.EqualTo(publishers));
            }
            else
            {
                Assert.That(resultModel.Publishers.Count(), Is.EqualTo(0));
            }
        });

        _validationServiceMock.Verify(x => x.CheckModifyActionAsync(It.Is<string>(x => x == entityId), It.IsAny<string>()), Times.Never);
        _categoryServiceMock.Verify(x => x.ExistsAsync(It.Is<int>(x => x == updatedEntityForm.CategoryId)), Times.Once);
        _categoryServiceMock.Verify(x => x.GetAllAsync(It.IsAny<string>()), Times.Once);

        _publisherServiceMock.Verify(x => x.GetPublisherIdAsync(It.IsAny<string>()), Times.Never);
        if (isAdmin)
        {
            _publisherServiceMock.Verify(x => x.GetAllAsync(), Times.Once);
            _publisherServiceMock.Verify(x => x.ExistsByIdAsync(It.Is<string>(x => x == updatedEntityForm.PublisherId)), Times.Once);
        }
        else
        {
            _publisherServiceMock.Verify(x => x.GetAllAsync(), Times.Never);
            _publisherServiceMock.Verify(x => x.ExistsByIdAsync(It.IsAny<string>()), Times.Never);
        }
    }

    [Test]
    [TestCase(true)]
    [TestCase(false)]
    public async Task Edit_ExceptionThrown(bool isAdmin)
    {
        // Arrange
        Controller.ThrowExceptionFlag = true;
        Controller.IsAdmin = isAdmin;
        string entityId = "entityId";
        var updatedEntityForm = new BaseFormModel()
        {
            Id = entityId,
            CategoryId = 1,
            PublisherId = isAdmin ? "publisherId" : null,
        };

        var categories = new List<CategoryServiceModel>();
        var publishers = new List<PublisherInfoViewModel>();

        _categoryServiceMock.Setup(x => x.GetAllAsync(It.IsAny<string>())).ReturnsAsync(categories);
        _categoryServiceMock.Setup(x => x.ExistsAsync(It.Is<int>(x => x == updatedEntityForm.CategoryId))).ReturnsAsync(true);
        if (isAdmin)
        {
            _publisherServiceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(publishers);
            _publisherServiceMock.Setup(x => x.ExistsByIdAsync(It.Is<string>(x => x == updatedEntityForm.PublisherId))).ReturnsAsync(true);
        }
        else
        {
            _publisherServiceMock.Setup(x => x.GetPublisherIdAsync(It.Is<string>(x => x == Controller.UserId))).ReturnsAsync(updatedEntityForm.PublisherId);
        }

        // Act
        var result = await Controller.Edit(updatedEntityForm);

        // Assert
        Assert.Multiple(() =>
        {
            AssertCounter(1, 1);
            Assert.That(Controller.ThrowExceptionCounter, Is.EqualTo(1));
            Assert.That(Controller.TempData[ErrorMessage], Is.EqualTo(string.Format(GeneralUnexpectedErrorMessage, $"edit {EntityName}")));
            Assert.That(Controller.TempData.ContainsKey(SuccessMessage), Is.False);

            Assert.That(result, Is.InstanceOf<ViewResult>());
            var resultModel = (BaseFormModel) ((ViewResult) result).Model!;
            Assert.That(resultModel.Categories, Is.EqualTo(categories));
            Assert.That(resultModel.Publishers, Is.EqualTo(publishers));
        });

        _categoryServiceMock.Verify(x => x.GetAllAsync(It.IsAny<string>()), Times.Once);
        _categoryServiceMock.Verify(x => x.ExistsAsync(It.Is<int>(x => x == updatedEntityForm.CategoryId)), Times.Once);
        if (isAdmin)
        {
            _publisherServiceMock.Verify(x => x.GetPublisherIdAsync(It.IsAny<string>()), Times.Never);
            _publisherServiceMock.Verify(x => x.GetAllAsync(), Times.Once);
            _publisherServiceMock.Verify(x => x.ExistsByIdAsync(It.Is<string>(x => x == updatedEntityForm.PublisherId)), Times.Once);
        }
        else
        {
            _publisherServiceMock.Verify(x => x.GetPublisherIdAsync(It.Is<string>(x => x == Controller.UserId)), Times.Once);
            _publisherServiceMock.Verify(x => x.GetAllAsync(), Times.Never);
            _publisherServiceMock.Verify(x => x.ExistsByIdAsync(It.IsAny<string>()), Times.Never);
        }
    }

    private void AssertCounter(int expectedCreateCallCount, int expectedGetAuthorIdCounter)
    {
        Assert.That(Controller.EditAsyncCounter, Is.EqualTo(expectedCreateCallCount));
        Assert.That(Controller.GetAuthorIdAsyncCounter, Is.EqualTo(expectedGetAuthorIdCounter));
    }
}
