using Tooark.Mediator.Abstractions;

namespace Tooark.Mediator.Handlers;

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
  where TCommand : ICommand<TResponse>
{
}

public interface ICommandHandler<in TCommand> : ICommandHandler<TCommand, Unit>
  where TCommand : ICommand
{
}
