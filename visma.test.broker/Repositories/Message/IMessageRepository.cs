namespace visma.test.broker.Repositories.Message;

/// <summary>
/// Messages
/// </summary>
public interface IMessageRepository
{
    /// <summary>
    /// Create a new message
    /// </summary>
    /// <param name="message">Message</param>
    /// <returns>Message</returns>
    Task<Models.Message> Create(Models.Message message);

    /// <summary>
    /// List all new and sent messages for subscription
    /// </summary>
    /// <param name="subscriptionId">Id of the subscription</param>
    /// <returns>List of messages</returns>
    Task<List<Models.Message>> ListNotReceivedBySubscriptionId(int subscriptionId);

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
    /// <param name="message">Message</param>
    /// <returns>Message</returns>
    Task<Models.Message> Update(int id, Models.Message message);

    /// <summary>
    /// Get message
    /// </summary>
    /// <param name="id">Id of the message</param>
    /// <returns>Message</returns>
    Task<Models.Message> Get(int id);
}