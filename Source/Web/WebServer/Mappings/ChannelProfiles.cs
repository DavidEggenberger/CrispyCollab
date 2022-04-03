using Application.ChannelAggregate;
using AutoMapper;
using Common.Features.Channel;
using Domain.Aggregates.ChannelAggregate;

namespace WebServer.Mappings
{
    public class ChannelProfiles : Profile
    {
        public ChannelProfiles()
        {
            CreateMap<CreateChannelCommandDTO, CreateChannelCommand>();
            CreateMap<Channel, ChannelDTO>();
        }
    }
}
