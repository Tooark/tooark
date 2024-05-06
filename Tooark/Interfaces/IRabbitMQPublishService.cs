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

  /// <summary>
  /// Publica uma mensagem no exchange_fanout.
  /// </summary>
  /// <typeparam name="T">O tipo de dados que a mensagem carrega.</typeparam>
  /// <param name="messageObject">Mensagem a ser publicada.</param>
  /// <param name="title">Titulo da mensagem a ser publicada.</param>
  Task PublishMessage<T>(T messageObject, string title);

  /// <summary>
  /// Publica uma mensagem no exchange_direct com uma chave de roteamento.
  /// </summary>
  /// <typeparam name="T">O tipo de dados que a mensagem carrega.</typeparam>
  /// <param name="messageObject">Mensagem a ser publicada.</param>
  /// <param name="title">Titulo da mensagem a ser publicada.</param>
  /// <param name="routingKey">Chave de roteamento para o exchange_direct.</param>
  Task PublishMessage<T>(T messageObject, string title, string routingKey);

  /// <summary>
  /// Publica uma mensagem em uma exchange customizada, fornecida como parâmetro.
  /// </summary>
  /// <typeparam name="T">O tipo de dados que a mensagem carrega.</typeparam>
  /// <param name="messageObject">Mensagem a ser publicada.</param>
  /// <param name="title">Titulo da mensagem a ser publicada.</param>
  /// <param name="routingKey">Chave de roteamento para o exchange_direct.</param>
  /// <param name="exchangeName">Mensagem a ser publicada.</param>
  Task PublishMessage<T>(T messageObject, string title, string routingKey, string exchangeName);
}
