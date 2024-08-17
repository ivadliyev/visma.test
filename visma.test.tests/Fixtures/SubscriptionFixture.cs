using visma.test.broker.Models;
using visma.test.broker.Models.Dtos;

namespace visma.test.tests.Fixtures;

public static class SubscriptionFixture
{
    public static Subscription GetOne()
    {
        return new Subscription() { Id = 1 };
    }

    public static List<Subscription> GetList()
    {
        return [
            new Subscription() { Id = 1 },
            new Subscription() { Id = 2 },
            new Subscription() { Id = 3 },
            new Subscription() { Id = 4 },
        ];
    }

    public static List<SubscriptionDto> GetListDto()
    {
        return [
            new SubscriptionDto() { Id = 1 },
            new SubscriptionDto() { Id = 2 },
            new SubscriptionDto() { Id = 3 },
            new SubscriptionDto() { Id = 4 },
        ];
    }
}