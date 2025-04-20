using Amazon.S3;
using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Tooark.Storages.Interfaces;
using Tooark.Storages.Options;

namespace Tooark.Storages.Factories;

/// <summary>
/// Fábrica de instâncias de <see cref="IStorageService"/>.
/// </summary>
/// <remarks>
/// A fábrica é responsável por criar instâncias de <see cref="IStorageService"/>.
/// </remarks>
public static class StorageServiceFactory
{
  /// <summary>
  /// Cria uma instância de <see cref="IStorageService"/>.
  /// </summary>
  /// <param name="logger">O logger da aplicação.</param>
  /// <param name="storageOptions">Opções de configuração do serviço de armazenamento.</param>
  /// <param name="awsClient">Cliente da AWS S3. Parâmetro opcional.</param>
  /// <param name="gcpClient">Cliente do Google Cloud Storage. Parâmetro opcional.</param>
  /// <param name="gcpClientSigner">Assinador de URL do Google Cloud Storage. Parâmetro opcional.</param>
  /// <returns></returns>
  public static IStorageService Create(
    ILogger<StorageService> logger,
    IOptions<StorageOptions> storageOptions,
    IAmazonS3? awsClient = null,
    StorageClient? gcpClient = null,
    IUrlSigner? gcpClientSigner = null
  )
  {
    // Cria o serviço de armazenamento
    return new StorageService(logger, storageOptions, awsClient, gcpClient, gcpClientSigner);
  }
}
