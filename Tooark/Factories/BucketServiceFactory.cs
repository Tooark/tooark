using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Tooark.Interfaces;
using Tooark.Options;
using Tooark.Services;

namespace Tooark.Factories;

/// <summary>
/// Fábrica de instâncias de <see cref="IBucketService"/>.
/// </summary>
public static class BucketServiceFactory
{
  /// <summary>
  /// Cria uma instância de <see cref="IBucketService"/>.
  /// </summary>
  /// <param name="logger">O serviço de log.</param>
  /// <param name="bucketOptions">As opções de configuração do bucket.</param>
  /// <returns>Uma instância de <see cref="IBucketService"/>.</returns>
  public static IBucketService Create(
    ILogger<BucketService> logger,
    IOptions<BucketOptions> bucketOptions
  )
  {
    return new BucketService(logger, bucketOptions);
  }
}
