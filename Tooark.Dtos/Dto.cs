using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Tooark.Extensions.Injections;

namespace Tooark.Dtos;

/// <summary>
/// Classe base para DTOs.
/// </summary>
/// <param name="localizer">Localizador de strings. Parâmetro opcional.</param>
/// <remarks>Se o localizador não for fornecido, será obtido um localizador padrão.</remarks>
public abstract class Dto(IStringLocalizer? localizer = null)
{
  /// <summary>
  /// Localizador de strings.
  /// </summary>
  internal readonly IStringLocalizer _localizer = localizer ?? GetLocalizer();


  /// <summary>
  /// Método para obter uma instância de IStringLocalizer.
  /// </summary>
  private static IStringLocalizer GetLocalizer()
  {
    // Cria um provedor de serviços
    var serviceProvider = new ServiceCollection()
      .AddJsonStringLocalizer()
      .BuildServiceProvider();

    // Retorna o localizador de strings
    return serviceProvider.GetRequiredService<IStringLocalizer>();
  }
}
