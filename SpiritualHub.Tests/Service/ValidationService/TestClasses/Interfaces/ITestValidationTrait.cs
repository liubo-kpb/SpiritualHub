namespace SpiritualHub.Tests.Service.ValidationService.TestClasses.Interfaces;

using SpiritualHub.Client.Infrastructure.Enums;

public interface ITestValidationTrait
{
    string ActionUrl { get; set; }

    object RouteValue { get; set; }

    int ExistsCallCount { get; set; }

    string EntityId { get; set; }

    bool Exists { get; set; }

    string AuthorId { get; set; }

    string ActualEntityId { get; set; }

    int GetUserIdCallCount { get; set; }

    string UserId { get; set; }

    int AdminCheckCallCount { get; set; }

    bool IsAdmin { get; set; }


    NotificationType ActualNotificationType { get; set; }

    string ActualErrorMessage { get; set; }

    Task<bool> ExistsMethodDefinition(string id)
    {
        ExistsCallCount++;

        if (ActualEntityId != id)
        {
            ActualEntityId = id;
        }

        return Task.FromResult(Exists);
    }

    Task<string> GetAuthorIdMethodDefinition(string id)
    {
        if (ActualEntityId != id)
        {
            ActualEntityId = id;
        }

        return Task.FromResult(AuthorId);
    }

    string GetUserIdMethodDefinition()
    {
        GetUserIdCallCount++;
        return UserId;
    }

    bool IsUserAdminMethodDefinition()
    {
        AdminCheckCallCount++;
        return IsAdmin;
    }

    void SetTempDataMethodDefinition(NotificationType type, string message)
    {
        ActualNotificationType = type;
        ActualErrorMessage = message;
    }
}