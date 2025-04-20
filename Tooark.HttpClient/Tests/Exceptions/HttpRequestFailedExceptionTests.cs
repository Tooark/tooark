using System.Net;
using Tooark.Exceptions;

namespace Tooark.Tests.Exceptions;

public class HttpRequestFailedExceptionTests
{
  [Fact]
  public void HttpRequestFailedException_ThrowsWithCorrectMessageAndStatusCode()
  {
    // Arrange
    var expectedStatusCode = HttpStatusCode.BadRequest;
    var expectedContent = "Bad Request";

    // Act
    var exception = new HttpRequestFailedException(expectedStatusCode, expectedContent);

    // Assert
    Assert.Equal(expectedStatusCode, exception.StatusCode);
    Assert.Contains(expectedStatusCode.ToString(), exception.Message);
    Assert.Contains(expectedContent, exception.Message);
  }
}
