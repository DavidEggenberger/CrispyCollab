using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Shared.Features.Messaging.Query;
using Shared.Features.Messaging.Command;
using Modules.Channels.Features.DomainFeatures.Channels;
using Modules.Channels.Features.DomainFeatures.Channels.Application.Commands;
using Modules.Channels.Features.DomainFeatures.Channels.Application.Queries;
using Shared.Kernel.BuildingBlocks.Authorization.Attributes;
using Modules.Channels.Web.Shared.DTOs.ChannelAggregate;
using Shared.Features.Server;

namespace Modules.Channels.Web.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChannelsController : BaseController
    {
        public ChannelsController(IServiceProvider serviceProvider) : base(serviceProvider) { }

        [HttpGet]
        public async Task<ActionResult> GetChannels(CancellationToken cancellationToken)
        {
            var channels = await queryDispatcher.DispatchAsync<GetAllChannels, List<Channel>>(new GetAllChannels(), cancellationToken);
            
            return Ok(channels);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetChannelById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var channel = await queryDispatcher.DispatchAsync<GetChannelById, Channel>(new GetChannelById() { Id = id }, cancellationToken);
            
            return Ok(channel);
        }

        [HttpPost]
        public async Task CreateChannel([FromBody] ChannelDTO createChannelDTO, CancellationToken cancellationToken)
        {
            //var createChannelCommand = mapper.Map<CreateChannel>(createChannelDTO);
            //await commandDispatcher.DispatchAsync(createChannelCommand, cancellationToken);
        }

        [HttpPut("{id}")]
        [AuthorizeTenantAdmin]
        public async Task UpdateChannel([FromBody] ChannelDTO updateChannelDTO, CancellationToken cancellationToken)
        {
            //ChangeChannelName updateChannelCommand = mapper.Map<ChangeChannelName>(updateChannelDTO);
            //await commandDispatcher.DispatchAsync(updateChannelCommand, cancellationToken);
        }

        //[HttpPost("createMessage")]
        //public async Task AddMessageToChannel([FromBody] AddMessageToChannelDTO addMessageToChannelDTO, CancellationToken cancellationToken)
        //{
        //    AddMessageToChannel addMessageToChannelCommand = mapper.Map<AddMessageToChannel>(addMessageToChannelDTO);
        //    await commandDispatcher.DispatchAsync(addMessageToChannelCommand, cancellationToken);
        //}

        //[HttpDelete("message/{id}")]
        //public async Task<ActionResult> DeleteMessageFromChannel([FromBody] DeleteMessageFromChannedDTO deleteMessageFromChannedDTO, CancellationToken cancellationToken)
        //{
        //    DeleteMessageFromChannel deleteMessageFromChannelCommand = mapper.Map<DeleteMessageFromChannel>(deleteMessageFromChannedDTO);
        //    Message message = await queryDispatcher.DispatchAsync<GetMessageById, Message>(new GetMessageById() { Id = deleteMessageFromChannelCommand.MessageId }, cancellationToken);
        //    if ((await authorizationService.AuthorizeAsync(HttpContext.User, message, PolicyConstants.CreatorPolicy)).Succeeded)
        //    {
        //        await commandDispatcher.DispatchAsync(deleteMessageFromChannelCommand, cancellationToken);
        //        return Ok();
        //    }
        //    else
        //    {
        //        return Forbid();
        //    }
        //}

        [HttpDelete("{id}")]
        [AuthorizeTenantAdmin]
        public async Task<ActionResult> DeleteChannel([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            Channel channel = await queryDispatcher.DispatchAsync<GetChannelById, Channel>(new GetChannelById { Id = id }, cancellationToken);


            return Ok(channel);
        }
    }
}
