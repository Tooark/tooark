using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System.Text;
using Tooark.Exceptions;
using Tooark.Services.Interface;
using Tooark.Services.RabbitMQ;

namespace Tooark.Services;

internal class RabbitMQService : IRabbitMQService
{
  private readonly RabbitMQConnectionService _connectionService;

  public RabbitMQService(RabbitMQConnectionService connectionService)
  {
    _connectionService = connectionService;
  }

  public void PublishMessage(string message)
  {
    try
    {
      using var _channel = _connectionService.CreateChannel();

      var body = Encoding.UTF8.GetBytes(message);

      _channel.BasicPublish(
        exchange: "exchange_fanout",
        routingKey: "",
        basicProperties: null,
        body: body);
    }
    catch (BrokerUnreachableException ex)
    {
      throw new RabbitMQServiceException("Não foi possível alcançar o broker do RabbitMQ.", ex);
    }
    catch (Exception ex)
    {
      throw new RabbitMQServiceException("Erro ao publicar mensagem.", ex);
    }
  }

  public void PublishMessage(string message, string routingKey)
  {
    try
    {
      using var _channel = _connectionService.CreateChannel();

      var body = Encoding.UTF8.GetBytes(message);

      _channel.BasicPublish(
        exchange: "exchange_direct",
        routingKey: routingKey,
        basicProperties: null,
        body: body);
    }
    catch (BrokerUnreachableException ex)
    {
      throw new RabbitMQServiceException("Não foi possível alcançar o broker do RabbitMQ.", ex);
    }
    catch (Exception ex)
    {
      throw new RabbitMQServiceException("Erro ao publicar mensagem.", ex);
    }
  }

  public void ConsumeMessage(string queueName, EventingBasicConsumer consumer)
  {
    try
    {
      using var _channel = _connectionService.CreateChannel();

      _channel.BasicConsume(
        queue: queueName,
        autoAck: true,
        consumer: consumer);
    }
    catch (BrokerUnreachableException ex)
    {
      throw new RabbitMQServiceException("Operação interrompida durante o consumo da mensagem.", ex);
    }
    catch (Exception ex)
    {
      throw new RabbitMQServiceException("Erro ao consumir mensagem.", ex);
    }
  }
}
