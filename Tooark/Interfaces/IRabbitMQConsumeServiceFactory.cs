using Tooark.Options;

namespace Tooark.Interfaces;

/// <summary>
/// Define uma interface para a fábrica do RabbitMQConsumeService.
/// </summary>
public interface IRabbitMQConsumeServiceFactory
{
  /// <summary>
  /// Cria uma nova instância do RabbitMQConsumeService.
  /// </summary>
  /// <param name="options">Parâmetros para os serviços RabbitMQ.</param>
  /// <param name="processMessageFunc">Função de callback para processar mensagens recebidas.</param> 
  IRabbitMQConsumeService CreateRabbitMQConsumeService(RabbitMQOptions options, Action<string> processMessageFunc);
}
