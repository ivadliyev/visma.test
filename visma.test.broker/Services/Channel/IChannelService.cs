using visma.test.broker.Models.Dtos;

namespace visma.test.broker.Services.Channel;

/// <summary>
/// Channels
/// </summary>
public interface IChannelService 
{
    /// <summary>
    /// List all channels
    /// </summary>
    /// <returns>List of channel dto</returns>
    Task<List<ChannelDto>> GetAll();

    /// <summary>
    /// Create a new channel
    /// </summary>
    /// <param name="model">ChannelCreateDto</param>
    /// <returns>Channel dto</returns>/
    Task<ChannelDto> Create(ChannelCreateDto model);

    /// <summary>
    /// Get one channel by id
    /// </summary>
    /// <param name="id">Id of the channel</param>
    /// <returns>ChannelDto</returns>
    Task<ChannelDto> Get(int id);
}