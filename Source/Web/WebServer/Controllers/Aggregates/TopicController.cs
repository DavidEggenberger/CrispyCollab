using Application.TopicAggregate.Queries;
using AutoMapper;
using Common.Features.Topic;
using Common.Features.Topic.Commands;
using Common.Misc.Attributes;
using Infrastructure.CQRS.Command;
using Infrastructure.CQRS.Query;
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
    public class TopicController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ICommandDispatcher commandDispatcher;
        private readonly IQueryDispatcher queryDispatcher;
        private readonly IAuthorizationService authorizationService;

        public TopicController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher, IMapper mapper, IAuthorizationService authorizationService)
        {
            this.commandDispatcher = commandDispatcher;
            this.queryDispatcher = queryDispatcher;
            this.mapper = mapper;
            this.authorizationService = authorizationService;
        }

        //[HttpGet]
        //public async Task<List<TopicDTO>> GetTopics(CancellationToken cancellationToken)
        //{
        //    await queryDispatcher.DispatchAsync(new GetAllTopicsQuery(), cancellationToken);
        //}

        //[HttpGet("{id}")]
        //public async Task<TopicDTO> GetTopicById([FromRoute] Guid id, CancellationToken cancellationToken)
        //{
        //    await queryDispatcher.DispatchAsync(new GetTopicByIdQuery() { }, cancellationToken);
        //}

        //[HttpPost]
        //public async Task CreateTopic([FromBody] CreateTopicDTO createTopicDTO, CancellationToken cancellationToken)
        //{
        //}

        //[HttpPost]
        //public async Task CreateApproach([FromBody] CreateApproachDTO createApproachDTO, CancellationToken cancellationToken)
        //{
        //}

        //[HttpDelete("{id}")]
        //[AuthorizeTeamAdmin]
        //public async Task DeleteApproach([FromRoute] Guid id, CancellationToken cancellationToken)
        //{

        //}

        //[HttpDelete("{id}")]
        //[AuthorizeTeamAdmin]
        //public async Task DeleteTopic([FromRoute] Guid id, CancellationToken cancellationToken)
        //{

        //}
    }
}
