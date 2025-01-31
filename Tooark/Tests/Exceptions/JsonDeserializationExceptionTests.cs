using Tooark.Exceptions;

namespace Tooark.Tests.Exceptions;

public class JsonDeserializationExceptionTests
{
  [Fact]
  public void JsonDeserializationException_ThrowsWithCorrectMessageAndInnerException()
  {
    // Arrange
    var expectedMessage = "An error occurred while deserializing the JSON response.";
    var innerException = new InvalidOperationException();

    // Act
    var exception = new JsonDeserializationException(expectedMessage, innerException);

    // Assert
    Assert.Equal(expectedMessage, exception.Message);
    Assert.Equal(innerException, exception.InnerException);
  }
}
