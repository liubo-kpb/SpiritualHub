namespace SpiritualHub.Tests.Service.ValidationService.BaseValidation.TestClasses.Interfaces;

using SpiritualHub.Services.Validation.Interfaces;

public interface ITestValidationService : IValidationService
{
    string ActionUrl { get; set; }

    object RouteValue { get; set; }
}
