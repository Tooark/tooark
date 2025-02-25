using Tooark.Notifications.Messages;

namespace Tooark.Notifications;

/// <summary>
/// Representa a estrutura de um item de notificação.
/// </summary>
/// <param name="message">Mensagem da notificação.</param>
/// <param name="key">Chave da notificação. Padronizado como 'Unknown'.</param>
/// <param name="code">Código de erro da notificação. Padronizado como 'T.ERR'. T[ooark].ERR[or]</param>
public class NotificationItem(string message, string key = "Unknown", string code = "T.ERR")
{
  /// <summary>
  /// O código de erro privado da notificação.
  /// </summary>
  private readonly string _code = string.IsNullOrWhiteSpace(code) ? "T.ERR" : code;

  /// <summary>
  /// A chave privada da notificação.
  /// </summary>
  private readonly string _key = string.IsNullOrWhiteSpace(key) ? "Unknown" : key.Trim().Replace(" ", string.Empty);

  /// <summary>
  /// A mensagem privada da notificação.
  /// </summary>
  private readonly string _message = message?.Trim() ?? NotificationErrorMessages.MessageUnknown;


  /// <summary>
  /// O código de erro da notificação.
  /// </summary>
  public string Code { get => _code; }

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
