using Microsoft.AspNetCore.Http;
using Moq;
using static Tooark.Utils.Util;

namespace Tooark.Tests.Utils;

public class ValidFileTest
{
  // Teste de arquivo de imagem válido
  [Fact]
  public void IsValidImageFile_ValidImage_ReturnsTrue()
  {
    // Arrange
    var fileMock = new Mock<IFormFile>();
    fileMock.Setup(f => f.Length).Returns(1024);
    fileMock.Setup(f => f.FileName).Returns("test.jpg");

    // Act
    var result = IsValidImageFile(fileMock.Object, 2048);

    // Assert
    Assert.True(result);
  }

  // Teste de arquivo de imagem inválido
  [Fact]
  public void IsValidImageFile_InvalidImage_ReturnsFalse()
  {
    // Arrange
    var fileMock = new Mock<IFormFile>();
    fileMock.Setup(f => f.Length).Returns(1024);
    fileMock.Setup(f => f.FileName).Returns("test.txt");

    // Act
    var result = IsValidImageFile(fileMock.Object, 2048);

    // Assert
    Assert.False(result);
  }

  // Teste de arquivo de documento válido
  [Fact]
  public void IsValidDocumentFile_ValidDocument_ReturnsTrue()
  {
    // Arrange
    var fileMock = new Mock<IFormFile>();
    fileMock.Setup(f => f.Length).Returns(1024);
    fileMock.Setup(f => f.FileName).Returns("test.pdf");

    // Act
    var result = IsValidDocumentFile(fileMock.Object, 2048);

    // Assert
    Assert.True(result);
  }

  // Teste de arquivo de documento inválido
  [Fact]
  public void IsValidDocumentFile_InvalidDocument_ReturnsFalse()
  {
    // Arrange
    var fileMock = new Mock<IFormFile>();
    fileMock.Setup(f => f.Length).Returns(1024);
    fileMock.Setup(f => f.FileName).Returns("test.mp4");

    // Act
    var result = IsValidDocumentFile(fileMock.Object, 2048);

    // Assert
    Assert.False(result);
  }

  // Teste de arquivo de vídeo válido
  [Fact]
  public void IsValidVideoFile_ValidVideo_ReturnsTrue()
  {
    // Arrange
    var fileMock = new Mock<IFormFile>();
    fileMock.Setup(f => f.Length).Returns(1024);
    fileMock.Setup(f => f.FileName).Returns("test.mp4");

    // Act
    var result = IsValidVideoFile(fileMock.Object, 2048);

    // Assert
    Assert.True(result);
  }

  // Teste de arquivo de vídeo inválido
  [Fact]
  public void IsValidVideoFile_InvalidVideo_ReturnsFalse()
  {
    // Arrange
    var fileMock = new Mock<IFormFile>();
    fileMock.Setup(f => f.Length).Returns(1024);
    fileMock.Setup(f => f.FileName).Returns("test.txt");

    // Act
    var result = IsValidVideoFile(fileMock.Object, 2048);

    // Assert
    Assert.False(result);
  }

  // Teste de arquivo com extensão personalizada válida
  [Fact]
  public void IsValidCustomExtensions_ValidCustomFile_ReturnsTrue()
  {
    // Arrange
    var fileMock = new Mock<IFormFile>();
    fileMock.Setup(f => f.Length).Returns(1024);
    fileMock.Setup(f => f.FileName).Returns("test.custom");

    // Act
    var result = IsValidCustomExtensions(fileMock.Object, 2048, [".CUSTOM"]);

    // Assert
    Assert.True(result);
  }

  // Teste de arquivo com extensão personalizada inválida
  [Fact]
  public void IsValidCustomExtensions_InvalidCustomFile_ReturnsFalse()
  {
    // Arrange
    var fileMock = new Mock<IFormFile>();
    fileMock.Setup(f => f.Length).Returns(1024);
    fileMock.Setup(f => f.FileName).Returns("test.txt");

    // Act
    var result = IsValidCustomExtensions(fileMock.Object, 2048, [".CUSTOM"]);

    // Assert
    Assert.False(result);
  }

  // Teste de arquivo com extensão personalizada válida
  [Fact]
  public void IsValidCustomExtensions_ValidCustomFileLower_ReturnsTrue()
  {
    // Arrange
    var fileMock = new Mock<IFormFile>();
    fileMock.Setup(f => f.Length).Returns(1024);
    fileMock.Setup(f => f.FileName).Returns("test.custom");

    // Act
    var result = IsValidCustomExtensions(fileMock.Object, 2048, [".custom"]);

    // Assert
    Assert.True(result);
  }

  // Teste de arquivo com extensão personalizada inválida
  [Fact]
  public void IsValidCustomExtensions_InvalidCustomFileLower_ReturnsFalse()
  {
    // Arrange
    var fileMock = new Mock<IFormFile>();
    fileMock.Setup(f => f.Length).Returns(1024);
    fileMock.Setup(f => f.FileName).Returns("test.txt");

    // Act
    var result = IsValidCustomExtensions(fileMock.Object, 2048, [".custom"]);

    // Assert
    Assert.False(result);
  }
  
  // Teste de arquivo com extensão personalizada válida sem parÂmetro de extensão
  [Fact]
  public void IsValidCustomExtensions_ValidCustomFileExtensionNull_ReturnsTrue()
  {
    // Arrange
    var fileMock = new Mock<IFormFile>();
    fileMock.Setup(f => f.Length).Returns(1024);
    fileMock.Setup(f => f.FileName).Returns("test.txt");

    // Act
    var result = IsValidCustomExtensions(fileMock.Object, 2048);

    // Assert
    Assert.True(result);
  }

  // Teste de arquivo com extensão personalizada inválida sem parÂmetro de extensão
  [Fact]
  public void IsValidCustomExtensions_ValidCustomFileExtensionNull_ReturnsFalse()
  {
    // Arrange
    var fileMock = new Mock<IFormFile>();
    fileMock.Setup(f => f.Length).Returns(1024);
    fileMock.Setup(f => f.FileName).Returns("test.custom");

    // Act
    var result = IsValidCustomExtensions(fileMock.Object, 2048);

    // Assert
    Assert.False(result);
  }
}
