using Tooark.Validations;

namespace Tooark.Tests.Validations;

public class TypeValidationTests
{
  // Teste para validar se o valor corresponde ao padrão e cria notificação, com valor que não corresponde
  [Theory]
  [InlineData("abc")]
  [InlineData("123")]
  [InlineData("")]
  [InlineData(null)]
  public void IsGuid_ShouldAddNotification_WhenValueNotIsGuid(string? valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var validation = new Validation();
    string value = valueParam!;

    // Act
    validation.IsGuid(value, property);

    // Assert
    Assert.Single(validation.Notifications);
    Assert.Equal(property, validation.Notifications.First().Key);
  }

  // Teste para validar se o valor corresponde ao padrão e não cria notificação, com valor que corresponde
  [Theory]
  [InlineData("00000000-0000-0000-0000-000000000000")]
  [InlineData("4a72eb00-36ae-4604-9789-8466bf0b6186")]
  public void IsGuid_ShouldNotAddNotification_WhenValueIsGuid(string valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var validation = new Validation();
    string value = valueParam;

    // Act
    validation.IsGuid(value, property);

    // Assert
    Assert.Empty(validation.Notifications);
  }

  // Teste para validar se o valor corresponde ao padrão e cria notificação, com valor que não corresponde
  [Theory]
  [InlineData("123")]
  [InlineData("!@#")]
  [InlineData("")]
  [InlineData(null)]
  public void IsLetter_ShouldAddNotification_WhenValueNotIsLetter(string? valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var validation = new Validation();
    string value = valueParam!;

    // Act
    validation.IsLetter(value, property);

    // Assert
    Assert.Single(validation.Notifications);
    Assert.Equal(property, validation.Notifications.First().Key);
  }

  // Teste para validar se o valor corresponde ao padrão e não cria notificação, com valor que corresponde
  [Theory]
  [InlineData("abc")]
  [InlineData("ABC")]
  [InlineData("aBc")]
  public void IsLetter_ShouldNotAddNotification_WhenValueIsLetter(string valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var validation = new Validation();
    string value = valueParam;

    // Act
    validation.IsLetter(value, property);

    // Assert
    Assert.Empty(validation.Notifications);
  }

  // Teste para validar se o valor corresponde ao padrão e cria notificação, com valor que não corresponde
  [Theory]
  [InlineData("ABC")]
  [InlineData("123")]
  [InlineData("!@#")]
  [InlineData("")]
  [InlineData(null)]
  public void IsLetterLower_ShouldAddNotification_WhenValueNotIsLetterLower(string? valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var validation = new Validation();
    string value = valueParam!;

    // Act
    validation.IsLetterLower(value, property);

    // Assert
    Assert.Single(validation.Notifications);
    Assert.Equal(property, validation.Notifications.First().Key);
  }

  // Teste para validar se o valor corresponde ao padrão e não cria notificação, com valor que corresponde
  [Theory]
  [InlineData("abc")]
  [InlineData("a")]
  public void IsLetterLower_ShouldNotAddNotification_WhenValueIsLetterLower(string valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var validation = new Validation();
    string value = valueParam;

    // Act
    validation.IsLetterLower(value, property);

    // Assert
    Assert.Empty(validation.Notifications);
  }

  // Teste para validar se o valor corresponde ao padrão e cria notificação, com valor que não corresponde
  [Theory]
  [InlineData("abc")]
  [InlineData("123")]
  [InlineData("!@#")]
  [InlineData("")]
  [InlineData(null)]
  public void IsLetterUpper_ShouldAddNotification_WhenValueNotIsLetterUpper(string? valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var validation = new Validation();
    string value = valueParam!;

    // Act
    validation.IsLetterUpper(value, property);

    // Assert
    Assert.Single(validation.Notifications);
    Assert.Equal(property, validation.Notifications.First().Key);
  }

  // Teste para validar se o valor corresponde ao padrão e não cria notificação, com valor que corresponde
  [Theory]
  [InlineData("ABC")]
  [InlineData("A")]
  public void IsLetterUpper_ShouldNotAddNotification_WhenValueIsLetterUpper(string valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var validation = new Validation();
    string value = valueParam;

    // Act
    validation.IsLetterUpper(value, property);

    // Assert
    Assert.Empty(validation.Notifications);
  }

