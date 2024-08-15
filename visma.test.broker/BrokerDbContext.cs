using Microsoft.EntityFrameworkCore;
using visma.test.broker.Models;

namespace visma.test.broker;

public class BrokerDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Channel> Channels => Set<Channel>();
    public DbSet<Subscription> Subscriptions => Set<Subscription>();
    public DbSet<Message> Messages => Set<Message>();
}