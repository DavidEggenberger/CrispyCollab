using Application.ChannelAggregate;
using Application.ChannelAggregate.Queries;
using AutoMapper;
using Common.Features.Channel;
using Domain.Aggregates.ChannelAggregate;
using Infrastructure.CQRS.Command;
using Infrastructure.CQRS.Query;
using Infrastructure.Identity;
using Infrastructure.Identity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace WebServer.Controllers.Aggregates
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChannelController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ICommandDispatcher commandDispatcher;
        private readonly IQueryDispatcher queryDispatcher;
        public ChannelController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher, IMapper mapper)
        {
            this.commandDispatcher = commandDispatcher;
            this.queryDispatcher = queryDispatcher;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<List<ChannelDTO>> GetChannels(CancellationToken cancellationToken)
        {
            List<Channel> channels = await queryDispatcher.DispatchAsync(new GetAllChannelsQuery(), cancellationToken);
            return mapper.Map<List<ChannelDTO>>(channels);
        }

        [HttpGet("{id}")]
        public async Task<ChannelDTO> GetChannelById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            Channel channel = await queryDispatcher.DispatchAsync(new GetChannelByIdQuery() { Id = id }, cancellationToken);
            return mapper.Map<ChannelDTO>(channel);
        }

        [HttpPost]
        public async Task CreateChannel([FromBody] ChannelDTO channelDTO, CancellationToken cancellationToken)
        {
            Channel channel = mapper.Map<Channel>(channelDTO);
            await commandDispatcher.DispatchAsync(new CreateChannelCommand() {  }, cancellationToken);
        }
    }
}
