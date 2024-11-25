using System.Net;

namespace Tooark.Exceptions;

/// <summary>
/// Representa uma exceção que é lançada quando uma requisição HTTP falha.
/// </summary>
/// <param name="statusCode">O código de status HTTP da resposta.</param>
/// <param name="content">O conteúdo da resposta HTTP.</param>
public class HttpRequestFailedException(
  HttpStatusCode? statusCode,
  string content
  ) : Exception($"RequestFailed;{statusCode};{content}")
{

  /// <summary>
  /// Obtém o código de status HTTP da resposta que causou a exceção.
  /// </summary>
  public HttpStatusCode StatusCode { get; } = statusCode ?? HttpStatusCode.InternalServerError;
}
