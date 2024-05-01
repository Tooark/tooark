using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;

namespace Tooark.Services.RabbitMQ;

/// <summary>
/// Serviço de inicialização para o RabbitMQ que implementa a interface IHostedService.
/// </summary>
/// <param name="channel">Canal do RabbitMQ.</param>
public class RabbitMQInitializeService(IModel channel) : IHostedService
{
  /// <summary>
  /// Canal do RabbitMQ que será utilizado pelo serviço.
  /// </summary>
  private readonly IModel _channel = channel;

  /// <summary>
  /// Método chamado quando o serviço de inicialização é iniciado.
  /// A configuração do RabbitMQ deve ser feita antes deste método ser chamado.
  /// </summary>
  /// <param name="cancellationToken">Token de cancelamento que pode ser usado para interromper o início do serviço.</param>
  /// <returns>Uma tarefa que representa a operação assíncrona de início do serviço.</returns>
  public Task StartAsync(CancellationToken cancellationToken)
  {
    // A configuração do RabbitMQ já foi feita no registro do serviço
    return Task.CompletedTask;
  }

  /// <summary>
  /// Método chamado quando o serviço de inicialização está sendo parado.
  /// Fecha o canal do RabbitMQ de forma segura.
  /// </summary>
  /// <param name="cancellationToken">Token de cancelamento que pode ser usado para interromper o encerramento do serviço.</param>
  /// <returns>Uma tarefa que representa a operação assíncrona de encerramento do serviço.</returns>
  public Task StopAsync(CancellationToken cancellationToken)
  {
    // Fecha o canal do RabbitMQ, se ele ainda estiver aberto
    _channel?.Close();
    return Task.CompletedTask;
  }
}
