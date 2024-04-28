using Tooark.Options;

namespace Tooark.Interfaces;

/// <summary>
/// Define uma interface para a fábrica do RabbitMQPublishService.
/// </summary>
public interface IRabbitMQPublishServiceFactory
{
  /// <summary>
  /// Cria uma nova instância do RabbitMQPublishService.
  /// </summary>
  /// <param name="options">Parâmetros para os serviços RabbitMQ.</param>
  IRabbitMQPublishService CreateRabbitMQPublishService(RabbitMQOptions options);
}
