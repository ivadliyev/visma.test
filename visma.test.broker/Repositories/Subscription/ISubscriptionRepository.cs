namespace visma.test.broker.Repositories.Subscription;

/// <summary>
/// Subscriptions
/// </summary>
public interface ISubscriptionRepository
{
    /// <summary>
    /// Create a new subscription
    /// </summary>
    /// <param name="subscription">Subscription</param>
    /// <returns>Subscription</returns>
    Task<Models.Subscription> Create(Models.Subscription subscription);

    /// <summary>
    /// Get list of subscriptions by channelId
    /// </summary>
    /// <param name="channelId">Id of the channel</param>
    /// <returns>List of subscriptions</returns>
    Task<List<Models.Subscription>> GetByChannelId(int channelId);

    /// <summary>
    /// Delete subscription by id
    /// </summary>
    /// <param name="id">Id of the subscription</param>
    /// <returns>void</returns>
    Task Delete(int id);
}