namespace Tooark.Dtos;

/// <summary>
/// Classe para metadados.
/// </summary>
/// <param name="key">Chave do metadado.</param>
/// <param name="value">Valor do metadado.</param>
public class MetadataDto(string key, string value)
{
  /// <summary>
  /// Chave do metadado.
  /// </summary>
  public string Key { get; private set; } = key ?? string.Empty;

  /// <summary>
  /// Valor do metadado.
  /// </summary>
  public string Value { get; private set; } = (!string.IsNullOrEmpty(key) ? value : key) ?? string.Empty;
}
