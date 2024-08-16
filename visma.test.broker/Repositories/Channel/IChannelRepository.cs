namespace visma.test.broker.Repositories.Channel;

/// <summary>
/// Channels
/// </summary>
public interface IChannelRepository
{
    /// <summary>
    /// Get list of all channels
    /// </summary>
    /// <returns>List of channels</returns>
    Task<List<Models.Channel>> GetAll();

    /// <summary>
    /// Create a new channel
    /// </summary>
    /// <param name="channel">Channel model</param>
    /// <returns>Created channel</returns>
    Task<Models.Channel> Create(Models.Channel channel);

    /// <summary>
    /// Get one channel by id
    /// </summary>
    /// <param name="id">Id of the channel</param>
    /// <returns>Channel</returns>
    Task<Models.Channel> Get(int id);
}