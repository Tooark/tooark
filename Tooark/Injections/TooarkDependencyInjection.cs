using Microsoft.Extensions.DependencyInjection;
using Tooark.Dtos.Injections;
using Tooark.Extensions.Injections;

namespace Tooark.Injections;

/// <summary>
/// Classe de extensão para injeção de dependência do projeto Tooark.
/// </summary>
public static partial class TooarkDependencyInjection
{
  /// <summary>
  /// Adiciona as injeções de dependência dos projetos Tooark.
  /// </summary>
  /// <param name="services">A coleção de serviços para adicionar o serviço.</param>
  /// <returns>A coleção de serviços adicionados.</returns>
  public static IServiceCollection AddTooarkService(this IServiceCollection services)
  {
    // Adiciona o serviço JsonStringLocalizer
    services.AddJsonStringLocalizer();

    // Adiciona o serviço StringLocalizer para Dto
    services.AddTooarkDtos();

    // Retorna a coleção de serviços
    return services;
  }
}
