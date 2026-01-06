using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tooark.Dtos.Injections;
using Tooark.Extensions.Injections;
using Tooark.Securities.Injections;
using Tooark.Securities.Options;
using Tooark.ValueObjects.Injections;

namespace Tooark.Injections;

/// <summary>
/// Classe de extensão para injeção de dependência do projeto Tooark.
/// </summary>
public static partial class TooarkDependencyInjection
{
  /// <summary>
  /// Adiciona as injeções de dependência dos projetos Tooark.
  /// </summary>
  /// <param name="services">A coleção de serviços para adicionar o serviço.</param>
  /// <param name="configuration">A configuração da aplicação.</param>
  /// <returns>A coleção de serviços adicionados.</returns>
  public static IServiceCollection AddTooarkService(this IServiceCollection services, IConfiguration? configuration = null)
  {
    // Adiciona as injeções de dependência Dtos
    services.AddTooarkDtos();

    // Adiciona as injeções de dependência Extensions
    services.AddTooarkExtensions();

    // Adiciona as injeções de dependência ValueObjects
    services.AddTooarkValueObjects();

    // Verifica se as configurações para JWT Token ou Criptografia existem
    if (configuration is not null &&
      (
        configuration.GetSection(JwtOptions.Section).Exists() ||
        configuration.GetSection(CryptographyOptions.Section).Exists()
      )
    )
    {
      // Adiciona as injeções de dependência Securities
      services.AddTooarkSecurities(configuration);
    }

    // Retorna a coleção de serviços
    return services;
  }
}
