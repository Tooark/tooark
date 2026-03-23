namespace Tooark.Mediator.Abstractions;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}
