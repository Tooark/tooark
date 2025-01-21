using Tooark.Notifications;

namespace Tooark.Validations;

/// <summary>
/// Classe de contrato
/// </summary>
public partial class Contract : Notification
{
  /// <summary>
  /// Juntar mais de uma notificação
  /// </summary>
  /// <param name="notifications"></param>
  /// <returns></returns>
  public Contract Join(params Notification[] notifications)
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
      if (notification.IsValid == false)
      {
        // Adiciona a notificação
        AddNotifications(notification);
      }
    }

    // Retorna a instância atual
    return this;
  }

  #region Validates
  /// <summary>
  /// Função para validar nulo.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="returnAwait">Retorno aguardado da comparação.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <returns>Contrato de validação.</returns>
  private Contract ValidateNull<T>(T? value, bool returnAwait, string property, string message)
  {
    // Se a condição for verdadeira, adicione a notificação.
    if (value == null == returnAwait)
    {
      // Adiciona a notificação.
      AddNotification(message, property);
    }

    // Retorna o contrato de validação.
    return this;
  }
  #endregion
}
