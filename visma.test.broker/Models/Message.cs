using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using visma.test.broker.Models.Enums;

namespace visma.test.broker.Models;

public class Message
{
    [Key]
    public int Id { get; set; }
    [ForeignKey("Subscription")]
    public int SubscriptionId { get; set; }
    public Subscription? Subscription { get; set; }

    public string? Body { get; set; }
    public MessageStatusEnum Status { get; set; }
}