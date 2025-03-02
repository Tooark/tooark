using System.Net;
using Tooark.Exceptions;

namespace Tooark.Tests.Exceptions;

public class TooarkExceptionTests
{
  private class TestTooarkException : TooarkException
  {
    public TestTooarkException(string message) : base(message) { }
    public TestTooarkException(IList<string> errors) : base(errors) { }

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;

  }

  // Teste de unidade para o construtor com uma única mensagem.
  [Fact]
  public void Constructor_WithSingleMessage_ShouldInitializeErrorList()
  {
    // Arrange
    var message = "Test error message";

    // Act
    var exception = new TestTooarkException(message);

    // Assert
    Assert.Single(exception.GetErrorMessages());
    Assert.Equal(message, exception.GetErrorMessages().First());
  }

  // Teste de unidade para o construtor com uma lista de mensagens.
  [Fact]
  public void Constructor_WithErrorList_ShouldInitializeErrorList()
  {
    // Arrange
    var errors = new List<string> { "Error 1", "Error 2" };

    // Act
    var exception = new TestTooarkException(errors);

    // Assert
    Assert.Equal(errors.Count, exception.GetErrorMessages().Count);
    Assert.Equal(errors, exception.GetErrorMessages());
  }

  // Teste de unidade para a função GetStatusCode.
  [Fact]
  public void GetStatusCode_ShouldReturnBadRequest()
  {
    // Arrange
    var exception = new TestTooarkException("Test error message");

    // Act
    var statusCode = exception.GetStatusCode();

    // Assert
    Assert.Equal(HttpStatusCode.BadRequest, statusCode);
  }
}
