namespace SpiritualHub.Tests.Service.BusinessService.ModuleService;

using Moq;

using Data.Models;

using static Extensions.Common.TestErrorMessagesConstants;

public class HideTests : MockConfiguration
{
    [Test]
    public async Task WhenSuccess()
    {
        // Arrange
        var module = new Module()
        {
            IsActive = true,
        };

        string id = module.Id.ToString();

        _moduleRepositoryMock.Setup(x => x.GetSingleByIdAsync(It.Is<string>(x => x == id))).ReturnsAsync(module);

        // Act
        await _moduleService.HideAsync(id);

        // Assert
        Assert.That(module.IsActive, Is.False);
        _moduleRepositoryMock.Verify(x => x.GetSingleByIdAsync(It.Is<string>(x => x == id)), Times.Once);
        _moduleRepositoryMock.Verify(x => x.SaveChangesAsync());
    }

    [Test]
    public void WhenWrongId_ThrowTest()
    {
        // Arrange
        var moduleId = "wrongId";

        // Act & Assert
        Assert.ThrowsAsync<NullReferenceException>(async () => await _moduleService.HideAsync(moduleId));
    }

    [Test]
    public async Task WhenWrongId_MethodCallTest()
    {
        // Arrange
        var moduleId = "wrongId";

        // Act
        try
        {
            await _moduleService.HideAsync(moduleId);
        }
        // Assert
        catch (NullReferenceException)
        {
            _moduleRepositoryMock.Verify(x => x.GetSingleByIdAsync(It.Is<string>(x => x == moduleId)), Times.Once);
            _moduleRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Never);
            return;
        }
        catch (Exception)
        {

        }

        Assert.Fail(NoNullReferenceExceptionErrorMessage);
    }

    protected override bool GenerateEntities { get; set; } = false;
}
