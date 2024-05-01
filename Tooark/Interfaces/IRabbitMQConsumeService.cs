using RabbitMQ.Client;

namespace Tooark.Interfaces;

/// <summary>
/// Define um contrato para serviços de consumo RabbitMQ que podem ser iniciados e parados.
/// </summary>
public interface IRabbitMQConsumeService
{
  /// <summary>
  /// Obtém o canal de comunicação com o RabbitMQ.
  /// </summary>
  /// <returns>O canal de comunicação.</returns>
  IModel GetChannel();
  
  /// <summary>
  /// Inicia o serviço de consumo RabbitMQ de forma assíncrona.
  /// </summary>
  /// <param name="cancellationToken">Um token de cancelamento que pode ser usado para enviar um sinal de cancelamento para o serviço.</param>
  Task StartServiceAsync(CancellationToken cancellationToken);

  /// <summary>
  /// Para o serviço de consumo RabbitMQ de forma assíncrona.
  /// </summary>
  /// <param name="cancellationToken">Um token de cancelamento que pode ser usado para enviar um sinal de cancelamento para o serviço.</param>
  Task StopServiceAsync(CancellationToken cancellationToken);
}
