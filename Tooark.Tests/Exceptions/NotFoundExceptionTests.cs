using System.Net;
using Tooark.Exceptions;

namespace Tooark.Tests.Exceptions;

public class NotFoundExceptionTests
{
  // Teste de unidade para a mensagem de erro.
  [Fact]
  public void NotFoundException_ShouldReturnCorrectMessage()
  {
    // Arrange
    var expectedMessage = "Not Found Error";

    // Act
    var exception = new NotFoundException(expectedMessage);

    // Assert
    Assert.Equal(expectedMessage, exception.Message);
  }

  // Teste de unidade para o código de status HTTP.
  [Fact]
  public void NotFoundException_ShouldReturnNotFoundStatusCode()
  {
    // Arrange
    var exception = new NotFoundException("Not Found Error");

    // Act
    var statusCode = exception.GetStatusCode();

    // Assert
    Assert.Equal(HttpStatusCode.NotFound, statusCode);
  }
}
