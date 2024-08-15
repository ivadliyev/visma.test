
using Microsoft.EntityFrameworkCore;

namespace visma.test.broker.Repositories.Channel;

public class ChannelRepository(BrokerDbContext context) : IChannelRepository
{
    private readonly BrokerDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<Models.Channel> Create(Models.Channel channel)
    {
        await _context.Channels.AddAsync(channel);
        await _context.SaveChangesAsync();

        return channel;
    }

    public async Task<List<Models.Channel>> GetAll()
    {
        return await _context.Channels.ToListAsync();
    }
}