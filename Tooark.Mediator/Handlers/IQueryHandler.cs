using Tooark.Mediator.Abstractions;

namespace Tooark.Mediator.Handlers;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
  where TQuery : IQuery<TResponse>
{
}
