using System.Net;
using Tooark.Exceptions;

namespace Tooark.Tests.Exceptions;

public class InternalServerErrorExceptionTests
{
  // Teste de unidade para a mensagem de erro.
  [Fact]
  public void InternalServerErrorException_ShouldReturnCorrectMessage()
  {
    // Arrange
    var expectedMessage = "Internal Server Error";

    // Act
    var exception = new InternalServerErrorException(expectedMessage);

    // Assert
    Assert.Equal(expectedMessage, exception.Message);
  }

  // Teste de unidade para o código de status HTTP.
  [Fact]
  public void InternalServerErrorException_ShouldReturnInternalServerErrorStatusCode()
  {
    // Arrange
    var exception = new InternalServerErrorException("Internal Server Error");

    // Act
    var statusCode = exception.GetStatusCode();

    // Assert
    Assert.Equal(HttpStatusCode.InternalServerError, statusCode);
  }
}
