using System.Net;

namespace Tooark.Exceptions;

/// <summary>
/// Representa uma exceção de requisição inválida (Bad Request).
/// </summary>
/// <param name="message">A mensagem de erro associada à exceção.</param>
public class BadRequestException(string message) : TooarkException(message)
{
  /// <summary>
  /// Obtém o código de status HTTP associado à exceção.
  /// </summary>
  /// <returns>O código de status HTTP 400 (Bad Request).</returns>
  public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;
}
