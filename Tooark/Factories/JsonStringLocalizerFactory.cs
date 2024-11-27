using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using Tooark.Extensions;

namespace Tooark.Factories;

/// <summary>
/// Classe de fábrica para criar instâncias de <see cref="JsonStringLocalizerExtension"/>.
/// </summary>
/// <param name="distributedCache">O cache distribuído a ser usado pelos localizadores.</param>
/// <param name="resourceAdditionalPaths">Caminhos adicionais para arquivos JSON de recursos.</param>
/// <seealso cref="IStringLocalizerFactory"/>
public class JsonStringLocalizerFactory(IDistributedCache distributedCache, Dictionary<string, string>? resourceAdditionalPaths = null) : IStringLocalizerFactory
{
  private readonly IDistributedCache _distributedCache = distributedCache;
  private readonly Dictionary<string, string>? _resourceAdditionalPaths = resourceAdditionalPaths;

  /// <summary>
  /// Cria um novo <see cref="JsonStringLocalizerExtension"/> para o tipo de recurso especificado.
  /// </summary>
  /// <param name="resourceSource">O tipo do recurso.</param>
  /// <returns>
  /// Uma nova instância de <see cref="JsonStringLocalizerExtension"/>.
  /// </returns>
  public IStringLocalizer Create(Type resourceSource)
  {
    return new JsonStringLocalizerExtension(_distributedCache, _resourceAdditionalPaths);
  }

  /// <summary>
  /// Cria um novo <see cref="JsonStringLocalizerExtension"/> para o nome base e localização especificados.
  /// </summary>
  /// <param name="baseName">O nome base do recurso.</param>
  /// <param name="location">A localização do recurso.</param>
  /// <returns>
  /// Uma nova instância de <see cref="JsonStringLocalizerExtension"/>.
  /// </returns>
  public IStringLocalizer Create(string baseName, string location)
  {
    return new JsonStringLocalizerExtension(_distributedCache, _resourceAdditionalPaths);
  }
}
