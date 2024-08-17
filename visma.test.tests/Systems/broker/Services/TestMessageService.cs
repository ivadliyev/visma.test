using FluentAssertions;
using Moq;
using visma.test.broker.Models;
using visma.test.broker.Models.Dtos;
using visma.test.broker.Repositories.Message;
using visma.test.broker.Services.Channel;
using visma.test.broker.Services.Message;
using visma.test.broker.Services.Subscription;
using visma.test.tests.Fixtures;

namespace visma.test.tests.Systems.broker.Services;

public class TestMessageService: TestServiceBase
{
    private readonly Mock<IMessageRepository> _mockMessageRepository;
    private readonly Mock<IChannelService> _mockChannelService;
    private readonly Mock<ISubscriptionService> _mockSubscriptionService;
    private const int idForValue = 1;
    private const int idForNull = 999;

    public TestMessageService()
    {
        _mockMessageRepository = new Mock<IMessageRepository>();
        _mockMessageRepository
            .Setup(_ => _.Create(It.IsAny<Message>()))
            .ReturnsAsync(MessageFixture.GetOne());

        _mockMessageRepository
            .Setup(_ => _.DeleteManyBySubscriptionId(It.IsAny<int>()))
            .Verifiable();

        _mockMessageRepository
            .Setup(_ => _.Get(idForValue))
            .ReturnsAsync(MessageFixture.GetOne());

        _mockMessageRepository
            .Setup(_ => _.Get(999))
            .ReturnsAsync(() => null);

        _mockMessageRepository
            .Setup(_ => _.ListNotReceivedBySubscriptionId(It.IsAny<int>()))
            .ReturnsAsync(MessageFixture.GetList());

        _mockMessageRepository
            .Setup(_ => _.Update(It.IsAny<int>(), It.IsAny<Message>()))
            .ReturnsAsync(MessageFixture.GetOne());

        _mockChannelService = new Mock<IChannelService>();
        _mockChannelService
            .Setup(_ => _.Get(idForValue))
            .ReturnsAsync(ChannelFixture.GetOneDto());
        _mockChannelService
            .Setup(_ => _.Get(999))
            .ReturnsAsync(() => null);

        _mockSubscriptionService = new Mock<ISubscriptionService>();
        _mockSubscriptionService
            .Setup(_ => _.GetByChannelId(It.IsAny<int>()))
            .ReturnsAsync(SubscriptionFixture.GetListDto());
    }

    [Test]
    public async Task Test_CreateMany_InvokesReposAndServices()
    {
        var sut = new MessageService(_mockMessageRepository.Object, _mapper, _mockChannelService.Object, _mockSubscriptionService.Object);

        var result = await sut.CreateMany(idForValue, MessageFixture.GetOneCreateDto());
        _mockMessageRepository.Verify(_ => _.Create(It.IsAny<Message>()), Times.AtLeastOnce);
        _mockChannelService.Verify(_ => _.Get(It.IsAny<int>()), Times.AtLeastOnce);
        _mockSubscriptionService.Verify(_ => _.GetByChannelId(It.IsAny<int>()), Times.AtLeastOnce);
    }

    [Test]
    public async Task Test_CreateMany_ReturnsDtosList()
    {
        var sut = new MessageService(_mockMessageRepository.Object, _mapper, _mockChannelService.Object, _mockSubscriptionService.Object);

        var result = await sut.CreateMany(idForValue, MessageFixture.GetOneCreateDto());
        result.Should().AllBeOfType(typeof(MessageDto));
    }

    [Test]
    public async Task Test_CreateMany_ThrowsException()
    {
        var sut = new MessageService(_mockMessageRepository.Object, _mapper, _mockChannelService.Object, _mockSubscriptionService.Object);
        
        Func<Task> act = async () => await sut.CreateMany(idForNull, MessageFixture.GetOneCreateDto());
        await act.Should().ThrowAsync<NullReferenceException>();
    }

    [Test]
    public async Task Test_DeleteManyBySubscriptionId_InvokesRepository()
    {
        var sut = new MessageService(_mockMessageRepository.Object, _mapper, _mockChannelService.Object, _mockSubscriptionService.Object);

        await sut.DeleteManyBySubscriptionId(idForValue);
        _mockMessageRepository.Verify(_ => _.DeleteManyBySubscriptionId(It.IsAny<int>()), Times.AtLeastOnce);
    }

    [Test]
    public async Task Test_ListNotReceivedBySubscriptionId_InvokesRepository()
    {
        var sut = new MessageService(_mockMessageRepository.Object, _mapper, _mockChannelService.Object, _mockSubscriptionService.Object);

        var result = await sut.ListNotReceivedBySubscriptionId(1);
        _mockMessageRepository.Verify(_ => _.ListNotReceivedBySubscriptionId(It.IsAny<int>()), Times.AtLeastOnce);
        _mockMessageRepository.Verify(_ => _.Update(It.IsAny<int>(), It.IsAny<Message>()), Times.AtLeastOnce);
    }

    [Test]
    public async Task Test_ListNotReceivedBySubscriptionId_ReturnsDtosList()
    {
        var sut = new MessageService(_mockMessageRepository.Object, _mapper, _mockChannelService.Object, _mockSubscriptionService.Object);

        var result = await sut.ListNotReceivedBySubscriptionId(idForValue);
        result.Should().AllBeOfType(typeof(MessageDto));
    }

    [Test]
    public async Task Test_Update_InvokesRepository()
    {
        var sut = new MessageService(_mockMessageRepository.Object, _mapper, _mockChannelService.Object, _mockSubscriptionService.Object);

        var result = await sut.Update(idForValue, MessageFixture.GetOneEditDto());
        _mockMessageRepository.Verify(_ => _.Update(It.IsAny<int>(), It.IsAny<Message>()), Times.AtLeastOnce);
    }

    [Test]
    public async Task Test_Update_ThrowsException()
    {
        var sut = new MessageService(_mockMessageRepository.Object, _mapper, _mockChannelService.Object, _mockSubscriptionService.Object);

        Func<Task> act = async () => await sut.Update(idForNull, MessageFixture.GetOneEditDto());
        await act.Should().ThrowAsync<NullReferenceException>();
    }
}