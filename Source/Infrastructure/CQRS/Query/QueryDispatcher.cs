using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.CQRS.Query
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IServiceProvider serviceProvider;

        public QueryDispatcher(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public Task<TQueryResult> DispatchAsync<TQuery, TQueryResult>(TQuery query, CancellationToken cancellation = default) where TQuery : IQuery<TQueryResult>
        {
            var handler = serviceProvider.GetRequiredService<IQueryHandler<TQuery, TQueryResult>>();
            return handler.HandleAsync(query, cancellation);
        }
    }
}
