namespace Tooark.Exceptions;

/// <summary>
/// Representa uma exceção que é lançada quando ocorre um erro durante a deserialização de JSON.
/// </summary>
/// <param name="message">A mensagem de erro que explica o motivo da exceção.</param>
/// <param name="innerException">A exceção que é a causa desta exceção.</param>
public class JsonDeserializationException(
  string message,
  Exception innerException
) : Exception(message, innerException)
{
}
