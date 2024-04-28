using Tooark.Options;

namespace Tooark.Interfaces;

/// <summary>
/// Define uma interface para a fábrica do RabbitMQService.
/// </summary>
public interface IRabbitMQServiceFactory
{
  /// <summary>
  /// Cria uma nova instância do RabbitMQService.
  /// </summary>
  /// <param name="options">Parâmetros para os serviços RabbitMQ.</param>
  IRabbitMQService CreateRabbitMQService(RabbitMQOptions options);
}
