using RabbitMQ.Client;

namespace Tooark.Interfaces;

/// <summary>
/// Serviço para interagir com o RabbitMQ, permitindo publicar mensagens.
/// </summary>
public interface IRabbitMQPublishService
{
  /// <summary>
  /// Publica uma mensagem no exchange_fanout.
  /// </summary>
  /// <param name="message">Mensagem a ser publicada.</param>
  void PublishMessage(string message);

  /// <summary>
  /// Publica uma mensagem no exchange_direct com uma chave de roteamento.
  /// </summary>
  /// <param name="message">Mensagem a ser publicada.</param>
  /// <param name="routingKey">Chave de roteamento para o exchange_direct.</param>
  void PublishMessage(string message, string routingKey);

  /// <summary>
  /// Publica uma mensagem em uma exchange customizada, fornecida como parâmetro.
  /// </summary>
  /// <param name="message">Mensagem a ser publicada.</param>
  /// <param name="routingKey">Chave de roteamento para o exchange_direct.</param>
  /// <param name="exchangeName">Mensagem a ser publicada.</param>
  void PublishMessage(string message, string routingKey, string exchangeName);

  /// <summary>
  /// Obtém o canal de comunicação com o RabbitMQ.
  /// </summary>
  /// <returns>O canal de comunicação.</returns>
  IModel GetChannel();
}
