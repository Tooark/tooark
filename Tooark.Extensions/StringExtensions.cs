using Tooark.Utils;

namespace Tooark.Extensions;

/// <summary>
/// Método de extensão para a classe String.
/// </summary>
public static class StringExtensions
{
  /// <summary>
  /// Normaliza uma string.
  /// </summary>
  /// <remarks>
  /// Remove espaços e substitui caracteres acentuados por seus equivalentes não acentuados e converte para maiúscula.
  /// </remarks>
  /// <param name="value">A string a ser normalizada.</param>
  /// <returns>Uma string normalizada.</returns>
  public static string ToNormalize(this string value)
  {
    return InternalStringExtensions.ToNormalize(value);
  }

  /// <summary>
  /// Normaliza uma string usando expressões regulares.
  /// </summary>
  /// <remarks>
  /// Remove espaços e substitui caracteres acentuados por seus equivalentes não acentuados e converte para maiúscula.
  /// </remarks>
  /// <param name="value">A string a ser normalizada.</param>
  /// <returns>Uma string normalizada.</returns>
  public static string ToNormalizeRegex(this string value)
  {
    return InternalStringExtensions.ToNormalizeRegex(value);
  }

  /// <summary>
  /// Converte uma string de Snake Case para Pascal Case.
  /// </summary>
  /// <param name="value">A string em Snake Case.</param>
  /// <returns>A string convertida para Pascal Case.</returns>
  public static string FromSnakeToPascalCase(this string value)
  {
    return InternalStringExtensions.FromSnakeToPascalCase(value);
  }

  /// <summary>
  /// Converte uma string de Snake Case para Camel Case.
  /// </summary>
  /// <param name="value">A string em Snake Case.</param>
  /// <returns>A string convertida para Camel Case.</returns>
  public static string FromSnakeToCamelCase(this string value)
  {
    return InternalStringExtensions.FromSnakeToCamelCase(value);
  }

  /// <summary>
  /// Converte uma string de Snake Case para Kebab Case.
  /// </summary>
  /// <param name="value">A string em Snake Case.</param>
  /// <returns>A string convertida para Kebab Case.</returns>
  public static string FromSnakeToKebabCase(this string value)
  {
    return InternalStringExtensions.FromSnakeToKebabCase(value);
  }

  /// <summary>
  /// Converte uma string de Pascal Case para Snake Case.
  /// </summary>
  /// <param name="value">A string em Pascal Case.</param>
  /// <returns>A string convertida para Snake Case.</returns>
  public static string FromPascalToSnakeCase(this string value)
  {
    return InternalStringExtensions.FromPascalToSnakeCase(value);
  }

  /// <summary>
  /// Converte uma string de Pascal Case para Camel Case.
  /// </summary>
  /// <param name="value">A string em Pascal Case.</param>
  /// <returns>A string convertida para Camel Case.</returns>
  public static string FromCamelToSnakeCase(this string value)
  {
    return InternalStringExtensions.FromCamelToSnakeCase(value);
  }

  /// <summary>
  /// Converte uma string de Camel Case para Snake Case.
  /// </summary>
  /// <param name="value">A string em Camel Case.</param>
  /// <returns>A string convertida para Snake Case.</returns>
  public static string FromKebabToSnakeCase(this string value)
  {
    return InternalStringExtensions.FromKebabToSnakeCase(value);
  }
}

/// <summary>
/// Método interno de extensão para a classe String.
/// </summary>
internal static class InternalStringExtensions
{
  /// <summary>
  /// Normaliza uma string.
  /// </summary>
  /// <remarks>
  /// Remove espaços e substitui caracteres acentuados por seus equivalentes não acentuados e converte para maiúscula.
  /// </remarks>
  /// <param name="value">A string a ser normalizada.</param>
  /// <returns>Uma string normalizada.</returns>
  internal static string ToNormalize(this string value)
  {
    return Normalize.Value(value);
  }

