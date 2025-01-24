using Tooark.Notifications.Messages;

namespace Tooark.Notifications;

/// <summary>
/// Representa a estrutura de uma notificação.
/// </summary>
/// <param name="message">Mensagem da notificação.</param>
/// <param name="key">Chave da notificação. Padronizado como 'Unknown'.</param>
public class NotificationItem(string message, string key = "Unknown")
{
  /// <summary>
  /// A chave privada da notificação.
  /// </summary>
  private readonly string _key = key?.Trim()?.Replace(" ", string.Empty) ?? "Unknown";

  /// <summary>
  /// A mensagem privada da notificação.
  /// </summary>
  private readonly string _message = message?.Trim() ?? NotificationErrorMessages.MessageUnknown;


  /// <summary>
  /// A chave da notificação.
  /// </summary>
  public string Key { get => _key; }

  /// <summary>
  /// A mensagem da notificação.
  /// </summary>
  public string Message { get => _message; }


  /// <summary>
  /// Sobrescrita do método <see cref="object.ToString"/> para retornar a mensagem da notificação.
  /// </summary>
  /// <returns>Uma string que representa a notificação.</returns>
  public override string ToString() => _message;

  /// <summary>
  /// Define uma conversão implícita de uma notificação para uma string.
  /// </summary>
  /// <param name="message">A notificação a ser convertida.</param>
  /// <returns>A mensagem da notificação.</returns>
  public static implicit operator string(NotificationItem message) => message._message;

  /// <summary>
  /// Define uma conversão implícita de uma string para uma notificação.
  /// </summary>
  /// <param name="message">A mensagem da notificação.</param>
  /// <returns>Uma nova instância de <see cref="NotificationItem"/> com a chave 'Unknown'.</returns>
  public static implicit operator NotificationItem(string message) => new(message);
}
