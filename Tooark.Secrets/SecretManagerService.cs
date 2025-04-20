using Google.Cloud.SecretManager.V1;
using Microsoft.Extensions.Caching.Memory;

namespace Tooark.Secrets;

public class SecretManagerService
{
  private readonly SecretManagerServiceClient _secretManagerClient;
  private readonly IMemoryCache _cache;

  public SecretManagerService(SecretManagerServiceClient secretManagerClient, IMemoryCache cache)
  {
    _secretManagerClient = secretManagerClient;
    _cache = cache;
  }

  public string GetSecret(string projectId, string secretId, string versionId = "latest")
  {
    string cacheKey = $"{projectId}-{secretId}-{versionId}";
    if (!_cache.TryGetValue(cacheKey, out string secret))
    {
      var secretName = new SecretVersionName(projectId, secretId, versionId);
      var secretVersion = _secretManagerClient.AccessSecretVersion(secretName);
      secret = secretVersion.Payload.Data.ToStringUtf8();

      // Cache the secret for 1 hour
      _cache.Set(cacheKey, secret, TimeSpan.FromHours(1));
    }

    return secret;
  }
}

// No seu StorageService
public StorageService(
    ILogger<StorageService> logger,
    IOptions<StorageOptions> bucketOptions,
    SecretManagerService secretManagerService)
  {
    _logger = logger;
    _storageOptions = bucketOptions.Value;

    if (_storageOptions == null || string.IsNullOrEmpty(_storageOptions.BucketName))
    {
      throw new InternalServerErrorException("Storage.StorageOptionsEmpty");
    }

    if (_storageOptions.CloudProvider == ECloudProvider.None)
    {
      throw new InternalServerErrorException("Storage.CloudProviderEmpty");
    }
    else if (_storageOptions.CloudProvider == ECloudProvider.Amazon && _storageOptions.AWS == null)
    {
      throw new InternalServerErrorException("Storage.CredentialsEmpty;AWS");
    }
    else if (_storageOptions.CloudProvider == ECloudProvider.Google)
    {
      if (_storageOptions.GCP == null)
      {
        throw new InternalServerErrorException("Storage.CredentialsEmpty;GCP");
      }

      // Carregar credenciais do Google Secret Manager e cachear
      var secretPayload = secretManagerService.GetSecret("my-project", "my-gcp-credentials");
      _storageOptions.GCP.CredentialsJson = secretPayload;
    }
  }
