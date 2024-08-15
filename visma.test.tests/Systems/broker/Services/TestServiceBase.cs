using AutoMapper;
using Moq;
using visma.test.broker;

namespace visma.test.tests.Systems.broker.Services;

public class TestServiceBase
{
    protected readonly IMapper _mapper;

    public TestServiceBase()
    {
        var mapperConfiguration = new MapperConfiguration(cfg => 
        {
            cfg.AddProfile(new AutomapperProfile());
        });
        _mapper = mapperConfiguration.CreateMapper();
    }
}