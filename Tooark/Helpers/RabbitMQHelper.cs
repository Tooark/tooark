using RabbitMQ.Client;
using Tooark.Exceptions;

namespace Tooark.Helpers;

/// <summary>
/// Fornece métodos estáticos para configurar exchanges e filas no RabbitMQ.
/// </summary>
public static class RabbitMQHelper
{
  /// <summary>
  /// Configura uma exchange do tipo fanout e uma exchange do tipo direct com uma fila associada.
  /// </summary>
  /// <param name="channel">O canal do RabbitMQ para configurar a exchange e a fila.</param>
  /// <param name="queueName">O nome da fila a ser configurada.</param>
  /// <param name="routingKey">A chave de roteamento para a exchange do tipo direct.</param>
  /// <param name="durable">Se a fila deve ser durável.</param>
  /// <param name="exclusive">Se a fila deve ser exclusiva.</param>
  /// <param name="autoDelete">Se a fila deve ser excluída automaticamente quando não estiver em uso.</param>
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
      if (channel != null)
      {
        ArgumentException.ThrowIfNullOrEmpty(queueName);

        var fanoutExchangeName = "exchange_fanout";
        var directExchangeName = "exchange_direct";

        // Configura uma exchange do tipo fanout e uma fila associada
        ConfigureExchangeQueue(channel, fanoutExchangeName, ExchangeType.Fanout, queueName, "", durable, exclusive, autoDelete);

        // Configura uma exchange do tipo direct e uma fila associada
        ConfigureExchangeQueue(channel, directExchangeName, ExchangeType.Direct, queueName, routingKey, durable, exclusive, autoDelete);
      }
      else
      {
        throw new ArgumentNullException(nameof(channel));
      }
    }
    catch (ArgumentException ex)
    {
      throw new RabbitMQServiceException($"Variável {ex.ParamName} é nula.", ex);
    }
    catch (Exception ex)
    {
      throw new RabbitMQServiceException("Erro ao configurar fila e exchange.", ex);
    }
  }

  /// <summary>
  /// Configura uma exchange e uma fila associada no RabbitMQ.
  /// </summary>
  /// <param name="channel">O canal do RabbitMQ para configurar a exchange e a fila.</param>
  /// <param name="exchangeName">O nome da exchange a ser configurada.</param>
  /// <param name="exchangeType">O tipo da exchange (fanout, direct, etc.).</param>
  /// <param name="queueName">O nome da fila a ser configurada.</param>
  /// <param name="routingKey">A chave de roteamento para a exchange.</param>
  /// <param name="durable">Se a fila deve ser durável.</param>
  /// <param name="exclusive">Se a fila deve ser exclusiva.</param>
  /// <param name="autoDelete">Se a fila deve ser excluída automaticamente quando não estiver em uso.</param>
  public static void ConfigureExchangeQueue(
    IModel channel,
    string exchangeName,
    string exchangeType,
    string queueName,
    string routingKey = "",
    bool durable = true,
    bool exclusive = false,
    bool autoDelete = false)
  {
    if (channel != null)
    {
      ArgumentException.ThrowIfNullOrEmpty(exchangeName);
      ArgumentException.ThrowIfNullOrEmpty(exchangeType);
      ArgumentException.ThrowIfNullOrEmpty(queueName);

      if (exchangeType != ExchangeType.Fanout && string.IsNullOrEmpty(routingKey))
      {
        throw new ArgumentNullException(nameof(routingKey));
      }

      try
      {
        // Configura a exchange
        channel.ConfigureExchange(
          exchangeName,
          exchangeType,
          durable,
          autoDelete);

        // Configura a fila e a vincula à exchange
        channel.ConfigureQueue(
          exchangeName,
          queueName,
          routingKey,
          durable,
          exclusive,
          autoDelete);
      }
      catch (Exception ex)
      {
        throw new RabbitMQServiceException("Erro ao configurar fila e exchange.", ex);
      }
    }
    else
    {
      throw new ArgumentNullException(nameof(channel));
    }
  }

  /// <summary>
  /// Declara uma exchange no RabbitMQ.
  /// </summary>
  /// <param name="channel">O canal do RabbitMQ para declarar a exchange.</param>
  /// <param name="exchangeName">O nome da exchange a ser declarada.</param>
  /// <param name="exchangeType">O tipo da exchange (fanout, direct, etc.).</param>
  /// <param name="durable">Se a exchange deve ser durável.</param>
  /// <param name="autoDelete">Se a exchange deve ser excluída automaticamente quando não estiver em uso.</param>
  public static void ConfigureExchange(
    this IModel channel,
    string exchangeName,
    string exchangeType,
    bool durable = true,
    bool autoDelete = false)
  {
    if (channel != null)
    {
      ArgumentException.ThrowIfNullOrEmpty(exchangeName);
      ArgumentException.ThrowIfNullOrEmpty(exchangeType);

      try
      {
        // Declara a exchange com os parâmetros especificados
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
    else
    {
      throw new ArgumentNullException(nameof(channel));
    }
  }

  /// <summary>
  /// Declara uma fila no RabbitMQ e a vincula a uma exchange.
  /// </summary>
  /// <param name="channel">O canal do RabbitMQ para declarar a fila e vinculá-la à exchange.</param>
  /// <param name="exchangeName">O nome da exchange à qual a fila será vinculada.</param>
  /// <param name="queueName">O nome da fila a ser declarada.</param>
  /// <param name="routingKey">A chave de roteamento para vincular a fila à exchange.</param>
  /// <param name="durable">Se a fila deve ser durável.</param>
  /// <param name="exclusive">Se a fila deve ser exclusiva.</param>
  /// <param name="autoDelete">Se a fila deve ser excluída automaticamente quando não estiver em uso.</param>
  public static void ConfigureQueue(
    this IModel channel,
    string exchangeName,
    string queueName,
    string routingKey,
    bool durable = true,
    bool exclusive = false,
    bool autoDelete = false)
  {
    if (channel != null)
    {
      ArgumentException.ThrowIfNullOrEmpty(exchangeName);
      ArgumentException.ThrowIfNullOrEmpty(queueName);

      if (!exchangeName.Contains("fanout") && string.IsNullOrEmpty(routingKey))
      {
        throw new ArgumentNullException(nameof(routingKey));
      }

      try
      {
        // Declara a fila com os parâmetros especificados
        channel.QueueDeclare(
          queue: queueName,
          durable: durable,
          exclusive: exclusive,
          autoDelete: autoDelete);

        // Vincula a fila à exchange com a chave de roteamento especificada
        channel.QueueBind(
          queue: queueName,
          exchange: exchangeName,
          routingKey: routingKey);
      }
      catch (Exception ex)
      {
        throw new RabbitMQServiceException("Erro ao configurar fila.", ex);
      }
    }
    else
    {
      throw new ArgumentNullException(nameof(channel));
    }
  }
}
