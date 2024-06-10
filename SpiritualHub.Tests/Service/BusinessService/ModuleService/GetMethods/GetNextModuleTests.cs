namespace SpiritualHub.Tests.Service.BusinessService.ModuleService.GetMethods;

using Client.ViewModels.Module;
using Services.Mappings;
using Data.Configuration.Seed;

public class GetNextModuleTests : MockConfiguration
{
    [Test]
    public void WhenModuleIsActive()
    {
        // Arrange
        var course = new SeedCourseConfiguration().GenerateEntities().First();
        course.Modules = _modules.Where(m => m.CourseID == course.Id).ToList();

        var module = course.Modules.First(m => m.Number == 1);
        var moduleId = module.Id.ToString();

        var moduleViewModel = _mapper.Map<ModuleDetailsViewModule>(module);
        _mapper.MapListToViewModel(course.Modules, moduleViewModel.Modules);

        var expectedId = course.Modules.First(m => m.Number == 2).Id.ToString();

        // Act
        var result = _moduleService.GetNextModuleId(moduleViewModel, false);

        // Assert
        Assert.That(result, Is.EqualTo(expectedId));
    }

    [Test]
    public void WhenSecondModuleIsInactive()
    {
        // Arrange
        var course = new SeedCourseConfiguration().GenerateEntities().First();
        course.Modules = _modules.Where(m => m.CourseID == course.Id).ToList();

        var module = course.Modules.First(m => m.Number == 1);
        var moduleId = module.Id.ToString();

        course.Modules.First(m => m.Number == 2).IsActive = false;

        var moduleViewModel = _mapper.Map<ModuleDetailsViewModule>(module);
        _mapper.MapListToViewModel(course.Modules, moduleViewModel.Modules);

        var expectedId = course.Modules.First(m => m.Number == 3).Id.ToString();

        // Act
        var result = _moduleService.GetNextModuleId(moduleViewModel, false);

        // Assert
        Assert.That(result, Is.EqualTo(expectedId));
    }

    [Test]
    public void WhenSeconModuleIsInactive_ButCanAccess()
    {
        // Arrange
        var course = new SeedCourseConfiguration().GenerateEntities().First();
        course.Modules = _modules.Where(m => m.CourseID == course.Id).ToList();

        var module = course.Modules.First(m => m.Number == 1);
        var moduleId = module.Id.ToString();

        course.Modules.First(m => m.Number == 2).IsActive = false;

        var moduleViewModel = _mapper.Map<ModuleDetailsViewModule>(module);
        _mapper.MapListToViewModel(course.Modules, moduleViewModel.Modules);

        var expectedId = course.Modules.First(m => m.Number == 2).Id.ToString();

        // Act
        var result = _moduleService.GetNextModuleId(moduleViewModel, true);

        // Assert
        Assert.That(result, Is.EqualTo(expectedId));
    }
}