  /// <summary>
  /// Normaliza uma string usando expressões regulares.
  /// </summary>
  /// <remarks>
  /// Remove espaços e substitui caracteres acentuados por seus equivalentes não acentuados e converte para maiúscula.
  /// </remarks>
  /// <param name="value">A string a ser normalizada.</param>
  /// <returns>Uma string normalizada.</returns>
  internal static string ToNormalizeRegex(this string value)
  {
    return Normalize.ValueRegex(value);
  }

  /// <summary>
  /// Converte uma string de Snake Case para Pascal Case.
  /// </summary>
  /// <param name="value">A string em Snake Case.</param>
  /// <returns>A string convertida para Pascal Case.</returns>
  internal static string FromSnakeToPascalCase(this string value)
  {
    // Verifica se a string é nula ou vazia
    if (string.IsNullOrEmpty(value))
    {
      return value;
    }

    // Converte a string para Pascal Case
    return string
            .Join("", value
            .Split('_')
            .Where(word => !string.IsNullOrEmpty(word))
            .Select(word => word.Length > 0 ? (char.ToUpperInvariant(word[0]) + word[1..].ToLowerInvariant()) : ""));
  }

  /// <summary>
  /// Converte uma string de Snake Case para Camel Case.
  /// </summary>
  /// <param name="value">A string em Snake Case.</param>
  /// <returns>A string convertida para Camel Case.</returns>
  internal static string FromSnakeToCamelCase(this string value)
  {
    // Verifica se a string é nula ou vazia
    if (string.IsNullOrEmpty(value))
    {
      return value;
    }

    // Converte a string para Pascal Case
    var pascalCase = FromSnakeToPascalCase(value);

    // Converte a string para Camel Case, mantendo a primeira letra minúscula
    return pascalCase.Length > 0 ? (char.ToLowerInvariant(pascalCase[0]) + pascalCase[1..]) : "";
  }

  /// <summary>
  /// Converte uma string de Snake Case para Kebab Case.
  /// </summary>
  /// <param name="value">A string em Snake Case.</param>
  /// <returns>A string convertida para Kebab Case.</returns>
  internal static string FromSnakeToKebabCase(this string value)
  {
    // Verifica se a string é nula ou vazia
    if (string.IsNullOrEmpty(value))
    {
      return value;
    }

    // Converte a string para Kebab Case
    return string
            .Join('-', value.Split('_')
            .Where(word => !string.IsNullOrEmpty(word))
            .Select(word => word.ToLowerInvariant()));
  }

  /// <summary>
  /// Converte uma string de Pascal Case para Snake Case.
  /// </summary>
  /// <param name="value">A string em Pascal Case.</param>
  /// <returns>A string convertida para Snake Case.</returns>
  internal static string FromPascalToSnakeCase(this string value)
  {
    // Verifica se a string é nula ou vazia
    if (string.IsNullOrEmpty(value))
    {
      return value;
    }

    // Converte a string para Snake Case
    return string
            .Concat(value
            .Select((x, i) =>
              i > 0 && char.IsUpper(x) ?
                "_" + char.ToLowerInvariant(x) :
                x.ToString()))
            .ToLower();
  }

  /// <summary>
  /// Converte uma string de Pascal Case para Camel Case.
  /// </summary>
  /// <param name="value">A string em Pascal Case.</param>
  /// <returns>A string convertida para Camel Case.</returns>
  internal static string FromCamelToSnakeCase(this string value)
  {
    // Verifica se a string é nula ou vazia
    if (string.IsNullOrEmpty(value))
    {
      return value;
    }

    // Converte a string para Snake Case
    return FromPascalToSnakeCase(value);
  }

  /// <summary>
  /// Converte uma string de Camel Case para Snake Case.
  /// </summary>
  /// <param name="value">A string em Camel Case.</param>
  /// <returns>A string convertida para Snake Case.</returns>
  internal static string FromKebabToSnakeCase(this string value)
  {
    // Verifica se a string é nula ou vazia
    if (string.IsNullOrEmpty(value))
    {
      return value;
    }

    // Converte a string para Kebab Case
    return string
            .Join('_', value.Split('-')
            .Where(word => !string.IsNullOrEmpty(word))
            .Select(word => word.ToLowerInvariant()));
  }
}