  // Teste para validar se o valor corresponde ao padrão e cria notificação, com valor que não corresponde
  [Theory]
  [InlineData("abc")]
  [InlineData("ABC")]
  [InlineData("!@#")]
  [InlineData("")]
  [InlineData(null)]
  public void IsNumeric_ShouldAddNotification_WhenValueNotIsNumeric(string? valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var validation = new Validation();
    string value = valueParam!;

    // Act
    validation.IsNumeric(value, property);

    // Assert
    Assert.Single(validation.Notifications);
    Assert.Equal(property, validation.Notifications.First().Key);
  }

  // Teste para validar se o valor corresponde ao padrão e não cria notificação, com valor que corresponde
  [Theory]
  [InlineData("123")]
  [InlineData("1")]
  public void IsNumeric_ShouldNotAddNotification_WhenValueIsNumeric(string valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var validation = new Validation();
    string value = valueParam;

    // Act
    validation.IsNumeric(value, property);

    // Assert
    Assert.Empty(validation.Notifications);
  }

  // Teste para validar se o valor corresponde ao padrão e cria notificação, com valor que não corresponde
  [Theory]
  [InlineData("!@#")]
  [InlineData("")]
  [InlineData(null)]
  public void IsLetterNumeric_ShouldAddNotification_WhenValueNotIsLetterNumeric(string? valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var validation = new Validation();
    string value = valueParam!;

    // Act
    validation.IsLetterNumeric(value, property);

    // Assert
    Assert.Single(validation.Notifications);
    Assert.Equal(property, validation.Notifications.First().Key);
  }

  // Teste para validar se o valor corresponde ao padrão e não cria notificação, com valor que corresponde
  [Theory]
  [InlineData("abc")]
  [InlineData("ABC")]
  [InlineData("123")]
  [InlineData("aA1")]
  public void IsLetterNumeric_ShouldNotAddNotification_WhenValueIsLetterNumeric(string valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var validation = new Validation();
    string value = valueParam;

    // Act
    validation.IsLetterNumeric(value, property);

    // Assert
    Assert.Empty(validation.Notifications);
  }

  // Teste para validar se o valor corresponde ao padrão e cria notificação, com valor que não corresponde
  [Theory]
  [InlineData("GHI")]
  [InlineData("!@#")]
  [InlineData("")]
  [InlineData(null)]
  public void IsHexadecimal_ShouldAddNotification_WhenValueNotIsHexadecimal(string? valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var validation = new Validation();
    string value = valueParam!;

    // Act
    validation.IsHexadecimal(value, property);

    // Assert
    Assert.Single(validation.Notifications);
    Assert.Equal(property, validation.Notifications.First().Key);
  }

  // Teste para validar se o valor corresponde ao padrão e não cria notificação, com valor que corresponde
  [Theory]
  [InlineData("abc")]
  [InlineData("ABC")]
  [InlineData("123")]
  [InlineData("aA1")]
  public void IsHexadecimal_ShouldNotAddNotification_WhenValueIsHexadecimal(string valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var validation = new Validation();
    string value = valueParam;

    // Act
    validation.IsHexadecimal(value, property);

    // Assert
    Assert.Empty(validation.Notifications);
  }

  // Teste para validar se o valor corresponde ao padrão e cria notificação, com valor que não corresponde
  [Theory]
  [InlineData("abc")]
  [InlineData("ABC")]
  [InlineData("123")]
  [InlineData("!@#")]
  [InlineData("aaaaa-aaa")]
  [InlineData("10000-0000")]
  [InlineData("10000-00")]
  [InlineData("10000-0")]
  [InlineData("1000")]
  [InlineData("")]
  [InlineData(null)]
  public void IsZipCode_ShouldAddNotification_WhenValueNotIsZipCode(string? valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var validation = new Validation();
    string value = valueParam!;

    // Act
    validation.IsZipCode(value, property);

    // Assert
    Assert.Single(validation.Notifications);
    Assert.Equal(property, validation.Notifications.First().Key);
  }

