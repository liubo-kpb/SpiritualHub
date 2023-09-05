namespace SpiritualHub.WebAPI.Responses;

public class CollectionResponse<TViewModel> : BaseResponse
    where TViewModel : class
{
    public CollectionResponse()
    {
        Data = new HashSet<TViewModel>();
    }

    public IEnumerable<TViewModel> Data { get; set; }
}
