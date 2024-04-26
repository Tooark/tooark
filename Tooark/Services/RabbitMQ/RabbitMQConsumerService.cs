using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client.Events;
using System.Text;
using Tooark.Interfaces;
using Tooark.Services.Interface;

namespace Tooark.Services.RabbitMQ;

/// <summary>
/// Serviço de background para consumir mensagens do RabbitMQ.
/// </summary>
/// <remarks>
/// Inicializa uma nova instância do serviço de consumo RabbitMQ.
/// </remarks>
/// <param name="logger">Logger para registrar informações e erros.</param>
/// <param name="rabbitMQService">Serviço para interação com o RabbitMQ.</param>
/// <param name="queueName">Nome da fila de onde as mensagens serão consumidas.</param>
/// <param name="processMessageFunc">Função para processar a mensagem recebida.</param>
public class RabbitMQConsumerService(
  ILogger<RabbitMQConsumerService> logger,
  IRabbitMQService rabbitMQService,
  string queueName, Action<string> processMessageFunc) : BackgroundService
{
  private readonly ILogger<RabbitMQConsumerService> _logger = logger;
  private readonly IRabbitMQService _rabbitMQService = rabbitMQService;
  private readonly string _queueName = queueName;
  private readonly Action<string> _processMessageFunc = processMessageFunc;

  /// <summary>
  /// Executa o serviço de consumo em background.
  /// </summary>
  /// <param name="stoppingToken">Token de cancelamento para parar o serviço.</param>
  /// <returns>Uma tarefa que representa a operação assíncrona.</returns>
  protected override Task ExecuteAsync(CancellationToken stoppingToken)
  {
    stoppingToken.ThrowIfCancellationRequested();

    var consumer = new EventingBasicConsumer(_rabbitMQService.GetChannel());

    consumer.Received += (model, ea) =>
    {
      var body = ea.Body.ToArray();
      var message = Encoding.UTF8.GetString(body);
      try
      {
        // Processar a mensagem recebida
        _processMessageFunc(message);

        // Confirmação de sucesso da mensagem
        _rabbitMQService.GetChannel().BasicAck(ea.DeliveryTag, false);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Erro ao processar a mensagem: {Error}", ex.Message);

        // Rejeição da mensagem e reenfileira
        _rabbitMQService.GetChannel().BasicNack(ea.DeliveryTag, false, true);
      }
    };

    _rabbitMQService.ConsumeMessage(_queueName, consumer);

    return Task.CompletedTask;
  }
}
