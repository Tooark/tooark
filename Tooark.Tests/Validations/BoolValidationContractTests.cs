using Tooark.Validations;

namespace Tooark.Tests.Validations;

public class BoolValidationContractTests
{
  // Teste para validar se o método IsFalse está adicionando uma notificação quando o valor é verdadeiro
  [Fact]
  public void IsFalse_ShouldAddNotification_WhenValueIsTrue()
  {
    // Arrange
    var property = "TooarkProperty";
    var contract = new Contract();

    // Act
    contract.IsFalse(true, property);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o método IsFalse não está adicionando uma notificação quando o valor é falso
  [Fact]
  public void IsFalse_ShouldNotAddNotification_WhenValueIsFalse()
  {
    // Arrange
    var property = "TooarkProperty";
    var contract = new Contract();

    // Act
    contract.IsFalse(false, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }

  // Teste para validar se o método IsTrue está adicionando uma notificação quando o valor é falso
  [Fact]
  public void IsTrue_ShouldAddNotification_WhenValueIsFalse()
  {
    // Arrange
    var property = "TooarkProperty";
    var contract = new Contract();

    // Act
    contract.IsTrue(false, property);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o método IsTrue não está adicionando uma notificação quando o valor é verdadeiro
  [Fact]
  public void IsTrue_ShouldNotAddNotification_WhenValueIsTrue()
  {
    // Arrange
    var property = "TooarkProperty";
    var contract = new Contract();

    // Act
    contract.IsTrue(true, property);

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
    bool value = true;
    bool[] listBoolean = [false, false, false];

    // Act
    contract.Contains(value, listBoolean, property);

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
    bool value = true;
    bool[] listBoolean = [true, false, false];

    // Act
    contract.Contains(value, listBoolean, property);

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
    bool value = true;
    bool[] listBoolean = [true, false, false];

    // Act
    contract.NotContains(value, listBoolean, property);

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
    bool value = true;
    bool[] listBoolean = [false, false, false];

    // Act
    contract.NotContains(value, listBoolean, property);

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
    bool value = true;
    bool[] listBoolean = [false, true, true];

    // Act
    contract.All(value, listBoolean, property);

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
    bool value = true;
    bool[] listBoolean = [true, true, true];

    // Act
    contract.All(value, listBoolean, property);

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
    bool value = true;
    bool[] listBoolean = [true, true, true];

    // Act
    contract.NotAll(value, listBoolean, property);

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
    bool value = true;
    bool[] listBoolean = [false, true, true];

    // Act
    contract.NotAll(value, listBoolean, property);

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
    bool? value = true;

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
    bool? value = null;

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
    bool? value = null;

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
    bool? value = true;

    // Act
    contract.IsNotNull(value, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }
}
