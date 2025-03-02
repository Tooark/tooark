using System.Net;

namespace Tooark.Exceptions;

/// <summary>
/// Representa uma exceção de serviço indisponível (Service Unavailable).
/// </summary>
/// <param name="message">A mensagem de erro associada à exceção.</param>
public class ServiceUnavailableException(string message) : TooarkException(message)
{
  /// <summary>
  /// Obtém o código de status HTTP associado à exceção.
  /// </summary>
  /// <returns>O código de status HTTP 503 (Service Unavailable).</returns>
  public override HttpStatusCode GetStatusCode() => HttpStatusCode.ServiceUnavailable;
}
