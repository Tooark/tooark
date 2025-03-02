using System.Net;
using Tooark.Notifications;

namespace Tooark.Exceptions;

/// <summary>
/// Classe base para exceções do Tooark.
/// </summary>
public abstract class TooarkException : Exception
{
  /// <summary>
  /// Lista privada de mensagens de erro.
  /// </summary>
  private readonly IList<string> _errors = [];

  /// <summary>
  /// Lista privada de itens de notificações.
  /// </summary>
  private readonly IList<NotificationItem> _notifications = [];


  /// <summary>
  /// Construtor da classe.
  /// </summary>
  /// <param name="message">Mensagem de erro.</param>
  public TooarkException(string message) : base(message)
  {
    // Adiciona a mensagem de erro na lista.
    _errors.Add(message);
    
    // Adiciona a mensagem de erro na lista de notificações.
    _notifications.Add(new NotificationItem(message));
  }

  /// <summary>
  /// Construtor da classe.
  /// </summary>
  /// <param name="errors">Lista de mensagens de erro.</param>
  public TooarkException(IList<string> errors) : base(errors.Count > 0 ? errors[0] : null)
  {
    // Itera sobre as mensagens de erro e adiciona na lista.
    foreach (var error in errors)
    {
      // Adiciona a mensagem de erro na lista.
      _errors.Add(error);

      // Adiciona a mensagem de erro na lista de notificações.
      _notifications.Add(new NotificationItem(error));
    }
  }

  /// <summary>
  /// Construtor da classe.
  /// </summary>
  /// <param name="notification">Notificação com as mensagens de erro.</param>
  public TooarkException(Notification notification) : base(notification.Messages.Count > 0 ? notification.Messages[0] : null)
  {
    // Itera sobre as mensagens de erro e adiciona na lista.
    foreach (var error in notification.Messages)
    {
      // Adiciona a mensagem de erro na lista.
      _errors.Add(error);
    }

    // Copia as notificações para a lista privada.
    _notifications = [.. notification.Notifications];
  }


  /// <summary>
  /// Função para obter as mensagens de erro.
  /// </summary>
  /// <returns>Lista de mensagens de erro.</returns>
  public IList<string> GetErrorMessages() => _errors;

  /// <summary>
  /// Função para obter as itens de notificação.
  /// </summary>
  /// <returns>Lista de itens de notificação.</returns>
  public IList<NotificationItem> GetNotifications() => _notifications;

  /// <summary>
  /// Função abstrata para obter o status code da exceção.
  /// </summary>
  /// <returns>Status code da exceção.</returns>
  public abstract HttpStatusCode GetStatusCode();
}
