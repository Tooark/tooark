using System.Text;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Tooark.Exceptions;
using Tooark.Options;

namespace Tooark.Services.RabbitMQ;

/// <summary>
/// Serviço de background para consumir mensagens de uma fila RabbitMQ.
/// Este serviço é responsável por estabelecer uma conexão com o RabbitMQ,
/// declarar a fila e iniciar o consumo de mensagens.
/// </summary>
public class RabbitMQConsumeService : BackgroundService
{
  private readonly ILogger<RabbitMQConsumeService> _logger;
  private readonly Action<string> _processMessageFunc;
  private readonly IModel _channel;
  private readonly string _queueName;

  /// <summary>
  /// Inicializa uma nova instância do serviço RabbitMQConsumeService.
  /// </summary>
  /// <param name="logger">Logger para registrar informações e erros.</param>
  /// <param name="options">Opções de configuração para o RabbitMQ.</param>
  /// <param name="processMessageFunc">Função de callback para processar mensagens recebidas.</param>
  public RabbitMQConsumeService(ILogger<RabbitMQConsumeService> logger, RabbitMQOptions options, Action<string> processMessageFunc)
  {
    _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    if (options != null)
    {
      _processMessageFunc = processMessageFunc ?? throw new ArgumentNullException(nameof(processMessageFunc));

      try
      {
        _queueName = options.QueueName;

        var factory = new ConnectionFactory
        {
          HostName = options.Hostname,
          Port = options.PortNumber,
          UserName = options.Username,
          Password = options.Password,
          AutomaticRecoveryEnabled = options.AutomaticRecovery,
          NetworkRecoveryInterval = TimeSpan.FromSeconds(options.RecoveryInterval)
        };

        var _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.QueueDeclare(
          queue: options.QueueName,
          durable: options.Durable,
          exclusive: options.Exclusive,
          autoDelete: options.AutoDelete,
          arguments: null);
      }
      catch (Exception ex)
      {
        throw new RabbitMQServiceException(
          "Não foi possível estabelecer uma conexão com o servidor RabbitMQ.",
          ex);
      }
    }
    else
    {
      throw new ArgumentNullException(nameof(options));
    }
  }

  /// <summary>
  /// Executa o serviço de consumo em um loop assíncrono.
  /// </summary>
  /// <param name="stoppingToken">Token de cancelamento para sinalizar a parada do serviço.</param>
  /// <returns>Uma tarefa que representa o loop de consumo assíncrono.</returns>
  protected override Task ExecuteAsync(CancellationToken stoppingToken)
  {
    stoppingToken.ThrowIfCancellationRequested();

    var consumer = new EventingBasicConsumer(_channel);

    consumer.Received += (moduleHandle, eventArgs) =>
    {
      try
      {
        var body = eventArgs.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);

        _processMessageFunc(message);
      }
      catch (Exception ex)
      {
        _logger.LogError(
          ex,
          "Erro ao processar a mensagem recebida.\n{Error}",
          ex.Message);

        throw new RabbitMQServiceException("Erro ao consumir a mensagem", ex);
      }
    };

    _channel.BasicConsume(queue: _queueName, autoAck: false, consumer: consumer);

    return Task.CompletedTask;
  }
}
