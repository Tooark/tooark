using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Tooark.Extensions.Injections;

namespace Tooark.Dtos.Injections;

/// <summary>
/// Classe de extensão para injeção de dependência do projeto Tooark.Dtos.
/// </summary>
public static partial class TooarkDependencyInjection
{
  /// <summary>
  /// Adiciona o StringLocalizer para Dto com injeção de dependência.
  /// </summary>
  /// <param name="services">A coleção de serviços para adicionar o StringLocalizer para Dto.</param>
  /// <returns>A coleção de serviços com o StringLocalizer para Dto adicionado.</returns>
  public static IServiceCollection AddDto(this IServiceCollection services)
  {
    // Adiciona o serviço JsonStringLocalizer
    services.AddJsonStringLocalizer();

    // Adiciona o serviço de localização de recurso padrão
    services.AddTransient<IStringLocalizer, StringLocalizer<Dto>>();

     // Configura o localizador de strings para a classe Dto
    var serviceProvider = services.BuildServiceProvider();
    var localizer = serviceProvider.GetRequiredService<IStringLocalizer>();
    Dto.Configure(localizer);

    // Retorna a coleção de serviços
    return services;
  }
}
