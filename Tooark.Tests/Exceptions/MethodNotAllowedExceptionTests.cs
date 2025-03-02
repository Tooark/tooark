using System.Net;
using Tooark.Exceptions;

namespace Tooark.Tests.Exceptions;

public class MethodNotAllowedExceptionTests
{
  // Teste de unidade para a mensagem de erro.
  [Fact]
  public void MethodNotAllowedException_ShouldReturnCorrectMessage()
  {
    // Arrange
    var expectedMessage = "Method Not Allowed Error";

    // Act
    var exception = new MethodNotAllowedException(expectedMessage);

    // Assert
    Assert.Equal(expectedMessage, exception.Message);
  }

  // Teste de unidade para o código de status HTTP.
  [Fact]
  public void MethodNotAllowedException_ShouldReturnMethodNotAllowedStatusCode()
  {
    // Arrange
    var exception = new MethodNotAllowedException("Method Not Allowed Error");

    // Act
    var statusCode = exception.GetStatusCode();

    // Assert
    Assert.Equal(HttpStatusCode.MethodNotAllowed, statusCode);
  }
}
