using Tooark.Validations;

namespace Tooark.Tests.Validations;

public class StringValidationContractTests
{
  // Teste para validar se o tamanho do valor é maior que o comparador e criar uma notificação, com comparador maior ou igual
  [Theory]
  [InlineData("Test Value", 10)]
  [InlineData("Test Value", 11)]
  public void IsGreater_ShouldAddNotification_WhenValueIsNotGreater(string value, int comparer)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();

    // Act
    contract.IsGreater(value, comparer, property);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o tamanho do valor é maior que o comparador e não criar uma notificação, com comparador menor
  [Theory]
  [InlineData("Test Value", 8)]
  [InlineData("Test Value", 9)]
  public void IsGreater_ShouldNotAddNotification_WhenValueIsGreater(string value, int comparer)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();

    // Act
    contract.IsGreater(value, comparer, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }

  // Teste para validar se o tamanho do valor é maior ou igual que o comparador e criar uma notificação, com comparador maior
  [Theory]
  [InlineData("Test Value", 11)]
  [InlineData("Test Value", 12)]
  public void IsGreaterOrEquals_ShouldAddNotification_WhenValueIsNotGreaterOrEquals(string value, int comparer)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();

    // Act
    contract.IsGreaterOrEquals(value, comparer, property);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o tamanho do valor é maior ou igual que o comparador e não criar uma notificação, com comparador menor ou igual
  [Theory]
  [InlineData("Test Value", 9)]
  [InlineData("Test Value", 10)]
  public void IsGreaterOrEquals_ShouldNotAddNotification_WhenValueIsGreaterOrEquals(string value, int comparer)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();

    // Act
    contract.IsGreaterOrEquals(value, comparer, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }

  // Teste para validar se o tamanho do valor é menor que o comparador e criar uma notificação, com comparador menor ou igual
  [Theory]
  [InlineData("Test Value", 9)]
  [InlineData("Test Value", 10)]
  public void IsLower_ShouldAddNotification_WhenValueIsNotGreater(string value, int comparer)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();

    // Act
    contract.IsLower(value, comparer, property);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o tamanho do valor é menor que o comparador e não criar uma notificação, com comparador maior
  [Theory]
  [InlineData("Test Value", 11)]
  [InlineData("Test Value", 12)]
  public void IsLower_ShouldNotAddNotification_WhenValueIsLower(string value, int comparer)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();

    // Act
    contract.IsLower(value, comparer, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }

  // Teste para validar se o tamanho do valor é menor ou igual que o comparador e criar uma notificação, com comparador menor
  [Theory]
  [InlineData("Test Value", 8)]
  [InlineData("Test Value", 9)]
  public void IsLowerOrEquals_ShouldAddNotification_WhenValueIsNotLowerOrEquals(string value, int comparer)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();

    // Act
    contract.IsLowerOrEquals(value, comparer, property);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o tamanho do valor é menor ou igual que o comparador e não criar uma notificação, com comparador maior ou igual
  [Theory]
  [InlineData("Test Value", 10)]
  [InlineData("Test Value", 11)]
  public void IsLowerOrEquals_ShouldNotAddNotification_WhenValueIsLowerOrEquals(string value, int comparer)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();

    // Act
    contract.IsLowerOrEquals(value, comparer, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }

  // Teste para validar se o tamanho do valor está entre o início e o fim e cria notificação, com valor menor e maior
  [Theory]
  [InlineData("Test Value", 5, 6)]
  [InlineData("Test Value", 15, 16)]
  public void IsBetween_ShouldAddNotification_WhenValueIsNotBetweenStartAndEnd(string value, int start, int end)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();

    // Act
    contract.IsBetween(value, start, end, property);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o tamanho do valor está entre o início e o fim e cria notificação, com valor menor e maior
  [Theory]
  [InlineData("Test Value", 5, 10)]
  [InlineData("Test Value", 10, 16)]
  public void IsBetween_ShouldNotAddNotification_WhenValueIsBetweenStartAndEnd(string value, int start, int end)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();

