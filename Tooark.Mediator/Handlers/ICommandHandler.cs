using Tooark.Mediator.Abstractions;

namespace Tooark.Mediator.Handlers;

/// <summary>
/// Interface para os manipuladores de comandos.
/// </summary>
/// <remarks>
/// Os manipuladores de comandos são responsáveis por processar os comandos e retornar uma resposta.
/// Eles implementam a interface <see cref="IRequestHandler{TCommand, TResponse}"/>.
/// </remarks>
/// <typeparam name="TCommand">O tipo de comando que o manipulador processa.</typeparam>
/// <typeparam name="TResponse">O tipo de resposta que o manipulador retorna.</typeparam>
public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
  where TCommand : ICommand<TResponse>
{ }

/// <summary>
/// Interface para os manipuladores de comandos.
/// </summary>
/// <remarks>
/// Os manipuladores de comandos são responsáveis por processar os comandos sem retornar uma resposta.
/// Eles implementam a interface <see cref="ICommandHandler{TCommand, Unit}"/>.
/// </remarks>
/// <typeparam name="TCommand">O tipo de comando que o manipulador processa.</typeparam>
public interface ICommandHandler<in TCommand> : ICommandHandler<TCommand, Unit>
  where TCommand : ICommand
{ }
