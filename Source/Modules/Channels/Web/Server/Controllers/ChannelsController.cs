using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Shared.Features.CQRS.Query;
using Shared.Features.CQRS.Command;
using Modules.Channels.Web.Shared;
using Modules.Channels.Features.Aggregates.ChannelAggregate;
using Modules.Channels.Features.Aggregates.ChannelAggregate.Application.Commands;
using Modules.Channels.Features.Aggregates.ChannelAggregate.Application.Queries;
using Shared.Kernel.BuildingBlocks.Authorization.Attributes;
using Shared.Kernel.BuildingBlocks.Auth.Constants;

namespace Modules.Channels.Web.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ChannelsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ICommandDispatcher commandDispatcher;
        private readonly IQueryDispatcher queryDispatcher;
        private readonly IAuthorizationService authorizationService;

        public ChannelsController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher, IMapper mapper, IAuthorizationService authorizationService)
        {
            this.commandDispatcher = commandDispatcher;
            this.queryDispatcher = queryDispatcher;
            this.mapper = mapper;
            this.authorizationService = authorizationService;
        }

        [HttpGet]
        public async Task<List<ChannelDTO>> GetChannels(CancellationToken cancellationToken)
        {
            List<Channel> channels = await queryDispatcher.DispatchAsync<GetAllChannels, List<Channel>>(new GetAllChannels(), cancellationToken);
            return mapper.Map<List<ChannelDTO>>(channels);
        }

        [HttpGet("{id}")]
        public async Task<ChannelDTO> GetChannelById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            Channel channel = await queryDispatcher.DispatchAsync<GetChannelById, Channel>(new GetChannelById() { Id = id }, cancellationToken);
            return mapper.Map<ChannelDTO>(channel);
        }

        [HttpPost]
        public async Task CreateChannel([FromBody] ChannelDTO createChannelDTO, CancellationToken cancellationToken)
        {
            CreateChannel createChannelCommand = mapper.Map<CreateChannel>(createChannelDTO);
            await commandDispatcher.DispatchAsync(createChannelCommand, cancellationToken);
        }

        [HttpPut("{id}")]
        [AuthorizeTenantAdmin]
        public async Task UpdateChannel([FromBody] ChannelDTO updateChannelDTO, CancellationToken cancellationToken)
        {
            ChangeChannelName updateChannelCommand = mapper.Map<ChangeChannelName>(updateChannelDTO);
            await commandDispatcher.DispatchAsync(updateChannelCommand, cancellationToken);
        }

        [HttpPost("createMessage")]
        public async Task AddMessageToChannel([FromBody] AddMessageToChannelDTO addMessageToChannelDTO, CancellationToken cancellationToken)
        {
            AddMessageToChannel addMessageToChannelCommand = mapper.Map<AddMessageToChannel>(addMessageToChannelDTO);
            await commandDispatcher.DispatchAsync(addMessageToChannelCommand, cancellationToken);
        }

        [HttpDelete("message/{id}")]
        public async Task<ActionResult> DeleteMessageFromChannel([FromBody] DeleteMessageFromChannedDTO deleteMessageFromChannedDTO, CancellationToken cancellationToken)
        {
            DeleteMessageFromChannel deleteMessageFromChannelCommand = mapper.Map<DeleteMessageFromChannel>(deleteMessageFromChannedDTO);
            Message message = await queryDispatcher.DispatchAsync<GetMessageById, Message>(new GetMessageById() { Id = deleteMessageFromChannelCommand.MessageId }, cancellationToken);
            if ((await authorizationService.AuthorizeAsync(HttpContext.User, message, PolicyConstants.CreatorPolicy)).Succeeded)
            {
                await commandDispatcher.DispatchAsync(deleteMessageFromChannelCommand, cancellationToken);
                return Ok();
            }
            else
            {
                return Forbid();
            }
        }

        [HttpDelete("{id}")]
        [AuthorizeTenantAdmin]
        public async Task<ActionResult> DeleteChannel([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            Channel channel = await queryDispatcher.DispatchAsync<GetChannelById, Channel>(new GetChannelById { Id = id }, cancellationToken);
            if ((await authorizationService.AuthorizeAsync(HttpContext.User, channel, "CreatorPolicy")).Succeeded)
            {
                await commandDispatcher.DispatchAsync(new DeleteChannel() { ChannelId = id }, cancellationToken);
                return Ok();
            }
            else
            {
                return Forbid();
            }
        }
    }
}
