using System.Text.RegularExpressions;

namespace Tooark.Extensions
{
  /// <summary>
  /// Métodos de extensão para a classe String.
  /// </summary>
  public static partial class StringExtensions
  {
    /// <summary>
    /// Normaliza uma string removendo espaços e substituindo caracteres acentuados por seus equivalentes não acentuados.
    /// </summary>
    /// <param name="value">A string a ser normalizada.</param>
    /// <returns>Uma string normalizada.</returns>
    public static string ToNormalize(this string value)
    {
      if (value == null)
      {
        return "";
      }

      // Converte para minúsculas
      value = value.ToLowerInvariant();

      // StringBuilder para construir o valor normalizado
      var normalizedValue = new System.Text.StringBuilder();

      foreach (char c in value)
      {
        // Verifica se o caractere é um espaço e o ignora
        if (c == ' ')
          continue;

        // Verifica se o caractere é um número ou letra minúscula
        if ((c >= '0' && c <= '9') || (c >= 'a' && c <= 'z'))
        {
          normalizedValue.Append(c);
        }
        else
        {
          // Substitui caracteres especiais por caracteres sem acentuação
          switch (c)
          {
            case 'ç': normalizedValue.Append('c'); break;
            case 'ñ': normalizedValue.Append('n'); break;
            case 'ÿ':
            case 'ý': normalizedValue.Append('y'); break;
            case 'â':
            case 'ä':
            case 'à':
            case 'å':
            case 'á':
            case 'ã': normalizedValue.Append('a'); break;
            case 'é':
            case 'ê':
            case 'ë':
            case 'è': normalizedValue.Append('e'); break;
            case 'ï':
            case 'î':
            case 'ì':
            case 'í': normalizedValue.Append('i'); break;
            case 'ô':
            case 'ö':
            case 'ò':
            case 'ó':
            case 'ð':
            case 'õ': normalizedValue.Append('o'); break;
            case 'ü':
            case 'û':
            case 'ù':
            case 'ú': normalizedValue.Append('u'); break;

            default: break; // Ignora outros caracteres
          }
        }
      }

      return normalizedValue.ToString();
    }

    /// <summary>
    /// Normaliza uma string usando expressões regulares para remover espaços e substituir caracteres acentuados.
    /// </summary>
    /// <param name="value">A string a ser normalizada.</param>
    /// <returns>Uma string normalizada.</returns>
    public static string ToNormalizeRegex(this string value)
    {
      if (value == null)
      {
        return "";
      }

      // Converte para minúsculas
      value = value.ToLowerInvariant();

      // Remove espaços
      value = SpaceRegex().Replace(value, "");

      // Substitui caracteres especiais por caracteres sem acentuação
      value = CRegex().Replace(value, "c");
      value = NRegex().Replace(value, "n");
      value = YRegex().Replace(value, "y");
      value = ARegex().Replace(value, "a");
      value = ERegex().Replace(value, "e");
      value = IRegex().Replace(value, "i");
      value = ORegex().Replace(value, "o");
      value = URegex().Replace(value, "u");

      // Remove caracteres diferente de números ou letras minúsculas
      value = AcceptRegex().Replace(value, "");

      return value;
    }

    // Definições de expressões regulares para uso na normalização
    // Caracteres espaço para ser removido
    [GeneratedRegex(@"\s", RegexOptions.IgnoreCase, matchTimeoutMilliseconds: 250)]
    private static partial Regex SpaceRegex();
    // Caracteres especiais substituir para c
    [GeneratedRegex("[ç]", RegexOptions.IgnoreCase, matchTimeoutMilliseconds: 250)]
    private static partial Regex CRegex();
    // Caracteres especiais substituir para n
    [GeneratedRegex("[ñ]", RegexOptions.IgnoreCase, matchTimeoutMilliseconds: 250)]
    private static partial Regex NRegex();
    // Caracteres especiais substituir para y
    [GeneratedRegex("[ÿý]", RegexOptions.IgnoreCase, matchTimeoutMilliseconds: 250)]
    private static partial Regex YRegex();
    // Caracteres especiais substituir para a
    [GeneratedRegex("[âäàåáã]", RegexOptions.IgnoreCase, matchTimeoutMilliseconds: 250)]
    private static partial Regex ARegex();
    // Caracteres especiais substituir para e
    [GeneratedRegex("[éêëè]", RegexOptions.IgnoreCase, matchTimeoutMilliseconds: 250)]
    private static partial Regex ERegex();
    // Caracteres especiais substituir para i
    [GeneratedRegex("[ïîìí]", RegexOptions.IgnoreCase, matchTimeoutMilliseconds: 250)]
    private static partial Regex IRegex();
    // Caracteres especiais substituir para o
    [GeneratedRegex("[ôöòóðõ]", RegexOptions.IgnoreCase, matchTimeoutMilliseconds: 250)]
    private static partial Regex ORegex();
    // Caracteres especiais substituir para u
    [GeneratedRegex("[üûùú]", RegexOptions.IgnoreCase, matchTimeoutMilliseconds: 250)]
    private static partial Regex URegex();
    // Caracteres que não são numéricos e letras minúsculas para ser removido
    [GeneratedRegex("[^0-9a-z]", RegexOptions.IgnoreCase, matchTimeoutMilliseconds: 250)]
    private static partial Regex AcceptRegex();
  }
}
