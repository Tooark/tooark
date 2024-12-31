using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tooark.Options;

namespace Tooark.Injections;

/// <summary>
/// Classe de extensão para configurar e adicionar serviços específicos da Tooark ao contêiner de injeção de dependência.
/// </summary>
public static partial class TooarkDependencyInjection
{
  /// <summary>
  /// Adiciona e configura os serviços da Tooark, incluindo serviços HTTP e RabbitMQ.
  /// </summary>
  /// <param name="services">A coleção de serviços para adicionar os serviços da Tooark.</param>
  /// <param name="configuration">A configuração da aplicação.</param>
  /// <param name="localizerOptions">Configurações para o serviço de localização de recursos. Parâmetro opcional.</param>
  /// <returns>A coleção de serviços com os serviços da Tooark adicionados.</returns>
  public static IServiceCollection AddTooarkServices(
    this IServiceCollection services,
    IConfiguration? configuration = null,
    LocalizerOptions? localizerOptions = null
  )
  {
    services.AddHttpClientService();
    services.AddJsonStringLocalizer(localizerOptions);
    services.AddAppException();

    if(configuration is not null)
    {
      services.AddBucketService(configuration);
    }

    return services;
  }
}
