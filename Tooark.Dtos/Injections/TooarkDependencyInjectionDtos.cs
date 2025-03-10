using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Tooark.Extensions.Injections;

namespace Tooark.Dtos.Injections;

/// <summary>
/// Classe de extensão para adicionar os objetos de transferência de dados (Dto) ao container de injeção de dependência.
/// </summary>
public static partial class TooarkDependencyInjection
{
  /// <summary>
  /// Adiciona o serviço de localização de recursos com o tipo Dto ao container de injeção de dependência.
  /// </summary>
  /// <param name="services">Coleção de serviços.</param>
  /// <returns>A coleção de serviços com o serviço de localização de recursos com o tipo Dto adicionado.</returns>
  public static IServiceCollection AddTooarkDtos(this IServiceCollection services)
  {
    // Adiciona o serviço JsonStringLocalizer
    services.AddJsonStringLocalizer();

    // Adiciona o serviço de localização de recurso padrão
    services.AddTransient<IStringLocalizer, StringLocalizer<Dto>>();

    // Cria o provedor de serviços
    var serviceProvider = services.BuildServiceProvider();

    // Obtenção do serviço de localização de recurso
    var localizer = serviceProvider.GetRequiredService<IStringLocalizer>();

    // Configura o tipo Dto
    Dto.Configure(localizer);

    // Retorna os serviços
    return services;
  }
}
