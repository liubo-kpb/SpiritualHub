namespace SpiritualHub.Common;

public static class ErrorMessagesConstants
{
    public const string GeneralUnexpectedErrorMessage = "An unexpected error occurred when attempting to {0}. Please try again later!";
    public const string NoEntityFoundErrorMessage = "No such {0} found. Please try again with a valid {0}!";

    public const string NoConnectedAuthorsErrorMessage = "You need to be affiliated with at least one author to be able to create an event.";
    public const string NotAConnectedPublisherErrorMessage = "You need to be a publisher of this author to be able to make changes.";
    public const string NotAPublisherErrorMessage = "You need to be a publisher to access this page.";
    public const string PublishersCannotSubscribeErrorMessage = "Publishers cannot subscribe to authors.";
    public const string UserHasSubscriptionErrorMessage = "You musn't have subscriptions to become a publisher";
    public const string AlreadyAPublisherErrorMessage = "You are already a publisher.";
    public const string AlreadyAConnectedPublisherErrorMessage = "You are already a connected publisher for this author.";

    public const string PhoneAlreadyRegisteredErrorMessage = "Phone number is already registered for a publisher!";

    public const string SpecifyParticipationErrorMessage = "You need to specify the event's participation type.";
}