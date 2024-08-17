using FluentAssertions;
using Moq;
using visma.test.broker.Models;
using visma.test.broker.Models.Dtos;
using visma.test.broker.Repositories.Subscription;
using visma.test.broker.Services.Subscription;
using visma.test.tests.Fixtures;

namespace visma.test.tests.Systems.broker.Services;

public class TestSubscriptionService: TestServiceBase
{
    private readonly Mock<ISubscriptionRepository> _mockSubscriptionRepository;

    public TestSubscriptionService()
    {
        _mockSubscriptionRepository = new Mock<ISubscriptionRepository>();
        _mockSubscriptionRepository
            .Setup(_ => _.Create(It.IsAny<Subscription>()))
            .ReturnsAsync(SubscriptionFixture.GetOne());

        _mockSubscriptionRepository
            .Setup(_ => _.GetByChannelId(It.IsAny<int>()))
            .ReturnsAsync(SubscriptionFixture.GetList());

        _mockSubscriptionRepository
            .Setup(_ => _.Delete(It.IsAny<int>()))
            .Verifiable();  
    }

    [Test]
    public async Task Test_GetByChannelId_InvokesRepository()
    {
        var sut = new SubscriptionService(_mockSubscriptionRepository.Object, _mapper);

        var result = await sut.GetByChannelId(1);
        _mockSubscriptionRepository.Verify(_ => _.GetByChannelId(It.IsAny<int>()), Times.AtLeastOnce);
    }

    [Test]
    public async Task Test_GetByChannelId_ReturnsDtosList()
    {
        var sut = new SubscriptionService(_mockSubscriptionRepository.Object, _mapper);

        var result = await sut.GetByChannelId(1);
        result.Should().AllBeOfType(typeof(SubscriptionDto));
    }

    [Test]
    public async Task Test_Delete_InvokesRepository()
    {
        var sut = new SubscriptionService(_mockSubscriptionRepository.Object, _mapper);

        await sut.Delete(1);
        _mockSubscriptionRepository.Verify(_ => _.Delete(It.IsAny<int>()), Times.AtLeastOnce);
    }

    [Test]
    public async Task Test_Create_InvokesRepository()
    {
        var sut = new SubscriptionService(_mockSubscriptionRepository.Object, _mapper);

        var result = await sut.Create(1);
        _mockSubscriptionRepository.Verify(_ => _.Create(It.IsAny<Subscription>()), Times.AtLeastOnce);
    }

    [Test]
    public async Task Test_Create_ReturnsDto()
    {
        var sut = new SubscriptionService(_mockSubscriptionRepository.Object, _mapper);

        var result = await sut.Create(1);

        result.Should().NotBeNull().And.BeOfType(typeof(SubscriptionDto));
    }
}