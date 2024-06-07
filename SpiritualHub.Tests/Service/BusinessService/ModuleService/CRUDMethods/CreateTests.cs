namespace SpiritualHub.Tests.Service.BusinessService.ModuleService.CRUDMethods;

using Moq;
using AutoMapper;

using Client.ViewModels.Module;
using Data.Models;

public class CreateTests : MockConfiguration
{
    private Mock<IMapper> _mapperMock;

    [Test]
    public async Task WhenSuccess()
    {
        // Arrange
        var newModuleForm = new ModuleFormModel()
        {
            Number = 2,
            Name = "name",
            Description = "description",
            VideoUrl = "video url",
            Text = "text",
            IsActive = false,
            CourseId = Guid.NewGuid().ToString(),
        };

        var newModuleEntity = new Module()
        {
            Number = newModuleForm.Number,
            Name = newModuleForm.Name,
            Description = newModuleForm.Description,
            VideoUrl = newModuleForm.VideoUrl,
            Text = newModuleForm.Text,
            IsActive = newModuleForm.IsActive,
            CourseID = Guid.Parse(newModuleForm.CourseId),
        };

        _mapperMock.Setup(x => x.Map<Module>(It.Is<ModuleFormModel>(x => x.Equals(newModuleForm)))).Returns(newModuleEntity);

        var expectedId = newModuleEntity.Id.ToString();

        // Act
        var result = await _moduleService.CreateAsync(newModuleForm);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.EqualTo(expectedId));
        });
        _mapperMock.Verify(x => x.Map<Module>(It.Is<ModuleFormModel>(x => x.Equals(newModuleForm))));
        _moduleRepositoryMock.Verify(x => x.AddAsync(It.Is<Module>(x => x.Equals(newModuleEntity))));
        _moduleRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    public override void OneTimeSetup()
    {
        GenerateEntities = false;
        _mapperMock = new Mock<IMapper>();
        _mapper = _mapperMock.Object;
    }
}
