using Tooark.Mediator.Abstractions;

namespace Tooark.Mediator.Handlers;

/// <summary>
/// Interface para os manipuladores de consultas.
/// </summary>
/// <remarks>
/// Os manipuladores de consultas são responsáveis por processar as consultas e retornar uma resposta.
/// Eles implementam a interface <see cref="IRequestHandler{TQuery, TResponse}"/>.
/// </remarks>
/// <typeparam name="TQuery">O tipo de consulta que o manipulador processa.</typeparam>
/// <typeparam name="TResponse">O tipo de resposta que o manipulador retorna.</typeparam>
public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
  where TQuery : IQuery<TResponse>
{ }
