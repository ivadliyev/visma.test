
using Microsoft.EntityFrameworkCore;

namespace visma.test.broker.Repositories.Subscription;

public class SubscriptionRepository(BrokerDbContext context): ISubscriptionRepository
{
    private readonly BrokerDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<Models.Subscription> Create(Models.Subscription subscription)
    {
        await _context.Subscriptions.AddAsync(subscription);
        await _context.SaveChangesAsync();

        return subscription;
    }

    public async Task Delete(int id)
    {
        var subscription = await _context.Subscriptions.FindAsync(id) ?? throw new NullReferenceException("Get subscription");
        _context.Subscriptions.Remove(subscription);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Models.Subscription>> GetByChannelId(int channelId)
    {
        return await _context.Subscriptions.Where(_ => _.ChannelId == channelId).ToListAsync();
    }
}