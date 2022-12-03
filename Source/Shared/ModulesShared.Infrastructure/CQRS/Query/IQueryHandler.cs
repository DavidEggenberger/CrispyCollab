using Shared.Modules.Layers.Infrastructure.CQRS.Query;

namespace Shared.Modules.Layers.Application.CQRS.Query
{
    public interface IQueryHandler<in TQuery, TQueryResult> where TQuery : IQuery<TQueryResult>
    {
        Task<TQueryResult> HandleAsync(TQuery query, CancellationToken cancellation);
    }
}
