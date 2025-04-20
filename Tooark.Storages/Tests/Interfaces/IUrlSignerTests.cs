using Google.Cloud.Storage.V1;
using Moq;
using Tooark.Storages.Interfaces;

namespace Tooark.Tests.Storages.Interfaces;

public class IUrlSignerTests
{
  // Testes de unidade para a interface com três parâmetros.
  [Fact]
  public async Task SignAsync_ShouldReturnSignedUrl_WhenThreeParam()
  {
    // Arrange
    var expectedSignedUrl = "https://signed-url.com";
    var mockUrlSigner = new Mock<IUrlSigner>();
    mockUrlSigner
      .Setup(s => s.SignAsync(
        It.IsAny<string>(),
        It.IsAny<string>(),
        It.IsAny<TimeSpan>(),
        It.IsAny<CancellationToken>()))
      .ReturnsAsync(expectedSignedUrl);

    // Act
    var result = await mockUrlSigner.Object.SignAsync(string.Empty, string.Empty, TimeSpan.Zero);

    // Assert
    Assert.Equal(expectedSignedUrl, result);
  }

  // Testes de unidade para a interface com quatro parâmetros. Com a adição de um HttpMethod.
  [Fact]
  public async Task SignAsync_ShouldReturnSignedUrl_WhenFourParam()
  {
    // Arrange
    var expectedSignedUrl = "https://signed-url.com";
    var mockUrlSigner = new Mock<IUrlSigner>();
    mockUrlSigner
      .Setup(s => s.SignAsync(
        It.IsAny<string>(),
        It.IsAny<string>(),
        It.IsAny<TimeSpan>(),
        It.IsAny<HttpMethod>(),
        It.IsAny<CancellationToken>()))
      .ReturnsAsync(expectedSignedUrl);

    // Act
    var result = await mockUrlSigner.Object.SignAsync(string.Empty, string.Empty, TimeSpan.Zero, HttpMethod.Get);

    // Assert
    Assert.Equal(expectedSignedUrl, result);
  }

  // Testes de unidade para a interface com cinco parâmetros. Com a adição do parâmetro SigningVersion.
  [Fact]
  public async Task SignAsync_ShouldReturnSignedUrl_WhenFiveParam()
  {
    // Arrange
    var expectedSignedUrl = "https://signed-url.com";
    var mockUrlSigner = new Mock<IUrlSigner>();
    mockUrlSigner
      .Setup(s => s.SignAsync(
        It.IsAny<string>(),
        It.IsAny<string>(),
        It.IsAny<TimeSpan>(),
        It.IsAny<HttpMethod>(),
        It.IsAny<SigningVersion?>(),
        It.IsAny<CancellationToken>()))
      .ReturnsAsync(expectedSignedUrl);

    // Act
    var result = await mockUrlSigner.Object.SignAsync(string.Empty, string.Empty, TimeSpan.Zero, HttpMethod.Get, SigningVersion.V4);

    // Assert
    Assert.Equal(expectedSignedUrl, result);
  }
}
