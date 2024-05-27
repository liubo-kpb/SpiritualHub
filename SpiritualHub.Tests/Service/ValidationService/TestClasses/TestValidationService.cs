namespace SpiritualHub.Tests.Service.ValidationService.TestClasses;

using Microsoft.AspNetCore.Mvc;

using Interfaces;
using Services.Interfaces;
using Services.Validation;
using Client.Infrastructure.Enums;

public class TestValidationService : ValidationService, ITestValidationTrait
{
    public TestValidationService(IPublisherService publisherService)
        : base(publisherService)
    {
        ITestValidationTrait testTrait = this;

        this.ExistsAsyncFunc = testTrait.ExistsMethodDefinition;
        this.GetAuthorIdAsyncFunc = testTrait.GetAuthorIdMethodDefinition;
        this.GetUserIdFunc = testTrait.GetUserIdMethodDefinition;
        this.IsUserAdminFunc = testTrait.IsUserAdminMethodDefinition;
        this.SetTempDataMessageAction = testTrait.SetTempDataMethodDefinition;

        this.Exists = true;
        this.IsAdmin = false;

        this.AuthorId = "authorId";
        this.UserId = "userId";

        this.ActualNotificationType = NotificationType.Null;
    }

    public string ActionUrl { get; set; } = null!;

    public object RouteValue { get; set; } = null!;

    public int ExistsCallCount { get; set; }

    public bool Exists { get; set; }

    public string AuthorId { get; }

    public string ActualEntityId { get; set; } = null!;

    public int GetUserIdCallCount { get; set; }

    public string UserId { get; }

    public int AdminCheckCallCount { get; set; }

    public bool IsAdmin { get; set; }

    public NotificationType ActualNotificationType { get; set; }

    public string ActualErrorMessage { get; set; } = null!;

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
