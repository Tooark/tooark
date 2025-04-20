using Tooark.Factories;
using Tooark.Interfaces;

namespace Tooark.Tests.Factories;

public class HttpClientServiceFactoryTests
{
  // Testa se o método Create retorna uma instância de HttpClientService
  [Fact]
  public void Create_ShouldReturnHttpClientServiceInstance()
  {
    // Arrange
    var httpClient = new HttpClient();

    // Act
    var result = HttpClientServiceFactory.Create(httpClient);

    // Assert
    Assert.NotNull(result);
    Assert.IsAssignableFrom<IHttpClientService>(result);
  }
}
