namespace SpiritualHub.Tests.Service.BusinessService.ModuleService.GetMethods;

using Moq;

using Client.ViewModels.Module;

public class GetModuleInfoTests : MockConfiguration
{
    [Test]
    public async Task WhenSuccess()
    {
        // Arrange
        var module = _modules.First();
        var moduleId = module.Id.ToString();

        var expected = _mapper.Map<ModuleFormModel>(module);

        _moduleRepositoryMock.Setup(x => x.GetSingleByIdAsync(It.Is<string>(x => x == moduleId))).ReturnsAsync(module);

        // Act
        var result = await _moduleService.GetModuleInfoAsync(moduleId);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
        _moduleRepositoryMock.Verify(x => x.GetSingleByIdAsync(It.Is<string>(x => x == moduleId)));
    }

    [Test]
    public async Task WhenWrongId_ReturnsNull()
    {
        // Arrange
        var moduleId = _modules.First().Id.ToString();

        // Act
        var result = await _moduleService.GetModuleInfoAsync(moduleId);

        // Assert
        Assert.That(result, Is.Null);
        _moduleRepositoryMock.Verify(x => x.GetSingleByIdAsync(It.Is<string>(x => x == moduleId)));
    }
}
