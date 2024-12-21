using Microsoft.Extensions.Options;
using Moq;
using Tooark.Factories;
using Tooark.Interfaces;
using Tooark.Options;

namespace Tooark.Tests.Factories;

public class StorageServiceFactoryTest
{
  // Testa se o método Create retorna uma instância de StorageService
  [Fact]
  public void Create_ShouldReturnStorageServiceInstance()
  {
    // Arrange
    var bucketOptionsMock = new Mock<IOptions<BucketOptions>>();
    bucketOptionsMock.Setup(bo => bo.Value).Returns(new BucketOptions());

    // Act
    var result = StorageServiceFactory.Create(bucketOptionsMock.Object);

    // Assert
    Assert.NotNull(result);
    Assert.IsAssignableFrom<IStorageService>(result);
  }
}
