using System.Text.RegularExpressions;
using Tooark.Validations;

namespace Tooark.Tests.Validations;

public class RegexValidationContractTest
{
  // Teste para validar se o valor corresponde ao padrão e cria notificação, com valor que não corresponde
  [Fact]
  public void Match_ShouldAddNotification_WhenValueNotMatchPattern()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = "abc";
    string pattern = @"^[0-9]*$";

    // Act
    contract.Match(value, pattern, property);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o valor corresponde ao padrão e não cria notificação, com valor que corresponde
  [Fact]
  public void Match_ShouldNotAddNotification_WhenValueMatchPattern()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = "abc";
    string pattern = @"^[a-z]*$";

    // Act
    contract.Match(value, pattern, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }

  // Teste para validar se o valor não corresponde ao padrão e cria notificação, com valor que corresponde
  [Fact]
  public void NotMatch_ShouldAddNotification_WhenValueMatchPattern()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = "abc";
    string pattern = @"^[a-z]*$";

    // Act
    contract.NotMatch(value, pattern, property);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o valor não corresponde ao padrão e não cria notificação, com valor que não corresponde
  [Fact]
  public void NotMatch_ShouldNotAddNotification_WhenValueNotMatchPattern()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = "abc";
    string pattern = @"^[0-9]*$";

    // Act
    contract.NotMatch(value, pattern, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }  

  // Teste para validar se o valor corresponde ao padrão e cria notificação, com valor que não corresponde
  [Fact]
  public void Match_ShouldAddNotification_WhenValueNotMatchPattern_WithCaseSensitive()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = "abc";
    string pattern = @"^[0-9]*$";
    RegexOptions options = RegexOptions.IgnoreCase;

    // Act
    contract.Match(value, pattern, property, options);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o valor corresponde ao padrão e não cria notificação, com valor que corresponde
  [Fact]
  public void Match_ShouldNotAddNotification_WhenValueMatchPattern_WithCaseSensitive()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string valueLower = "abc";
    string valueUpper = "ABC";
    string pattern = @"^[a-z]*$";
    RegexOptions options = RegexOptions.IgnoreCase;

    // Act
    contract
      .Match(valueLower, pattern, property, options)
      .Match(valueUpper, pattern, property, options);

    // Assert
    Assert.Empty(contract.Notifications);
  }

  // Teste para validar se o valor não corresponde ao padrão e cria notificação, com valor que corresponde
  [Fact]
  public void NotMatch_ShouldAddNotification_WhenValueMatchPattern_WithCaseSensitive()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string valueLower = "abc";
    string valueUpper = "ABC";
    string pattern = @"^[a-z]*$";
    RegexOptions options = RegexOptions.IgnoreCase;

    // Act
    contract
      .NotMatch(valueLower, pattern, property, options)
      .NotMatch(valueUpper, pattern, property, options);

    // Assert
    Assert.Equal(2, contract.Notifications.Count);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o valor não corresponde ao padrão e não cria notificação, com valor que não corresponde
  [Fact]
  public void NotMatch_ShouldNotAddNotification_WhenValueNotMatchPattern_WithCaseSensitive()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = "abc";
    string pattern = @"^[0-9]*$";
    RegexOptions options = RegexOptions.IgnoreCase;

    // Act
    contract.NotMatch(value, pattern, property, options);

    // Assert
    Assert.Empty(contract.Notifications);
  }  
}
