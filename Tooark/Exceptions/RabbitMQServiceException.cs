namespace Tooark.Exceptions;

/// <summary>
/// Representa uma exceção específica do serviço RabbitMQ.
/// </summary>
public class RabbitMQServiceException : Exception
{
  /// <summary>
  /// Obtém o nome da exchange onde ocorreu a exceção.
  /// </summary>
  public string? ExchangeName { get; }

  /// <summary>
  /// Obtém o nome da fila onde ocorreu a exceção.
  /// </summary>
  public string? QueueName { get; }

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
  /// <param name="exchangeName">O nome da exchange onde ocorreu a exceção.</param>
  /// <param name="innerException">A exceção que é a causa desta exceção.</param>
  public RabbitMQServiceException(
    string message,
    string exchangeName,
    Exception innerException
  ) : base($"Exchange: {exchangeName}. Message: {message}", innerException)
  {
    ExchangeName = exchangeName;
  }

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
  {
    ExchangeName = exchangeName;
    QueueName = queueName;
  }
}
