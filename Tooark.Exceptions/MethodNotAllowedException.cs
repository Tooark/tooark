using System.Net;
using Tooark.Notifications;

namespace Tooark.Exceptions;

/// <summary>
/// Representa uma exceção de método não permitido (Method Not Allowed).
/// </summary>
public class MethodNotAllowedException : TooarkException
{
  /// <summary>
  /// Construtor padrão da exceção com mensagem única.
  /// </summary>
  /// <param name="message">A mensagem de erro associada à exceção.</param>
  public MethodNotAllowedException(string message) : base(message) { }

  /// <summary>
  /// Construtor padrão da exceção com lista de mensagens.
  /// </summary>
  /// <param name="messages">A lista de mensagens de erros associadas à exceção.</param>
  public MethodNotAllowedException(IList<string> messages) : base(messages) { }

  /// <summary>
  /// Construtor padrão da exceção com notificação.
  /// </summary>
  /// <param name="notification">A notificação com as mensagens de erros associadas à exceção.</param>
  public MethodNotAllowedException(Notification notification) : base(notification) { }


  /// <summary>
  /// Obtém o código de status HTTP associado à exceção.
  /// </summary>
  /// <returns>O código de status HTTP 405 (Method Not Allowed).</returns>
  public override HttpStatusCode GetStatusCode() => HttpStatusCode.MethodNotAllowed;
}
