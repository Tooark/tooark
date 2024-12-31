using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Tooark.Factories;
using Tooark.Interfaces;
using Tooark.Options;
using Tooark.Services;

namespace Tooark.Injections;

/// <summary>
/// Classe para adicionar o serviço de armazenamento ao contêiner de injeção de dependência.
/// </summary>
public static partial class TooarkDependencyInjection
{
  /// <summary>
  /// Adiciona o serviço de armazenamento ao contêiner de injeção de dependência.
  /// </summary>
  /// <param name="services">A coleção de serviços para adicionar o serviço de armazenamento.</param>
  /// <param name="configuration">A configuração da aplicação.</param>
  /// <returns>A coleção de serviços com o serviço de armazenamento adicionado.</returns>
  public static IServiceCollection AddBucketService(this IServiceCollection services, IConfiguration configuration)
  {
    services.Configure<BucketOptions>(configuration.GetSection(BucketOptions.Section));
    services.AddSingleton<IBucketService>(provider =>
    {
      var options = provider.GetRequiredService<IOptions<BucketOptions>>();
      var logger = provider.GetRequiredService<ILogger<BucketService>>();

      return BucketServiceFactory.Create(logger, options);
    });

    return services;
  }
}
