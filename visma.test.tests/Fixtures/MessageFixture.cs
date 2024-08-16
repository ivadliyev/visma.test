using visma.test.broker.Models;

namespace visma.test.tests.Fixtures;

public static class MessageFixture
{
    public static List<Message> GetListToInsert()
    {
        return [
            new Message() { Status = broker.Models.Enums.MessageStatusEnum.Created, Body = "Test 1" },
            new Message() { Status = broker.Models.Enums.MessageStatusEnum.Created, Body = "Test 2" },
            new Message() { Status = broker.Models.Enums.MessageStatusEnum.Sent, Body = "Test 3" },
            new Message() { Status = broker.Models.Enums.MessageStatusEnum.Received, Body = "Test 4" },
        ];
    }

    public static Message GetOneItemToInsert()
    {
        return new Message() { Status = broker.Models.Enums.MessageStatusEnum.Created, Body = "Test body" };
    }
}