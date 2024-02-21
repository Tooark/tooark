using RabbitMQ.Client;
using Tooark.Exceptions;

namespace Tooark.Helpers;

public static class RabbitMQHelper
{
  public static void ConfigureFanoutDirect(
   IModel channel,
   string queueName,
   string routingKey,
   bool durable = true,
   bool exclusive = false,
   bool autoDelete = false)
  {
    try
    {
      var fanoutExchangeName = "exchange_fanout";
      var directExchangeName = "exchange_direct";

      ConfigureExchangeQueue(channel, fanoutExchangeName, ExchangeType.Fanout, queueName, "", durable, exclusive, autoDelete);
      ConfigureExchangeQueue(channel, directExchangeName, ExchangeType.Direct, queueName, routingKey, durable, exclusive, autoDelete);
    }
    catch (Exception ex)
    {
      throw new RabbitMQServiceException("Erro ao configurar fila e exchange.", ex);
    }
  }

  public static void ConfigureExchangeQueue(
   IModel channel,
   string exchangeName,
   string exchangeType,
   string queueName,
   string routingKey,
   bool durable = true,
   bool exclusive = false,
   bool autoDelete = false)
  {
    if (channel is null)
    {
      throw new ArgumentNullException(nameof(channel));
    }

    if (string.IsNullOrEmpty(exchangeName))
    {
      throw new ArgumentException($"'{nameof(exchangeName)}' não pode ser nulo nem vazio.", nameof(exchangeName));
    }

    if (string.IsNullOrEmpty(exchangeType))
    {
      throw new ArgumentException($"'{nameof(exchangeType)}' não pode ser nulo nem vazio.", nameof(exchangeType));
    }

    if (string.IsNullOrEmpty(queueName))
    {
      throw new ArgumentException($"'{nameof(queueName)}' não pode ser nulo nem vazio.", nameof(queueName));
    }

    if (string.IsNullOrEmpty(routingKey))
    {
      throw new ArgumentException($"'{nameof(routingKey)}' não pode ser nulo nem vazio.", nameof(routingKey));
    }

    try
    {
      channel.ConfigureExchange(exchangeName, exchangeType, durable, autoDelete);
      channel.ConfigureQueue(exchangeName, queueName, routingKey, durable, exclusive, autoDelete);
    }
    catch (Exception ex)
    {
      throw new RabbitMQServiceException("Erro ao configurar fila e exchange.", ex);
    }
  }

  public static void ConfigureExchange(
    this IModel channel,
    string exchangeName,
    string exchangeType,
    bool durable = true,
    bool autoDelete = false)
  {
    try
    {
      channel.ExchangeDeclare(
        exchange: exchangeName,
        type: exchangeType,
        durable: durable,
        autoDelete: autoDelete);
    }
    catch (Exception ex)
    {
      throw new RabbitMQServiceException("Erro ao configurar exchange.", ex);
    }
  }

  public static void ConfigureQueue(
    this IModel channel,
    string exchangeName,
    string queueName,
    string routingKey,
    bool durable = true,
    bool exclusive = false,
    bool autoDelete = false)
  {
    try
    {
      channel.QueueDeclare(
        queue: queueName,
        durable: durable,
        exclusive: exclusive,
        autoDelete: autoDelete);

      channel.QueueBind(
        queue: queueName,
        exchange: exchangeName,
        routingKey: routingKey);
    }
    catch (Exception ex)
    {
      throw new RabbitMQServiceException("Erro ao configurar queue.", ex);
    }
  }
}
