using visma.test.broker.Models.Enums;

namespace visma.test.broker.Models.Dtos;

public class MessageDto
{
    public int Id { get; set; }
    public int SubscriptionId { get; set; }
    public string? Body { get; set; }
    public MessageStatusEnum Status { get; set; }
}