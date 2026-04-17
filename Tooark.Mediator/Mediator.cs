using Microsoft.Extensions.DependencyInjection;
using Tooark.Exceptions;
using Tooark.Mediator.Abstractions;
using Tooark.Mediator.Enums;
using Tooark.Mediator.Handlers;
using Tooark.Mediator.Options;

namespace Tooark.Mediator;

/// <summary>
/// Implementação do mediador.
/// </summary>
/// <remarks>
/// O mediador é responsável por enviar requisições para os manipuladores correspondentes e publicar
/// notificações para os manipuladores de notificações registrados. Ele utiliza o <see cref="IServiceProvider"/>
/// para resolver os manipuladores necessários para processar as mensagens.
/// </remarks>
/// <param name="serviceProvider">O provedor de serviços para resolver os manipuladores.</param>
/// <param name="options">As opções de configuração do mediador.</param>
public sealed class Mediator(IServiceProvider serviceProvider, MediatorOptions options) : IMediator
{
  #region Private Fields

  /// <summary>
  /// O provedor de serviços para resolver os manipuladores necessários para processar as mensagens.
  /// </summary>
  private readonly IServiceProvider _serviceProvider = serviceProvider;

  /// <summary>
  /// As opções de configuração do mediador.
  /// </summary>
  private readonly MediatorOptions _options = options;

  #endregion

  #region Constructors

  /// <summary>
  /// Construtor do mediador que aceita apenas o provedor de serviços, usando as opções padrão.
  /// </summary>
  public Mediator(IServiceProvider serviceProvider) : this(serviceProvider, new MediatorOptions())
  { }

  #endregion

  #region Methods

  /// <inheritdoc/>
  public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
  {
    // Verifica se a requisição é nula e lança uma exceção personalizada se for o caso.
    if (request is null)
    {
      throw new BadRequestException("Request.Null");
    }

    // Obtém o tipo da requisição e constrói o tipo do manipulador correspondente.
    var requestType = request.GetType();
    var handlerType = typeof(IRequestHandler<,>).MakeGenericType(requestType, typeof(TResponse));

    // Tenta resolver o manipulador do provedor de serviços. Se não encontrar, lança uma exceção.
    var handler = _serviceProvider.GetService(handlerType)
      ?? throw new InternalServerErrorException($"Handler.NotFound;{requestType.FullName}");

    // Tenta obter o método HandleAsync do manipulador. Se não encontrar, lança uma exceção.
    var handleMethod = handlerType.GetMethod(nameof(IRequestHandler<IRequest<TResponse>, TResponse>.HandleAsync))
      ?? throw new InternalServerErrorException($"Handler.Method.NotFound;{handlerType.FullName}");

    // Tenta invocar o método HandleAsync do manipulador com a requisição e o token de cancelamento. Se falhar, lança uma exceção.
    var task = handleMethod.Invoke(handler, [request, cancellationToken])
      ?? throw new InternalServerErrorException($"Handler.ExecutionFailed;{requestType.FullName}");

    return await (Task<TResponse>)task;
  }

  /// <inheritdoc/>
  public async Task PublishAsync(INotify notify, CancellationToken cancellationToken = default)
  {
    // Verifica se a notificação é nula e lança uma exceção personalizada se for o caso.
    if (notify is null)
    {
      throw new BadRequestException("Notify.Null");
    }

    // Obtém o tipo da notificação e constrói o tipo do manipulador de notificações correspondente.
    var notifyType = notify.GetType();
    var handlerType = typeof(INotifyHandler<>).MakeGenericType(notifyType);

    // Tenta resolver os manipuladores de notificações do provedor de serviços.
    var handlers = _serviceProvider.GetServices(handlerType);

    // Cria uma lista de tarefas para armazenar as tarefas de execução dos manipuladores de notificações.
    var tasks = new List<Task>();

    // Itera sobre os manipuladores de notificações encontrados.
    foreach (var handler in handlers)
    {
      // Tenta obter o método HandleAsync do manipulador de notificações. Se não encontrar, lança uma exceção.
      var handleMethod = handlerType.GetMethod(nameof(INotifyHandler<INotify>.HandleAsync))
        ?? throw new InternalServerErrorException($"Handler.Method.NotFound;{handlerType.FullName}");

      // Tenta invocar o método HandleAsync do manipulador de notificações com a notificação e o token de cancelamento. Se falhar, lança uma exceção.
      var task = handleMethod.Invoke(handler, [notify, cancellationToken]) as Task
        ?? throw new InternalServerErrorException($"Handler.ExecutionFailed;{notifyType.FullName}");

      tasks.Add(task);
    }

    // Verifica se a estratégia de publicação de notificações é sequencial.
    if (_options.NotifyPublishStrategy == ENotifyStrategy.Sequential)
    {
      // Itera sobre as tarefas de execução dos manipuladores de notificações.
      foreach (var task in tasks)
      {
        await task;
      }

      return;
    }

    await Task.WhenAll(tasks);
  }

  #endregion
}
