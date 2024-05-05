namespace SpiritualHub.Tests.Service.ValidationService.TestClasses.Interfaces;

using SpiritualHub.Services.Validation.Interfaces;

public interface ITestValidationService : IValidationService
{
    string ActionUrl { get; set; }

    object RouteValue { get; set; }
}
