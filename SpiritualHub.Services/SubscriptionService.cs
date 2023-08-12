namespace SpiritualHub.Services;

using System.Threading.Tasks;

using Interfaces;
using Data.Models;
using Data.Repository.Interface;

public class SubscriptionService : ISubscriptionService
{
    private readonly IRepository<Subscription> _subscriptionRepository;

    public SubscriptionService(IRepository<Subscription> subscriptionRepository)
    {
        _subscriptionRepository = subscriptionRepository;
    }

    public async Task<bool> ExistsByIdAsync(string id)
    {
        return await _subscriptionRepository.AnyAsync(s => s.Id.ToString() == id);
    }
}
