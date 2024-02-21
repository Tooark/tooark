using Microsoft.Extensions.Hosting;
using RabbitMQ.Client.Events;
using System.Text;
using Tooark.Services.Interface;

namespace Tooark.Services;

public class RabbitMQConsumerService : BackgroundService
{
  private readonly IRabbitMQService _rabbitMQService;
  private readonly string _queueName;
  private readonly Action<string> _processMessageFunc;

  public RabbitMQConsumerService(IRabbitMQService rabbitMQService, string queueName, Action<string> processMessageFunc)
  {
    _rabbitMQService = rabbitMQService;
    _queueName = queueName;
    _processMessageFunc = processMessageFunc;
  }

  protected override Task ExecuteAsync(CancellationToken stoppingToken)
  {
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
        Console.WriteLine("Erro ao processar a mensagem: {0}", ex.Message);
        // Rejeição da mensagem e reenfileiramento
        _rabbitMQService.GetChannel().BasicNack(ea.DeliveryTag, false, true);
      }
    };

    _rabbitMQService.ConsumeMessage(_queueName, consumer);

    return Task.CompletedTask;
  }
}
