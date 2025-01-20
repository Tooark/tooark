using Tooark.Validations;

namespace Tooark.Tests.Validations;

public class FloatValidationContractTests
{
  // Teste para validar se o valor é maior que o comparador e criar uma notificação, com comparador maior ou igual
  [Theory]
  [InlineData(0, 0)]
  [InlineData(0, 1)]
  public void IsGreater_ShouldAddNotification_WhenValueIsNotGreater(float valueParam, float comparer)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    float value = valueParam;
    decimal comparerDecimal = (decimal)comparer;
    double comparerDouble = (double)comparer;
    float comparerFloat = (float)comparer;
    int comparerInt = (int)comparer;
    long comparerLong = (long)comparer;

    // Act
    contract
      .IsGreater(value, comparerDecimal, property)
      .IsGreater(value, comparerDouble, property)
      .IsGreater(value, comparerFloat, property)
      .IsGreater(value, comparerInt, property)
      .IsGreater(value, comparerLong, property);

    // Assert
    Assert.Equal(5, contract.Notifications.Count);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o valor é maior que o comparador e não criar uma notificação, com comparador menor
  [Theory]
  [InlineData(0, -2)]
  [InlineData(0, -1)]
  public void IsGreater_ShouldNotAddNotification_WhenValueIsGreater(float valueParam, float comparer)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    float value = valueParam;
    decimal comparerDecimal = (decimal)comparer;
    double comparerDouble = (double)comparer;
    float comparerFloat = (float)comparer;
    int comparerInt = (int)comparer;
    long comparerLong = (long)comparer;

    // Act
    contract
      .IsGreater(value, comparerDecimal, property)
      .IsGreater(value, comparerDouble, property)
      .IsGreater(value, comparerFloat, property)
      .IsGreater(value, comparerInt, property)
      .IsGreater(value, comparerLong, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }

  // Teste para validar se o valor é maior ou igual que o comparador e criar uma notificação, com comparador maior
  [Theory]
  [InlineData(0, 1)]
  [InlineData(0, 2)]
  public void IsGreaterOrEquals_ShouldAddNotification_WhenValueIsNotGreaterOrEquals(float valueParam, float comparer)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    float value = valueParam;
    decimal comparerDecimal = (decimal)comparer;
    double comparerDouble = (double)comparer;
    float comparerFloat = (float)comparer;
    int comparerInt = (int)comparer;
    long comparerLong = (long)comparer;

    // Act
    contract
      .IsGreaterOrEquals(value, comparerDecimal, property)
      .IsGreaterOrEquals(value, comparerDouble, property)
      .IsGreaterOrEquals(value, comparerFloat, property)
      .IsGreaterOrEquals(value, comparerInt, property)
      .IsGreaterOrEquals(value, comparerLong, property);

    // Assert
    Assert.Equal(5, contract.Notifications.Count);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o valor é maior ou igual que o comparador e não criar uma notificação, com comparador menor ou igual
  [Theory]
  [InlineData(0, -1)]
  [InlineData(0, 0)]
  public void IsGreaterOrEquals_ShouldNotAddNotification_WhenValueIsGreaterOrEquals(float valueParam, float comparer)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    float value = valueParam;
    decimal comparerDecimal = (decimal)comparer;
    double comparerDouble = (double)comparer;
    float comparerFloat = (float)comparer;
    int comparerInt = (int)comparer;
    long comparerLong = (long)comparer;

    // Act
    contract
      .IsGreaterOrEquals(value, comparerDecimal, property)
      .IsGreaterOrEquals(value, comparerDouble, property)
      .IsGreaterOrEquals(value, comparerFloat, property)
      .IsGreaterOrEquals(value, comparerInt, property)
      .IsGreaterOrEquals(value, comparerLong, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }

  // Teste para validar se o valor é menor que o comparador e criar uma notificação, com comparador menor ou igual
  [Theory]
  [InlineData(0, 0)]
  [InlineData(0, -1)]
  public void IsLower_ShouldAddNotification_WhenValueIsNotGreater(float valueParam, float comparer)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    float value = valueParam;
    decimal comparerDecimal = (decimal)comparer;
    double comparerDouble = (double)comparer;
    float comparerFloat = (float)comparer;
    int comparerInt = (int)comparer;
    long comparerLong = (long)comparer;

