using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Tooark.Extensions.Factories;
using Tooark.Extensions.Options;

namespace Tooark.Extensions.Injections;

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
  public static IServiceCollection AddJsonStringLocalizer(
    this IServiceCollection services,
    LocalizerOptions? localizerOptions = null)
  {
    // Caso as opções de configuração não sejam informadas, cria um novo objeto de opções
    var options = localizerOptions ?? new LocalizerOptions();

    // Adiciona o serviço de cache distribuído
    services.AddDistributedMemoryCache();

    // Adiciona o serviço de localização de recursos
    services.AddLocalization(options => options.ResourcesPath = "Resources");

    // Adiciona o serviço de localização de recurso padrão
    services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();

    // Adiciona o serviço de localização de recursos com cache distribuído e opções de configuração
    services.AddSingleton<IStringLocalizerFactory>(provider =>
      new JsonStringLocalizerFactory(
        provider.GetRequiredService<IDistributedCache>(),
        options.ResourceAdditionalPath,
        options.ResourceAdditionalStream
      )
    );

    // Adiciona o serviço de localização de recursos com cache distribuído e opções de configuração
    services.AddTransient<IStringLocalizer>(provider =>
    {
      var factory = provider.GetRequiredService<IStringLocalizerFactory>();

      return factory.Create(typeof(JsonStringLocalizerExtension));
    });

    // Retorna a coleção de serviços
    return services;
  }
}
