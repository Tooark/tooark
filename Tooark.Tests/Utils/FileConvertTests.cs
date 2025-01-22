using Microsoft.AspNetCore.Http;
using Moq;
using Tooark.Utils;

namespace Tooark.Tests.Utils;

public class FileConvertTests
{
  // Teste para o método ToMemoryStream(string) com uma string base64 válida.
  [Fact]
  public void ToMemoryStream_ValidBase64String_ReturnsMemoryStream()
  {
    // Arrange
    string base64String = "data:application/pdf;base64,JVBERi0xLjMKJcfs"; // Example base64 string

    // Act
    MemoryStream? result = FileConvert.ToMemoryStream(base64String);

    // Assert
    Assert.NotNull(result);
    Assert.True(result.Length > 0);
  }

  // Teste para o método ToMemoryStream(string) com uma string base64 inválida.
  [Fact]
  public void ToMemoryStream_InvalidBase64String_ReturnsNull()
  {
    // Arrange
    string invalidBase64String = "invalid base64, string";

    // Act
    MemoryStream? result = FileConvert.ToMemoryStream(invalidBase64String);

    // Assert
    Assert.Null(result);
  }

  // Teste para o método ToMemoryStream(string) com uma string vazia.
  [Fact]
  public void ToMemoryStream_EmptyString_ReturnsNull()
  {
    // Arrange
    string emptyString = "";

    // Act
    MemoryStream? result = FileConvert.ToMemoryStream(emptyString);

    // Assert
    Assert.Null(result);
  }

  // Teste para o método ToMemoryStream(IFormFile) com um IFormFile válido.
  [Fact]
  public void ToMemoryStream_FromIFormFile_ReturnsMemoryStream()
  {
    // Arrange
    var mockFile = new Mock<IFormFile>();
    var content = "data:application/pdf;base64,JVBERi0xLjMKJcfs";
    var fileName = "test.pdf";
    var ms = new MemoryStream();
    var writer = new StreamWriter(ms);
    writer.Write(content);
    writer.Flush();
    ms.Position = 0;
    mockFile.Setup(_ => _.OpenReadStream()).Returns(ms);
    mockFile.Setup(_ => _.FileName).Returns(fileName);
    mockFile.Setup(_ => _.Length).Returns(ms.Length);

    // Act
    MemoryStream? result = FileConvert.ToMemoryStream(mockFile.Object);

    // Assert
    Assert.NotNull(result);
    Assert.True(result.Length > 0);
  }

  // Teste para o método Extension(string) com uma string base64 válida.
  [Fact]
  public void Extension_ValidBase64String_ReturnsExtension()
  {
    // Arrange
    string base64String = "data:application/pdf;base64,JVBERi0xLjMKJcfs"; // Example base64 string

    // Act
    string? result = FileConvert.Extension(base64String);

    // Assert
    Assert.Equal("PDF", result);
  }

  // Teste para o método Extension(string) com uma string base64 inválida.
  [Fact]
  public void Extension_InvalidBase64String_ReturnsNull()
  {
    // Arrange
    string invalidBase64String = "invalid base64, string";

    // Act
    string? result = FileConvert.Extension(invalidBase64String);

    // Assert
    Assert.Null(result);
  }

  // Teste para o método Extension(string) com uma string vazia.
  [Fact]
  public void Extension_EmptyString_ReturnsNull()
  {
    // Arrange
    string emptyString = "";

    // Act
    string? result = FileConvert.Extension(emptyString);

    // Assert
    Assert.Null(result);
  }

  // Teste para o método Extension(IFormFile) com um IFormFile válido.
  [Fact]
  public void Extension_FromIFormFile_ReturnsExtension()
  {
    // Arrange
    var mockFile = new Mock<IFormFile>();
    var content = "data:application/pdf;base64,JVBERi0xLjMKJcfs";
    var fileName = "test.pdf";
    var ms = new MemoryStream();
    var writer = new StreamWriter(ms);
    writer.Write(content);
    writer.Flush();
    ms.Position = 0;
    mockFile.Setup(_ => _.OpenReadStream()).Returns(ms);
    mockFile.Setup(_ => _.FileName).Returns(fileName);
    mockFile.Setup(_ => _.Length).Returns(ms.Length);

    // Act
    string? result = FileConvert.Extension(mockFile.Object);

    // Assert
    Assert.Equal("PDF", result);
  }
}
