using Tooark.Validations;

namespace Tooark.Tests.Validations;

public class ListValidationContractTests
{
  // Lista com valor único para testes
  private readonly int[] _singleValue = [0];

  // Lista com dois valores para testes
  private readonly int[] _doubleValue = [0, 1];

  // Lista com múltiplos valores para testes
  private readonly int[] _multiValue = [0, 1, 2, 3];


  // Teste para validar se o tamanho da lista é maior que o comparador e criar uma notificação, com comparador maior ou igual
  [Theory]
  [InlineData(1)]
  [InlineData(2)]
  public void IsGreater_ShouldAddNotification_WhenValueIsNotGreater(int comparer)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    int[] list = _singleValue;

    // Act
    contract.IsGreater(list, comparer, property);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o tamanho da lista é maior que o comparador e não criar uma notificação, com comparador menor
  [Theory]
  [InlineData(-1)]
  [InlineData(0)]
  public void IsGreater_ShouldNotAddNotification_WhenValueIsGreater(int comparer)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    int[] list = _singleValue;

    // Act
    contract.IsGreater(list, comparer, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }

  // Teste para validar se o tamanho da lista é maior ou igual que o comparador e criar uma notificação, com comparador maior
  [Theory]
  [InlineData(2)]
  [InlineData(3)]
  public void IsGreaterOrEquals_ShouldAddNotification_WhenValueIsNotGreaterOrEquals(int comparer)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    int[] list = _singleValue;

    // Act
    contract.IsGreaterOrEquals(list, comparer, property);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o tamanho da lista é maior ou igual que o comparador e não criar uma notificação, com comparador menor ou igual
  [Theory]
  [InlineData(0)]
  [InlineData(1)]
  public void IsGreaterOrEquals_ShouldNotAddNotification_WhenValueIsGreaterOrEquals(int comparer)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    int[] list = _singleValue;

    // Act
    contract.IsGreaterOrEquals(list, comparer, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }

  // Teste para validar se o tamanho da lista é menor que o comparador e criar uma notificação, com comparador menor ou igual
  [Theory]
  [InlineData(0)]
  [InlineData(1)]
  public void IsLower_ShouldAddNotification_WhenValueIsNotGreater(int comparer)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    int[] list = _singleValue;

    // Act
    contract.IsLower(list, comparer, property);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o tamanho da lista é menor que o comparador e não criar uma notificação, com comparador maior
  [Theory]
  [InlineData(2)]
  [InlineData(3)]
  public void IsLower_ShouldNotAddNotification_WhenValueIsLower(int comparer)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    int[] list = _singleValue;

    // Act
    contract.IsLower(list, comparer, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }

  // Teste para validar se o tamanho da lista é menor ou igual que o comparador e criar uma notificação, com comparador menor
  [Theory]
  [InlineData(-1)]
  [InlineData(0)]
  public void IsLowerOrEquals_ShouldAddNotification_WhenValueIsNotLowerOrEquals(int comparer)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    int[] list = _singleValue;

    // Act
    contract.IsLowerOrEquals(list, comparer, property);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o tamanho da lista é menor ou igual que o comparador e não criar uma notificação, com comparador maior ou igual
  [Theory]
  [InlineData(1)]
  [InlineData(2)]
  public void IsLowerOrEquals_ShouldNotAddNotification_WhenValueIsLowerOrEquals(int comparer)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    int[] list = _singleValue;

    // Act
    contract.IsLowerOrEquals(list, comparer, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }

  // Teste para validar se a lista é igual a lista comparadora e cria notificação, com listas diferentes
  [Fact]
  public void AreEquals_ShouldAddNotification_WhenValueIsNotEqual()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    
    // Act
    contract
      .AreEquals(_doubleValue, _singleValue, property)
      .AreEquals(_doubleValue, _doubleValue, property)
      .AreEquals(_doubleValue, _multiValue, property);

    // Assert
    Assert.Equal(2, contract.Notifications.Count);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se a lista é igual a lista comparadora e não cria notificação, com listas iguais
  [Fact]
  public void AreEquals_ShouldNotAddNotification_WhenValueIsEqual()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    
    // Act
    contract
      .AreEquals(_singleValue, _singleValue, property)
      .AreEquals(_doubleValue, _doubleValue, property)
      .AreEquals(_multiValue, _multiValue, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }

  // Teste para validar se a lista não é igual a lista comparadora e cria notificação, com listas iguais
  [Fact]
  public void AreNotEquals_ShouldAddNotification_WhenValueIsEqual()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    
    // Act
    contract
      .AreNotEquals(_singleValue, _singleValue, property)
      .AreNotEquals(_doubleValue, _doubleValue, property)
      .AreNotEquals(_multiValue, _multiValue, property);

    // Assert
    Assert.Equal(3, contract.Notifications.Count);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se a lista não é igual a lista comparadora e cria notificação, com listas diferentes
  [Fact]
  public void AreNotEquals_ShouldNotAddNotification_WhenValueIsNotEqual()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    
    // Act
    contract
      .AreNotEquals(_doubleValue, _singleValue, property)
      .AreNotEquals(_multiValue, _doubleValue, property)
      .AreNotEquals(_singleValue, _multiValue, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }

  // Teste para validar se a lista é nula e cria notificação, com valor não nula
  [Fact]
  public void IsNull_ShouldAddNotification_WhenIsNotNull()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    int[]? list = _singleValue;

    // Act
    contract.IsNull(list, property);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se a lista é nula e não cria notificação, com valor nula
  [Fact]
  public void IsNull_ShouldNotAddNotification_WhenIsNull()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    int[]? list = null;

    // Act
    contract.IsNull(list, property);

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
    int[]? list = null;

    // Act
    contract.IsNotNull(list, property);

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
    int[]? list = _singleValue;

    // Act
    contract.IsNotNull(list, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }

  // Teste para validar se a lista é nula e cria notificação, com valor não nula
  [Fact]
  public void IsEmpty_ShouldAddNotification_WhenIsNotEmpty()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    int[]? list = _singleValue;

    // Act
    contract.IsEmpty(list, property);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se a lista é nula e não cria notificação, com valor nula
  [Fact]
  public void IsEmpty_ShouldNotAddNotification_WhenIsEmpty()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    int[]? list = [];

    // Act
    contract.IsEmpty(list, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }

  // Teste para validar se o valor não é nulo e cria notificação, com valor nulo
  [Fact]
  public void IsNotEmpty_ShouldAddNotification_WhenIsEmpty()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    int[]? list = [];

    // Act
    contract.IsNotEmpty(list, property);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o valor não é nulo e não cria notificação, com valor não nulo
  [Fact]
  public void IsNotEmpty_ShouldNotAddNotification_WhenIsNotEmpty()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    int[]? list = _singleValue;

    // Act
    contract.IsNotEmpty(list, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }
}
