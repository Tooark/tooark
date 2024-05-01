using Tooark.Interfaces;
using Tooark.Options;
using Tooark.Services.RabbitMQ;

namespace Tooark.Factories;

/// <summary>
/// Factory para criar instâncias do RabbitMQConsumeService.
/// </summary>
public class RabbitMQConsumeServiceFactory : IRabbitMQConsumeServiceFactory
{
  /// <summary>
  /// Cria uma nova instância do RabbitMQConsumeService com as opções fornecidas.
  /// </summary>
  /// <param name="options">Opções de configuração para o RabbitMQ.</param>
  /// <param name="processMessageFunc">Função de callback para processar mensagens recebidas.</param>
  /// <returns>Uma nova instância do RabbitMQConsumeService.</returns>
  public IRabbitMQConsumeService CreateRabbitMQConsumeService(RabbitMQOptions options, Action<string> processMessageFunc)
  {
    if (options == null)
    {
      throw new ArgumentNullException(nameof(options), "O parâmetro de 'options' do RabbitMQConsumeService não podem ser nulo.");
    }

    if (processMessageFunc == null)
    {
      throw new ArgumentNullException(nameof(processMessageFunc), "O parâmetro de 'processMessageFunc' do RabbitMQConsumeService não pode ser nulo.");
    }

    ArgumentException.ThrowIfNullOrEmpty(options.Hostname);
    ArgumentException.ThrowIfNullOrEmpty(options.Username);
    ArgumentException.ThrowIfNullOrEmpty(options.Password);
    ArgumentException.ThrowIfNullOrEmpty(options.QueueName);

    if (options.PortNumber <= 0)
    {
      options.PortNumber = 5672;
    }

    if (options.RecoveryInterval <= 0)
    {
      options.RecoveryInterval = 5;
    }

    return new RabbitMQConsumeService(options, processMessageFunc);
  }
}
