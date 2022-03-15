using Application.Topic.CreateTopic;
using Common.Features.Topic;
using Infrastructure.CQRS.Command;
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
        private readonly ICommandDispatcher commandDispatcher;
        public TopicController(ICommandDispatcher commandDispatcher)
        {
            this.commandDispatcher = commandDispatcher;
        }

        [HttpGet]
        public async Task CreateTopic(CancellationToken cancellationToken)
        {
            await commandDispatcher.DispatchAsync<CreateTopicCommand>(new CreateTopicCommand(), cancellationToken);
        }
    }
}
