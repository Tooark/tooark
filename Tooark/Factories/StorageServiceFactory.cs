using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Tooark.Interfaces;
using Tooark.Options;
using Tooark.Services.Storage;

namespace Tooark.Factories;

/// <summary>
/// Classe de fábrica para criar instâncias de <see cref="IStorageService"/>.
/// </summary>
public static class StorageServiceFactory
{
  /// <summary>
  /// Cria uma instância de <see cref="IStorageService"/> usando as opções de bucket e o logger fornecidos.
  /// </summary>
  /// <param name="bucketOptions">As opções de bucket para configurar o serviço de armazenamento.</param>
  /// <returns>Uma instância de <see cref="IStorageService"/>.</returns>
  public static IStorageService Create(IOptions<BucketOptions> bucketOptions)
  {
    // Verifica se as opções do bucket são válidas
    if (bucketOptions == null || bucketOptions.Value == null)
    {
      throw new ArgumentNullException(nameof(bucketOptions));
    }

    return new StorageService(bucketOptions, new LoggerFactory().CreateLogger<StorageService>());
  }
}
