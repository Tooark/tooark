using System.Globalization;

namespace Tooark.Utils;

/// <summary>
/// Classe estática que fornece funcionalidades relacionadas a idiomas.
/// </summary>
public static partial class Util
{
  /// <summary>
  /// Classe aninhada estática que contém constantes e propriedades para gerenciamento de idiomas.
  /// </summary>
  public static class Languages
  {
    /// <summary>
    /// O código de idioma padrão usado na aplicação.
    /// </summary>
    public static readonly string Default = "en-US";

    /// <summary>
    /// Propriedade que obtém o código de idioma atual do ambiente de execução.
    /// </summary>
    public static string Current => _currentCulture;

    /// <summary>
    /// Campo privado que armazena o código de idioma atual. 
    /// </summary>
    private static string _currentCulture = CultureInfo.CurrentCulture.Name;

    /// <summary>
    /// Define a cultura atual para a aplicação.
    /// </summary>
    /// <param name="culture">O nome da cultura a ser definida, como "en-US" ou "pt-BR".</param>
    public static void SetCulture(string culture)
    {
      CultureInfo.CurrentCulture = new CultureInfo(culture);
      _currentCulture = culture;
    }
  }
}

/// <summary>
/// Classe interna estática que fornece funcionalidades relacionadas a idiomas para uso interno.
/// </summary>
internal static partial class InternalUtil
{

}
