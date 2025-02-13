using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Tooark.Extensions.Injections;
using Tooark.Extensions.Options;

namespace Tooark.Dtos.Injections;

/// <summary>
/// Classe de extensão para injeção de dependência do projeto Tooark.Extensions.
/// </summary>
public static partial class TooarkDependencyInjection
{
  /// <summary>
  /// Adiciona o serviço JsonStringLocalizer com injeção de dependência.
  /// </summary>
  /// <param name="services">A coleção de serviços para adicionar o serviço JsonStringLocalizer.</param>
  /// <param name="localizerOptions">Configurações para o serviço de localização de recursos. Parâmetro opcional.</param>
  /// <returns>A coleção de serviços com o serviço JsonStringLocalizer adicionado.</returns>
  public static IServiceCollection AddDto(
    this IServiceCollection services,
    LocalizerOptions? localizerOptions = null
  )
  {
    // Adiciona o serviço JsonStringLocalizer
    services.AddJsonStringLocalizer(localizerOptions);

    // Adiciona o serviço de localização de recursos
    services.AddLocalization(options => options.ResourcesPath = "Resources");
    
    // Adiciona o serviço de localização de recurso padrão
    services.AddTransient<IStringLocalizer, StringLocalizer<Dto>>();
    
    // Retorna a coleção de serviços
    return services;
  }
}
