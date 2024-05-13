namespace SpiritualHub.Tests.Service.ValidationService.TestClasses;

using Microsoft.AspNetCore.Mvc;

using Interfaces;
using Services.Interfaces;
using Services.Validation;

public class TestAuthorValidationService : AuthorValidationService, ITestAuthorValidationService
{
    public TestAuthorValidationService(IAuthorService authorService, IPublisherService publisherService)
        : base(authorService, publisherService)
    {
    }

    public string ActionUrl { get; set; } = null!;

    public object RouteValue { get; set; } = null!;

    protected override IActionResult RedirectToAction(string action, string? controller = null, object? routeValue = null)
    {
        controller ??= ControllerName;
        ActionUrl = $"*address*/{controller}/{action}";
        if (routeValue != null)
        {
            RouteValue = routeValue;
        }

        return new RedirectResult(ActionUrl);
    }
}
