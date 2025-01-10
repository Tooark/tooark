using System.Net;
using Microsoft.Extensions.Localization;

namespace Tooark.Exceptions;

/// <summary>
/// Representa uma exceção que é lançada quando uma requisição HTTP falha.
/// </summary>
public class AppException : Exception
{
  /// <summary>
  /// Localizador de strings.
  /// </summary>
  private static IStringLocalizer? _localizer;

  /// <summary>
  /// Código de status da exceção
  /// </summary>
  public HttpStatusCode HttpStatusCode { get; set; }

  /// <summary>
  /// Configura o localizador de strings.
  /// </summary>
  /// <param name="localizer">Localizador de strings.</param>
  public static void Configure(IStringLocalizer localizer)
  {
    _localizer = localizer;
  }

  /// <summary>
  /// Criar uma exceção BadRequest (400) com a mensagem padrão.
  /// </summary>
  /// <returns>Exceção BadRequest.</returns>
  public AppException() : base(_localizer?["BadRequest"] ?? "BadRequest")
  {
    HttpStatusCode = HttpStatusCode.BadRequest;
  }

  /// <summary>
  /// Criar uma exceção BadRequest (400) com a mensagem.
  /// </summary>
  /// <param name="message">Mensagem da exceção.</param>
  /// <returns>Exceção BadRequest.</returns>
  public AppException(string message) : base(_localizer?[message] ?? message)
  {
    HttpStatusCode = HttpStatusCode.BadRequest;
  }

  /// <summary>
  /// Criar uma exceção com a mensagem e o código de status.
  /// </summary>
  /// <param name="message">Mensagem da exceção.</param>
  /// <param name="statusCode">Código de status da exceção.</param>
  /// <returns>Exceção com a mensagem e o código de status.</returns>
  public AppException(string message, HttpStatusCode statusCode) : base(_localizer?[message] ?? message)
  {
    HttpStatusCode = statusCode;
  }

  /// <summary>
  /// Criar uma exceção BadRequest (400).
  /// </summary>
  /// <param name="message">Mensagem da exceção.</param>
  /// <returns>Exceção BadRequest.</returns>
  public static AppException BadRequest(string message = "BadRequest")
  {
    return new AppException(message, HttpStatusCode.BadRequest);
  }

  /// <summary>
  /// Criar uma exceção Unauthorized (401).
  /// </summary>
  /// <param name="message">Mensagem da exceção.</param>
  /// <returns>Exceção Unauthorized.</returns>
  public static AppException Unauthorized(string message = "Unauthorized")
  {
    return new AppException(message, HttpStatusCode.Unauthorized);
  }

  /// <summary>
  /// Criar uma exceção Forbidden (403).
  /// </summary>
  /// <param name="message">Mensagem da exceção.</param>
  /// <returns>Exceção Forbidden.</returns>
  public static AppException Forbidden(string message = "Forbidden")
  {
    return new AppException(message, HttpStatusCode.Forbidden);
  }

  /// <summary>
  /// Criar uma exceção NotFound (404).
  /// </summary>
  /// <param name="message">Mensagem da exceção.</param>
  /// <returns>Exceção NotFound.</returns>
  public static AppException NotFound(string message = "NotFound")
  {
    return new AppException(message, HttpStatusCode.NotFound);
  }

  /// <summary>
  /// Criar uma exceção Conflict (409).
  /// </summary>
  /// <param name="message">Mensagem da exceção.</param>
  /// <returns>Exceção Conflict.</returns>
  public static AppException Conflict(string message = "Conflict")
  {
    return new AppException(message, HttpStatusCode.Conflict);
  }

  /// <summary>
  /// Criar uma exceção PayloadTooLarge (413).
  /// </summary>
  /// <param name="message">Mensagem da exceção.</param>
  /// <returns>Exceção PayloadTooLarge.</returns>
  public static AppException PayloadTooLarge(string message = "PayloadTooLarge")
  {
    return new AppException(message, HttpStatusCode.RequestEntityTooLarge);
  }

  /// <summary>
  /// Criar uma exceção InternalServerError (500).
  /// </summary>
  /// <param name="message">Mensagem da exceção.</param>
  /// <returns>Exceção InternalServerError.</returns>
  public static AppException InternalServerError(string message = "InternalServerError")
  {
    return new AppException(message, HttpStatusCode.InternalServerError);
  }

  /// <summary>
  /// Criar uma exceção ServiceUnavailable (503).
  /// </summary>
  /// <param name="message">Mensagem da exceção.</param>
  /// <returns>Exceção ServiceUnavailable.</returns>
  public static AppException ServiceUnavailable(string message = "ServiceUnavailable")
  {
    return new AppException(message, HttpStatusCode.ServiceUnavailable);
  }
}
