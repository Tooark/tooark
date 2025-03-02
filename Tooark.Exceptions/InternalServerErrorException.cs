using System.Net;
using Tooark.Notifications;

namespace Tooark.Exceptions;

/// <summary>
/// Representa uma exceção de erro interno do servidor (Internal Server Error).
/// </summary>
public class InternalServerErrorException : TooarkException
{
  /// <summary>
  /// Construtor padrão da exceção com mensagem única.
  /// </summary>
  /// <param name="message">A mensagem de erro associada à exceção.</param>
  public InternalServerErrorException(string message) : base(message) { }

  /// <summary>
  /// Construtor padrão da exceção com lista de mensagens.
  /// </summary>
  /// <param name="messages">A lista de mensagens de erros associadas à exceção.</param>
  public InternalServerErrorException(IList<string> messages) : base(messages) { }

  /// <summary>
  /// Construtor padrão da exceção com notificação.
  /// </summary>
  /// <param name="notification">A notificação com as mensagens de erros associadas à exceção.</param>
  public InternalServerErrorException(Notification notification) : base(notification) { }


  /// <summary>
  /// Obtém o código de status HTTP associado à exceção.
  /// </summary>
  /// <returns>O código de status HTTP 500 (Internal Server Error).</returns>
  public override HttpStatusCode GetStatusCode() => HttpStatusCode.InternalServerError;
}
