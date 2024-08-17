using visma.test.broker.Models;
using visma.test.broker.Models.Dtos;

namespace visma.test.tests.Fixtures;

public static class ChannelFixture
{
    public static List<Channel> GetListToInsert()
    {
        return [
            new Channel() { Name = "Default", Subscriptions = [ new Subscription(), new Subscription() ] },
            new Channel() { Name = "Channel 1", Subscriptions = [ new Subscription(), new Subscription() ] }
        ];
    }

    public static Channel GetOneItemToInsert()
    {
        return new Channel() { Name = "Channel 2" };
    }

    public static List<Channel> GetList()
    {
        return [
            new Channel() { Id = 1, Name = "Default" },
            new Channel() { Id = 2, Name = "Channel 1" }
        ];
    }

    public static Channel GetOne()
    {
        return new Channel() { Id = 1, Name = "Default" };
    }

    public static ChannelDto GetOneDto()
    {
        return new ChannelDto() { Id = 1, Name = "Default" };
    }

    public static ChannelCreateDto GetOneCreateDto()
    {
        return new ChannelCreateDto() { Name = "Default" };
    }
}