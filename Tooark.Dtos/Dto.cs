using Microsoft.Extensions.Localization;

namespace Tooark.Dtos;

/// <summary>
/// Classe base para DTOs.
/// </summary>
public abstract class Dto
{
  /// <summary>
  /// Localizador de strings privado.
  /// </summary>
  private static IStringLocalizer? _localizer;


  /// <summary>
  /// Configura o localizador de strings.
  /// </summary>
  /// <param name="localizer">Localizador de strings.</param>
  internal static void Configure(IStringLocalizer localizer)
  {
    // Atribui o localizador de strings.
    _localizer = localizer;
  }

  /// <summary>
  /// Obtém o localizador de strings.
  /// </summary>
  internal static IStringLocalizer? LocalizerString => _localizer;
}
