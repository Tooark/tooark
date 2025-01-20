using Tooark.Validations;

namespace Tooark.Tests.Validations;

public class GuidValidationContractTests
{
  // Teste para validar se o valor é igual ao comparador e cria notificação, com valores diferentes
  [Fact]
  public void AreEquals_ShouldAddNotification_WhenValueIsNotEqual()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    Guid value = Guid.NewGuid();
    Guid comparer = Guid.NewGuid();

    // Act
    contract.AreEquals(value, comparer, property);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o valor é igual ao comparador e não cria notificação, com valores iguais
  [Fact]
  public void AreEquals_ShouldNotAddNotification_WhenValueIsEqual()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    Guid value = Guid.NewGuid();
    Guid comparer = value;

    // Act
    contract.AreEquals(value, comparer, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }

  // Teste para validar se o valor é não igual ao comparador e cria notificação, com valores iguais
  [Fact]
  public void AreNotEquals_ShouldAddNotification_WhenValueIsEqual()
  {
    // Arrange
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    Guid value = Guid.NewGuid();
    Guid comparer = value;

    // Act
    contract.AreNotEquals(value, comparer, property);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o valor não é igual ao comparador e não cria notificação, com valores diferentes
  [Fact]
  public void AreNotEquals_ShouldNotAddNotification_WhenValueIsNotEqual()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    Guid value = Guid.NewGuid();
    Guid comparer = Guid.NewGuid();

    // Act
    contract.AreNotEquals(value, comparer, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }

  // Teste para validar se o valor contém na lista e cria notificação, com valor não contido
  [Fact]
  public void Contains_ShouldAddNotification_WhenValueIsNotInList()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    Guid value = Guid.NewGuid();
    Guid[] list = [Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid()];

    // Act
    contract.Contains(value, list, property);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o valor contém na lista e não cria notificação, com valor contido
  [Fact]
  public void Contains_ShouldNotAddNotification_WhenValueIsInList()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    Guid value = Guid.NewGuid();
    Guid[] list = [Guid.NewGuid(), Guid.NewGuid(), value];

    // Act
    contract.Contains(value, list, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }

  // Teste para validar se o valor não contém na lista e cria notificação, com valor contido
  [Fact]
  public void NotContains_ShouldAddNotification_WhenValueIsInList()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    Guid value = Guid.NewGuid();
    Guid[] list = [Guid.NewGuid(), Guid.NewGuid(), value];

    // Act
    contract.NotContains(value, list, property);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o valor não contém na lista e não cria notificação, com valor não contido
  [Fact]
  public void NotContains_ShouldNotAddNotification_WhenValueIsNotInList()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    Guid value = Guid.NewGuid();
    Guid[] list = [Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid()];

    // Act
    contract.NotContains(value, list, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }

  // Teste para validar se todos valores da lista é igual o valor e cria notificação, algum valor diferente
  [Fact]
  public void All_ShouldAddNotification_WhenNotAllValueInList()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    Guid value = Guid.NewGuid();
    Guid[] list = [Guid.NewGuid(), value, value];

    // Act
    contract.All(value, list, property);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se todos valores da lista é igual o valor e cria notificação, todos valores são iguais
  [Fact]
  public void All_ShouldNotAddNotification_WhenAllValueInList()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    Guid value = Guid.NewGuid();
    Guid[] list = [value, value, value];

    // Act
    contract.All(value, list, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }

  // Teste para validar se nem todos valores da lista é igual o valor e cria notificação, todos valores são iguais
  [Fact]
  public void NotAll_ShouldAddNotification_WhenNotAllValueInLis()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    Guid value = Guid.NewGuid();
    Guid[] list = [value, value, value];

    // Act
    contract.NotAll(value, list, property);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se nem todos valores da lista é igual o valor e cria notificação, algum valor diferente
  [Fact]
  public void NotAll_ShouldNotAddNotification_WhenAllValueInList()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    Guid value = Guid.NewGuid();
    Guid[] list = [Guid.NewGuid(), value, value];

    // Act
    contract.NotAll(value, list, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }

  // Teste para validar se o valor é nulo e cria notificação, com valor não nulo
  [Fact]
  public void IsNull_ShouldAddNotification_WhenIsNotNull()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    Guid? value = Guid.NewGuid();

    // Act
    contract.IsNull(value, property);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o valor é nulo e não cria notificação, com valor nulo
  [Fact]
  public void IsNull_ShouldNotAddNotification_WhenIsNull()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    Guid? value = null;

    // Act
    contract.IsNull(value, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }

  // Teste para validar se o valor não é nulo e cria notificação, com valor nulo
  [Fact]
  public void IsNotNull_ShouldAddNotification_WhenIsNull()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    Guid? value = null;

    // Act
    contract.IsNotNull(value, property);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o valor não é nulo e não cria notificação, com valor não nulo
  [Fact]
  public void IsNotNull_ShouldNotAddNotification_WhenIsNotNull()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    Guid? value = Guid.NewGuid();

    // Act
    contract.IsNotNull(value, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }

  // Teste para validar se o valor é empty e cria notificação, com valor não empty
  [Fact]
  public void IsEmpty_ShouldAddNotification_WhenIsNotEmpty()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    Guid value = Guid.NewGuid();

    // Act
    contract.IsEmpty(value, property);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o valor é empty e não cria notificação, com valor empty
  [Fact]
  public void IsEmpty_ShouldNotAddNotification_WhenIsEmpty()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    Guid value = Guid.Empty;

    // Act
    contract.IsEmpty(value, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }

  // Teste para validar se o valor não é empty e cria notificação, com valor empty
  [Fact]
  public void IsNotEmpty_ShouldAddNotification_WhenIsEmpty()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    Guid value = Guid.Empty;

    // Act
    contract.IsNotEmpty(value, property);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o valor não é empty e não cria notificação, com valor não empty
  [Fact]
  public void IsNotEmpty_ShouldNotAddNotification_WhenIsNotEmpty()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    Guid value = Guid.NewGuid();

    // Act
    contract.IsNotEmpty(value, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }
}
