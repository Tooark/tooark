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

  /// <summary>
  /// Converte uma string de snake_case para PascalCase.
  /// </summary>
  /// <param name="value">A string em snake_case.</param>
  /// <returns>A string convertida para PascalCase.</returns>
  public static string ToPascalCase(this string value)
  {
    // Chama o método interno para converter a string para PascalCase
    return InternalString.ToPascalCase(value);
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

  /// <summary>
  /// Converte uma string de snake_case para PascalCase.
  /// </summary>
  /// <param name="value">A string em snake_case.</param>
  /// <returns>A string convertida para PascalCase.</returns>
  internal static string ToPascalCase(this string value)
  {
    // Verifica se a string é nula ou vazia
    if (string.IsNullOrEmpty(value))
    {
      return value;
    }

    // Converte a string para PascalCase
    return string.Concat(value.Split('_').Where(word => !string.IsNullOrEmpty(word)).Select(word => char.ToUpper(word[0]) + word[1..]));
  }
}
