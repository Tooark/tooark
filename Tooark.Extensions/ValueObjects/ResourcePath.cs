namespace Tooark.Extensions.ValueObjects;

/// <summary>
/// Classe para armazenar o código do idioma e o caminho do arquivo de recurso adicional.
/// </summary>
/// <param name="languageCode">Código do idioma do recurso.</param>
/// <param name="path">Caminho do arquivo de recurso adicional.</param>
public class ResourcePath(string languageCode, string path)
{
  /// <summary>
  /// Código do idioma do recurso
  /// </summary>
  public string LanguageCode { get; private set; } = string.IsNullOrEmpty(languageCode) ?
    throw new ArgumentNullException(nameof(languageCode)) :
    languageCode;

  /// <summary>
  /// Caminho do arquivo de recurso adicional
  /// </summary>
  public string Path { get; private set; } = string.IsNullOrEmpty(path) ?
    throw new ArgumentNullException(nameof(path)) :
    path;
}
