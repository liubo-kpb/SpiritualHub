namespace SpiritualHub.Tests.Service.BusinessService.ModuleService.GetMethods;

using Moq;

using Data.Configuration.Seed;
using Services.Mappings;
using Client.ViewModels.Module;

public class GetModuleDetailsTests : MockConfiguration
{
    [Test]
    public async Task WhenSucccess()
    {
        // Arrange
        var course = new SeedCourseConfiguration().GenerateEntities().First();
        course.Modules = _modules.Where(m => m.CourseID == course.Id).ToList();

        var module = course.Modules.First(m => m.Number == 2);
        var moduleId = module.Id.ToString();

        var expectedModule = _mapper.Map<ModuleDetailsViewModule>(module);
        var expectedModulesList = new List<ModuleInfoViewModel>();
        _mapper.MapListToViewModel(course.Modules, expectedModulesList);

        _courseRepositoryMock.Setup(x => x.GetCourseWithModulesByModuleIdAsync(It.Is<string>(x => x == moduleId))).ReturnsAsync(course);

        // Act
        var result = await _moduleService.GetModuleDetailsAsync(moduleId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.EqualTo(expectedModule));
            Assert.That(result.Modules, Has.Count.EqualTo(course.Modules.Count));
            Assert.That(result.Modules, Is.EqualTo(expectedModulesList));
            Assert.That(result.CourseId, Is.EqualTo(course.Id.ToString()));
            Assert.That(result.AuthorId, Is.EqualTo(course.AuthorID.ToString()));
        });
        _courseRepositoryMock.Verify(x => x.GetCourseWithModulesByModuleIdAsync(It.Is<string>(x => x == moduleId)));
    }
}
