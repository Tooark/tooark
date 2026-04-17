using Tooark.Mediator.Abstractions;

namespace Tooark.Mediator.Handlers;

/// <summary>
/// Interface para os manipuladores de requisições.
/// </summary>
/// <remarks>
/// Os manipuladores de requisições são responsáveis por processar as requisições e retornar uma resposta.
/// Eles implementam a interface <see cref="IRequestHandler{TRequest, TResponse}"/>.
/// </remarks>
/// <typeparam name="TRequest">O tipo de requisição que o manipulador processa.</typeparam>
/// <typeparam name="TResponse">O tipo de resposta que o manipulador retorna.</typeparam>
public interface IRequestHandler<in TRequest, TResponse>
  where TRequest : IRequest<TResponse>
{
  /// <summary>
  /// Processa a requisição e retorna uma resposta.
  /// </summary>
  /// <param name="request">A requisição a ser processada.</param>
  /// <param name="cancellationToken">O token de cancelamento para a operação assíncrona. Opcional.</param>
  /// <returns>A resposta da requisição.</returns>
  Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken = default);
}
