using Microsoft.Extensions.DependencyInjection;
using Tooark.Mediator.Abstractions;
using Tooark.Mediator.Handlers;

namespace Tooark.Mediator;

public sealed class Mediator(IServiceProvider serviceProvider) : IMediator
{
  public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(request);

    var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
    var handler = serviceProvider.GetService(handlerType)
      ?? throw new InvalidOperationException($"Nenhum handler encontrado para {request.GetType().FullName}.");

    var handleMethod = handlerType.GetMethod(nameof(IRequestHandler<IRequest<TResponse>, TResponse>.Handle))
      ?? throw new InvalidOperationException($"Método Handle não encontrado para {handlerType.FullName}.");

    var task = handleMethod.Invoke(handler, [request, cancellationToken])
      ?? throw new InvalidOperationException($"Falha ao executar handler para {request.GetType().FullName}.");

    return await (Task<TResponse>)task;
  }

  public async Task Publish(INotification notification, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(notification);

    var handlerType = typeof(INotificationHandler<>).MakeGenericType(notification.GetType());
    var handlers = serviceProvider.GetServices(handlerType);

    var tasks = new List<Task>();

    foreach (var handler in handlers)
    {
      var handleMethod = handlerType.GetMethod(nameof(INotificationHandler<INotification>.Handle))
        ?? throw new InvalidOperationException($"Método Handle não encontrado para {handlerType.FullName}.");

      var task = handleMethod.Invoke(handler, [notification, cancellationToken]) as Task
        ?? throw new InvalidOperationException($"Falha ao executar notification handler para {notification.GetType().FullName}.");

      tasks.Add(task);
    }

    await Task.WhenAll(tasks);
  }
}
