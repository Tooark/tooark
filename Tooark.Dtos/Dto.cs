using Microsoft.Extensions.Localization;

namespace Tooark.Dtos;

/// <summary>
/// Classe base para DTOs.
/// </summary>
/// <remarks>Se o localizador não for fornecido, será obtido um localizador padrão.</remarks>
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
  public static void Configure(IStringLocalizer localizer)
  {
    // Atribui o localizador de strings.
    _localizer = localizer;
  }

  /// <summary>
  /// Obtém o localizador de strings.
  /// </summary>
  public static IStringLocalizer? LocalizerString => _localizer;
}
