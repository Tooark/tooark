using Tooark.Interfaces;
using Tooark.Options;
using Tooark.Services.RabbitMQ;

namespace Tooark.Factories;

/// <summary>
/// Fábrica para criar instâncias do RabbitMQPublishService.
/// </summary>
public class RabbitMQPublishServiceFactory : IRabbitMQPublishServiceFactory
{
  /// <summary>
  /// Cria uma nova instância de IRabbitMQPublishService com os parâmetros especificados.
  /// </summary>
  /// <param name="options">Parâmetros para os serviços RabbitMQ.</param>
  /// <returns>Uma instância de IRabbitMQPublishService configurada e pronta para uso.</returns>
  /// <exception cref="ArgumentNullException">Lançado o argumento options obrigatório for nulo.</exception>
  /// <exception cref="ArgumentException">Lançado se algum dos argumentos obrigatórios for nulo ou vazio.</exception>
  /// <exception cref="ArgumentOutOfRangeException">Lançado se o valor de 'port' ou 'recoveryInterval' for zero.</exception>
  public IRabbitMQPublishService CreateRabbitMQPublishService(RabbitMQOptions options)
  {
    if (options == null)
    {
      throw new ArgumentNullException(nameof(options), "O parâmetro de 'options' do RabbitMQPublishService não podem ser nulo.");
    }

    ArgumentException.ThrowIfNullOrEmpty(options.Hostname);
    ArgumentException.ThrowIfNullOrEmpty(options.Username);
    ArgumentException.ThrowIfNullOrEmpty(options.Password);
    
    if (options.PortNumber <= 0)
    {
      options.PortNumber = 5672;
    }

    if (options.RecoveryInterval <= 0)
    {
      options.RecoveryInterval = 5;
    }

    return new RabbitMQPublishService(options);
  }
}
