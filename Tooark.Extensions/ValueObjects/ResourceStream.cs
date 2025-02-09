namespace Tooark.Extensions.ValueObjects;

/// <summary>
/// Classe para armazenar o código do idioma e o stream do arquivo de recurso adicional.
/// </summary>
/// <param name="languageCode">Código do idioma do recurso.</param>
/// <param name="stream">Stream do arquivo de recurso adicional.</param>
public class ResourceStream(string languageCode, Stream stream)
{
  /// <summary>
  /// Código do idioma do recurso
  /// </summary>
  public string LanguageCode { get; private set; } = string.IsNullOrWhiteSpace(languageCode) ?
    throw new ArgumentNullException(nameof(languageCode)) :
    languageCode;

  /// <summary>
  /// Stream do arquivo de recurso adicional
  /// </summary>
  public Stream Stream { get; private set; } = stream ??
    throw new ArgumentNullException(nameof(stream));
}
