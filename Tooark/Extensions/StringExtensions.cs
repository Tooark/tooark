using static Tooark.Utils.Util;

namespace Tooark.Extensions;

/// <summary>
/// Métodos de extensão para a classe String.
/// </summary>
public static class StringExtensions
{
  /// <summary>
  /// Normaliza uma string removendo espaços e substituindo caracteres acentuados por seus equivalentes não acentuados.
  /// </summary>
  /// <param name="value">A string a ser normalizada.</param>
  /// <returns>Uma string normalizada.</returns>
  public static string ToNormalize(this string value)
  {
    // Chama o método interno para normalizar a string
    return InternalString.ToNormalize(value);
  }

  /// <summary>
  /// Normaliza uma string usando expressões regulares para remover espaços e substituir caracteres acentuados.
  /// </summary>
  /// <param name="value">A string a ser normalizada.</param>
  /// <returns>Uma string normalizada.</returns>
  public static string ToNormalizeRegex(this string value)
  {
    // Chama o método interno para normalizar a string usando regex
    return InternalString.ToNormalizeRegex(value);
  }
}

/// <summary>
/// Métodos internos de extensão para a classe String.
/// </summary>
internal static partial class InternalString
{
  /// <summary>
  /// Normaliza uma string removendo espaços e substituindo caracteres acentuados por seus equivalentes não acentuados.
  /// </summary>
  /// <param name="value">A string a ser normalizada.</param>
  /// <returns>Uma string normalizada.</returns>
  internal static string ToNormalize(this string value)
  {
    return NormalizeValue(value);
  }

  /// <summary>
  /// Normaliza uma string usando expressões regulares para remover espaços e substituir caracteres acentuados.
  /// </summary>
  /// <param name="value">A string a ser normalizada.</param>
  /// <returns>Uma string normalizada.</returns>
  internal static string ToNormalizeRegex(this string value)
  {
    return NormalizeValueRegex(value);
  }
}
