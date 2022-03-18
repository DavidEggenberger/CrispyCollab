using Application.ChannelAggregate;
using Application.ChannelAggregate.Commands;
using Application.ChannelAggregate.Queries;
using AutoMapper;
using Common.Features.Channel;
using Common.Misc.Attributes;
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
using WebServer.Authorization;

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
        public async Task UpdateChannel([FromBody] UpdateChannelDTO updateChannelDTO, CancellationToken cancellationToken)
        {
            UpdateChannelCommand updateChannelCommand = mapper.Map<UpdateChannelCommand>(updateChannelDTO);
            await commandDispatcher.DispatchAsync(updateChannelCommand, cancellationToken);
        }

        [HttpPost]
        public async Task CreateChannel([FromBody] CreateChannelDTO createChannelDTO, CancellationToken cancellationToken)
        {
            CreateChannelCommand createChannelCommand = mapper.Map<CreateChannelCommand>(createChannelDTO);
            await commandDispatcher.DispatchAsync(createChannelCommand, cancellationToken);
        }

        [HttpPost]
        public async Task AddMessageToChannel([FromBody] AddMessageToChannelDTO addMessageToChannelDTO, CancellationToken cancellationToken)
        {
            AddMessageToChannelCommand addMessageToChannelCommand = mapper.Map<AddMessageToChannelCommand>(addMessageToChannelDTO);
            await commandDispatcher.DispatchAsync(addMessageToChannelCommand, cancellationToken);
        }

        [HttpDelete]
        [CreatorPolicy]
        public async Task DeleteMessageFromChannel([FromBody] DeleteMessageFromChannedDTO deleteMessageFromChannedDTO, CancellationToken cancellationToken)
        {
            DeleteMessageFromChannelCommand deleteMessageFromChannelCommand = mapper.Map<DeleteMessageFromChannelCommand>(deleteMessageFromChannedDTO);
            await commandDispatcher.DispatchAsync(deleteMessageFromChannelCommand, cancellationToken);
        }

        [HttpDelete("{id}")]
        [AuthorizeTeamAdmin]
        public async Task DeleteChannel([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            await commandDispatcher.DispatchAsync(new DeleteChannelCommand() { Id = id }, cancellationToken);
        }
    }
}