  // Teste para validar se o valor corresponde ao padrão e não cria notificação, com valor que corresponde
  [Theory]
  [InlineData("10000")]
  [InlineData("10000-000")]
  public void IsZipCode_ShouldNotAddNotification_WhenValueIsZipCode(string valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var validation = new Validation();
    string value = valueParam;

    // Act
    validation.IsZipCode(value, property);

    // Assert
    Assert.Empty(validation.Notifications);
  }

  // Teste para validar se o valor corresponde ao padrão e cria notificação, com valor que não corresponde
  [Theory]
  [InlineData("abc")]
  [InlineData("ABC")]
  [InlineData("!@#")]
  [InlineData("data:;base64,iVBORw0KGgoAAAANSUhEUgAAAAUA=")]
  [InlineData("data:image/png;base64,")]
  [InlineData("data:;base64,")]
  [InlineData("")]
  [InlineData(null)]
  public void IsBase64_ShouldAddNotification_WhenValueNotIsBase64(string? valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var validation = new Validation();
    string value = valueParam!;

    // Act
    validation.IsBase64(value, property);

    // Assert
    Assert.Single(validation.Notifications);
    Assert.Equal(property, validation.Notifications.First().Key);
  }

  // Teste para validar se o valor corresponde ao padrão e não cria notificação, com valor que corresponde
  [Theory]
  [InlineData("data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAUA")]
  [InlineData("data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAUA==")]
  public void IsBase64_ShouldNotAddNotification_WhenValueIsBase64(string valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var validation = new Validation();
    string value = valueParam;

    // Act
    validation.IsBase64(value, property);

    // Assert
    Assert.Empty(validation.Notifications);
  }

