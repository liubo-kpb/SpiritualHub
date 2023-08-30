namespace SpiritualHub.Services.Interfaces;

using Models.Event;
using Client.ViewModels.Event;

public interface IEventService
{
    Task<int> GetAllCountAsync();

    Task<FilteredEventsServiceModel> GetAllAsync(AllEventsQueryModel queryModel, string userId);
}
