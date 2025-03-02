using System.Net;
using Tooark.Notifications;

namespace Tooark.Exceptions;

/// <summary>
/// Classe para exceções para métodos buscar campo name, title e description.
/// </summary>
public class GetInfoException : TooarkException
{
  /// <summary>
  /// Construtor padrão da exceção com mensagem única.
  /// </summary>
  /// <param name="message">A mensagem de erro associada à exceção.</param>
  public GetInfoException(string message) : base(message) { }

  /// <summary>
  /// Construtor padrão da exceção com lista de mensagens.
  /// </summary>
  /// <param name="messages">A lista de mensagens de erros associadas à exceção.</param>
  public GetInfoException(IList<string> messages) : base(messages) { }

  /// <summary>
  /// Construtor padrão da exceção com notificação.
  /// </summary>
  /// <param name="notification">A notificação com as mensagens de erros associadas à exceção.</param>
  public GetInfoException(Notification notification) : base(notification) { }


  /// <summary>
  /// Função virtual para obter o status code da exceção.
  /// </summary>
  /// <returns>Status code da exceção. Padronizado para 400 (BadRequest).</returns>
  public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;
}
