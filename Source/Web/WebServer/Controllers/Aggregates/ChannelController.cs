using Common.Features.Channel;
using Infrastructure.CQRS.Command;
using Infrastructure.CQRS.Query;
using Infrastructure.Identity;
using Infrastructure.Identity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebServer.Controllers.Aggregates
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChannelController : ControllerBase
    {
        private readonly ICommandDispatcher commandDispatcher;
        private readonly IQueryDispatcher queryDispatcher;
        private readonly TeamManager teamManager;
        public ChannelController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher, TeamManager teamManager)
        {
            this.commandDispatcher = commandDispatcher;
            this.queryDispatcher = queryDispatcher;
            this.teamManager = teamManager;
        }

        [HttpGet]
        public async Task<List<ChannelDTO>> GetChannels()
        {
            Team team = await teamManager.FindByClaimsPrincipalAsync(HttpContext.User);
            return null;
        }

        [HttpGet("{id}")]
        public async Task<List<ChannelDTO>> GetChannelById(Guid id)
        {
            Team team = await teamManager.FindByClaimsPrincipalAsync(HttpContext.User);
            return null;
        }

        [HttpPost]
        public async Task<List<ChannelDTO>> GetChannels()
        {
            Team team = await teamManager.FindByClaimsPrincipalAsync(HttpContext.User);
            return null;
        }
    }
}
