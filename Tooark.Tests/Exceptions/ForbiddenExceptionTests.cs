using System.Net;
using Tooark.Exceptions;

namespace Tooark.Tests.Exceptions;

public class ForbiddenExceptionTests
{
  // Teste de unidade para a mensagem de erro.
  [Fact]
  public void ForbiddenException_ShouldReturnCorrectMessage()
  {
    // Arrange
    var expectedMessage = "Forbidden Error";

    // Act
    var exception = new ForbiddenException(expectedMessage);

    // Assert
    Assert.Equal(expectedMessage, exception.Message);
  }

  // Teste de unidade para o código de status HTTP.
  [Fact]
  public void ForbiddenException_ShouldReturnForbiddenStatusCode()
  {
    // Arrange
    var exception = new ForbiddenException("Forbidden Error");

    // Act
    var statusCode = exception.GetStatusCode();

    // Assert
    Assert.Equal(HttpStatusCode.Forbidden, statusCode);
  }
}
