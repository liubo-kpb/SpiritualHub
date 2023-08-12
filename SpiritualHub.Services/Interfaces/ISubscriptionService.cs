namespace SpiritualHub.Services.Interfaces;

public interface ISubscriptionService
{
    Task<bool> ExistsByIdAsync(string id);
}
