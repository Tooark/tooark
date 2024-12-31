using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Tooark.Exceptions;

namespace Tooark.Injections;

/// <summary>
/// Classe para adicionar o serviço JsonStringLocalizer ao contêiner de injeção de dependência.
/// </summary>
public static partial class TooarkDependencyInjection
{
  /// <summary>
  /// Adiciona o serviço AddAppException ao contêiner de injeção de dependência.
  /// </summary>
  /// <param name="services">A coleção de serviços para adicionar o serviço AddAppException.</param>
  /// <returns>A coleção de serviços com o serviço AddAppException adicionado.</returns>
  public static IServiceCollection AddAppException(this IServiceCollection services)
  {
    services.AddSingleton<IStringLocalizer<AppException>>(provider =>
    {
      var factory = provider.GetRequiredService<IStringLocalizerFactory>();

      return (IStringLocalizer<AppException>)factory.Create(typeof(AppException));
    });

    // Configura o localizador de strings para a classe AppException
    var serviceProvider = services.BuildServiceProvider();
    var localizer = serviceProvider.GetRequiredService<IStringLocalizer<AppException>>();
    AppException.Configure(localizer);

    return services;
  }
}
