using Tooark.Mediator.Abstractions;

namespace Tooark.Mediator.Handlers;

/// <summary>
/// Interface para os manipuladores de notificações.
/// </summary>
/// <typeparam name="TNotify">O tipo de notificação que o manipulador processa.</typeparam>
public interface INotifyHandler<in TNotify>
  where TNotify : INotify
{
  /// <summary>
  /// Processa a notificação.
  /// </summary>
  /// <param name="notify">A notificação a ser processada.</param>
  /// <param name="cancellationToken">O token de cancelamento para a operação assíncrona.</param>
  /// <returns>Uma tarefa que representa a operação assíncrona.</returns>
  Task HandleAsync(TNotify notify, CancellationToken cancellationToken = default);
}
