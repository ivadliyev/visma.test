using AutoMapper;
using visma.test.broker.Models.Dtos;
using visma.test.broker.Repositories.Subscription;
using visma.test.broker.Services.Message;

namespace visma.test.broker.Services.Subscription;

public class SubscriptionService(ISubscriptionRepository subscriptionRepository, IMapper mapper) : ISubscriptionService
{
    private readonly ISubscriptionRepository _subscriptionRepository = subscriptionRepository ?? throw new ArgumentNullException(nameof(subscriptionRepository));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<SubscriptionDto> Create(int channelId)
    {
        return _mapper.Map<Models.Subscription, SubscriptionDto>(await _subscriptionRepository.Create(new Models.Subscription {
            ChannelId = channelId
        }));
    }

    public async Task Delete(int id)
    {
        await _subscriptionRepository.Delete(id);
    }

    public async Task<List<SubscriptionDto>> GetByChannelId(int channelId)
    {
        return (await _subscriptionRepository.GetByChannelId(channelId))
            .Select(_ => _mapper.Map<Models.Subscription, SubscriptionDto>(_))
            .ToList();
    }
}