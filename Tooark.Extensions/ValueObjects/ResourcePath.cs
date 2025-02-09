using System.Globalization;

namespace Tooark.Extensions.ValueObjects;

/// <summary>
/// Classe para armazenar o código do idioma e o caminho do arquivo de recurso adicional.
/// </summary>
public class ResourcePath
{
  /// <summary>
  /// Código do idioma do recurso
  /// </summary>
  public string LanguageCode { get; private set; }

  /// <summary>
  /// Caminho do arquivo de recurso adicional
  /// </summary>
  public string Path { get; private set; }


  /// <summary>
  /// Construtor da classe ResourcePath.
  /// </summary>
  /// <param name="languageCode">Código do idioma do recurso.</param>
  /// <param name="path">Caminho do arquivo de recurso adicional.</param>
  public ResourcePath(string languageCode, string path)
  {
    // Verifica se o código do idioma é nulo ou vazio.
    languageCode = string.IsNullOrEmpty(languageCode) ? throw new ArgumentNullException(nameof(languageCode)) : languageCode;

    // Verifica se o código do idioma é nulo ou vazio.
    CultureInfo cultureInfo = new(languageCode);

    // Atribui o código do idioma ao campo da classe.
    LanguageCode = cultureInfo.Name;

    // Verifica se o caminho do arquivo de recurso adicional é nulo ou vazio, caso não seja, atribui ao campo Path da classe.
    Path = string.IsNullOrEmpty(path) ? throw new ArgumentNullException(nameof(path)) : path;
  }

  /// <summary>
  /// Construtor da classe ResourcePath.
  /// </summary>
  /// <param name="cultureInfo">Informações sobre a cultura do recurso.</param>
  /// <param name="path">Caminho do arquivo de recurso adicional.</param>
  public ResourcePath(CultureInfo cultureInfo, string path)
  {
    // Atribui o código do idioma ao campo da classe.
    LanguageCode = cultureInfo.Name;

    // Verifica se o caminho do arquivo de recurso adicional é nulo ou vazio, caso não seja, atribui ao campo Path da classe.
    Path = string.IsNullOrEmpty(path) ? throw new ArgumentNullException(nameof(path)) : path;
  }
}
