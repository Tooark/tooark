using Tooark.Extensions.ValueObjects;

namespace Tooark.Extensions.Options;

/// <summary>
/// Opções para configuração do serviço de localização de recursos.
/// </summary>
public class LocalizerOptions
{
  /// <summary>
  /// Lista de caminhos adicionais para arquivos de recursos.
  /// </summary>
  public IList<ResourcePath>? ResourceAdditionalPath { get; set; }

  /// <summary>
  /// Lista de streams adicionais para arquivos de recursos.
  /// </summary>
  public IList<ResourceStream>? ResourceAdditionalStream { get; set; }
}
