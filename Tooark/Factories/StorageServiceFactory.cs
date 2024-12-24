using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Tooark.Interfaces;
using Tooark.Options;
using Tooark.Services.Storage;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Amazon.S3;
using Amazon;

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

    // Cria o StorageClient para o Google Cloud Storage
    GoogleCredential? googleCredential = null;
    StorageClient? storageClient = null;

    if (bucketOptions.Value.GCPPath != null)
    {
      googleCredential = GoogleCredential.FromFile(bucketOptions.Value.GCPPath);
    }
    else if (bucketOptions.Value.GCP != null)
    {
      googleCredential = GoogleCredential.FromJsonParameters(bucketOptions.Value.GCP);
    }    

    if(googleCredential != null)
    {
      storageClient = StorageClient.Create(googleCredential);
    }

    // Cria o AmazonS3Client para o AWS S3
    var awsCredentials = bucketOptions.Value.AWS;
    var region = bucketOptions.Value.AWSRegion ?? RegionEndpoint.USEast1;
    AmazonS3Client? s3Client = null;

    if (awsCredentials != null)
    {
      s3Client = new AmazonS3Client(awsCredentials, region);
    }

    return new StorageService(bucketOptions, new LoggerFactory().CreateLogger<StorageService>(), storageClient, s3Client);
  }
}
