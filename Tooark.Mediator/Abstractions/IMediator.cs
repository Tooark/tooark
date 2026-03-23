namespace Tooark.Mediator.Abstractions;

public interface IMediator
{
  Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);

  Task Publish(INotification notification, CancellationToken cancellationToken = default);
}
