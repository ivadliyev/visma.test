using visma.test.broker.Models.Enums;

namespace visma.test.broker.Models.Dtos;

public class MessageCreateDto
{
    public int SubscriptionId { get; set; }
    public string? Body { get; set; }
    public MessageStatusEnum Status { get; set; }
}