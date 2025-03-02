using System.Net;
using Tooark.Exceptions;

namespace Tooark.Tests.Exceptions;

public class GatewayTimeoutExceptionTests
{
  // Teste de unidade para a mensagem de erro.
  [Fact]
  public void GatewayTimeoutException_ShouldReturnCorrectMessage()
  {
    // Arrange
    var expectedMessage = "Gateway Timeout Error";

    // Act
    var exception = new GatewayTimeoutException(expectedMessage);

    // Assert
    Assert.Equal(expectedMessage, exception.Message);
  }

  // Teste de unidade para o código de status HTTP.
  [Fact]
  public void GatewayTimeoutException_ShouldReturnGatewayTimeoutStatusCode()
  {
    // Arrange
    var exception = new GatewayTimeoutException("Gateway Timeout Error");

    // Act
    var statusCode = exception.GetStatusCode();

    // Assert
    Assert.Equal(HttpStatusCode.GatewayTimeout, statusCode);
  }
}
