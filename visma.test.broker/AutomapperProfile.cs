using AutoMapper;
using System;
using visma.test.broker.Models;
using visma.test.broker.Models.Dtos;

namespace visma.test.broker;
public class AutomapperProfile : Profile
{
    public AutomapperProfile()
    {
        CreateMap<Channel, ChannelDto>();
        CreateMap<ChannelCreateDto, Channel>();
        
        CreateMap<Message, MessageDto>();
        CreateMap<MessageCreateDto, Message>();
        CreateMap<MessageEditDto, Message>();

        CreateMap<Subscription, SubscriptionDto>();
    }
}