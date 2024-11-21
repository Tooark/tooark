namespace Tooark.Exceptions;

/// <summary>
/// Representa uma exceção que é lançada quando ocorre um erro durante a desserialização de JSON.
/// </summary>
public class JsonDeserializationException : Exception
{
  /// <summary>
  /// Inicializa construtor da class.
  /// </summary>
  /// <param name="message">A mensagem de erro que explica o motivo da exceção.</param>
  /// <param name="innerException">A exceção que é a causa desta exceção.</param>
  public JsonDeserializationException(
    string message,
    Exception innerException
  ) : base(message, innerException)
  { }
}
