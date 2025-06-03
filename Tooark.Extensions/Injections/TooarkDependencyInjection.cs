using Microsoft.Extensions.DependencyInjection;

namespace Tooark.Extensions.Injections;

/// <summary>
/// Classe de extensão para adicionar os serviços de extensões ao container de injeção de dependência.
/// </summary>
public static partial class TooarkDependencyInjection
{
  /// <summary>
  /// Adiciona os serviços de extensões ao container de injeção de dependência.
  /// </summary>
  /// <param name="services">Coleção de serviços.</param>
  /// <returns>A coleção de serviços com os serviços de extensões adicionados.</returns>
  public static IServiceCollection AddTooarkExtensions(this IServiceCollection services)
  {
    // Adiciona o serviço de localização de recursos
    services.AddJsonStringLocalizer();

    // Retorna os serviços
    return services;
  }
}
