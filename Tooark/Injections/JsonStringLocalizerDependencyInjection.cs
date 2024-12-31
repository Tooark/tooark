using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Tooark.Extensions;
using Tooark.Factories;
using Tooark.Options;

namespace Tooark.Injections;

/// <summary>
/// Classe para adicionar o serviço JsonStringLocalizer ao contêiner de injeção de dependência.
/// </summary>
public static partial class TooarkDependencyInjection
{
  /// <summary>
  /// Adiciona o serviço JsonStringLocalizer ao contêiner de injeção de dependência.
  /// </summary>
  /// <param name="services">A coleção de serviços para adicionar o serviço JsonStringLocalizer.</param>
  /// <param name="localizerOptions">Configurações para o serviço de localização de recursos. Parâmetro opcional.</param>
  /// <returns>A coleção de serviços com o serviço JsonStringLocalizer adicionado.</returns>
  public static IServiceCollection AddJsonStringLocalizer(
    this IServiceCollection services,
    LocalizerOptions? localizerOptions = null)
  {
    var options = localizerOptions ?? new LocalizerOptions();

    services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();
    services.AddSingleton<IStringLocalizerFactory>(provider =>
      new JsonStringLocalizerFactory(
        provider.GetRequiredService<IDistributedCache>(),
        options.ResourceAdditionalPaths,
        options.FileStream
      )
    );

    services.AddTransient<IStringLocalizer>(provider =>
    {
      var factory = provider.GetRequiredService<IStringLocalizerFactory>();

      return factory.Create(typeof(JsonStringLocalizerExtension));
    });

    return services;
  }
}
