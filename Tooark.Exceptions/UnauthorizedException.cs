using System.Net;

namespace Tooark.Exceptions;

/// <summary>
/// Representa uma exceção de acesso não autorizado (Unauthorized).
/// </summary>
/// <param name="message">A mensagem de erro associada à exceção.</param>
public class UnauthorizedException(string message) : TooarkException(message)
{
  /// <summary>
  /// Obtém o código de status HTTP associado à exceção.
  /// </summary>
  /// <returns>O código de status HTTP 401 (Unauthorized).</returns>
  public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
}
