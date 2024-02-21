using RabbitMQ.Client.Events;

namespace Tooark.Services.Interface;

public interface IRabbitMQService
{
  /// <summary>
  /// Publica uma mensagem em um exchange do tipo 'fanout'.
  /// </summary>
  /// <param name="message">Mensagem a ser publicada.</param>
  void PublishMessage(string message);

  /// <summary>
  /// Publica uma mensagem em um exchange do tipo 'direct' com uma chave de roteamento específica.
  /// </summary>
  /// <param name="message">Mensagem a ser publicada.</param>
  /// <param name="routingKey">Chave de roteamento para a mensagem.</param>
  void PublishMessage(string message, string routingKey);

  /// <summary>
  /// Consome mensagens de uma fila específica.
  /// </summary>
  /// <param name="queueName">Nome da fila.</param>
  /// <param name="consumer">Consumidor que irá processar as mensagens.</param>
  void ConsumeMessage(string queueName, EventingBasicConsumer consumer);
}
