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
}