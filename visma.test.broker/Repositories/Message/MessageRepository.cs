
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

    public async Task DeleteManyBySubscriptionId(int subscriptionId)
    {
        var messagesToDelete = await _context.Messages.Where(_ => _.SubscriptionId == subscriptionId).ToListAsync();
        _context.Messages.RemoveRange(messagesToDelete);
        await _context.SaveChangesAsync();
    }

    public async Task<Models.Message> Get(int id)
    {
        return await _context.Messages.FindAsync(id) ?? throw new NullReferenceException("Get message");
    }

    public async Task<List<Models.Message>> ListNotReceivedBySubscriptionId(int subscriptionId)
    {
        return await _context
            .Messages
            .Where(_ => _.SubscriptionId == subscriptionId && _.Status != Models.Enums.MessageStatusEnum.Received)
            .ToListAsync(); 
    }

    public async Task<Models.Message> Update(int id, Models.Message message)
    {
        var dbMessage = await _context.Messages.FindAsync(id);
        if (dbMessage == null) throw new NullReferenceException("Find message");

        dbMessage.Status = message.Status;
        dbMessage.Body = message.Body;

        await _context.SaveChangesAsync();
        return dbMessage;
    }
}