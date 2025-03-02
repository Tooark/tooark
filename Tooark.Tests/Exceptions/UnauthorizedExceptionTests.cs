using System.Net;
using Tooark.Exceptions;

namespace Tooark.Tests.Exceptions;

public class UnauthorizedExceptionTest
{
  // Teste de unidade para a mensagem de erro.
  [Fact]
  public void UnauthorizedException_ShouldReturnCorrectMessage()
  {
    // Arrange
    var expectedMessage = "Unauthorized Error";

    // Act
    var exception = new UnauthorizedException(expectedMessage);

    // Assert
    Assert.Equal(expectedMessage, exception.Message);
  }

  // Teste de unidade para o código de status HTTP.
  [Fact]
  public void UnauthorizedException_ShouldReturnUnauthorizedStatusCode()
  {
    // Arrange
    var exception = new UnauthorizedException("Unauthorized Error");

    // Act
    var statusCode = exception.GetStatusCode();

    // Assert
    Assert.Equal(HttpStatusCode.Unauthorized, statusCode);
  }
}
