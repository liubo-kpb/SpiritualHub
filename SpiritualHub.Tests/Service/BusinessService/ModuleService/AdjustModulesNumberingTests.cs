namespace SpiritualHub.Tests.Service.BusinessService.ModuleService;

using Moq;

using Client.ViewModels.Module;
using Data.Models;

public class AdjustModulesNumberingTests : MockConfiguration
{
    [Test]
    [TestCase(3, TestName = "Set to be last")]
    [TestCase(2, TestName = "Set to be in the middle (U)")]
    [TestCase(22, TestName = "Number over-exceeds modules count")]
    public async Task WhenModuleIsEditedUp(int newNumber)
    {
        // Arrange
        var module = _modules.First();
        var moduleForm = _mapper.Map<ModuleFormModel>(module);
        moduleForm.Number = newNumber;

        var courseModules = _modules.Where(m => m.CourseID == module.CourseID).OrderBy(m => m.Number).ToArray();

        var modulesToAdjust = GetModulesToAdjust(courseModules, module.Id, module.Number, newNumber);

        _moduleRepositoryMock.Setup(x => x.GetModulesByCourseId(It.Is<string>(x => x == moduleForm.CourseId))).Returns(courseModules.AsQueryable());

        // Act
        await _moduleService.AdjustModulesNumberingAsync(moduleForm);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(moduleForm.Number, Is.LessThanOrEqualTo(courseModules.Length));

            int expectedValue = 1;
            for (int i = 0; i < courseModules.Length; i++)
            {
                if (courseModules[i].Id == module.Id)
                {
                    Assert.That(courseModules[i].Number, Is.Not.EqualTo(moduleForm.Number), "Edited module's number shouldn't be updated in AdjustModulesNumbering() method.");
                    Assert.That(courseModules[i].Number, Is.EqualTo(module.Number), "Edited module's number was changed.");
                }
                else
                {
                    if (expectedValue == moduleForm.Number)
                    {
                        ++expectedValue;
                    }

                    Assert.That(courseModules[i].Number, Is.EqualTo(expectedValue++), "New module order is wrong.");
                }
            }

            for (int i = 0; i < modulesToAdjust.Length; i++)
            {
                Assert.That(modulesToAdjust[i].Number, Is.EqualTo(module.Number + i), "Modules were not adjusted correctly.");
            }
        });
        _moduleRepositoryMock.Verify(x => x.GetModulesByCourseId(It.Is<string>(x => x == moduleForm.CourseId)), Times.Once);
        _moduleRepositoryMock.Verify(x => x.SaveChangesAsync());
    }

    [Test]
    [TestCase(1, TestName = "Set to be first")]
    [TestCase(2, TestName = "Set to be in the middle (D)")]
    public async Task WhenEditedDown(int newNumber)
    {
        // Arrange
        var module = _modules.Last();
        var moduleForm = _mapper.Map<ModuleFormModel>(module);
        moduleForm.Number = newNumber;

        var courseModules = _modules.Where(m => m.CourseID == module.CourseID).OrderBy(m => m.Number).ToArray();

        var modulesToAdjust = GetModulesToAdjust(courseModules, module.Id, module.Number, newNumber);

        _moduleRepositoryMock.Setup(x => x.GetModulesByCourseId(It.Is<string>(x => x == moduleForm.CourseId))).Returns(courseModules.AsQueryable());

        // Act
        await _moduleService.AdjustModulesNumberingAsync(moduleForm);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(moduleForm.Number, Is.LessThanOrEqualTo(courseModules.Length));

            int expectedValue = courseModules.Length;
            for (int i = expectedValue - 1; i >= 0; i--)
            {
                if (courseModules[i].Id == module.Id)
                {
                    Assert.That(courseModules[i].Number, Is.Not.EqualTo(moduleForm.Number), "Edited module's number shouldn't be updated in AdjustModulesNumbering() method.");
                    Assert.That(courseModules[i].Number, Is.EqualTo(module.Number), "Edited module's number was changed.");
                }
                else
                {
                    if (expectedValue == moduleForm.Number)
                    {
                        --expectedValue;
                    }

                    Assert.That(courseModules[i].Number, Is.EqualTo(expectedValue--), "New module order is wrong.");
                }
            }

            for (int i = 0; i < modulesToAdjust.Length; i++)
            {
                Assert.That(modulesToAdjust[i].Number, Is.EqualTo(moduleForm.Number + 1 + i), "Modules were not adjusted correctly.");
            }
        });
        _moduleRepositoryMock.Verify(x => x.GetModulesByCourseId(It.Is<string>(x => x == moduleForm.CourseId)), Times.Once);
        _moduleRepositoryMock.Verify(x => x.SaveChangesAsync());
    }

    [Test]
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(4)]
    [TestCase(50)]
    public async Task WhenNewModule(int moduleNumber)
    {
        // Arrange
        var courseId = _modules.First().CourseID;
        var moduleForm = new ModuleFormModel()
        {
            Number = moduleNumber,
            CourseId = courseId.ToString(),
        };

        var courseModules = _modules.Where(m => m.CourseID == courseId).OrderBy(m => m.Number).ToArray();
        var modulesToAdjust = courseModules.Where(m => m.Number >= moduleForm.Number).ToArray();

        _moduleRepositoryMock.Setup(x => x.GetModulesByCourseId(It.Is<string>(x => x == moduleForm.CourseId))).Returns(courseModules.AsQueryable());

        // Act
        await _moduleService.AdjustModulesNumberingAsync(moduleForm, true);

        // Assert
        var times = Times.AtLeastOnce;
        Assert.Multiple(() =>
        {
            int expectedValue = 1;
            for (int i = 0; i < courseModules.Length; i++)
            {
                if (expectedValue == moduleNumber)
                {
                    expectedValue++;
                }

                Assert.That(courseModules[i].Number, Is.EqualTo(expectedValue++), "New Module numbering is wrong.");
            }

            if (moduleNumber > courseModules.Length)
            {
                Assert.That(moduleForm.Number, Is.EqualTo(courseModules.Length + 1));
                Assert.That(expectedValue, Is.EqualTo(courseModules.Length + 1));
                times = Times.Never;
            }
        });
        _moduleRepositoryMock.Verify(x => x.GetModulesByCourseId(It.Is<string>(x => x == moduleForm.CourseId)), Times.Once);
        _moduleRepositoryMock.Verify(x => x.SaveChangesAsync(), times);
    }

    private static Module[] GetModulesToAdjust(Module[] courseModules, Guid editedModuleId, int moduleOldNumber, int moduleNewNumber)
    {
        var modulesToAdjust = courseModules.Where(m => m.Id != editedModuleId);

        if (moduleOldNumber < moduleNewNumber)
        {
            modulesToAdjust = courseModules.Where(m => m.Number > moduleOldNumber && m.Number <= moduleNewNumber);
        }
        else
        {
            modulesToAdjust = courseModules.Where(m => m.Number >= moduleNewNumber && m.Number < moduleOldNumber);
        }

        return modulesToAdjust.OrderBy(m => m.Number).ToArray();
    }
}
