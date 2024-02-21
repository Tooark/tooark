namespace Tooark.Exceptions;

/// <summary>
/// Representa uma exceção específica do serviço RabbitMQ.
/// </summary>
public class RabbitMQServiceException : Exception
{
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
}
