namespace SpiritualHub.Tests.Extensions.Common;

public static class TestErrorMessagesConstants
{
    public const string NotDbUpdateExceptionErrorMessage = "Database did not throw an exception when updating.";
    public const string NoNullReferenceExceptionErrorMessage = "Method did not throw a NullReferenceException.";
    public const string NoArgumentExceptionErrorMessage = "Method did not throw a ArgumentException.";

    public const string EntityWasUpdatedErrorMessage = "Entity was but shouldn't have been updated.";
}
