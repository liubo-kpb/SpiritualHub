namespace SpiritualHub.Tests.Service.BusinessService.ModuleService.CRUDMethods;

using Moq;

using Data.Models;
using Client.ViewModels.Module;

public class DeleteTests : MockConfiguration
{
    public class SingleModule : DeleteTests
    {

        [Test]
        public async Task WhenSuccess()
        {
            // Arrange
            var module = new Module();

            var moduleId = module.Id.ToString();

            _moduleRepositoryMock.Setup(x => x.GetSingleByIdAsync(It.Is<string>(x => x == moduleId))).ReturnsAsync(module);

            // Act
            await _moduleService.DeleteAsync(moduleId);

            // Assert
            _moduleRepositoryMock.Verify(x => x.GetSingleByIdAsync(It.Is<string>(x => x == moduleId)));
            _moduleRepositoryMock.Verify(x => x.Delete(It.Is<Module>(x => x.Equals(module))));
            _moduleRepositoryMock.Verify(x => x.SaveChangesAsync());
        }

        [Test]
        public void WhenWrongId()
        {
            // Arrange
            var moduleId = "wrongId";

            // Act & Assert
            Assert.DoesNotThrowAsync(async () => await _moduleService.DeleteAsync(moduleId));
        }

        public override void OneTimeSetup()
        {
            GenerateEntities = false;
        }
    }

    public class MultipleModules : DeleteTests
    {
        [Test]
        public void WhenSuccess()
        {
            // Arrange
            var moduleEntities = new List<Module>(_modules.Where(m => m.CourseID == _modules.First().CourseID));

            var deletedModules = new List<CourseModuleFormModel>();

            var moduleFormModel = _mapper.Map<CourseModuleFormModel>(moduleEntities[0]);
            moduleFormModel.IsDeleted = true;

            deletedModules.Add(moduleFormModel);

            moduleFormModel = _mapper.Map<CourseModuleFormModel>(moduleEntities[2]);
            moduleFormModel.IsDeleted = true;

            deletedModules.Add(moduleFormModel);

            var expected = new List<Module>()
            {
                moduleEntities[0],
                moduleEntities[2],
            };

            // Act
            var result = _moduleService.DeleteMultiple(moduleEntities, deletedModules);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.EqualTo(expected));
                Assert.That(result.Any(m => m.Id == moduleEntities[1].Id), Is.False);
            });
            _moduleRepositoryMock.Verify(x => x.DeleteMultiple(It.IsAny<ICollection<Module>>()));
        }

        [Test]
        public void WhenNoEntities()
        {
            // Arrange
            var moduleEntities = new List<Module>(_modules.Where(m => m.CourseID == _modules.First().CourseID));

            var deletedModules = new List<CourseModuleFormModel>();

            var moduleFormModel = _mapper.Map<CourseModuleFormModel>(moduleEntities[0]);
            moduleFormModel.IsDeleted = true;

            deletedModules.Add(moduleFormModel);

            moduleFormModel = _mapper.Map<CourseModuleFormModel>(moduleEntities[2]);
            moduleFormModel.IsDeleted = true;

            deletedModules.Add(moduleFormModel);

            moduleEntities = new List<Module>();
            var expected = moduleEntities;


            // Act
            var result = _moduleService.DeleteMultiple(moduleEntities, deletedModules);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
            _moduleRepositoryMock.Verify(x => x.DeleteMultiple(It.IsAny<ICollection<Module>>()));
        }
    }
}
