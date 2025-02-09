using System.Globalization;

namespace Tooark.Extensions.ValueObjects;

/// <summary>
/// Classe para armazenar o código do idioma e o stream do arquivo de recurso adicional.
/// </summary>
public class ResourceStream
{
  /// <summary>
  /// Código do idioma do recurso
  /// </summary>
  public string LanguageCode { get; private set; }

  /// <summary>
  /// Stream do arquivo de recurso adicional
  /// </summary>
  public Stream Stream { get; private set; }


  /// <summary>
  /// Construtor da classe ResourceStream.
  /// </summary>
  /// <param name="languageCode">Código do idioma do recurso.</param>
  /// <param name="stream">Stream do arquivo de recurso adicional.</param>
  public ResourceStream(string languageCode, Stream stream)
  {
    // Verifica se o código do idioma é nulo ou vazio.
    languageCode = string.IsNullOrEmpty(languageCode) ? throw new ArgumentNullException(nameof(languageCode)) : languageCode;

    // Verifica se o código do idioma é nulo ou vazio.
    CultureInfo cultureInfo = new(languageCode);

    // Atribui o código do idioma ao campo da classe.
    LanguageCode = cultureInfo.Name;

    // Verifica se o stream do arquivo de recurso adicional é nulo, caso não seja, atribui ao campo Stream da classe.
    Stream = stream ?? throw new ArgumentNullException(nameof(stream));
  }

  /// <summary>
  /// Construtor da classe ResourceStream.
  /// </summary>
  /// <param name="cultureInfo">Informações sobre a cultura do recurso.</param>
  /// <param name="stream">Stream do arquivo de recurso adicional.</param>
  public ResourceStream(CultureInfo cultureInfo, Stream stream)
  {
    // Atribui o código do idioma ao campo da classe.
    LanguageCode = cultureInfo.Name;

    // Verifica se o stream do arquivo de recurso adicional é nulo, caso não seja, atribui ao campo Stream da classe.
    Stream = stream ?? throw new ArgumentNullException(nameof(stream));
  }
}
