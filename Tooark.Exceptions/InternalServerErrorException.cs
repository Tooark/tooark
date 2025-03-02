using System.Net;

namespace Tooark.Exceptions;

/// <summary>
/// Representa uma exceção de erro interno do servidor (Internal Server Error).
/// </summary>
/// <param name="message">A mensagem de erro associada à exceção.</param>
public class InternalServerErrorException(string message) : TooarkException(message)
{
  /// <summary>
  /// Obtém o código de status HTTP associado à exceção.
  /// </summary>
  /// <returns>O código de status HTTP 500 (Internal Server Error).</returns>
  public override HttpStatusCode GetStatusCode() => HttpStatusCode.InternalServerError;
}
