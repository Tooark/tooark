using Microsoft.Extensions.DependencyInjection;
using Tooark.Services.Factory;
using Tooark.Services.Interface;

namespace Tooark.Injections;

/// <summary>
/// Classe de extensão para IServiceCollection para adicionar serviços do Tooark ao contêiner de injeção de dependências.
/// </summary>
public static class TooarkDependencyInjection
{
  /// <summary>
  /// Adiciona os serviços do Tooark ao IServiceCollection.
  /// </summary>
  /// <param name="services">O IServiceCollection ao qual os serviços serão adicionados.</param>
  /// <returns>O IServiceCollection com os serviços do Tooark adicionados.</returns>
  public static IServiceCollection AddTooarkServices(this IServiceCollection services)
  {
    // Registra o HttpClientService para ser usado com a interface IHttpClientService
    services.AddHttpClient<IHttpClientService>(client => HttpClientServiceFactory.Create(client));

    return services;
  }
}
