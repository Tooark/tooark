using Tooark.ValueObjects;
using Tooark.Exceptions;
using Tooark.Extensions;

namespace Tooark.Tests.ValueObjects;

public class DocumentTests
{
  // Teste para construtor da classe Document com parâmetros válidos.
  [Fact]
  public void Constructor_ShouldCreatedDocument_WhenValidParameters()
  {
    // Arrange
    var number = "123456789";
    var type = "cpf";

    // Act
    var document = new Document(number, type);

    // Assert
    Assert.Equal(number, document.Number);
    Assert.Equal(type.ToNormalize(), document.Type);
  }

  // Teste para construtor da classe Document com tipo nulo.
  [Fact]
  public void Constructor_ShouldCreatedDocument_WhenTypeIsNull()
  {
    // Arrange
    var number = "123456789";
    string type = null!;

    // Act
    var document = new Document(number, type);

    // Assert
    Assert.Equal(number, document.Number);
    Assert.Null(document.Type);
  }

  // Teste para método ToString da classe Document.	
  [Fact]
  public void ToString_ShouldReturnNumber()
  {
    // Arrange
    var number = "123456789";
    var type = "cpf";
    var document = new Document(number, type);

    // Act
    var result = document.ToString();

    // Assert
    Assert.Equal(number, result);
  }

  // Teste para operador implícito de conversão de Document para string.
  [Fact]
  public void ImplicitConversionToString_ShouldReturnNumber()
  {
    // Arrange
    var number = "123456789";
    var type = "cpf";
    var document = new Document(number, type);

    // Act
    string result = document;

    // Assert
    Assert.Equal(number, result);
  }

  // Teste para operador implícito de conversão de string para Document.
  [Fact]
  public void ImplicitConversionToDocument_ShouldReturnDocument()
  {
    // Arrange
    var number = "123456789";

    // Act
    Document document = number;

    // Assert
    Assert.Equal(number, document.Number);
    Assert.Null(document.Type);
  }

  // Teste para construtor da classe Document com número nulo.
  [Fact]
  public void Constructor_ShouldThrowException_WhenNumberIsNull()
  {
    // Arrange
    string number = null!;

    // Act
    var exception = Assert.Throws<AppException>(() => new Document(number));

    // Assert
    Assert.Equal("Field.Required;Number", exception.Message);
  }
}
