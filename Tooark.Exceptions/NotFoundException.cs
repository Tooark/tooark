using System.Net;

namespace Tooark.Exceptions;

/// <summary>
/// Representa uma exceção de recurso não encontrado (Not Found).
/// </summary>
/// <param name="message">A mensagem de erro associada à exceção.</param>
public class NotFoundException(string message) : TooarkException(message)
{
  /// <summary>
  /// Obtém o código de status HTTP associado à exceção.
  /// </summary>
  /// <returns>O código de status HTTP 404 (Not Found).</returns>
  public override HttpStatusCode GetStatusCode() => HttpStatusCode.NotFound;
}
