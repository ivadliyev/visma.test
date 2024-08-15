using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace visma.test.broker.Models;

public class Subscription
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Channel")]
    public int ChannelId { get; set; }
    public Channel? Channel { get; set; }
}