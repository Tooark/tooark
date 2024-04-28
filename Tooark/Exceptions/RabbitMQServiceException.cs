namespace Tooark.Exceptions;

/// <summary>
/// Representa uma exceção específica do serviço RabbitMQ.
/// </summary>
public class RabbitMQServiceException : Exception
{
  /// <summary>
  /// Inicializa uma nova instância da classe RabbitMQServiceException com
  /// uma mensagem de erro especificada.
  /// </summary>
  /// <param name="message">A mensagem de erro que explica o motivo da exceção.</param>
  public RabbitMQServiceException(
    string message
  ) : base(message)
  { }

  /// <summary>
  /// Inicializa uma nova instância da classe RabbitMQServiceException com
  /// uma mensagem de erro especificada e uma referência à exceção interna que é a causa desta exceção.
  /// </summary>
  /// <param name="message">A mensagem de erro que explica o motivo da exceção.</param>
  /// <param name="innerException">A exceção que é a causa desta exceção.</param>
  public RabbitMQServiceException(
    string message,
    Exception innerException
  ) : base(message, innerException)
  { }

  /// <summary>
  /// Inicializa uma nova instância da classe RabbitMQServiceException com
  /// uma mensagem de erro especificada, o nome da exchange, o nome da fila e uma referência à exceção interna que é a causa desta exceção.
  /// </summary>
  /// <param name="message">A mensagem de erro que explica o motivo da exceção.</param>
  /// <param name="exchangeQueueName">O nome da exchange ou fila onde ocorreu a exceção.</param>
  /// <param name="innerException">A exceção que é a causa desta exceção.</param>
  public RabbitMQServiceException(
    string message,
    string exchangeQueueName,
    Exception innerException
  ) : base($"{exchangeQueueName}. Message: {message}", innerException)
  { }

  /// <summary>
  /// Inicializa uma nova instância da classe RabbitMQServiceException com
  /// uma mensagem de erro especificada, o nome da exchange, o nome da fila e uma referência à exceção interna que é a causa desta exceção.
  /// </summary>
  /// <param name="message">A mensagem de erro que explica o motivo da exceção.</param>
  /// <param name="exchangeName">O nome da exchange onde ocorreu a exceção.</param>
  /// <param name="queueName">O nome da fila onde ocorreu a exceção.</param>
  /// <param name="innerException">A exceção que é a causa desta exceção.</param>
  public RabbitMQServiceException(
    string message,
    string exchangeName,
    string queueName,
    Exception innerException
  ) : base($"Exchange: {exchangeName}. Queue: {queueName}. Message: {message}", innerException)
  { }
}
