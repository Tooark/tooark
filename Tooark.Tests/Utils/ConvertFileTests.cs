using Microsoft.AspNetCore.Http;
using Moq;
using Tooark.Utils;

namespace Tooark.Tests.Utils;

public class ConvertFileTests
{
  [Fact]
  public void ConvertBase64ToMemoryStream_ValidBase64String_ReturnsMemoryStream()
  {
    // Arrange
    string base64String = "data:application/pdf;base64,JVBERi0xLjMKJcfs"; // Example base64 string

    // Act
    MemoryStream? result = Util.ConvertBase64ToMemoryStream(base64String);

    // Assert
    Assert.NotNull(result);
    Assert.True(result.Length > 0);
  }

  [Fact]
  public void ConvertBase64ToMemoryStream_InvalidBase64String_ReturnsNull()
  {
    // Arrange
    string invalidBase64String = "invalid base64 string";

    // Act
    MemoryStream? result = Util.ConvertBase64ToMemoryStream(invalidBase64String);

    // Assert
    Assert.Null(result);
  }

  [Fact]
  public void ConvertBase64ToMemoryStream_EmptyString_ReturnsNull()
  {
    // Arrange
    string emptyString = "";

    // Act
    MemoryStream? result = Util.ConvertBase64ToMemoryStream(emptyString);

    // Assert
    Assert.Null(result);
  }

  [Fact]
  public void ConvertBase64ToMemoryStream_FromIFormFile_ReturnsMemoryStream()
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
    MemoryStream? result = Util.ConvertBase64ToMemoryStream(mockFile.Object);

    // Assert
    Assert.NotNull(result);
    Assert.True(result.Length > 0);
  }

  [Fact]
  public void ExtractExtension_ValidBase64String_ReturnsExtension()
  {
    // Arrange
    string base64String = "data:application/pdf;base64,JVBERi0xLjMKJcfs"; // Example base64 string

    // Act
    string? result = Util.ExtractExtension(base64String);

    // Assert
    Assert.Equal("pdf", result);
  }

  [Fact]
  public void ExtractExtension_InvalidBase64String_ReturnsNull()
  {
    // Arrange
    string invalidBase64String = "invalid base64 string";

    // Act
    string? result = Util.ExtractExtension(invalidBase64String);

    // Assert
    Assert.Null(result);
  }

  [Fact]
  public void ExtractExtension_EmptyString_ReturnsNull()
  {
    // Arrange
    string emptyString = "";

    // Act
    string? result = Util.ExtractExtension(emptyString);

    // Assert
    Assert.Null(result);
  }

  [Fact]
  public void ExtractExtension_FromIFormFile_ReturnsExtension()
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
    string? result = Util.ExtractExtension(mockFile.Object);

    // Assert
    Assert.Equal("pdf", result);
  }
}