    // Act
    contract
      .IsLower(value, comparerDecimal, property)
      .IsLower(value, comparerDouble, property)
      .IsLower(value, comparerFloat, property)
      .IsLower(value, comparerInt, property)
      .IsLower(value, comparerLong, property);

    // Assert
    Assert.Equal(5, contract.Notifications.Count);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o valor é menor que o comparador e não criar uma notificação, com comparador maior
  [Theory]
  [InlineData(0, 2)]
  [InlineData(0, 1)]
  public void IsLower_ShouldNotAddNotification_WhenValueIsLower(float valueParam, float comparer)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    float value = valueParam;
    decimal comparerDecimal = (decimal)comparer;
    double comparerDouble = (double)comparer;
    float comparerFloat = (float)comparer;
    int comparerInt = (int)comparer;
    long comparerLong = (long)comparer;

    // Act
    contract
      .IsLower(value, comparerDecimal, property)
      .IsLower(value, comparerDouble, property)
      .IsLower(value, comparerFloat, property)
      .IsLower(value, comparerInt, property)
      .IsLower(value, comparerLong, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }

  // Teste para validar se o valor é menor ou igual que o comparador e criar uma notificação, com comparador menor
  [Theory]
  [InlineData(0, -2)]
  [InlineData(0, -1)]
  public void IsLowerOrEquals_ShouldAddNotification_WhenValueIsNotLowerOrEquals(float valueParam, float comparer)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    float value = valueParam;
    decimal comparerDecimal = (decimal)comparer;
    double comparerDouble = (double)comparer;
    float comparerFloat = (float)comparer;
    int comparerInt = (int)comparer;
    long comparerLong = (long)comparer;

    // Act
    contract
      .IsLowerOrEquals(value, comparerDecimal, property)
      .IsLowerOrEquals(value, comparerDouble, property)
      .IsLowerOrEquals(value, comparerFloat, property)
      .IsLowerOrEquals(value, comparerInt, property)
      .IsLowerOrEquals(value, comparerLong, property);

    // Assert
    Assert.Equal(5, contract.Notifications.Count);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o valor é menor ou igual que o comparador e não criar uma notificação, com comparador maior ou igual
  [Theory]
  [InlineData(0, 0)]
  [InlineData(0, 1)]
  public void IsLowerOrEquals_ShouldNotAddNotification_WhenValueIsLowerOrEquals(float valueParam, float comparer)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    float value = valueParam;
    decimal comparerDecimal = (decimal)comparer;
    double comparerDouble = (double)comparer;
    float comparerFloat = (float)comparer;
    int comparerInt = (int)comparer;
    long comparerLong = (long)comparer;

    // Act
    contract
      .IsLowerOrEquals(value, comparerDecimal, property)
      .IsLowerOrEquals(value, comparerDouble, property)
      .IsLowerOrEquals(value, comparerFloat, property)
      .IsLowerOrEquals(value, comparerInt, property)
      .IsLowerOrEquals(value, comparerLong, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }

  // Teste para validar se o valor está entre o início e o fim e cria notificação, com valor menor e maior
  [Theory]
  [InlineData(-3, -1, 1)]
  [InlineData(3, -2, 2)]
  public void IsBetween_ShouldAddNotification_WhenValueIsNotBetweenStartAndEnd(float valueParam, float start, float end)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    float value = valueParam;
    decimal startDecimal = (decimal)start;
    double startDouble = (double)start;
    float startFloat = (float)start;
    int startInt = (int)start;
    long startLong = (long)start;
    decimal endDecimal = (decimal)end;
    double endDouble = (double)end;
    float endFloat = (float)end;
    int endInt = (int)end;
    long endLong = (long)end;

    // Act
    contract
      .IsBetween(value, startDecimal, endDecimal, property)
      .IsBetween(value, startDouble, endDouble, property)
      .IsBetween(value, startFloat, endFloat, property)
      .IsBetween(value, startInt, endInt, property)
      .IsBetween(value, startLong, endLong, property);

