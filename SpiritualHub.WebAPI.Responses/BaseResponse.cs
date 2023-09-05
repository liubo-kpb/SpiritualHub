namespace SpiritualHub.WebAPI.Responses;

public class BaseResponse
{
    public BaseResponse()
    {
        Error = new ErrorResponse();
    }

    public ErrorResponse Error { get; }

    public void AddError(string message)
    {
        Error.HasError = true;
        Error.ErrorMessages.Add(message);
    }
}
