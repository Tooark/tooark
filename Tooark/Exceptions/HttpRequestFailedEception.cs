using System.Net;

namespace Tooark.Exceptions;

/// <summary>
/// Representa uma exceção que é lançada quando uma requisição HTTP falha.
/// </summary>
public class HttpRequestFailedException : Exception
{
  /// <summary>
  /// Inicializa construtor da class.
  /// </summary>
  /// <param name="statusCode">O código de status HTTP da resposta.</param>
  /// <param name="content">O conteúdo da resposta HTTP.</param>
  public HttpRequestFailedException(
    HttpStatusCode? statusCode,
    string content
  ) : base($"Request failed with status code {statusCode} and content: {content}")
  {
    StatusCode = statusCode ?? HttpStatusCode.InternalServerError;
  }

  /// <summary>
  /// Obtém o código de status HTTP da resposta que causou a exceção.
  /// </summary>
  public HttpStatusCode StatusCode { get; }
}
