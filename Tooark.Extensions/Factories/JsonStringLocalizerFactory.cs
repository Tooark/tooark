using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;

namespace Tooark.Extensions.Factories;

/// <summary>
/// Fábrica de instâncias de <see cref="JsonStringLocalizerExtension"/>.
/// </summary>
/// <remarks>
/// A fábrica é responsável por criar instâncias de <see cref="JsonStringLocalizerExtension"/>.
/// </remarks>
/// <param name="distributedCache">Cache distribuído para armazenar as traduções.</param>
/// <seealso cref="IStringLocalizerFactory"/>
public class JsonStringLocalizerFactory(IDistributedCache distributedCache) : IStringLocalizerFactory
{
  /// <summary>
  /// Cache distribuído para armazenar as traduções.
  /// </summary>
  private readonly IDistributedCache _distributedCache = distributedCache;


  /// <summary>
  /// Cria uma instância de <see cref="JsonStringLocalizerExtension"/>.
  /// </summary>
  /// <param name="resourceSource">Parâmetro não utilizado.</param>	
  /// <returns>Instância de <see cref="IStringLocalizer"/>.</returns>
  public IStringLocalizer Create(Type resourceSource) => new JsonStringLocalizerExtension(_distributedCache);

  /// <summary>
  /// Cria uma instância de <see cref="JsonStringLocalizerExtension"/>.
  /// </summary>
  /// <param name="baseName">Parâmetro não utilizado.</param>
  /// <param name="location">Parâmetro não utilizado.</param>
  /// <returns>Instância de <see cref="IStringLocalizer"/>.</returns>
  public IStringLocalizer Create(string baseName, string location) => new JsonStringLocalizerExtension(_distributedCache);
}
