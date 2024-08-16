
using Microsoft.EntityFrameworkCore;

namespace visma.test.broker.Repositories.Message;

public class MessageRepository(BrokerDbContext context): IMessageRepository
{
    private readonly BrokerDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<Models.Message> Create(Models.Message message)
    {
        await _context.Messages.AddAsync(message);
        await _context.SaveChangesAsync();

        return message;
    }

    public async Task<List<Models.Message>> ListNotReceivedBySubscriptionId(int subscriptionId)
    {
        return await _context
            .Messages
            .Where(_ => _.SubscriptionId == subscriptionId && _.Status != Models.Enums.MessageStatusEnum.Received)
            .ToListAsync(); 
    }
}