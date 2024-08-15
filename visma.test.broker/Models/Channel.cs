using System.ComponentModel.DataAnnotations;

namespace visma.test.broker.Models;

public class Channel 
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }
}