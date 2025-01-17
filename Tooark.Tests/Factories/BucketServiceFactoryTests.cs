using Amazon.Runtime;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Tooark.Enums;
using Tooark.Exceptions;
using Tooark.Factories;
using Tooark.Options;
using Tooark.Services;

namespace Tooark.Tests.Factories
{
  public class BucketServiceFactoryTests
  {
    // Teste para criar uma instância de BucketService
    [Fact]
    public void Create_ShouldReturnBucketServiceInstance()
    {
      // Arrange
      var mockLogger = new Mock<ILogger<BucketService>>();
      var mockOptions = new Mock<IOptions<BucketOptions>>();
      mockOptions.Setup(o => o.Value).Returns(new BucketOptions()
      {
        BucketName = "bucket-name",
        CloudProvider = CloudProvider.AWS,
        AWS = new BasicAWSCredentials("access-key", "secret-key")
      });

      // Act
      var result = BucketServiceFactory.Create(mockLogger.Object, mockOptions.Object);

      // Assert
      Assert.NotNull(result);
      Assert.IsType<BucketService>(result);
    }

    // Teste para verificar se AppException é lançado quando logger é nulo
    [Fact]
    public void Create_ShouldThrowAppException_WhenLoggerIsNull()
    {
      // Arrange
      var mockOptions = new Mock<IOptions<BucketOptions>>();
      mockOptions.Setup(o => o.Value).Returns(new BucketOptions());

      // Act & Assert
      Assert.Throws<AppException>(() => BucketServiceFactory.Create(null!, mockOptions.Object));
    }

    // Teste para verificar se NullReferenceException é lançado quando bucketOptions é nulo
    [Fact]
    public void Create_ShouldThrowNullReferenceException_WhenBucketOptionsIsNull()
    {
      // Arrange
      var mockLogger = new Mock<ILogger<BucketService>>();

      // Act & Assert
      Assert.Throws<NullReferenceException>(() => BucketServiceFactory.Create(mockLogger.Object, null!));
    }

    // Teste para verificar se AppException é lançado quando BucketOptions.Value é nulo
    [Fact]
    public void Create_ShouldThrowAppException_WhenBucketOptionsValueIsNull()
    {
      // Arrange
      var mockLogger = new Mock<ILogger<BucketService>>();
      var mockOptions = new Mock<IOptions<BucketOptions>>();
      mockOptions.Setup(o => o.Value).Returns((BucketOptions)null!);

      // Act & Assert
      Assert.Throws<AppException>(() => BucketServiceFactory.Create(mockLogger.Object, mockOptions.Object));
    }
  }
}
