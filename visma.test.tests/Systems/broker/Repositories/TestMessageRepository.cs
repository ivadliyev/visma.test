using FluentAssertions;
using visma.test.broker;
using visma.test.broker.Models;
using visma.test.broker.Repositories.Message;
using visma.test.tests.Fixtures;

namespace visma.test.tests.Systems.broker.Repositories;

public class TestMessageRepository: TestRepositoryBase
{
    [Test]
    public async Task Test_Create_ReturnsObjectWithId()
    {
        using var context = new BrokerDbContext(_options);
        var sut = new MessageRepository(context);

        var subscription = context.Subscriptions.First();
        var itemToInsert = MessageFixture.GetOneItemToInsert();
        itemToInsert.SubscriptionId = subscription.Id;
        var result = await sut.Create(itemToInsert);
        result.As<Message>().Id.Should().BeGreaterThan(0);
    }

    [Test]
    public async Task Test_ListNotReceivedBySubscriptionId_ReturnsNotReceivedMessages()
    {
        using var context = new BrokerDbContext(_options);
        var sut = new MessageRepository(context);

        var subscription = context.Subscriptions.First();

        var result = await sut.ListNotReceivedBySubscriptionId(subscription.Id);

        result.Should().NotBeEmpty();
        result.Select(_ => _.Status).Should().NotContain(test.broker.Models.Enums.MessageStatusEnum.Received);
    }
}