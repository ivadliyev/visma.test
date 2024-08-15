using FluentAssertions;
using visma.test.broker;
using visma.test.broker.Models;
using visma.test.broker.Repositories.Channel;
using visma.test.tests.Fixtures;

namespace visma.test.tests.Systems.broker.Repositories;

public class TestChannelRepository: TestRepositoryBase
{
    [Test]
    public async Task Test_GetAll_ReturnsNotEmpty() 
    {
         using var context = new BrokerDbContext(_options);
         var sut = new ChannelRepository(context);

         var result = await sut.GetAll();
         result.Should().NotBeEmpty();
    }

    [Test]
    public async Task Test_Create_ReturnsObjectWithId()
    {
        using var context = new BrokerDbContext(_options);
        var sut = new ChannelRepository(context);

        var result = await sut.Create(ChannelFixture.GetOneItemToInsert());
        result.As<Channel>().Id.Should().BeGreaterThan(0);
    }
}