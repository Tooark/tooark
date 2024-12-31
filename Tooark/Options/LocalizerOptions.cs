namespace Tooark.Options;

/// <summary>
/// Opções para o serviço de localização de recursos.
/// </summary>
public class LocalizerOptions
{
  /// <summary>
  /// Dicionário com caminhos adicionais para arquivos JSON de recursos.
  /// </summary>
  public Dictionary<string, string>? ResourceAdditionalPaths { get; set; }

  /// <summary>
  /// O Stream de arquivo JSON de recursos.
  /// </summary>
  public Stream? FileStream { get; set; }
}