    // Assert
    Assert.Equal(5, contract.Notifications.Count);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o valor está entre o início e o fim e cria notificação, com valor menor e maior
  [Theory]
  [InlineData(0, -1, 1)]
  [InlineData(0, -2, 2)]
  public void IsBetween_ShouldNotAddNotification_WhenValueIsBetweenStartAndEnd(float valueParam, float start, float end)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    float value = valueParam;
    decimal startDecimal = (decimal)start;
    double startDouble = (double)start;
    float startFloat = (float)start;
    int startInt = (int)start;
    long startLong = (long)start;
    decimal endDecimal = (decimal)end;
    double endDouble = (double)end;
    float endFloat = (float)end;
    int endInt = (int)end;
    long endLong = (long)end;

    // Act
    contract
      .IsBetween(value, startDecimal, endDecimal, property)
      .IsBetween(value, startDouble, endDouble, property)
      .IsBetween(value, startFloat, endFloat, property)
      .IsBetween(value, startInt, endInt, property)
      .IsBetween(value, startLong, endLong, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }

  // Teste para validar se o valor não está entre o início e o fim e cria notificação, com valor entre
  [Theory]
  [InlineData(0, -1, 1)]
  [InlineData(0, -2, 2)]
  public void IsNotBetween_ShouldAddNotification_WhenValueIsBetweenStartAndEnd(float valueParam, float start, float end)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    float value = valueParam;
    decimal startDecimal = (decimal)start;
    double startDouble = (double)start;
    float startFloat = (float)start;
    int startInt = (int)start;
    long startLong = (long)start;
    decimal endDecimal = (decimal)end;
    double endDouble = (double)end;
    float endFloat = (float)end;
    int endInt = (int)end;
    long endLong = (long)end;

    // Act
    contract
      .IsNotBetween(value, startDecimal, endDecimal, property)
      .IsNotBetween(value, startDouble, endDouble, property)
      .IsNotBetween(value, startFloat, endFloat, property)
      .IsNotBetween(value, startInt, endInt, property)
      .IsNotBetween(value, startLong, endLong, property);

    // Assert
    Assert.Equal(5, contract.Notifications.Count);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o valor não está entre o início e o fim e não cria notificação, com valor menor e maior
  [Theory]
  [InlineData(-3, -1, 1)]
  [InlineData(3, -2, 2)]
  public void IsNotBetween_ShouldNotAddNotification_WhenValueIsNotBetweenStartAndEnd(float valueParam, float start, float end)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    float value = valueParam;
    decimal startDecimal = (decimal)start;
    double startDouble = (double)start;
    float startFloat = (float)start;
    int startInt = (int)start;
    long startLong = (long)start;
    decimal endDecimal = (decimal)end;
    double endDouble = (double)end;
    float endFloat = (float)end;
    int endInt = (int)end;
    long endLong = (long)end;

    // Act
    contract
      .IsNotBetween(value, startDecimal, endDecimal, property)
      .IsNotBetween(value, startDouble, endDouble, property)
      .IsNotBetween(value, startFloat, endFloat, property)
      .IsNotBetween(value, startInt, endInt, property)
      .IsNotBetween(value, startLong, endLong, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }

  // Teste para validar se o valor é o mínimo do tipo e cria notificação, com valor diferente
  [Fact]
  public void IsMin_ShouldAddNotification_WhenValueIsNotMin()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    float value = 0;

    // Act
    contract.IsMin(value, property);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o valor é o mínimo do tipo e não cria notificação, com valor mínimo do tipo
  [Fact]
  public void IsMin_ShouldNotAddNotification_WhenValueIsMin()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    float value = float.MinValue;

    // Act
    contract.IsMin(value, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }

  // Teste para validar se o valor não é o mínimo do tipo e cria notificação, com valor mínimo do tipo
  [Fact]
  public void IsNotMin_ShouldAddNotification_WhenValueIsMin()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    float value = float.MinValue;

    // Act
    contract.IsNotMin(value, property);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o valor não é o mínimo do tipo e não cria notificação, com valor diferente
  [Fact]
  public void IsNotMin_ShouldNotAddNotification_WhenValueIsNotMin()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    float value = 0;

    // Act
    contract.IsNotMin(value, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }

