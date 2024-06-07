namespace SpiritualHub.Tests.Service.BusinessService.ModuleService.CRUDMethods;

using Moq;

using Client.ViewModels.Module;
using Data.Models;

public class EditTests : MockConfiguration
{
    [Test]
    public async Task WhenSuccess()
    {
        // Arrange
        var module = new Module()
        {
            Number = 1,
            Name = "Name",
            Description = "Description",
            VideoUrl = "VideoUrl",
            Text = "Text",
            IsActive = true,
            CourseID = Guid.NewGuid(),
        };

        var updatedModule = new ModuleFormModel()
        {
            Id = module.Id.ToString(),
            Number = 2,
            Name = "name",
            Description = "description",
            VideoUrl = "video url",
            Text = "text",
            IsActive = false,
            CourseId = Guid.NewGuid().ToString(),
        };

        var expected = _mapper.Map<Module>(updatedModule);

        _moduleRepositoryMock.Setup(x => x.GetSingleByIdAsync(It.Is<string>(x => x == updatedModule.Id))).ReturnsAsync(module);

        // Act
        await _moduleService.EditAsync(updatedModule);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(module, Is.EqualTo(expected));
            Assert.That(module.CourseID, Is.EqualTo(expected.CourseID));
        });
        _moduleRepositoryMock.Verify(x => x.GetSingleByIdAsync(It.Is<string>(x => x == updatedModule.Id)));
    }

    public override void OneTimeSetup()
    {
        GenerateEntities = false;
        base.OneTimeSetup();
    }
}
