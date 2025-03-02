using System.Net;
using Tooark.Exceptions;

namespace Tooark.Tests.Exceptions;

public class BadRequestExceptionTests
{
  // Teste de unidade para a mensagem de erro.
  [Fact]
  public void BadRequestException_ShouldReturnCorrectMessage()
  {
    // Arrange
    var expectedMessage = "Bad Request Error";

    // Act
    var exception = new BadRequestException(expectedMessage);

    // Assert
    Assert.Equal(expectedMessage, exception.Message);
  }

  // Teste de unidade para o código de status HTTP.
  [Fact]
  public void BadRequestException_ShouldReturnBadRequestStatusCode()
  {
    // Arrange
    var exception = new BadRequestException("Bad Request Error");

    // Act
    var statusCode = exception.GetStatusCode();

    // Assert
    Assert.Equal(HttpStatusCode.BadRequest, statusCode);
  }
}
