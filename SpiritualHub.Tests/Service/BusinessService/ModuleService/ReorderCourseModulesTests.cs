namespace SpiritualHub.Tests.Service.BusinessService.ModuleService;

using Data.Models;

public class ReorderCourseModulesTests : MockConfiguration
{
    [Test]
    public void MultipleCases()
    {
        //Arrange
        var testCases = new Module[][]
        {
            new Module[] { new Module() { Name = "a", Number = 3 }, new Module() { Name = "a", Number = 22 }, new Module() { Name = "a", Number = 1 }, new Module() { Name = "a", Number = 4 }, new Module() { Name = "a", Number = 0 } },
            new Module[] { new Module() { Name = "a", Number = 3 }, new Module() { Name = "b", Number = 3 }, new Module() { Name = "c", Number = 3 }, new Module() { Name = "d", Number = 3 }, new Module() { Name = "e", Number = 3 } },
            new Module[] { new Module() { Name = "a", Number = 6 }, new Module() { Name = "b", Number = 5 }, new Module() { Name = "e", Number = 3 }, new Module() { Name = "d", Number = 1 }, new Module() { Name = "c", Number = 3 } },
        };

        for (int i = 0; i < testCases.Length; i++)
        {
            var modules = testCases[i];

            // Act
            _moduleService.ReorderCourseModules(modules);
            var result = modules.OrderBy(m => m.Number).ToArray();

            // Assert
            Assert.Multiple(() =>
            {
                int expectedNumber = 1;
                for (int j = 0; j < result.Length - 1; j++)
                {
                    Assert.That(result[j].Number, Is.EqualTo(expectedNumber++), "Numbering is wrong.");
                    Assert.That(result[j].Number - result[j + 1].Number, Is.EqualTo(-1), "Order is wrong.");
                }

                Assert.That(result[^1].Number, Is.EqualTo(expectedNumber), "Last Module numnber is wrong.");
            });
        }
    }

    [Test]
    public void WhenSkippingANumber()
    {
        // Arrange
        var modules = new[] { new Module() { Name = "a", Number = 1 }, new Module() { Name = "b", Number = 2 }, new Module() { Name = "c", Number = 3 }, new Module() { Name = "f", Number = 7 }, new Module() { Name = "d", Number = 4 }, new Module() { Name = "e", Number = 5 }, new Module() { Name = "g", Number = 4 }, new Module() { Name = "g", Number = 5 }, new Module() { Name = "g", Number = 22 } };
        int skipNumber = 4;

        var moduleToSkip = modules.First(m => m.Number == skipNumber);

        // Act
        _moduleService.ReorderCourseModules(modules.Where(m => m.Id != moduleToSkip.Id), skipNumber);
        var result = modules.OrderBy(m => m.Number).ToArray();

        // Assert
        Assert.Multiple(() =>
        {
            int expectedNumber = 1;
            for (int j = 0; j < result.Length - 1; j++)
            {
                Assert.That(result[j].Number, Is.EqualTo(expectedNumber++), "Numbering is wrong.");
                Assert.That(result[j].Number - result[j + 1].Number, Is.EqualTo(-1), "Order or numbering is wrong.");
            }
            Assert.That(result[^1].Number, Is.EqualTo(expectedNumber), "Last Module numnber is wrong.");
        });
    }
}
