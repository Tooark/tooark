using RabbitMQ.Client;

namespace Tooark.Interfaces;

/// <summary>
/// Define uma interface para o RabbitMQService.
/// </summary>
public interface IRabbitMQPublishService
{
  /// <summary>
  /// Publica uma mensagem no exchange_fanout.
  /// </summary>
  /// <param name="message">Mensagem a ser publicada.</param>
  Task PublishMessage(string message);

  /// <summary>
  /// Publica uma mensagem no exchange_direct com uma chave de roteamento.
  /// </summary>
  /// <param name="message">Mensagem a ser publicada.</param>
  /// <param name="routingKey">Chave de roteamento para o exchange_direct.</param>
  Task PublishMessage(string message, string routingKey);

  /// <summary>
  /// Publica uma mensagem em uma exchange customizada, fornecida como parâmetro.
  /// </summary>
  /// <param name="message">Mensagem a ser publicada.</param>
  /// <param name="routingKey">Chave de roteamento para o exchange_direct.</param>
  /// <param name="exchangeName">Mensagem a ser publicada.</param>
  Task PublishMessage(string message, string routingKey, string exchangeName);
}
