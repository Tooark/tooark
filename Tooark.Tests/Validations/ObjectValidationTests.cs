using Tooark.Tests.Moq.Entities.Person;
using Tooark.Validations;

namespace Tooark.Tests.Validations;

public class ObjectValidationTests
{
  // Teste para validar se o valor é igual ao comparador e cria notificação, com valores diferentes
  [Fact]
  public void AreEquals_ShouldAddNotification_WhenValueIsNotEqual()
  {
    // Arrange
    var property = "TestProperty";
    var validation = new Validation();
    object value = new Person("Test", 0);
    object comparer = new Person("Test", 1);

    // Act
    validation.AreEquals(value, comparer, property);

    // Assert
    Assert.Single(validation.Notifications);
    Assert.Equal(property, validation.Notifications.First().Key);
  }

  // Teste para validar se o valor é igual ao comparador e não cria notificação, com valores iguais
  [Fact]
  public void AreEquals_ShouldNotAddNotification_WhenValueIsEqual()
  {
    // Arrange
    var property = "TestProperty";
    var validation = new Validation();
    object value = new Person("Test", 0);
    object comparer = value;

    // Act
    validation.AreEquals(value, comparer, property);

    // Assert
    Assert.Empty(validation.Notifications);
  }

  // Teste para validar se o valor é não igual ao comparador e cria notificação, com valores iguais
  [Fact]
  public void AreNotEquals_ShouldAddNotification_WhenValueIsEqual()
  {
    // Arrange
    // Arrange
    var property = "TestProperty";
    var validation = new Validation();
    object value = new Person("Test", 0);
    object comparer = value;

    // Act
    validation.AreNotEquals(value, comparer, property);

    // Assert
    Assert.Single(validation.Notifications);
    Assert.Equal(property, validation.Notifications.First().Key);
  }

  // Teste para validar se o valor não é igual ao comparador e não cria notificação, com valores diferentes
  [Fact]
  public void AreNotEquals_ShouldNotAddNotification_WhenValueIsNotEqual()
  {
    // Arrange
    var property = "TestProperty";
    var validation = new Validation();
    object value = new Person("Test", 0);
    object comparer = new Person("Test", 1);

    // Act
    validation.AreNotEquals(value, comparer, property);

    // Assert
    Assert.Empty(validation.Notifications);
  }

  // Teste para validar se o valor é nulo e cria notificação, com valor não nulo
  [Fact]
  public void IsNull_ShouldAddNotification_WhenIsNotNull()
  {
    // Arrange
    var property = "TestProperty";
    var validation = new Validation();
    object? value = new Person("Test", 0);

    // Act
    validation.IsNull(value, property);

    // Assert
    Assert.Single(validation.Notifications);
    Assert.Equal(property, validation.Notifications.First().Key);
  }

  // Teste para validar se o valor é nulo e não cria notificação, com valor nulo
  [Fact]
  public void IsNull_ShouldNotAddNotification_WhenIsNull()
  {
    // Arrange
    var property = "TestProperty";
    var validation = new Validation();
    object? value = null;

    // Act
    validation.IsNull(value, property);

    // Assert
    Assert.Empty(validation.Notifications);
  }

  // Teste para validar se o valor não é nulo e cria notificação, com valor nulo
  [Fact]
  public void IsNotNull_ShouldAddNotification_WhenIsNull()
  {
    // Arrange
    var property = "TestProperty";
    var validation = new Validation();
    object? value = null;

    // Act
    validation.IsNotNull(value, property);

    // Assert
    Assert.Single(validation.Notifications);
    Assert.Equal(property, validation.Notifications.First().Key);
  }

  // Teste para validar se o valor não é nulo e não cria notificação, com valor não nulo
  [Fact]
  public void IsNotNull_ShouldNotAddNotification_WhenIsNotNull()
  {
    // Arrange
    var property = "TestProperty";
    var validation = new Validation();
    object? value = new Person("Test", 0);

    // Act
    validation.IsNotNull(value, property);

    // Assert
    Assert.Empty(validation.Notifications);
  }
}
