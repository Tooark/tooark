using System.Net;
using Tooark.Exceptions;

namespace Tooark.Tests.Exceptions;

public class ServiceUnavailableExceptionTests
{
  // Teste de unidade para a mensagem de erro.
  [Fact]
  public void ServiceUnavailableException_ShouldReturnCorrectMessage()
  {
    // Arrange
    var expectedMessage = "Service Unavailable Error";

    // Act
    var exception = new ServiceUnavailableException(expectedMessage);

    // Assert
    Assert.Equal(expectedMessage, exception.Message);
  }

  // Teste de unidade para o código de status HTTP.
  [Fact]
  public void ServiceUnavailableException_ShouldReturnServiceUnavailableStatusCode()
  {
    // Arrange
    var exception = new ServiceUnavailableException("Service Unavailable Error");

    // Act
    var statusCode = exception.GetStatusCode();

    // Assert
    Assert.Equal(HttpStatusCode.ServiceUnavailable, statusCode);
  }
}
