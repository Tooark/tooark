using Amazon.S3;
using Amazon.S3.Transfer;
using Google.Cloud.Storage.V1;
using Tooark.Options;

namespace Tooark.Services;

internal partial class BucketService(BucketOptions bucketOptions)
{
  private readonly BucketOptions _bucketOptions = bucketOptions;

  internal async Task UploadAsync(string filePath)
  {
    if (_bucketOptions.CloudProvider == Enums.CloudProvider.AWS)
    {
      using var client = new AmazonS3Client(_bucketOptions.AWS);
      var fileTransferUtility = new TransferUtility(client);
      await fileTransferUtility.UploadAsync(filePath, _bucketOptions.BucketName);
    }
    else if (_bucketOptions.CloudProvider == Enums.CloudProvider.GCP)
    {
      var storageClient = StorageClient.Create();
      using var fileStream = File.OpenRead(filePath);
      await storageClient.UploadObjectAsync(_bucketOptions.BucketName, Path.GetFileName(filePath), null, fileStream);
    }
    else
    {
      throw new NotSupportedException("Provedor de nuvem não suportado.");
    }
  }

  internal async Task DownloadAsync(string objectName, string destinationPath)
  {
    if (_bucketOptions.CloudProvider == Enums.CloudProvider.AWS)
    {
      using var client = new AmazonS3Client(_bucketOptions.AWS);
      var response = await client.GetObjectAsync(_bucketOptions.BucketName, objectName);
      await response.WriteResponseStreamToFileAsync(destinationPath, false, default);
    }
    else if (_bucketOptions.CloudProvider == Enums.CloudProvider.GCP)
    {
      var storageClient = StorageClient.Create();
      using var outputFileStream = File.OpenWrite(destinationPath);
      await storageClient.DownloadObjectAsync(_bucketOptions.BucketName, objectName, outputFileStream);
    }
    else
    {
      throw new NotSupportedException("Provedor de nuvem não suportado.");
    }
  }

  internal async Task DeleteAsync(string objectName)
  {
    if (_bucketOptions.CloudProvider == Enums.CloudProvider.AWS)
    {
      using var client = new AmazonS3Client(_bucketOptions.AWS);
      await client.DeleteObjectAsync(_bucketOptions.BucketName, objectName);
    }
    else if (_bucketOptions.CloudProvider == Enums.CloudProvider.GCP)
    {
      var storageClient = StorageClient.Create();
      await storageClient.DeleteObjectAsync(_bucketOptions.BucketName, objectName);
    }
    else
    {
      throw new NotSupportedException("Provedor de nuvem não suportado.");
    }
  }
}
