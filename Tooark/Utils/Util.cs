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
    /// Campo privado que armazena o código de idioma atual. 
    /// </summary>
    private static CultureInfo _currentCulture = CultureInfo.CurrentCulture;

    /// <summary>
    /// O código de idioma padrão usado na aplicação.
    /// </summary>
    public static readonly string Default = "en-US";

    /// <summary>
    /// Propriedade que obtém o código de idioma atual do ambiente de execução.
    /// </summary>
    public static string Current { get => _currentCulture.Name; }

    /// <summary>
    /// Propriedade que obtém a cultura atual do ambiente de execução.
    /// </summary>
    public static CultureInfo CurrentCulture { get => _currentCulture; }

    /// <summary>
    /// Define a cultura atual para a aplicação.
    /// </summary>
    /// <param name="culture">O nome da cultura a ser definida, exemplo: "en-US" ou "pt-BR".</param>
    public static void SetCulture(string culture)
    {
      var newCulture = new CultureInfo(culture);

      CultureInfo.CurrentCulture = newCulture;
      CultureInfo.CurrentUICulture = newCulture;
      CultureInfo.DefaultThreadCurrentCulture = newCulture;
      CultureInfo.DefaultThreadCurrentUICulture = newCulture;

      _currentCulture = newCulture;
    }

    /// <summary>
    /// Define a cultura atual para a aplicação.
    /// </summary>
    /// <param name="culture">A cultura a ser definida, exemplo: "en-US" ou "pt-BR".</param>
    public static void SetCulture(CultureInfo culture)
    {
      CultureInfo.CurrentCulture = culture;
      CultureInfo.CurrentUICulture = culture;
      CultureInfo.DefaultThreadCurrentCulture = culture;
      CultureInfo.DefaultThreadCurrentUICulture = culture;

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
