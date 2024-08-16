using AutoMapper;
using visma.test.broker.Models.Dtos;
using visma.test.broker.Repositories.Message;
using visma.test.broker.Services.Channel;
using visma.test.broker.Services.Subscription;

namespace visma.test.broker.Services.Message;

public class MessageService(IMessageRepository messageRepository, 
    IMapper mapper,
    IChannelService channelService,
    ISubscriptionService subscriptionService) : IMessageService
{
    private readonly IMessageRepository _messageRepository = messageRepository ?? throw new ArgumentNullException(nameof(messageRepository));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly IChannelService _channelService = channelService ?? throw new ArgumentNullException(nameof(channelService));
    private readonly ISubscriptionService _subscriptionService = subscriptionService ?? throw new ArgumentNullException(nameof(subscriptionService));

    public async Task<List<MessageDto>> CreateMany(int channelId, MessageCreateDto model)
    {
        var channel = await _channelService.Get(channelId) ?? throw new NullReferenceException("Get channel");
        var subscriptions = await _subscriptionService.GetByChannelId(channel.Id);

        var result = new List<Models.Message>();

        subscriptions.ForEach(async s => {
            var messageToCreate = _mapper.Map<MessageCreateDto, Models.Message>(model);
            messageToCreate.SubscriptionId = s.Id;
            messageToCreate.Status = Models.Enums.MessageStatusEnum.Created;
            await _messageRepository.Create(messageToCreate);
            result.Add(messageToCreate);
        });

        return result.Select(_ => _mapper.Map<Models.Message, MessageDto>(_)).ToList();
    }

    public Task DeleteManyBySubscriptionId(int subscriptionId)
    {
        return _messageRepository.DeleteManyBySubscriptionId(subscriptionId);
    }

    public async Task<List<MessageDto>> ListNotReceivedBySubscriptionId(int subscriptionId)
    {
        var messages = await _messageRepository.ListNotReceivedBySubscriptionId(subscriptionId);

        messages.ForEach(async m => {
            m.Status = Models.Enums.MessageStatusEnum.Sent;
            await _messageRepository.Update(m.Id, m);
        });

        return messages
            .Select(_ => _mapper.Map<Models.Message, MessageDto>(_))
            .ToList();
    }

    public async Task<MessageDto> Update(int id, MessageEditDto model)
    {
        var message = await _messageRepository.Get(id) ?? throw new NullReferenceException("Get message");
        
        message = _mapper.Map(model, message);
        await _messageRepository.Update(id, message);

        return _mapper.Map<Models.Message, MessageDto>(message);
    }
}