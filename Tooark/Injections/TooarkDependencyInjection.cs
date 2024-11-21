using Microsoft.Extensions.DependencyInjection;
using Tooark.Services.Factory;
using Tooark.Services.Interface;

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
  /// <returns>A coleção de serviços com os serviços da Tooark adicionados.</returns>
  public static IServiceCollection AddTooarkServices(this IServiceCollection services)
  {
    services.AddHttpClientService();

    return services;
  }

  /// <summary>
  /// Adiciona o serviço HttpClient ao contêiner de injeção de dependência.
  /// </summary>
  /// <param name="services">A coleção de serviços para adicionar o serviço HttpClient.</param>
  /// <returns>A coleção de serviços com o serviço HttpClient adicionado.</returns>
  public static IServiceCollection AddHttpClientService(
    this IServiceCollection services)
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
}
