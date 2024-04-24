using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System.Text;
using Tooark.Exceptions;
using Tooark.Services.Interface;
using Tooark.Services.RabbitMQ;

namespace Tooark.Services;

/// <summary>
/// Serviço para interagir com o RabbitMQ, permitindo publicar e consumir mensagens.
/// </summary>
internal class RabbitMQService : IRabbitMQService
{
  private readonly RabbitMQConnectionService _connectionService;
  private readonly IModel _channel;

  /// <summary>
  /// Construtor recebe serviço de conexão RabbitMQ.
  /// </summary>
  /// <param name="connectionService">Serviço de conexão com o RabbitMQ.</param>
  public RabbitMQService(RabbitMQConnectionService connectionService)
  {
    _connectionService = connectionService;
    _channel = _connectionService.CreateChannel();
  }

  /// <summary>
  /// Publica uma mensagem em um exchange do tipo 'fanout'.
  /// </summary>
  /// <param name="message">A mensagem a ser publicada.</param>
  public void PublishMessage(string message)
  {
    string exchange = "exchange_fanout";

    try
    {
      using var _channel = _connectionService.CreateChannel();

      var body = Encoding.UTF8.GetBytes(message);

      _channel.BasicPublish(
        exchange: exchange,
        routingKey: "",
        basicProperties: null,
        body: body);
    }
    catch (BrokerUnreachableException ex)
    {
      throw new RabbitMQServiceException(
        "Não foi possível alcançar o broker do RabbitMQ.",
        exchange,
        ex);
    }
    catch (Exception ex)
    {
      throw new RabbitMQServiceException(
        "Erro ao publicar mensagem.",
        exchange,
        ex);
    }
  }

  /// <summary>
  /// Publica uma mensagem em um exchange do tipo 'direct' com uma chave de roteamento específica.
  /// </summary>
  /// <param name="message">A mensagem a ser publicada.</param>
  /// <param name="routingKey">A chave de roteamento para a mensagem.</param>
  public void PublishMessage(string message, string routingKey)
  {
    string exchange = "exchange_direct";

    try
    {
      using var _channel = _connectionService.CreateChannel();

      var body = Encoding.UTF8.GetBytes(message);

      _channel.BasicPublish(
        exchange: exchange,
        routingKey: routingKey,
        basicProperties: null,
        body: body);
    }
    catch (BrokerUnreachableException ex)
    {
      throw new RabbitMQServiceException(
        "Não foi possível alcançar o broker do RabbitMQ.",
        exchange,
        routingKey,
        ex);
    }
    catch (Exception ex)
    {
      throw new RabbitMQServiceException(
        "Erro ao publicar mensagem.",
        exchange,
        routingKey,
        ex);
    }
  }

  /// <summary>
  /// Consome mensagens de uma fila específica.
  /// </summary>
  /// <param name="queueName">O nome da fila de onde as mensagens serão consumidas.</param>
  /// <param name="consumer">O consumidor que processará as mensagens recebidas.</param>
  public void ConsumeMessage(string queueName, EventingBasicConsumer consumer)
  {
    try
    {
      using var _channel = _connectionService.CreateChannel();

      _channel.BasicConsume(
        queue: queueName,
        autoAck: false,
        consumer: consumer);
    }
    catch (BrokerUnreachableException ex)
    {
      throw new RabbitMQServiceException(
        "Operação interrompida durante o consumo da mensagem.",
        ex);
    }
    catch (Exception ex)
    {
      throw new RabbitMQServiceException(
        "Erro ao consumir mensagem.",
        ex);
    }
  }

  /// <summary>
  /// Obtém o canal de comunicação com o RabbitMQ.
  /// Este canal é usado para publicar e consumir mensagens do RabbitMQ.
  /// </summary>
  /// <returns>O canal de comunicação com o RabbitMQ.</returns>
  public IModel GetChannel()
  {
    return _channel;
  }
}
