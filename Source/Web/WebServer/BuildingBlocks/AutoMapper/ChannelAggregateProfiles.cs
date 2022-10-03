using Application.ChannelAggregate;
using AutoMapper;
using WebShared.Features.Channel;
using Domain.Aggregates.ChannelAggregate;
using Modules.ChannelModule.Domain;

namespace WebServer.Mappings
{
    public class ChannelAggregateProfiles : Profile
    {
        public ChannelAggregateProfiles()
        {
            CreateMap<CreateChannelCommandDTO, CreateChannelCommand>();
            CreateMap<Channel, ChannelDTO>();
        }
    }
}