  // Teste para validar se o valor é o máximo do tipo e cria notificação, com valor diferente
  [Fact]
  public void IsMax_ShouldAddNotification_WhenValueIsNotMax()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    float value = 0;

    // Act
    contract.IsMax(value, property);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o valor é o máximo do tipo e não cria notificação, com valor máximo do tipo
  [Fact]
  public void IsMax_ShouldNotAddNotification_WhenValueIsMax()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    float value = float.MaxValue;

    // Act
    contract.IsMax(value, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }

  // Teste para validar se o valor não é o máximo do tipo e cria notificação, com valor máximo do tipo
  [Fact]
  public void IsNotMax_ShouldAddNotification_WhenValueIsMa()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    float value = float.MaxValue;

    // Act
    contract.IsNotMax(value, property);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o valor não é o máximo do tipo e não cria notificação, com valor diferente
  [Fact]
  public void IsNotMax_ShouldNotAddNotification_WhenValueIsNotMax()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    float value = 0;

    // Act
    contract.IsNotMax(value, property);

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
    float value = 0;
    decimal comparerDecimal = 10;
    double comparerDouble = 10;
    float comparerFloat = 10;
    int comparerInt = 10;
    long comparerLong = 10;

    // Act
    contract
      .AreEquals(value, comparerDecimal, property)
      .AreEquals(value, comparerDouble, property)
      .AreEquals(value, comparerFloat, property)
      .AreEquals(value, comparerInt, property)
      .AreEquals(value, comparerLong, property);

    // Assert
    Assert.Equal(5, contract.Notifications.Count);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o valor é igual ao comparador e não cria notificação, com valores iguais
  [Fact]
  public void AreEquals_ShouldNotAddNotification_WhenValueIsEqual()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    float value = 10;
    decimal comparerDecimal = 10;
    double comparerDouble = 10;
    float comparerFloat = 10;
    int comparerInt = 10;
    long comparerLong = 10;

    // Act
    contract
      .AreEquals(value, comparerDecimal, property)
      .AreEquals(value, comparerDouble, property)
      .AreEquals(value, comparerFloat, property)
      .AreEquals(value, comparerInt, property)
      .AreEquals(value, comparerLong, property);

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
    float value = 10;
    decimal comparerDecimal = 10;
    double comparerDouble = 10;
    float comparerFloat = 10;
    int comparerInt = 10;
    long comparerLong = 10;

    // Act
    contract
      .AreNotEquals(value, comparerDecimal, property)
      .AreNotEquals(value, comparerDouble, property)
      .AreNotEquals(value, comparerFloat, property)
      .AreNotEquals(value, comparerInt, property)
      .AreNotEquals(value, comparerLong, property);

    // Assert
    Assert.Equal(5, contract.Notifications.Count);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o valor não é igual ao comparador e não cria notificação, com valores diferentes
  [Fact]
  public void AreNotEquals_ShouldNotAddNotification_WhenValueIsNotEqual()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    float value = 0;
    decimal comparerDecimal = 10;
    double comparerDouble = 10;
    float comparerFloat = 10;
    int comparerInt = 10;
    long comparerLong = 10;

    // Act
    contract
      .AreNotEquals(value, comparerDecimal, property)
      .AreNotEquals(value, comparerDouble, property)
      .AreNotEquals(value, comparerFloat, property)
      .AreNotEquals(value, comparerInt, property)
      .AreNotEquals(value, comparerLong, property);

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
    float value = 0;
    decimal[] listDecimal = [5, 10, 15];
    double[] listDouble = [5, 10, 15];
    float[] listFloat = [5, 10, 15];
    int[] listInt = [5, 10, 15];
    long[] listLong = [5, 10, 15];

    // Act
    contract
      .Contains(value, listDecimal, property)
      .Contains(value, listDouble, property)
      .Contains(value, listFloat, property)
      .Contains(value, listInt, property)
      .Contains(value, listLong, property);

    // Assert
    Assert.Equal(5, contract.Notifications.Count);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o valor contém na lista e não cria notificação, com valor contido
  [Fact]
  public void Contains_ShouldNotAddNotification_WhenValueIsInList()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    float value = 10;
    decimal[] listDecimal = [5, 10, 15];
    double[] listDouble = [5, 10, 15];
    float[] listFloat = [5, 10, 15];
    int[] listInt = [5, 10, 15];
    long[] listLong = [5, 10, 15];

