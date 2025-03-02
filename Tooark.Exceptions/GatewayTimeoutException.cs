using System.Net;

namespace Tooark.Exceptions;

/// <summary>
/// Representa uma exceção de tempo limite de gateway (Gateway Timeout).
/// </summary>
/// <param name="message">A mensagem de erro associada à exceção.</param>
public class GatewayTimeoutException(string message) : TooarkException(message)
{
  /// <summary>
  /// Obtém o código de status HTTP associado à exceção.
  /// </summary>
  /// <returns>O código de status HTTP 504 (Gateway Timeout).</returns>
  public override HttpStatusCode GetStatusCode() => HttpStatusCode.GatewayTimeout;
}
