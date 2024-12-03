using System.Net;
using Microsoft.Extensions.Localization;
using Moq;
using Tooark.Exceptions;

namespace Tooark.Tests.Exceptions;

public class AppExceptionTest
{
  // Testes para BadRequest
  [Fact]
  public void BadRequest_WithCustomMessage_ShouldReturnBadRequestException()
  {
    // Arrange
    var message = "Custom Message Error";

    // Act
    var exception = AppException.BadRequest(message);

    // Assert
    Assert.Equal(HttpStatusCode.BadRequest, exception.HttpStatusCode);
    Assert.Equal(message, exception.Message);
  }

  // Testes para Unauthorized
  [Fact]
  public void Unauthorized_WithCustomMessage_ShouldReturnUnauthorizedException()
  {
    // Arrange
    var message = "Custom Message Error";

    // Act
    var exception = AppException.Unauthorized(message);

    // Assert
    Assert.Equal(HttpStatusCode.Unauthorized, exception.HttpStatusCode);
    Assert.Equal(message, exception.Message);
  }

  // Testes para Forbidden
  [Fact]
  public void Forbidden_WithCustomMessage_ShouldReturnForbiddenException()
  {
    // Arrange
    var message = "Custom Message Error";

    // Act
    var exception = AppException.Forbidden(message);

    // Assert
    Assert.Equal(HttpStatusCode.Forbidden, exception.HttpStatusCode);
    Assert.Equal(message, exception.Message);
  }

  // Testes para NotFound
  [Fact]
  public void NotFound_WithCustomMessage_ShouldReturnNotFoundException()
  {
    // Arrange
    var message = "Custom Message Error";

    // Act
    var exception = AppException.NotFound(message);

    // Assert
    Assert.Equal(HttpStatusCode.NotFound, exception.HttpStatusCode);
    Assert.Equal(message, exception.Message);
  }

  // Testes para Conflict
  [Fact]
  public void Conflict_WithCustomMessage_ShouldReturnConflictException()
  {
    // Arrange
    var message = "Custom Message Error";

    // Act
    var exception = AppException.Conflict(message);

    // Assert
    Assert.Equal(HttpStatusCode.Conflict, exception.HttpStatusCode);
    Assert.Equal(message, exception.Message);
  }

  // Testes para InternalServerError
  [Fact]
  public void InternalServerError_WithCustomMessage_ShouldReturnInternalServerErrorException()
  {
    // Arrange
    var message = "Custom Message Error";

    // Act
    var exception = AppException.InternalServerError(message);

    // Assert
    Assert.Equal(HttpStatusCode.InternalServerError, exception.HttpStatusCode);
    Assert.Equal(message, exception.Message);
  }

  // Testes para ServiceUnavailable
  [Fact]
  public void ServiceUnavailable_WithCustomMessage_ShouldReturnServiceUnavailableException()
  {
    // Arrange
    var message = "Custom Message Error";

    // Act
    var exception = AppException.ServiceUnavailable(message);

    // Assert
    Assert.Equal(HttpStatusCode.ServiceUnavailable, exception.HttpStatusCode);
    Assert.Equal(message, exception.Message);
  }

  // Testes para BadRequest
  [Fact]
  public void BadRequest_WithOutParameter_ShouldReturnBadRequestException()
  {
    // Arrange
    var message = "BadRequest";

    // Act
    var exception = AppException.BadRequest();

    // Assert
    Assert.Equal(HttpStatusCode.BadRequest, exception.HttpStatusCode);
    Assert.Equal(message, exception.Message);
  }

  // Testes para Unauthorized
  [Fact]
  public void Unauthorized_WithOutParameter_ShouldReturnUnauthorizedException()
  {
    // Arrange
    var message = "Unauthorized";

    // Act
    var exception = AppException.Unauthorized();

    // Assert
    Assert.Equal(HttpStatusCode.Unauthorized, exception.HttpStatusCode);
    Assert.Equal(message, exception.Message);
  }

  // Testes para Forbidden
  [Fact]
  public void Forbidden_WithOutParameter_ShouldReturnForbiddenException()
  {
    // Arrange
    var message = "Forbidden";

    // Act
    var exception = AppException.Forbidden();

    // Assert
    Assert.Equal(HttpStatusCode.Forbidden, exception.HttpStatusCode);
    Assert.Equal(message, exception.Message);
  }

  // Testes para NotFound
  [Fact]
  public void NotFound_WithOutParameter_ShouldReturnNotFoundException()
  {
    // Arrange
    var message = "NotFound";

    // Act
    var exception = AppException.NotFound();

    // Assert
    Assert.Equal(HttpStatusCode.NotFound, exception.HttpStatusCode);
    Assert.Equal(message, exception.Message);
  }

  // Testes para Conflict
  [Fact]
  public void Conflict_WithOutParameter_ShouldReturnConflictException()
  {
    // Arrange
    var message = "Conflict";

    // Act
    var exception = AppException.Conflict();

    // Assert
    Assert.Equal(HttpStatusCode.Conflict, exception.HttpStatusCode);
    Assert.Equal(message, exception.Message);
  }

  // Testes para InternalServerError
  [Fact]
  public void InternalServerError_WithOutParameter_ShouldReturnInternalServerErrorException()
  {
    // Arrange
    var message = "InternalServerError";

    // Act
    var exception = AppException.InternalServerError();

    // Assert
    Assert.Equal(HttpStatusCode.InternalServerError, exception.HttpStatusCode);
    Assert.Equal(message, exception.Message);
  }

  // Testes para ServiceUnavailable
  [Fact]
  public void ServiceUnavailable_WithOutParameter_ShouldReturnServiceUnavailableException()
  {
    // Arrange
    var message = "ServiceUnavailable";

    // Act
    var exception = AppException.ServiceUnavailable();

    // Assert
    Assert.Equal(HttpStatusCode.ServiceUnavailable, exception.HttpStatusCode);
    Assert.Equal(message, exception.Message);
  }

  // Testes para construtores valores padrões
  [Fact]
  public void Constructor_ShouldSetDefaultValues()
  {
    // Act
    var exception = new AppException();

    // Assert
    Assert.Equal(HttpStatusCode.BadRequest, exception.HttpStatusCode);
    Assert.Equal("BadRequest", exception.Message);
  }

  // Testes para construtores com mensagem customizada
  [Fact]
  public void Constructor_WithMessage_ShouldSetMessage()
  {
    // Arrange
    var message = "Custom message";

    // Act
    var exception = new AppException(message);

    // Assert
    Assert.Equal(HttpStatusCode.BadRequest, exception.HttpStatusCode);
    Assert.Equal(message, exception.Message);
  }

  // Testes para construtores com mensagem e código de status customizado
  [Fact]
  public void Constructor_WithMessageAndStatusCode_ShouldSetMessageAndStatusCode()
  {
    // Arrange
    var message = "Custom message";
    var statusCode = HttpStatusCode.NotFound;

    // Act
    var exception = new AppException(message, statusCode);

    // Assert
    Assert.Equal(statusCode, exception.HttpStatusCode);
    Assert.Equal(message, exception.Message);
  }

  // Testes para Configure
  [Fact]
  public void Configure_ShouldSetLocalizer()
  {
    // Arrange
    var localizerMock = new Mock<IStringLocalizer>();

    // Act
    AppException.Configure(localizerMock.Object);

    // Assert
    var exception = new AppException();
    Assert.Equal("BadRequest", exception.Message); // Assuming the default message is "BadRequest"
  }
}
