
using visma.test.broker.Services.Message;
using visma.test.broker.Services.Subscription;

namespace visma.test.broker.Services.SubscriptionMessage;

public class SubscriptionMessageService(ISubscriptionService subscriptionService, IMessageService messageService) : ISubscriptionMessageService
{
    private readonly ISubscriptionService _subscriptionService = subscriptionService ?? throw new ArgumentNullException(nameof(subscriptionService));
    private readonly IMessageService _messageService = messageService ?? throw new ArgumentNullException(nameof(messageService));
    
    public async Task Delete(int subscriptionId)
    {
        await _messageService.DeleteManyBySubscriptionId(subscriptionId);
        await _subscriptionService.Delete(subscriptionId);
    }
}