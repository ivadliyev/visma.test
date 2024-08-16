using visma.test.broker.Models.Dtos;

namespace visma.test.broker.Services.Message;

/// <summary>
/// Messages
/// </summary>
public interface IMessageService
{
    /// <summary>
    /// Create a new messages by given channelId
    /// </summary>
    /// <param name="channelId">Id of the channel</param>
    /// <param name="model">MessageCreateDto</param>
    /// <returns>List of MessageDto</returns>
    Task<List<MessageDto>> CreateMany(int channelId, MessageCreateDto model);
    
    /// <summary>
    /// List not received message for subscription
    /// </summary>
    /// <param name="subscriptionId">Id of subscription</param>
    /// <returns>List of message dtos</returns>
    Task<List<MessageDto>> ListNotReceivedBySubscriptionId(int subscriptionId);

    /// <summary>
    /// Delete messages by subscriptionId
    /// </summary>
    /// <param name="subscriptionId">Id of the subscription</param>
    /// <returns>void</returns>
    Task DeleteManyBySubscriptionId(int subscriptionId);

    /// <summary>
    /// Update message
    /// </summary>
    /// <param name="id">Id of the message</param>
    /// <param name="model">Message edit model</param>
    /// <returns>Message dto</returns>
    Task<MessageDto> Update(int id, MessageEditDto model);
}