    // Act
    contract.IsBetween(value, start, end, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }

  // Teste para validar se o tamanho do valor não está entre o início e o fim e cria notificação, com valor entre
  [Theory]
  [InlineData("Test Value", 5, 10)]
  [InlineData("Test Value", 10, 16)]
  public void IsNotBetween_ShouldAddNotification_WhenValueIsBetweenStartAndEnd(string value, int start, int end)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();

    // Act
    contract.IsNotBetween(value, start, end, property);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o tamanho do valor não está entre o início e o fim e não cria notificação, com valor menor e maior
  [Theory]
  [InlineData("Test Value", 5, 6)]
  [InlineData("Test Value", 15, 16)]
  public void IsNotBetween_ShouldNotAddNotification_WhenValueIsNotBetweenStartAndEnd(string value, int start, int end)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();

    // Act
    contract.IsNotBetween(value, start, end, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }

  // Teste para validar se o tamanho do valor é igual ao comparador e cria notificação, com valores diferentes
  [Fact]
  public void AreEquals_ShouldAddNotification_WhenValueIsNotEqualLength()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = "Value";
    int comparer = 0;

    // Act
    contract.AreEquals(value, comparer, property);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o tamanho do valor é igual ao comparador e não cria notificação, com valores iguais
  [Fact]
  public void AreEquals_ShouldNotAddNotification_WhenValueIsEqualLength()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = "Test";
    int comparer = 4;

    // Act
    contract.AreEquals(value, comparer, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }

  // Teste para validar se o valor é igual ao comparador e cria notificação, com valores diferentes
  [Fact]
  public void AreEquals_ShouldAddNotification_WhenValueIsNotEqual()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = "Value";
    string comparer = "Comparer";

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
    string value = "Test";
    string comparer = "Test";

    // Act
    contract.AreEquals(value, comparer, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }

  // Teste para validar se o tamanho do valor é não igual ao comparador e cria notificação, com valores iguais
  [Fact]
  public void AreNotEquals_ShouldAddNotification_WhenValueIsEqualLength()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = "Test";
    int comparer = 4;

    // Act
    contract.AreNotEquals(value, comparer, property);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o tamanho do valor não é igual ao comparador e não cria notificação, com valores diferentes
  [Fact]
  public void AreNotEquals_ShouldNotAddNotification_WhenValueIsNotEqualLength()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = "Value";
    int comparer = 0;

    // Act
    contract.AreNotEquals(value, comparer, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }

  // Teste para validar se o valor é não igual ao comparador e cria notificação, com valores iguais
  [Fact]
  public void AreNotEquals_ShouldAddNotification_WhenValueIsEqual()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = "Test";
    string comparer = "Test";

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
    string value = "Value";
    string comparer = "Comparer";

    // Act
    contract.AreNotEquals(value, comparer, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }

  // Teste para validar se o valor contém no comparador e cria notificação, com valor não contido
  [Fact]
  public void Contains_ShouldAddNotification_WhenValueIsNotInComparer()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = "Test Value";
    string comparer = "Comparer";

    // Act
    contract.Contains(value, comparer, property);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o valor contém no comparador e não cria notificação, com valor contido
  [Fact]
  public void Contains_ShouldNotAddNotification_WhenValueIsInComparer()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = "Test Value";
    string comparer = "Test";

    // Act
    contract.Contains(value, comparer, property);

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
    string value = "Value";
    string[] comparer = ["Test", "Comparer"];

    // Act
    contract.Contains(value, comparer, property);

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
    string value = "Test";
    string[] comparer = ["Test", "Comparer"];

    // Act
    contract.Contains(value, comparer, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }

  // Teste para validar se o valor não contém no comparador e cria notificação, com valor contido
  [Fact]
  public void NotContains_ShouldAddNotification_WhenValueIsInComparer()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = "Test Value";
    string comparer = "Test";

