using System.Net;
using Tooark.Exceptions;

namespace Tooark.Tests.Exceptions;

public class GetInfoExceptionTests
{
  // Teste de unidade para a mensagem de erro.
  [Fact]
  public void GetInfoException_ShouldReturnCorrectMessage()
  {
    // Arrange
    var expectedMessage = "Get Info Error";

    // Act
    var exception = new GetInfoException(expectedMessage);

    // Assert
    Assert.Equal(expectedMessage, exception.Message);
  }

  // Teste de unidade para o código de status HTTP.
  [Fact]
  public void GetInfoException_ShouldReturnBadRequestStatusCode()
  {
    // Arrange
    var exception = new GetInfoException("Get Info Error");

    // Act
    var statusCode = exception.GetStatusCode();

    // Assert
    Assert.Equal(HttpStatusCode.BadRequest, statusCode);
  }
}
