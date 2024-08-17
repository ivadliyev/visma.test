using FluentAssertions;
using Moq;
using visma.test.broker.Models;
using visma.test.broker.Models.Dtos;
using visma.test.broker.Repositories.Channel;
using visma.test.broker.Services.Channel;
using visma.test.tests.Fixtures;

namespace visma.test.tests.Systems.broker.Services;

public class TestChannelService: TestServiceBase
{
    private readonly Mock<IChannelRepository> _mockChannelRepository;

    public TestChannelService()
    {
        _mockChannelRepository = new Mock<IChannelRepository>();
        _mockChannelRepository
            .Setup(_ => _.GetAll())
            .ReturnsAsync(ChannelFixture.GetList());

        _mockChannelRepository
            .Setup(_ => _.Create(It.IsAny<Channel>()))
            .ReturnsAsync(ChannelFixture.GetOne());

        _mockChannelRepository
            .Setup(_ => _.Get(It.IsAny<int>()))
            .ReturnsAsync(ChannelFixture.GetOne());
    }

    [Test]
    public async Task Test_GetAll_InvokesRepository()
    {
        var sut = new ChannelService(_mockChannelRepository.Object, _mapper);

        var result = await sut.GetAll();
        _mockChannelRepository.Verify(_ => _.GetAll(), Times.AtLeastOnce);
    }

    [Test]
    public async Task Test_GetAll_ReturnsDtosList()
    {
        var sut = new ChannelService(_mockChannelRepository.Object, _mapper);

        var result = await sut.GetAll();
        result.Should().AllBeOfType(typeof(ChannelDto));
    }

    [Test]
    public async Task Test_Create_InvokesRepository()
    {
        var sut = new ChannelService(_mockChannelRepository.Object, _mapper);

        var result = await sut.Create(ChannelFixture.GetOneCreateDto());
        _mockChannelRepository.Verify(_ => _.Create(It.IsAny<Channel>()), Times.AtLeastOnce);
    }

    [Test]
    public async Task Test_Create_ReturnsDto()
    {
        var sut = new ChannelService(_mockChannelRepository.Object, _mapper);

        var result = await sut.Create(ChannelFixture.GetOneCreateDto());

        result.Should().NotBeNull().And.BeOfType(typeof(ChannelDto));
    }

    [Test]
    public async Task Test_Get_InvokesRepository()
    {
        var sut = new ChannelService(_mockChannelRepository.Object, _mapper);

        var result = await sut.Get(1);
        _mockChannelRepository.Verify(_ => _.Get(It.IsAny<int>()), Times.AtLeastOnce);
    }

    [Test]
    public async Task Test_Get_ReturnsDto()
    {
        var sut = new ChannelService(_mockChannelRepository.Object, _mapper);

        var result = await sut.Get(1);

        result.Should().NotBeNull().And.BeOfType(typeof(ChannelDto));
    }
}