    // Act
    contract.NotContains(value, comparer, property);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o valor não contém no comparador e não cria notificação, com valor não contido
  [Fact]
  public void NotContains_ShouldNotAddNotification_WhenValueIsNotInComparer()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = "Test Value";
    string comparer = "Comparer";

    // Act
    contract.NotContains(value, comparer, property);

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
    string value = "Test";
    string[] comparer = ["Test", "Comparer"];

    // Act
    contract.NotContains(value, comparer, property);

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
    string value = "Test Value";
    string[] comparer = ["Test", "Comparer"];

    // Act
    contract.NotContains(value, comparer, property);

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
    string? value = "Value";

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
    string? value = null;

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
    string? value = null;

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
    string? value = "Value";

    // Act
    contract.IsNotNull(value, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }

  // Teste para validar se o valor é nulo ou vazio e cria notificação, com valor não nulo ou vazio
  
  [Theory]
  [InlineData("Value")]
  [InlineData(" ")]
  public void IsNullOrEmpty_ShouldAddNotification_WhenIsNotNullOrEmpty(string? valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string? value = valueParam;

    // Act
    contract.IsNullOrEmpty(value, property);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o valor é nulo ou vazio e não cria notificação, com valor nulo ou vazio
  [Theory]
  [InlineData("")]
  [InlineData(null)]
  public void IsNullOrEmpty_ShouldNotAddNotification_WhenIsNullOrEmpty(string? valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string? value = valueParam;

    // Act
    contract.IsNullOrEmpty(value, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }

  // Teste para validar se o valor não é nulo ou vazio e cria notificação, com valor nulo ou vazio
  [Theory]
  [InlineData("")]
  [InlineData(null)]
  public void IsNotNullOrEmpty_ShouldAddNotification_WhenIsNullOrEmpty(string? valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string? value = valueParam;

    // Act
    contract.IsNotNullOrEmpty(value, property);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o valor não é nulo ou vazio e não cria notificação, com valor não nulo ou vazio
  [Theory]
  [InlineData("Value")]
  [InlineData(" ")]
  public void IsNotNullOrEmpty_ShouldNotAddNotification_WhenIsNotNullOrEmpty(string? valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string? value = valueParam;

    // Act
    contract.IsNotNullOrEmpty(value, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }

  // Teste para validar se o valor é nulo ou espaço em branco e cria notificação, com valor não nulo ou espaço em branco
  [Theory]
  [InlineData("Value")]
  [InlineData("Test")]
  public void IsNullOrWhiteSpace_ShouldAddNotification_WhenIsNotNullOrWhiteSpace(string? valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string? value = valueParam;

    // Act
    contract.IsNullOrWhiteSpace(value, property);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o valor é nulo ou espaço em branco e não cria notificação, com valor nulo ou espaço em branco
  [Theory]
  [InlineData("")]
  [InlineData(" ")]
  [InlineData(null)]
  public void IsNullOrWhiteSpace_ShouldNotAddNotification_WhenIsNullOrWhiteSpace(string? valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string? value = valueParam;

    // Act
    contract.IsNullOrWhiteSpace(value, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }

  // Teste para validar se o valor não é nulo ou espaço em branco e cria notificação, com valor nulo ou espaço em branco
  [Theory]
  [InlineData("")]
  [InlineData(" ")]
  [InlineData(null)]
  public void IsNotNullOrWhiteSpace_ShouldAddNotification_WhenIsNullOrWhiteSpace(string? valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string? value = valueParam;

    // Act
    contract.IsNotNullOrWhiteSpace(value, property);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o valor não é nulo ou espaço em branco e não cria notificação, com valor não nulo ou espaço em branco
  [Theory]
  [InlineData("Value")]
  [InlineData("Test")]
  public void IsNotNullOrWhiteSpace_ShouldNotAddNotification_WhenIsNotNullOrWhiteSpace(string? valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string? value = valueParam;

    // Act
    contract.IsNotNullOrWhiteSpace(value, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }
}
