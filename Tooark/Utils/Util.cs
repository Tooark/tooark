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
    public static string Current => CultureInfo.CurrentCulture.Name;
  }
}

/// <summary>
/// Classe interna estática que fornece funcionalidades relacionadas a idiomas para uso interno.
/// </summary>
internal static partial class InternalUtil
{

}
