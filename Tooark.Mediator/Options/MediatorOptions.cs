using Tooark.Mediator.Enums;

namespace Tooark.Mediator.Options;

/// <summary>
/// Opções de configuração para o mediador.
/// </summary>
public sealed class MediatorOptions
{
  /// <summary>
  /// A estratégia de publicação de notificações a ser utilizada pelo mediador.
  /// O valor padrão é <see cref="ENotifyStrategy.ParallelWhenAll"/>.
  /// </summary>
  /// <remarks>
  /// Opções de estratégia de publicação de notificações:
  /// - <see cref="ENotifyStrategy.ParallelWhenAll"/>: Executa os manipuladores de notificações em paralelo e aguarda a conclusão de todos eles usando Task.WhenAll.
  /// - <see cref="ENotifyStrategy.Sequential"/>: Executa os manipuladores de notificações um a um, aguardando a conclusão de cada um antes de iniciar o próximo.
  /// </remarks>
  public ENotifyStrategy NotifyPublishStrategy { get; set; } = ENotifyStrategy.ParallelWhenAll;
}
