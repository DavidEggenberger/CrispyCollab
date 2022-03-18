using Application.Topic.CreateTopic;
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
    public class TopicController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ICommandDispatcher commandDispatcher;
        private readonly IQueryDispatcher queryDispatcher;

        public TopicController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher, IMapper mapper)
        {
            this.commandDispatcher = commandDispatcher;
            this.queryDispatcher = queryDispatcher;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task CreateTopic(CancellationToken cancellationToken)
        {
            await commandDispatcher.DispatchAsync(new CreateTopicCommand(), cancellationToken);
        }
    }
}
