using Application.Topic.CreateTopic;
using Application.TopicAggregate.Queries;
using AutoMapper;
using Common.Features.Topic;
using Infrastructure.CQRS.Command;
using Infrastructure.CQRS.Query;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace WebServer.Controllers.Aggregates
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ICommandDispatcher commandDispatcher;
        private readonly IQueryDispatcher queryDispatcher;

        public TopicsController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher, IMapper mapper)
        {
            this.commandDispatcher = commandDispatcher;
            this.queryDispatcher = queryDispatcher;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task CreateTopic(CancellationToken cancellationToken)
        {
            await queryDispatcher.DispatchAsync(new GetAllTopicsQuery(), cancellationToken);
        }
    }
}
