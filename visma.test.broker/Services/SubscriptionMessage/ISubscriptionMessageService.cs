namespace visma.test.broker.Services.SubscriptionMessage;

/// <summary>
/// Subscription messages
/// </summary>
public interface ISubscriptionMessageService 
{
    /// <summary>
    /// Delete subscription and messages
    /// </summary>
    /// <param name="subscriptionId">Id of the subscription</param>
    /// <returns>void</returns>
    Task Delete(int subscriptionId);
}