using FluentAssertions;
using visma.test.broker;
using visma.test.broker.Models;
using visma.test.broker.Models.Enums;
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

    [Test]
    public async Task Test_DeleteManyBySubscriptionId_DeletesFromDb()
    {
        using var context = new BrokerDbContext(_options);
        var sut = new MessageRepository(context);
        var subscription = context.Subscriptions.OrderBy(_ => _.Id).Last();
        await sut.DeleteManyBySubscriptionId(subscription.Id);

        var result = context.Messages.Where(_ => _.SubscriptionId == subscription.Id).ToList();
        result.Should().BeEmpty();
    }

    [Test]
    public async Task Test_Get_ReturnsSame()
    {
        using var context = new BrokerDbContext(_options);
        var sut = new MessageRepository(context);
        var message = context.Messages.First();
        var result = await sut.Get(message.Id);
        result.Should().BeSameAs(message);
    }

    [Test]
    public async Task Test_Get_ThrowsException()
    {
        using var context = new BrokerDbContext(_options);
        var sut = new MessageRepository(context);

        Func<Task> act = async () => await sut.Get(999);
        await act.Should().ThrowAsync<NullReferenceException>();
    }

    [Test]
    public async Task Test_Update_UpdatesStatus()
    {
        using var context = new BrokerDbContext(_options);
        var sut = new MessageRepository(context);
        var message = context.Messages.First();
        message.Status = 
            Enum.GetValues(typeof(MessageStatusEnum))
            .Cast<MessageStatusEnum>()
            .Where(t => t != message.Status).ToList()[0];
        
        var result = await sut.Update(message.Id, message);
        result.Status.Should().Be(message.Status);
    }

    [Test]
    public async Task Test_Update_ThrowsException()
    {
        using var context = new BrokerDbContext(_options);
        var sut = new MessageRepository(context);

        Func<Task> act = async () => await sut.Update(999, new Message());
        await act.Should().ThrowAsync<NullReferenceException>();
    }
}