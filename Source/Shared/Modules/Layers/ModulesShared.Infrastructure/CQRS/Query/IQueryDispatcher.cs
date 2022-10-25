using Shared.Modules.Layers.Infrastructure.CQRS.Query;

namespace Shared.Modules.Layers.Application.CQRS.Query
{
    public interface IQueryDispatcher
    {
        Task<TQueryResult> DispatchAsync<TQuery, TQueryResult>(TQuery query, CancellationToken cancellation = default) where TQuery : IQuery<TQueryResult>;
    }
}
