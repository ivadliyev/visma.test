using visma.test.broker.Models.Dtos;

namespace visma.test.broker.Services.Subscription;

/// <summary>
/// Subscriptions
/// </summary>
public interface ISubscriptionService
{
    /// <summary>
    /// List subscriptions by channelId
    /// </summary>
    /// <param name="channelId">Id of the channel</param>
    /// <returns>List of subscription dto</returns>
    Task<List<SubscriptionDto>> GetByChannelId(int channelId);

    /// <summary>
    /// Create new subscription
    /// </summary>
    /// <param name="channelId">Id of the channel</param>
    /// <returns>Subscription dto</returns>
    Task<SubscriptionDto>Create(int channelId);

    /// <summary>
    /// Delete subscription
    /// </summary>
    /// <param name="id">Id of the subscription</param>
    /// <returns>void</returns>
    Task Delete(int id);
}