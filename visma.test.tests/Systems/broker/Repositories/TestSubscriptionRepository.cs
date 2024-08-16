using FluentAssertions;
using visma.test.broker;
using visma.test.broker.Models;
using visma.test.broker.Repositories.Subscription;

namespace visma.test.tests.Systems.broker.Repositories;

public class TestSubscriptionRepository: TestRepositoryBase
{
    [Test]
    public async Task Test_Create_ReturnsObjectWithId()
    {
        using var context = new BrokerDbContext(_options);
        var sut = new SubscriptionRepository(context);
        var channel = context.Channels.First();

        var result = await sut.Create(new test.broker.Models.Subscription {
            ChannelId = channel.Id
        });
        result.As<Subscription>().Id.Should().BeGreaterThan(0);
    }

    [Test]
    public async Task Test_Delete_DeletesFromDb()
    {
        using var context = new BrokerDbContext(_options);
        var sut = new SubscriptionRepository(context);
        var channel = context.Channels.First();
        var subscription = new Subscription {
            ChannelId = channel.Id
        };
        context.Subscriptions.Add(subscription);
        context.SaveChanges();

        await sut.Delete(subscription.Id);
        var result = context.Subscriptions.Find(subscription.Id);
        result.Should().BeNull();
    }

    [Test]
    public async Task Test_GetByChannelId_ReturnsNotEmpty()
    {
        using var context = new BrokerDbContext(_options);
        var sut = new SubscriptionRepository(context);
        var channel = context.Channels.First();
        var subscription = new Subscription {
            ChannelId = channel.Id
        };
        context.Subscriptions.Add(subscription);
        context.SaveChanges();

        var result = await sut.GetByChannelId(channel.Id);
        result.Should().NotBeEmpty();
    }
}