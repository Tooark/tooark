using Tooark.Notifications;

namespace Tooark.Validations;

/// <summary>
/// Classe de Validação
/// </summary>
public partial class Validation : Notification
{
  #region Default Validation
  /// <summary>
  /// Juntar mais de uma notificação
  /// </summary>
  /// <param name="notifications"></param>
  /// <returns></returns>
  public Validation Join(params Notification[] notifications)
  {
    // Se a lista de notificações for nula, retorna a instância atual
    if (notifications == null)
    {
      return this;
    }

    // Percorre a lista de notificações
    foreach (var notification in notifications)
    {
      // Se a notificação for inválida, adiciona a notificação
      if (!notification.IsValid)
      {
        // Adiciona a notificação
        AddNotifications(notification);
      }
    }

    // Retorna a instância atual
    return this;
  }
  #endregion

  #region Validates
  /// <summary>
  /// Função para validar nulo.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="returnAwait">Retorno aguardado da comparação.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <returns>Validação.</returns>
  private Validation ValidateNull<T>(T? value, bool returnAwait, string property, string message)
  {
    // Se a condição for verdadeira, adicione a notificação.
    if (EqualityComparer<T>.Default.Equals(value, default) == returnAwait)
    {
      // Adiciona a notificação.
      AddNotification(message, property);
    }

    // Retorna uma validação.
    return this;
  }
  #endregion
}
