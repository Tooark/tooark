using Tooark.Validations;

namespace Tooark.Tests.Validations;

public class GuidValidationTests
{
  // Teste para validar se o valor é igual ao comparador e cria notificação, com valores diferentes
  [Fact]
  public void AreEquals_ShouldAddNotification_WhenValueIsNotEqual()
  {
    // Arrange
    var property = "TestProperty";
    var validation = new Validation();
    Guid value = Guid.NewGuid();
    Guid comparer = Guid.NewGuid();

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
    Guid value = Guid.NewGuid();
    Guid comparer = value;

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
    Guid value = Guid.NewGuid();
    Guid comparer = value;

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
    Guid value = Guid.NewGuid();
    Guid comparer = Guid.NewGuid();

    // Act
    validation.AreNotEquals(value, comparer, property);

    // Assert
    Assert.Empty(validation.Notifications);
  }

  // Teste para validar se o valor contém na lista e cria notificação, com valor não contido
  [Fact]
  public void Contains_ShouldAddNotification_WhenValueIsNotInList()
  {
    // Arrange
    var property = "TestProperty";
    var validation = new Validation();
    Guid value = Guid.NewGuid();
    Guid[] list = [Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid()];

    // Act
    validation.Contains(value, list, property);

    // Assert
    Assert.Single(validation.Notifications);
    Assert.Equal(property, validation.Notifications.First().Key);
  }

  // Teste para validar se o valor contém na lista e não cria notificação, com valor contido
  [Fact]
  public void Contains_ShouldNotAddNotification_WhenValueIsInList()
  {
    // Arrange
    var property = "TestProperty";
    var validation = new Validation();
    Guid value = Guid.NewGuid();
    Guid[] list = [Guid.NewGuid(), Guid.NewGuid(), value];

    // Act
    validation.Contains(value, list, property);

    // Assert
    Assert.Empty(validation.Notifications);
  }

  // Teste para validar se o valor não contém na lista e cria notificação, com valor contido
  [Fact]
  public void NotContains_ShouldAddNotification_WhenValueIsInList()
  {
    // Arrange
    var property = "TestProperty";
    var validation = new Validation();
    Guid value = Guid.NewGuid();
    Guid[] list = [Guid.NewGuid(), Guid.NewGuid(), value];

    // Act
    validation.NotContains(value, list, property);

    // Assert
    Assert.Single(validation.Notifications);
    Assert.Equal(property, validation.Notifications.First().Key);
  }

  // Teste para validar se o valor não contém na lista e não cria notificação, com valor não contido
  [Fact]
  public void NotContains_ShouldNotAddNotification_WhenValueIsNotInList()
  {
    // Arrange
    var property = "TestProperty";
    var validation = new Validation();
    Guid value = Guid.NewGuid();
    Guid[] list = [Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid()];

    // Act
    validation.NotContains(value, list, property);

    // Assert
    Assert.Empty(validation.Notifications);
  }

  // Teste para validar se todos valores da lista é igual o valor e cria notificação, algum valor diferente
  [Fact]
  public void All_ShouldAddNotification_WhenNotAllValueInList()
  {
    // Arrange
    var property = "TestProperty";
    var validation = new Validation();
    Guid value = Guid.NewGuid();
    Guid[] list = [Guid.NewGuid(), value, value];

    // Act
    validation.All(value, list, property);

    // Assert
    Assert.Single(validation.Notifications);
    Assert.Equal(property, validation.Notifications.First().Key);
  }

  // Teste para validar se todos valores da lista é igual o valor e cria notificação, todos valores são iguais
  [Fact]
  public void All_ShouldNotAddNotification_WhenAllValueInList()
  {
    // Arrange
    var property = "TestProperty";
    var validation = new Validation();
    Guid value = Guid.NewGuid();
    Guid[] list = [value, value, value];

    // Act
    validation.All(value, list, property);

    // Assert
    Assert.Empty(validation.Notifications);
  }

  // Teste para validar se nem todos valores da lista é igual o valor e cria notificação, todos valores são iguais
  [Fact]
  public void NotAll_ShouldAddNotification_WhenNotAllValueInLis()
  {
    // Arrange
    var property = "TestProperty";
    var validation = new Validation();
    Guid value = Guid.NewGuid();
    Guid[] list = [value, value, value];

    // Act
    validation.NotAll(value, list, property);

    // Assert
    Assert.Single(validation.Notifications);
    Assert.Equal(property, validation.Notifications.First().Key);
  }

  // Teste para validar se nem todos valores da lista é igual o valor e cria notificação, algum valor diferente
  [Fact]
  public void NotAll_ShouldNotAddNotification_WhenAllValueInList()
  {
    // Arrange
    var property = "TestProperty";
    var validation = new Validation();
    Guid value = Guid.NewGuid();
    Guid[] list = [Guid.NewGuid(), value, value];

    // Act
    validation.NotAll(value, list, property);

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
    Guid? value = Guid.NewGuid();

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
    Guid? value = null;

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
    Guid? value = null;

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
    Guid? value = Guid.NewGuid();

    // Act
    validation.IsNotNull(value, property);

    // Assert
    Assert.Empty(validation.Notifications);
  }

  // Teste para validar se o valor é empty e cria notificação, com valor não empty
  [Fact]
  public void IsEmpty_ShouldAddNotification_WhenIsNotEmpty()
  {
    // Arrange
    var property = "TestProperty";
    var validation = new Validation();
    Guid value = Guid.NewGuid();

    // Act
    validation.IsEmpty(value, property);

    // Assert
    Assert.Single(validation.Notifications);
    Assert.Equal(property, validation.Notifications.First().Key);
  }

  // Teste para validar se o valor é empty e não cria notificação, com valor empty
  [Fact]
  public void IsEmpty_ShouldNotAddNotification_WhenIsEmpty()
  {
    // Arrange
    var property = "TestProperty";
    var validation = new Validation();
    Guid value = Guid.Empty;

    // Act
    validation.IsEmpty(value, property);

    // Assert
    Assert.Empty(validation.Notifications);
  }

  // Teste para validar se o valor não é empty e cria notificação, com valor empty
  [Fact]
  public void IsNotEmpty_ShouldAddNotification_WhenIsEmpty()
  {
    // Arrange
    var property = "TestProperty";
    var validation = new Validation();
    Guid value = Guid.Empty;

    // Act
    validation.IsNotEmpty(value, property);

    // Assert
    Assert.Single(validation.Notifications);
    Assert.Equal(property, validation.Notifications.First().Key);
  }

  // Teste para validar se o valor não é empty e não cria notificação, com valor não empty
  [Fact]
  public void IsNotEmpty_ShouldNotAddNotification_WhenIsNotEmpty()
  {
    // Arrange
    var property = "TestProperty";
    var validation = new Validation();
    Guid value = Guid.NewGuid();

    // Act
    validation.IsNotEmpty(value, property);

    // Assert
    Assert.Empty(validation.Notifications);
  }
}
