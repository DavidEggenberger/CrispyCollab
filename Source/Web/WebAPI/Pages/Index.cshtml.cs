using Domain.SharedKernel;
using Infrastructure.CQRS.DomainEvent;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebAPI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet([FromServices] IDomainEventDispatcher edvent)
        {
            edvent.Dispatch(new David(), new CancellationToken());
        }
    }
    public class David : IDomainEvent
    {

    }
    public class DavidHandler : IDomainEventHandler<David>
    {
        public Task Handle(David query, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }
    }
}
