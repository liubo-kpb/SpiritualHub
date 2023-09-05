namespace SpiritualHub.WebAPI.Responses;

public class SingleResponse<TViewModel> : BaseResponse
    where TViewModel : class
{
    public TViewModel Model { get; set; } = null!;
}
