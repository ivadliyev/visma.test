using AutoMapper;
using visma.test.broker.Models.Dtos;
using visma.test.broker.Repositories.Channel;

namespace visma.test.broker.Services.Channel;

public class ChannelService(IChannelRepository channelRepository, IMapper mapper) : IChannelService
{
    private readonly IChannelRepository _channelRepository = channelRepository ?? throw new ArgumentNullException(nameof(channelRepository));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<ChannelDto> Create(ChannelCreateDto model)
    {
        var channel = _mapper.Map<ChannelCreateDto, Models.Channel>(model);
        return _mapper.Map<Models.Channel, ChannelDto>(await _channelRepository.Create(channel));
    }

    public async Task<List<ChannelDto>> GetAll()
    {
        return (await _channelRepository.GetAll())
            .Select(_ => _mapper.Map<Models.Channel, ChannelDto>(_))
            .ToList();
    }
}
