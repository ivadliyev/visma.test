using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using visma.test.broker;
using visma.test.tests.Fixtures;

namespace visma.test.tests.Systems.broker.Repositories;

public class TestRepositoryBase
{
    protected readonly DbContextOptions<BrokerDbContext> _options;
    public TestRepositoryBase()
    {
        var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
 
        var options = new DbContextOptionsBuilder<BrokerDbContext>().UseSqlite(connection).Options;
        _options = options;

        using var context = new BrokerDbContext(options);
        context.Database.EnsureCreated();

        //seed memory db
        context.Channels.AddRange(ChannelFixture.GetListToInsert());

        context.SaveChanges();       
    }
}