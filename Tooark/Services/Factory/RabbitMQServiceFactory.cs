using Tooark.Services.Interface;
using Tooark.Services.RabbitMQ;

namespace Tooark.Services.Factory;

/// <summary>
/// Fábrica para criar instâncias de serviços RabbitMQ.
/// </summary>
public static class RabbitMQServiceFactory
{
  /// <summary>
  /// Cria uma nova instância de IRabbitMQService.
  /// </summary>
  /// <param name="connectionService">O serviço de conexão com o RabbitMQ.</param>
  /// <returns>Uma instância de IRabbitMQService.</returns>
  /// <exception cref="ArgumentNullException">Lançado se o connectionService for nulo.</exception>
  public static IRabbitMQService Create(RabbitMQConnectionService connectionService)
  {
    if (connectionService == null)
    {
      throw new ArgumentNullException(nameof(connectionService), "O serviço de conexão não pode ser nulo.");
    }

    return new RabbitMQService(connectionService);
  }
}
