using System.Globalization;
using System.Text.RegularExpressions;
using Tooark.Validations.Patterns;

namespace Tooark.Utils;

/// <summary>
/// Classe estática Language que contém constantes e propriedades para gerenciamento de idiomas.
/// </summary>
public static class Language
{
  /// <summary>
  /// O código de idioma padrão usado na aplicação. Padrão "en-US".
  /// </summary>
  public static readonly string Default = InternalLanguage.Default;

  /// <summary>
  /// O código de idioma atual do ambiente de execução.
  /// </summary>
  public static string Current { get => InternalLanguage.Current; }

  /// <summary>
  /// A cultura atual do ambiente de execução.
  /// </summary>
  public static CultureInfo CurrentCulture { get => InternalLanguage.CurrentCulture; }

  /// <summary>
  /// Função para definir a cultura atual para a aplicação.
  /// </summary>
  /// <param name="culture">O nome da cultura a ser definida. Exemplo: "en-US" ou "pt-BR".</param>
  public static void SetCulture(string culture)
  {
    InternalLanguage.SetCulture(culture);
  }

  /// <summary>
  /// Função para definir a cultura atual para a aplicação.
  /// </summary>
  /// <param name="culture">A cultura a ser definida. Exemplo: "en-US" ou "pt-BR".</param>
  public static void SetCulture(CultureInfo culture)
  {
    InternalLanguage.SetCulture(culture);
  }
}

/// <summary>
/// Classe estática interna Language que contém constantes e propriedades para gerenciamento de idiomas.
/// </summary>
internal static class InternalLanguage
{
  /// <summary>
  /// Campo privado que armazena a cultura de idioma atual. 
  /// </summary>
  private static CultureInfo _currentCulture = CultureInfo.CurrentCulture;

  /// <summary>
  /// O código de idioma padrão usado na aplicação. Padrão "en-US".
  /// </summary>
  internal static readonly string Default = "en-US";

  /// <summary>
  /// O código de idioma atual do ambiente de execução.
  /// </summary>
  internal static string Current { get => _currentCulture.Name; }

  /// <summary>
  /// A cultura atual do ambiente de execução.
  /// </summary>
  internal static CultureInfo CurrentCulture { get => _currentCulture; }

  /// <summary>
  /// Função para definir a cultura atual para a aplicação.
  /// </summary>
  /// <param name="culture">O nome da cultura a ser definida. Exemplo: "en-US" ou "pt-BR".</param>
  internal static void SetCulture(string culture)
  {
    // Verifica se o código de idioma é válido
    culture = Regex.IsMatch(culture, RegexPattern.Culture, RegexOptions.None, TimeSpan.FromMilliseconds(250)) ? culture : Default;

    // Cria uma nova cultura com o código de idioma especificado
    var newCulture = new CultureInfo(culture);

    // Define a cultura atual
    SetCulture(newCulture);
  }

  /// <summary>
  /// Função para definir a cultura atual para a aplicação.
  /// </summary>
  /// <param name="culture">A cultura a ser definida. Exemplo: "en-US" ou "pt-BR".</param>
  internal static void SetCulture(CultureInfo culture)
  {
    // Define a cultura atual e a cultura de interface do usuário
    CultureInfo.CurrentCulture = culture;
    CultureInfo.CurrentUICulture = culture;

    // Define a cultura atual
    _currentCulture = culture;
  }
}
