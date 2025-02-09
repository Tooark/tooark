using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using Tooark.Extensions.ValueObjects;

namespace Tooark.Extensions.Factories;

/// <summary>
/// Fábrica de instâncias de <see cref="JsonStringLocalizerExtension"/>.
/// </summary>
/// <remarks>
/// A fábrica é responsável por criar instâncias de <see cref="JsonStringLocalizerExtension"/>.
/// </remarks>
/// <param name="distributedCache">Cache distribuído para armazenar as traduções.</param>
/// <param name="additionalResourcePath">Caminhos adicionais para buscar os arquivos de tradução. Parâmetro opcional.</param>
/// <param name="additionalResourceStream">Stream do arquivo de tradução. Parâmetro opcional.</param>
/// <seealso cref="IStringLocalizerFactory"/>
public class JsonStringLocalizerFactory(
  IDistributedCache distributedCache,
  IList<ResourcePath>? additionalResourcePath = null,
  IList<ResourceStream>? additionalResourceStream = null
) : IStringLocalizerFactory
{
  /// <summary>
  /// Cache distribuído para armazenar as traduções.
  /// </summary>
  private readonly IDistributedCache _distributedCache = distributedCache;

  /// <summary>
  /// Caminhos adicionais para buscar os arquivos de tradução.
  /// </summary>
  private readonly IList<ResourcePath>? _additionalResourcePath = additionalResourcePath;

  /// <summary>
  /// Stream do arquivo de tradução.
  /// </summary>
  private readonly IList<ResourceStream>? _additionalResourceStream = additionalResourceStream;

  /// <summary>
  /// Cria uma instância de <see cref="JsonStringLocalizerExtension"/>.
  /// </summary>
  /// <param name="resourceSource">Parâmetro não utilizado.</param>	
  /// <returns>Instância de <see cref="IStringLocalizer"/>.</returns>
  public IStringLocalizer Create(Type resourceSource)
  {
    return new JsonStringLocalizerExtension(_distributedCache, _additionalResourcePath, _additionalResourceStream);
  }

  /// <summary>
  /// Cria uma instância de <see cref="JsonStringLocalizerExtension"/>.
  /// </summary>
  /// <param name="baseName">Parâmetro não utilizado.</param>
  /// <param name="location">Parâmetro não utilizado.</param>
  /// <returns>Instância de <see cref="IStringLocalizer"/>.</returns>
  public IStringLocalizer Create(string baseName, string location)
  {
    return new JsonStringLocalizerExtension(_distributedCache, _additionalResourcePath, _additionalResourceStream);
  }
}
