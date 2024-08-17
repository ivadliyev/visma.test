using visma.test.broker.Models;
using visma.test.broker.Models.Dtos;

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

    public static Message GetOne()
    {
        return new Message() 
        {
            Id = 1,
            Body = "Test body"
        };
    }

    public static List<Message> GetList()
    {
        return [
            new Message() { Id = 1, Status = broker.Models.Enums.MessageStatusEnum.Created, Body = "Test 1" },
            new Message() { Id = 2, Status = broker.Models.Enums.MessageStatusEnum.Created, Body = "Test 2" },
            new Message() { Id = 3, Status = broker.Models.Enums.MessageStatusEnum.Sent, Body = "Test 3" }
        ];
    }

    public static MessageCreateDto GetOneCreateDto()
    {
        return new MessageCreateDto()
        {
            Body = "Test body"
        };
    }

    public static MessageEditDto GetOneEditDto()
    {
        return new MessageEditDto()
        {
            Status = broker.Models.Enums.MessageStatusEnum.Received
        };
    }
}