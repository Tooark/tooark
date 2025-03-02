using System.Net;
using Tooark.Exceptions;

namespace Tooark.Tests.Exceptions;

public class BadGatewayExceptionTests
{
  // Teste de unidade para a mensagem de erro.
  [Fact]
  public void BadGatewayException_ShouldReturnCorrectMessage()
  {
    // Arrange
    var expectedMessage = "Bad Gateway Error";

    // Act
    var exception = new BadGatewayException(expectedMessage);

    // Assert
    Assert.Equal(expectedMessage, exception.Message);
  }

  // Teste de unidade para o código de status HTTP.
  [Fact]
  public void BadGatewayException_ShouldReturnBadGatewayStatusCode()
  {
    // Arrange
    var exception = new BadGatewayException("Bad Gateway Error");

    // Act
    var statusCode = exception.GetStatusCode();

    // Assert
    Assert.Equal(HttpStatusCode.BadGateway, statusCode);
  }
}
