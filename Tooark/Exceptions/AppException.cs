using System.Globalization;
using System.Net;
using static Tooark.Utils.Util;

namespace Tooark.Exceptions;

/// <summary>
/// Representa uma exceção que é lançada quando uma requisição HTTP falha.
/// </summary>
public class AppException : Exception
{
  /// <summary>
  /// Código de status da exceção. O padrão é BadRequest (400).
  /// </summary>
  public HttpStatusCode HttpStatusCode { get; set; } = HttpStatusCode.BadRequest;

  /// <summary>
  /// Criar uma exceção padrão BadRequest (400).
  /// </summary>
  public AppException() : base("BadRequest") { }

  /// <summary>
  /// Criar uma exceção com a mensagem.
  /// </summary>
  /// <param name="message"></param>
  public AppException(string message) : base(message) { }

  /// <summary>
  /// Criar uma exceção com a mensagem e os argumentos.
  /// </summary>
  /// <param name="message"></param>
  /// <param name="args"></param>
  public AppException(string message, params object[] args) : base(string.Format(CultureInfo.CurrentCulture, message, args)) { }

  /// <summary>
  /// Criar uma exceção com a mensagem e o código de status.
  /// </summary>
  /// <param name="message"></param>
  /// <param name="statusCode"></param>
  public AppException(string message, HttpStatusCode statusCode) : base(message)
  {
    HttpStatusCode = statusCode;
  }

  /// <summary>
  /// Criar uma exceção com o código de status e a mensagem.
  /// </summary>
  /// <param name="statusCode">Código de status da exceção.</param>
  /// <param name="message">Mensagem da exceção.</param>
  /// <returns>Exceção com o código de status e a mensagem.</returns>
  private static AppException CreateException(HttpStatusCode statusCode, string message)
  {
    return new AppException(message) { HttpStatusCode = statusCode };
  }

  /// <summary>
  /// Criar uma exceção BadRequest (400).
  /// </summary>
  /// <param name="message">Mensagem da exceção.</param>
  /// <returns>Exceção BadRequest.</returns>
  public static AppException BadRequest(string message = "BadRequest")
  {
    return CreateException(HttpStatusCode.BadRequest, message);
  }

  /// <summary>
  /// Criar uma exceção Unauthorized (401).
  /// </summary>
  /// <param name="message">Mensagem da exceção.</param>
  /// <returns>Exceção Unauthorized.</returns>
  public static AppException Unauthorized(string message = "Unauthorized")
  {
    return CreateException(HttpStatusCode.Unauthorized, message);
  }

  /// <summary>
  /// Criar uma exceção Forbidden (403).
  /// </summary>
  /// <param name="message">Mensagem da exceção.</param>
  /// <returns>Exceção Forbidden.</returns>
  public static AppException Forbidden(string message = "Forbidden")
  {
    return CreateException(HttpStatusCode.Forbidden, message);
  }

  /// <summary>
  /// Criar uma exceção NotFound (404).
  /// </summary>
  /// <param name="message">Mensagem da exceção.</param>
  /// <returns>Exceção NotFound.</returns>
  public static AppException NotFound(string message = "NotFound")
  {
    return CreateException(HttpStatusCode.NotFound, message);
  }

  /// <summary>
  /// Criar uma exceção Conflict (409).
  /// </summary>
  /// <param name="message">Mensagem da exceção.</param>
  /// <returns>Exceção Conflict.</returns>
  public static AppException Conflict(string message = "Conflict")
  {
    return CreateException(HttpStatusCode.Conflict, message);
  }

  /// <summary>
  /// Criar uma exceção InternalServerError (500).
  /// </summary>
  /// <param name="message">Mensagem da exceção.</param>
  /// <returns>Exceção InternalServerError.</returns>
  public static AppException InternalServerError(string message = "InternalServerError")
  {
    return CreateException(HttpStatusCode.InternalServerError, message);
  }

  /// <summary>
  /// Criar uma exceção ServiceUnavailable (503).
  /// </summary>
  /// <param name="message">Mensagem da exceção.</param>
  /// <returns>Exceção ServiceUnavailable.</returns>
  public static AppException ServiceUnavailable(string message = "ServiceUnavailable")
  {
    return CreateException(HttpStatusCode.ServiceUnavailable, message);
  }
}
