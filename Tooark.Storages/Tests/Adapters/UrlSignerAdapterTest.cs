using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Options;
using Moq;
using Tooark.Enums;
using Tooark.Storages.Adapters;
using Tooark.Storages.Options;
using Tooark.Tests.Moq.Options;

namespace Tooark.Tests.Storages.Adapters
{
  public class UrlSignerAdapterTest
  {
    private readonly Mock<IOptions<StorageOptions>> _optionsMock = new();
    private readonly StorageOptions _storageOptions;
    private readonly UrlSignerAdapter _urlSignerAdapter;

    public UrlSignerAdapterTest()
    {
      _storageOptions = new StorageOptions
      {
        BucketName = "test-bucket",
        CloudProvider = ECloudProvider.Google,
        AWS = null,
        GCP = new MGcpOptions(),
        FileSize = 1024, // 1 MB
        SignerMinutes = 5
      };
      _optionsMock.Setup(o => o.Value).Returns(_storageOptions);

      GoogleCredential credential = GoogleCredential.FromJsonParameters(_optionsMock.Object.Value.GCP!.GetCredentials());

      var urlSigner = UrlSigner.FromCredential(credential);
      
      _urlSignerAdapter = new UrlSignerAdapter(urlSigner);
    }

    // Teste para o método SignAsync com três parâmetros
    [Fact]
    public async Task SignAsync_WithThreeParameters_ReturnsSignedUrl()
    {
      // Arrange
      var bucketName = "test-bucket";
      var objectName = "test-object";
      var expiration = TimeSpan.FromHours(1);
      var expectedSignedUrl = "https://storage.googleapis.com/test-bucket/test-object";

      // Act
      var result = await _urlSignerAdapter.SignAsync(bucketName, objectName, expiration);

      // Assert
      Assert.StartsWith(expectedSignedUrl, result);
    }

    // Teste para o método SignAsync com quatro parâmetros, adicionando o HttpMethod
    [Fact]
    public async Task SignAsync_WithFourParameters_ReturnsSignedUrl()
    {
      // Arrange
      var bucketName = "test-bucket";
      var objectName = "test-object";
      var expiration = TimeSpan.FromHours(1);
      var httpMethod = HttpMethod.Get;
      var expectedSignedUrl = "https://storage.googleapis.com/test-bucket/test-object";

      // Act
      var result = await _urlSignerAdapter.SignAsync(bucketName, objectName, expiration, httpMethod);

      // Assert
      Assert.StartsWith(expectedSignedUrl, result);
    }

    // Teste para o método SignAsync com cinco parâmetros, adicionando o SigningVersion
    [Fact]
    public async Task SignAsync_WithFiveParameters_ReturnsSignedUrl()
    {
      // Arrange
      var bucketName = "test-bucket";
      var objectName = "test-object";
      var expiration = TimeSpan.FromHours(1);
      var httpMethod = HttpMethod.Get;
      SigningVersion? signingVersion = null;
      var expectedSignedUrl = "https://storage.googleapis.com/test-bucket/test-object";

      // Act
      var result = await _urlSignerAdapter.SignAsync(bucketName, objectName, expiration, httpMethod, signingVersion);

      // Assert
      Assert.StartsWith(expectedSignedUrl, result);
    }
  }
}
