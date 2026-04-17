namespace Tooark.Mediator.Abstractions;

/// <summary>
/// Define uma notificação, que é um tipo de mensagem que não espera uma resposta.
/// </summary>
/// <remarks>
/// As notificações são usadas para eventos ou mensagens que não exigem uma resposta, e geralmente
/// são manipuladas por múltiplos manipuladores, permitindo que várias partes do sistema respondam
/// a um evento ou mensagem de forma independente.
/// </remarks>
public interface INotify
{ }
