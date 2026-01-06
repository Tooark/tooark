using Moq;
using Tooark.Securities.Interfaces;

namespace Tooark.Tests.Securities.Interfaces;

public class ICryptographyServiceTests
{
  // Teste para criptografar um texto
  [Fact]
  public void Encrypt_ShouldReturnEncryptedString()
  {
    // Arrange
    var mockService = new Mock<ICryptographyService>();
    var plainText = "texto-para-criptografar";
    var expectedCipherText = "dGV4dG8tY3JpcHRvZ3JhZmFkbw==";
    mockService.Setup(s => s.Encrypt(plainText)).Returns(expectedCipherText);

    // Act
    var result = mockService.Object.Encrypt(plainText);

    // Assert
    Assert.Equal(expectedCipherText, result);
  }

  // Teste para descriptografar um texto
  [Fact]
  public void Decrypt_ShouldReturnPlainText()
  {
    // Arrange
    var mockService = new Mock<ICryptographyService>();
    var cipherText = "dGV4dG8tY3JpcHRvZ3JhZmFkbw==";
    var expectedPlainText = "texto-para-criptografar";
    mockService.Setup(s => s.Decrypt(cipherText)).Returns(expectedPlainText);

    // Act
    var result = mockService.Object.Decrypt(cipherText);

    // Assert
    Assert.Equal(expectedPlainText, result);
  }

  // Teste para verificar que Encrypt é chamado com o argumento correto
  [Fact]
  public void Encrypt_ShouldBeCalledWithCorrectArgument()
  {
    // Arrange
    var mockService = new Mock<ICryptographyService>();
    var plainText = "dados-sensiveis";
    mockService.Setup(s => s.Encrypt(It.IsAny<string>())).Returns("encrypted");

    // Act
    mockService.Object.Encrypt(plainText);

    // Assert
    mockService.Verify(s => s.Encrypt(plainText), Times.Once);
  }

  // Teste para verificar que Decrypt é chamado com o argumento correto
  [Fact]
  public void Decrypt_ShouldBeCalledWithCorrectArgument()
  {
    // Arrange
    var mockService = new Mock<ICryptographyService>();
    var cipherText = "encrypted-data";
    mockService.Setup(s => s.Decrypt(It.IsAny<string>())).Returns("decrypted");

    // Act
    mockService.Object.Decrypt(cipherText);

    // Assert
    mockService.Verify(s => s.Decrypt(cipherText), Times.Once);
  }
}