    // Act
    contract
      .Contains(value, listDecimal, property)
      .Contains(value, listDouble, property)
      .Contains(value, listFloat, property)
      .Contains(value, listInt, property)
      .Contains(value, listLong, property);

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
    float value = 10;
    decimal[] listDecimal = [5, 10, 15];
    double[] listDouble = [5, 10, 15];
    float[] listFloat = [5, 10, 15];
    int[] listInt = [5, 10, 15];
    long[] listLong = [5, 10, 15];

    // Act
    contract
      .NotContains(value, listDecimal, property)
      .NotContains(value, listDouble, property)
      .NotContains(value, listFloat, property)
      .NotContains(value, listInt, property)
      .NotContains(value, listLong, property);

    // Assert
    Assert.Equal(5, contract.Notifications.Count);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o valor não contém na lista e não cria notificação, com valor não contido
  [Fact]
  public void NotContains_ShouldNotAddNotification_WhenValueIsNotInList()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    float value = 0;
    decimal[] listDecimal = [5, 10, 15];
    double[] listDouble = [5, 10, 15];
    float[] listFloat = [5, 10, 15];
    int[] listInt = [5, 10, 15];
    long[] listLong = [5, 10, 15];

    // Act
    contract
      .NotContains(value, listDecimal, property)
      .NotContains(value, listDouble, property)
      .NotContains(value, listFloat, property)
      .NotContains(value, listInt, property)
      .NotContains(value, listLong, property);

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
    float value = 0;
    decimal[] listDecimal = [10, 10, 10];
    double[] listDouble = [10, 10, 10];
    float[] listFloat = [10, 10, 10];
    int[] listInt = [10, 10, 10];
    long[] listLong = [10, 10, 10];

    // Act
    contract
      .All(value, listDecimal, property)
      .All(value, listDouble, property)
      .All(value, listFloat, property)
      .All(value, listInt, property)
      .All(value, listLong, property);

    // Assert
    Assert.Equal(5, contract.Notifications.Count);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se todos valores da lista é igual o valor e cria notificação, todos valores são iguais
  [Fact]
  public void All_ShouldNotAddNotification_WhenAllValueInList()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    float value = 10;
    decimal[] listDecimal = [10, 10, 10];
    double[] listDouble = [10, 10, 10];
    float[] listFloat = [10, 10, 10];
    int[] listInt = [10, 10, 10];
    long[] listLong = [10, 10, 10];

    // Act
    contract
      .All(value, listDecimal, property)
      .All(value, listDouble, property)
      .All(value, listFloat, property)
      .All(value, listInt, property)
      .All(value, listLong, property);

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
    float value = 10;
    decimal[] listDecimal = [10, 10, 10];
    double[] listDouble = [10, 10, 10];
    float[] listFloat = [10, 10, 10];
    int[] listInt = [10, 10, 10];
    long[] listLong = [10, 10, 10];

    // Act
    contract
      .NotAll(value, listDecimal, property)
      .NotAll(value, listDouble, property)
      .NotAll(value, listFloat, property)
      .NotAll(value, listInt, property)
      .NotAll(value, listLong, property);

    // Assert
    Assert.Equal(5, contract.Notifications.Count);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se nem todos valores da lista é igual o valor e cria notificação, algum valor diferente
  [Fact]
  public void NotAll_ShouldNotAddNotification_WhenAllValueInList()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    float value = 0;
    decimal[] listDecimal = [10, 10, 10];
    double[] listDouble = [10, 10, 10];
    float[] listFloat = [10, 10, 10];
    int[] listInt = [10, 10, 10];
    long[] listLong = [10, 10, 10];

    // Act
    contract
      .NotAll(value, listDecimal, property)
      .NotAll(value, listDouble, property)
      .NotAll(value, listFloat, property)
      .NotAll(value, listInt, property)
      .NotAll(value, listLong, property);

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
    float? value = 0;

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
    float? value = null;

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
    float? value = null;

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
    float? value = 0;

    // Act
    contract.IsNotNull(value, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }
}
