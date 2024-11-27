using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Tooark.Extensions;
using Tooark.Factories;
using Tooark.Interfaces;

namespace Tooark.Injections;

/// <summary>
/// Classe de extensão para configurar e adicionar serviços específicos da Tooark ao contêiner de injeção de dependência.
/// </summary>
public static class TooarkDependencyInjection
{
  /// <summary>
  /// Adiciona e configura os serviços da Tooark, incluindo serviços HTTP e RabbitMQ.
  /// </summary>
  /// <param name="services">A coleção de serviços para adicionar os serviços da Tooark.</param>
  /// <param name="resourceAdditionalPaths">Dicionário com caminhos adicionais para arquivos JSON de recursos.</param>
  /// <returns>A coleção de serviços com os serviços da Tooark adicionados.</returns>
  public static IServiceCollection AddTooarkServices(this IServiceCollection services, Dictionary<string, string>? resourceAdditionalPaths = null)
  {
    services.AddHttpClientService();
    services.AddJsonStringLocalizer(resourceAdditionalPaths);

    return services;
  }

  /// <summary>
  /// Adiciona o serviço HttpClient ao contêiner de injeção de dependência.
  /// </summary>
  /// <param name="services">A coleção de serviços para adicionar o serviço HttpClient.</param>
  /// <returns>A coleção de serviços com o serviço HttpClient adicionado.</returns>
  public static IServiceCollection AddHttpClientService(this IServiceCollection services)
  {
    services.AddHttpClient();

    services.AddTransient<IHttpClientService>(provider =>
    {
      var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
      var httpClient = httpClientFactory.CreateClient();

      return HttpClientServiceFactory.Create(httpClient);
    });

    return services;
  }

  /// <summary>
  /// Adiciona o serviço JsonStringLocalizer ao contêiner de injeção de dependência.
  /// </summary>
  /// <param name="services">A coleção de serviços para adicionar o serviço JsonStringLocalizer.</param>
  /// /// <param name="resourceAdditionalPaths">Dicionário com caminhos adicionais para arquivos JSON de recursos.</param>
  /// <returns>A coleção de serviços com o serviço JsonStringLocalizer adicionado.</returns>
  public static IServiceCollection AddJsonStringLocalizer(this IServiceCollection services, Dictionary<string, string>? resourceAdditionalPaths = null)
  {
    services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();
    services.AddSingleton<IStringLocalizerFactory>(provider =>
      new JsonStringLocalizerFactory(
        provider.GetRequiredService<IDistributedCache>(),
        resourceAdditionalPaths
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
