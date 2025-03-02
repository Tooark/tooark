using System.Net;

namespace Tooark.Exceptions;

/// <summary>
/// Representa uma exceção de método não permitido (Method Not Allowed).
/// </summary>
/// <param name="message">A mensagem de erro associada à exceção.</param>
public class MethodNotAllowedException(string message) : TooarkException(message)
{
  /// <summary>
  /// Obtém o código de status HTTP associado à exceção.
  /// </summary>
  /// <returns>O código de status HTTP 405 (Method Not Allowed).</returns>
  public override HttpStatusCode GetStatusCode() => HttpStatusCode.MethodNotAllowed;
}