  // Teste para validar se o valor corresponde ao padrão e cria notificação, com valor que não corresponde
  [Theory]
  [InlineData("abcdefghijklmnopqrstuvwxyz")]
  [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
  [InlineData("0123456789")]
  [InlineData("!@#$%¨&*()-_+=´`~^'\"[{]]<,>.:;?/\\")]
  [InlineData("aA0!")]
  [InlineData("")]
  [InlineData(null)]
  public void IsPassword_ShouldAddNotification_WhenValueNotIsPassword(string? valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var validation = new Validation();
    string value = valueParam!;

    // Act
    validation.IsPassword(value, property);

    // Assert
    Assert.Single(validation.Notifications);
    Assert.Equal(property, validation.Notifications.First().Key);
  }

  // Teste para validar se o valor corresponde ao padrão com mensagem e cria notificação, com valor que não corresponde
  [Theory]
  [InlineData("abcdefghijklmnopqrstuvwxyz")]
  [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
  [InlineData("0123456789")]
  [InlineData("!@#$%¨&*()-_+=´`~^'\"[{]]<,>.:;?/\\")]
  [InlineData("aA0!")]
  [InlineData("")]
  [InlineData(null)]
  public void IsPassword_ShouldAddNotification_WhenValueNotIsPassword_WithMessage(string? valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var validation = new Validation();
    string value = valueParam!;

    // Act
    validation.IsPassword(value, property, "Custom message");

    // Assert
    Assert.Single(validation.Notifications);
    Assert.Equal(property, validation.Notifications.First().Key);
  }

  // Teste para validar se o valor corresponde ao padrão e não cria notificação, com valor que corresponde
  [Theory]
  [InlineData("aA0!@#$%¨&*()-_+=´`~^'\"[{]]<,>.:;?/\\")]
  [InlineData("aA0123456789!")]
  [InlineData("aABCDEFGHIJKLMNOPQRSTUVWXYZ0!")]
  [InlineData("abcdefghijklmnopqrstuvwxyzA0!")]
  [InlineData("abAB01!@")]
  public void IsPassword_ShouldNotAddNotification_WhenValueIsPassword(string valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var validation = new Validation();
    string value = valueParam;

    // Act
    validation.IsPassword(value, property);

    // Assert
    Assert.Empty(validation.Notifications);
  }

  // Teste para validar se o valor corresponde ao padrão com parâmetro de tamanho e cria notificação, com valor que não corresponde
  [Theory]
  [InlineData("aA012!", 6)]
  [InlineData("aA01234!", 10)]
  public void IsPassword_ShouldAddNotification_WhenValueNotIsPassword_WithLength(string? valueParam, int length)
  {
    // Arrange
    var property = "TestProperty";
    var validation = new Validation();
    string value = valueParam!;

    // Act
    validation.IsPassword(value, length, property);

    // Assert
    Assert.Single(validation.Notifications);
    Assert.Equal(property, validation.Notifications.First().Key);
  }

  // Teste para validar se o valor corresponde ao padrão com parâmetro de tamanho, com mensagem e cria notificação, com valor que não corresponde
  [Theory]
  [InlineData("aA012!", 6)]
  [InlineData("aA01234!", 10)]
  public void IsPassword_ShouldAddNotification_WhenValueNotIsPassword_WithLengthAndMessage(string? valueParam, int length)
  {
    // Arrange
    var property = "TestProperty";
    var validation = new Validation();
    string value = valueParam!;

    // Act
    validation.IsPassword(value, length, property, "Custom message");

    // Assert
    Assert.Single(validation.Notifications);
    Assert.Equal(property, validation.Notifications.First().Key);
  }

  // Teste para validar se o valor corresponde ao padrão com parâmetro de tamanho e não cria notificação, com valor que corresponde
  [Theory]
  [InlineData("aA0123456789!", 6)]
  [InlineData("aA0123456789!", 10)]
  public void IsPassword_ShouldNotAddNotification_WhenValueIsPassword_WithLength(string valueParam, int length)
  {
    // Arrange
    var property = "TestProperty";
    var validation = new Validation();
    string value = valueParam;

    // Act
    validation.IsPassword(value, length, property);

    // Assert
    Assert.Empty(validation.Notifications);
  }

  // Teste para validar se o valor corresponde ao padrão e cria notificação, com valor que não corresponde
  [Theory]
  [InlineData("pt-br")]
  [InlineData("PT-br")]
  [InlineData("PT-BR")]
  [InlineData("12-12")]
  [InlineData("ptbr")]
  [InlineData("ptBR")]
  [InlineData("PTBR")]
  [InlineData("pt")]
  [InlineData("PT")]
  [InlineData("")]
  [InlineData(null)]
  public void IsCulture_ShouldAddNotification_WhenValueNotIsCulture(string? valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var validation = new Validation();
    string value = valueParam!;

    // Act
    validation.IsCulture(value, property);

    // Assert
    Assert.Single(validation.Notifications);
    Assert.Equal(property, validation.Notifications.First().Key);
  }

  // Teste para validar se o valor corresponde ao padrão e não cria notificação, com valor que corresponde
  [Theory]
  [InlineData("aa-AA")]
  [InlineData("pt-BR")]
  public void IsCulture_ShouldNotAddNotification_WhenValueIsCulture(string valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var validation = new Validation();
    string value = valueParam;

    // Act
    validation.IsCulture(value, property);

    // Assert
    Assert.Empty(validation.Notifications);
  }

  // Teste para validar se o valor corresponde ao padrão e cria notificação, com valor que não corresponde
  [Theory]
  [InlineData("12-12")]
  [InlineData("ptbr")]
  [InlineData("ptBR")]
  [InlineData("PTBR")]
  [InlineData("pt")]
  [InlineData("PT")]
  [InlineData("")]
  [InlineData(null)]
  public void IsCultureIgnoreCase_ShouldAddNotification_WhenValueNotIsCultureIgnoreCase(string? valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var validation = new Validation();
    string value = valueParam!;

    // Act
    validation.IsCultureIgnoreCase(value, property);

    // Assert
    Assert.Single(validation.Notifications);
    Assert.Equal(property, validation.Notifications.First().Key);
  }

  // Teste para validar se o valor corresponde ao padrão e não cria notificação, com valor que corresponde
  [Theory]
  [InlineData("aa-AA")]
  [InlineData("pt-BR")]
  [InlineData("pt-br")]
  [InlineData("PT-br")]
  [InlineData("PT-BR")]
  public void IsCultureIgnoreCase_ShouldNotAddNotification_WhenValueIsCultureIgnoreCase(string valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var validation = new Validation();
    string value = valueParam;

    // Act
    validation.IsCultureIgnoreCase(value, property);

    // Assert
    Assert.Empty(validation.Notifications);
  }
}
