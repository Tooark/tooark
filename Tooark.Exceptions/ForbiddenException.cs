using System.Net;

namespace Tooark.Exceptions;

/// <summary>
/// Representa uma exceção de acesso negado (Forbidden).
/// </summary>
/// <param name="message">A mensagem de erro associada à exceção.</param>
public class ForbiddenException(string message) : TooarkException(message)
{
  /// <summary>
  /// Obtém o código de status HTTP associado à exceção.
  /// </summary>
  /// <returns>O código de status HTTP 403 (Forbidden).</returns>
  public override HttpStatusCode GetStatusCode() => HttpStatusCode.Forbidden;
}
