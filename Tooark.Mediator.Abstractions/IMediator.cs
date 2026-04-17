namespace Tooark.Mediator.Abstractions;

/// <summary>
/// Interface para o mediador, que combina as funcionalidades de remetente e publicador.
/// </summary>
/// <remarks>
/// O mediador é a peça central do padrão Mediator, permitindo a comunicação entre diferentes partes
/// do sistema sem que elas precisem conhecer os detalhes umas das outras. Ele é responsável por enviar
/// requisições para os manipuladores correspondentes e publicar notificações para os manipuladores interessados.
/// </remarks>
public interface IMediator : ISender, IPublisher
{ }
