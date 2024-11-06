using System.Text.RegularExpressions;

namespace Tooark.Utils;

/// <summary>
/// Classe estática parcial que fornece métodos para normalização de strings.
/// </summary>
public static partial class Util
{
  /// <summary>
  /// Normaliza um valor removendo espaços, convertendo para minúsculas e substituindo caracteres especiais.
  /// </summary>
  /// <param name="value">O valor a ser normalizado.</param>
  /// <returns>Uma string contendo o valor normalizado.</returns>
  public static string NormalizeValue(string value)
  {
    return InternalUtil.NormalizeValue(value);
  }

  /// <summary>
  /// Normaliza um valor removendo espaços, convertendo para minúsculas e substituindo caracteres especiais.
  /// </summary>
  /// <param name="value">O valor a ser normalizado.</param>
  /// <returns>Uma string contendo o valor normalizado.</returns>
  public static string NormalizeValueRegex(string value)
  {
    return InternalUtil.NormalizeValueRegex(value);
  }
}

/// <summary>
/// Classe interna estática parcial que fornece métodos para normalização de strings.
/// </summary>
internal static partial class InternalUtil
{
  /// <summary>
  /// Normaliza um valor removendo espaços, convertendo para minúsculas e substituindo caracteres especiais.
  /// </summary>
  /// <param name="value">O valor a ser normalizado.</param>
  /// <returns>Uma string contendo o valor normalizado.</returns>
  internal static string NormalizeValue(string value)
  {
    if (value == null)
    {
      return "";
    }

    // Converte para minúsculas
    value = value.ToUpperInvariant();

    // StringBuilder para construir o valor normalizado
    var normalizedValue = new System.Text.StringBuilder();

    foreach (char c in value)
    {
      // Verifica se o caractere é um espaço e o ignora
      if (c == ' ')
        continue;

      // Verifica se o caractere é um número ou letra minúscula
      if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z'))
      {
        normalizedValue.Append(c);
      }
      else
      {
        // Substitui caracteres especiais por caracteres sem acentuação
        switch (c)
        {
          case 'Ç': normalizedValue.Append('C'); break;
          case 'Ñ': normalizedValue.Append('N'); break;
          case 'Ÿ':
          case 'Ý': normalizedValue.Append('Y'); break;
          case 'Â':
          case 'Ä':
          case 'À':
          case 'Å':
          case 'Á':
          case 'Ã': normalizedValue.Append('A'); break;
          case 'É':
          case 'Ê':
          case 'Ë':
          case 'È': normalizedValue.Append('E'); break;
          case 'Ï':
          case 'Î':
          case 'Ì':
          case 'Í': normalizedValue.Append('I'); break;
          case 'Ô':
          case 'Ö':
          case 'Ò':
          case 'Ó':
          case 'Õ': normalizedValue.Append('O'); break;
          case 'Ü':
          case 'Û':
          case 'Ù':
          case 'Ú': normalizedValue.Append('U'); break;

          default: break; // Ignora outros caracteres
        }
      }
    }

    return normalizedValue.ToString();
  }

  /// <summary>
  /// Normaliza um valor removendo espaços, convertendo para minúsculas e substituindo caracteres especiais.
  /// </summary>
  /// <param name="value">O valor a ser normalizado.</param>
  /// <returns>Uma string contendo o valor normalizado.</returns>
  internal static string NormalizeValueRegex(string value)
  {
    if (value == null)
    {
      return "";
    }

    // Converte para minúsculas
    value = value.ToUpperInvariant();

    // Remove espaços
    value = SpaceRegex().Replace(value, "");

    // Substitui caracteres especiais por caracteres sem acentuação
    value = CRegex().Replace(value, "C");
    value = NRegex().Replace(value, "N");
    value = YRegex().Replace(value, "Y");
    value = ARegex().Replace(value, "A");
    value = ERegex().Replace(value, "E");
    value = IRegex().Replace(value, "I");
    value = ORegex().Replace(value, "O");
    value = URegex().Replace(value, "U");

    // Remove caracteres diferente de números ou letras minúsculas
    value = AcceptRegex().Replace(value, "");

    return value;
  }

  // Definições de expressões regulares para uso na normalização
  // Caracteres espaço para ser removido
  [GeneratedRegex(@"\s", RegexOptions.IgnoreCase, matchTimeoutMilliseconds: 250)]
  private static partial Regex SpaceRegex();
  // Caracteres especiais substituir para c
  [GeneratedRegex("[Ç]", RegexOptions.IgnoreCase, matchTimeoutMilliseconds: 250)]
  private static partial Regex CRegex();
  // Caracteres especiais substituir para n
  [GeneratedRegex("[Ñ]", RegexOptions.IgnoreCase, matchTimeoutMilliseconds: 250)]
  private static partial Regex NRegex();
  // Caracteres especiais substituir para y
  [GeneratedRegex("[ŸÝ]", RegexOptions.IgnoreCase, matchTimeoutMilliseconds: 250)]
  private static partial Regex YRegex();
  // Caracteres especiais substituir para a
  [GeneratedRegex("[ÂÄÀÅÁÃ]", RegexOptions.IgnoreCase, matchTimeoutMilliseconds: 250)]
  private static partial Regex ARegex();
  // Caracteres especiais substituir para e
  [GeneratedRegex("[ÉÊËÈ]", RegexOptions.IgnoreCase, matchTimeoutMilliseconds: 250)]
  private static partial Regex ERegex();
  // Caracteres especiais substituir para i
  [GeneratedRegex("[ÏÎÌÍ]", RegexOptions.IgnoreCase, matchTimeoutMilliseconds: 250)]
  private static partial Regex IRegex();
  // Caracteres especiais substituir para o
  [GeneratedRegex("[ÔÖÒÓÕ]", RegexOptions.IgnoreCase, matchTimeoutMilliseconds: 250)]
  private static partial Regex ORegex();
  // Caracteres especiais substituir para u
  [GeneratedRegex("[ÜÛÙÚ]", RegexOptions.IgnoreCase, matchTimeoutMilliseconds: 250)]
  private static partial Regex URegex();
  // Caracteres que não são numéricos e letras minúsculas para ser removido
  [GeneratedRegex("[^0-9A-Z]", RegexOptions.IgnoreCase, matchTimeoutMilliseconds: 250)]
  private static partial Regex AcceptRegex();
}
