namespace SpiritualHub.WebAPI.Responses;

public class ErrorResponse
{
    public ErrorResponse()
    {
        ErrorMessages = new HashSet<string>();
    }

    public bool HasError { get; set; }

    public ICollection<string> ErrorMessages { get; set; }
}
