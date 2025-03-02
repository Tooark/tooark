using System.Net;

namespace Tooark.Exceptions;

/// <summary>
/// Representa uma exceção de gateway inválido (Bad Gateway).
/// </summary>
/// <param name="message">A mensagem de erro associada à exceção.</param>
public class BadGatewayException(string message) : TooarkException(message)
{
  /// <summary>
  /// Obtém o código de status HTTP associado à exceção.
  /// </summary>
  /// <returns>O código de status HTTP 502 (Bad Gateway).</returns>
  public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadGateway;
}
