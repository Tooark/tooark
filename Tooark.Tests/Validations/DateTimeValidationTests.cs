using Tooark.Validations;

namespace Tooark.Tests.Validations;

public class DateTimeValidationTests
{
  // Teste para validar se o valor é maior que o comparador e criar uma notificação, com comparador maior ou igual
  [Theory]
  [InlineData(0, 0)]
  [InlineData(0, 1)]
  public void IsGreater_ShouldAddNotification_WhenValueIsNotGreater(int valueParam, int comparerParam)
  {
    // Arrange
    var property = "TestProperty";
    var current = DateTime.Now;
    var validation = new Validation();
    DateTime value = current.AddDays(valueParam);
    DateTime comparer = current.AddDays(comparerParam);

    // Act
    validation.IsGreater(value, comparer, property);

    // Assert
    Assert.Single(validation.Notifications);
    Assert.Equal(property, validation.Notifications.First().Key);
  }

  // Teste para validar se o valor é maior que o comparador e não criar uma notificação, com comparador menor
  [Theory]
  [InlineData(0, -2)]
  [InlineData(0, -1)]
  public void IsGreater_ShouldNotAddNotification_WhenValueIsGreater(int valueParam, int comparerParam)
  {
    // Arrange
    var property = "TestProperty";
    var current = DateTime.Now;
    var validation = new Validation();
    DateTime value = current.AddDays(valueParam);
    DateTime comparer = current.AddDays(comparerParam);

    // Act
    validation.IsGreater(value, comparer, property);

    // Assert
    Assert.Empty(validation.Notifications);
  }

  // Teste para validar se o valor é maior ou igual que o comparador e criar uma notificação, com comparador maior
  [Theory]
  [InlineData(0, 1)]
  [InlineData(0, 2)]
  public void IsGreaterOrEquals_ShouldAddNotification_WhenValueIsNotGreaterOrEquals(int valueParam, int comparerParam)
  {
    // Arrange
    var property = "TestProperty";
    var current = DateTime.Now;
    var validation = new Validation();
    DateTime value = current.AddDays(valueParam);
    DateTime comparer = current.AddDays(comparerParam);

    // Act
    validation.IsGreaterOrEquals(value, comparer, property);

    // Assert
    Assert.Single(validation.Notifications);
    Assert.Equal(property, validation.Notifications.First().Key);
  }

  // Teste para validar se o valor é maior ou igual que o comparador e não criar uma notificação, com comparador menor ou igual
  [Theory]
  [InlineData(0, -1)]
  [InlineData(0, 0)]
  public void IsGreaterOrEquals_ShouldNotAddNotification_WhenValueIsGreaterOrEquals(int valueParam, int comparerParam)
  {
    // Arrange
    var property = "TestProperty";
    var current = DateTime.Now;
    var validation = new Validation();
    DateTime value = current.AddDays(valueParam);
    DateTime comparer = current.AddDays(comparerParam);

    // Act
    validation.IsGreaterOrEquals(value, comparer, property);

    // Assert
    Assert.Empty(validation.Notifications);
  }

  // Teste para validar se o valor é menor que o comparador e criar uma notificação, com comparador menor ou igual
  [Theory]
  [InlineData(0, 0)]
  [InlineData(0, -1)]
  public void IsLower_ShouldAddNotification_WhenValueIsNotGreater(int valueParam, int comparerParam)
  {
    // Arrange
    var property = "TestProperty";
    var current = DateTime.Now;
    var validation = new Validation();
    DateTime value = current.AddDays(valueParam);
    DateTime comparer = current.AddDays(comparerParam);

    // Act
    validation.IsLower(value, comparer, property);

    // Assert
    Assert.Single(validation.Notifications);
    Assert.Equal(property, validation.Notifications.First().Key);
  }

  // Teste para validar se o valor é menor que o comparador e não criar uma notificação, com comparador maior
  [Theory]
  [InlineData(0, 2)]
  [InlineData(0, 1)]
  public void IsLower_ShouldNotAddNotification_WhenValueIsLower(int valueParam, int comparerParam)
  {
    // Arrange
    var property = "TestProperty";
    var current = DateTime.Now;
    var validation = new Validation();
    DateTime value = current.AddDays(valueParam);
    DateTime comparer = current.AddDays(comparerParam);

    // Act
    validation.IsLower(value, comparer, property);

    // Assert
    Assert.Empty(validation.Notifications);
  }

  // Teste para validar se o valor é menor ou igual que o comparador e criar uma notificação, com comparador menor
  [Theory]
  [InlineData(0, -2)]
  [InlineData(0, -1)]
  public void IsLowerOrEquals_ShouldAddNotification_WhenValueIsNotLowerOrEquals(int valueParam, int comparerParam)
  {
    // Arrange
    var property = "TestProperty";
    var current = DateTime.Now;
    var validation = new Validation();
    DateTime value = current.AddDays(valueParam);
    DateTime comparer = current.AddDays(comparerParam);

    // Act
    validation.IsLowerOrEquals(value, comparer, property);

    // Assert
    Assert.Single(validation.Notifications);
    Assert.Equal(property, validation.Notifications.First().Key);
  }

  // Teste para validar se o valor é menor ou igual que o comparador e não criar uma notificação, com comparador maior ou igual
  [Theory]
  [InlineData(0, 0)]
  [InlineData(0, 1)]
  public void IsLowerOrEquals_ShouldNotAddNotification_WhenValueIsLowerOrEquals(int valueParam, int comparerParam)
  {
    // Arrange
    var property = "TestProperty";
    var current = DateTime.Now;
    var validation = new Validation();
    DateTime value = current.AddDays(valueParam);
    DateTime comparer = current.AddDays(comparerParam);

    // Act
    validation.IsLowerOrEquals(value, comparer, property);

    // Assert
    Assert.Empty(validation.Notifications);
  }

  // Teste para validar se o valor está entre o início e o fim e cria notificação, com valor menor e maior
  [Theory]
  [InlineData(-3, -1, 1)]
  [InlineData(3, -2, 2)]
  public void IsBetween_ShouldAddNotification_WhenValueIsNotBetweenStartAndEnd(int valueParam, int startParam, int endParam)
  {
    // Arrange
    var property = "TestProperty";
    var current = DateTime.Now;
    var validation = new Validation();
    DateTime value = current.AddDays(valueParam);
    DateTime start = current.AddDays(startParam);
    DateTime end = current.AddDays(endParam);

    // Act
    validation.IsBetween(value, start, end, property);

    // Assert
    Assert.Single(validation.Notifications);
    Assert.Equal(property, validation.Notifications.First().Key);
  }

  // Teste para validar se o valor está entre o início e o fim e cria notificação, com valor menor e maior
  [Theory]
  [InlineData(0, -1, 1)]
  [InlineData(0, -2, 2)]
  public void IsBetween_ShouldNotAddNotification_WhenValueIsBetweenStartAndEnd(int valueParam, int startParam, int endParam)
  {
    // Arrange
    var property = "TestProperty";
    var current = DateTime.Now;
    var validation = new Validation();
    DateTime value = current.AddDays(valueParam);
    DateTime start = current.AddDays(startParam);
    DateTime end = current.AddDays(endParam);

    // Act
    validation.IsBetween(value, start, end, property);

    // Assert
    Assert.Empty(validation.Notifications);
  }

  // Teste para validar se o valor não está entre o início e o fim e cria notificação, com valor entre
  [Theory]
  [InlineData(0, -1, 1)]
  [InlineData(0, -2, 2)]
  public void IsNotBetween_ShouldAddNotification_WhenValueIsBetweenStartAndEnd(int valueParam, int startParam, int endParam)
  {
    // Arrange
    var property = "TestProperty";
    var current = DateTime.Now;
    var validation = new Validation();
    DateTime value = current.AddDays(valueParam);
    DateTime start = current.AddDays(startParam);
    DateTime end = current.AddDays(endParam);

    // Act
    validation.IsNotBetween(value, start, end, property);

    // Assert
    Assert.Single(validation.Notifications);
    Assert.Equal(property, validation.Notifications.First().Key);
  }

  // Teste para validar se o valor não está entre o início e o fim e não cria notificação, com valor menor e maior
  [Theory]
  [InlineData(-3, -1, 1)]
  [InlineData(3, -2, 2)]
  public void IsNotBetween_ShouldNotAddNotification_WhenValueIsNotBetweenStartAndEnd(int valueParam, int startParam, int endParam)
  {
    // Arrange
    var property = "TestProperty";
    var current = DateTime.Now;
    var validation = new Validation();
    DateTime value = current.AddDays(valueParam);
    DateTime start = current.AddDays(startParam);
    DateTime end = current.AddDays(endParam);

    // Act
    validation.IsNotBetween(value, start, end, property);

    // Assert
    Assert.Empty(validation.Notifications);
  }

  // Teste para validar se o valor é o mínimo do tipo e cria notificação, com valor diferente
  [Fact]
  public void IsMin_ShouldAddNotification_WhenValueIsNotMin()
  {
    // Arrange
    var property = "TestProperty";
    var validation = new Validation();
    DateTime value = DateTime.Now;

    // Act
    validation.IsMin(value, property);

    // Assert
    Assert.Single(validation.Notifications);
    Assert.Equal(property, validation.Notifications.First().Key);
  }

  // Teste para validar se o valor é o mínimo do tipo e não cria notificação, com valor mínimo do tipo
  [Fact]
  public void IsMin_ShouldNotAddNotification_WhenValueIsMin()
  {
    // Arrange
    var property = "TestProperty";
    var validation = new Validation();
    DateTime value = DateTime.MinValue;

    // Act
    validation.IsMin(value, property);

    // Assert
    Assert.Empty(validation.Notifications);
  }

  // Teste para validar se o valor não é o mínimo do tipo e cria notificação, com valor mínimo do tipo
  [Fact]
  public void IsNotMin_ShouldAddNotification_WhenValueIsMin()
  {
    // Arrange
    var property = "TestProperty";
    var validation = new Validation();
    DateTime value = DateTime.MinValue;

    // Act
    validation.IsNotMin(value, property);

    // Assert
    Assert.Single(validation.Notifications);
    Assert.Equal(property, validation.Notifications.First().Key);
  }

  // Teste para validar se o valor não é o mínimo do tipo e não cria notificação, com valor diferente
  [Fact]
  public void IsNotMin_ShouldNotAddNotification_WhenValueIsNotMin()
  {
    // Arrange
    var property = "TestProperty";
    var validation = new Validation();
    DateTime value = DateTime.Now;

    // Act
    validation.IsNotMin(value, property);

    // Assert
    Assert.Empty(validation.Notifications);
  }

  // Teste para validar se o valor é o máximo do tipo e cria notificação, com valor diferente
  [Fact]
  public void IsMax_ShouldAddNotification_WhenValueIsNotMax()
  {
    // Arrange
    var property = "TestProperty";
    var validation = new Validation();
    DateTime value = DateTime.Now;

    // Act
    validation.IsMax(value, property);

    // Assert
    Assert.Single(validation.Notifications);
    Assert.Equal(property, validation.Notifications.First().Key);
  }

  // Teste para validar se o valor é o máximo do tipo e não cria notificação, com valor máximo do tipo
  [Fact]
  public void IsMax_ShouldNotAddNotification_WhenValueIsMax()
  {
    // Arrange
    var property = "TestProperty";
    var validation = new Validation();
    DateTime value = DateTime.MaxValue;

    // Act
    validation.IsMax(value, property);

    // Assert
    Assert.Empty(validation.Notifications);
  }

  // Teste para validar se o valor não é o máximo do tipo e cria notificação, com valor máximo do tipo
  [Fact]
  public void IsNotMax_ShouldAddNotification_WhenValueIsMa()
  {
    // Arrange
    var property = "TestProperty";
    var validation = new Validation();
    DateTime value = DateTime.MaxValue;

    // Act
    validation.IsNotMax(value, property);

    // Assert
    Assert.Single(validation.Notifications);
    Assert.Equal(property, validation.Notifications.First().Key);
  }

  // Teste para validar se o valor não é o máximo do tipo e não cria notificação, com valor diferente
  [Fact]
  public void IsNotMax_ShouldNotAddNotification_WhenValueIsNotMax()
  {
    // Arrange
    var property = "TestProperty";
    var validation = new Validation();
    DateTime value = DateTime.Now;

    // Act
    validation.IsNotMax(value, property);

    // Assert
    Assert.Empty(validation.Notifications);
  }

  // Teste para validar se o valor é igual ao comparador e cria notificação, com valores diferentes
  [Fact]
  public void AreEquals_ShouldAddNotification_WhenValueIsNotEqual()
  {
    // Arrange
    var property = "TestProperty";
    var current = DateTime.Now;
    var validation = new Validation();
    DateTime value = current;
    DateTime comparer = current.AddDays(1);

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
    var current = DateTime.Now;
    var validation = new Validation();
    DateTime value = current;
    DateTime comparer = current;

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
    var current = DateTime.Now;
    var validation = new Validation();
    DateTime value = current;
    DateTime comparer = current;

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
    var current = DateTime.Now;
    var validation = new Validation();
    DateTime value = current;
    DateTime comparer = current.AddDays(1);

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
    var current = DateTime.Now;
    var validation = new Validation();
    DateTime value = current;
    DateTime[] list = [current.AddDays(1), current.AddDays(2), current.AddDays(3)];

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
    var current = DateTime.Now;
    var validation = new Validation();
    DateTime value = current;
    DateTime[] list = [current.AddDays(1), current.AddDays(2), current];

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
    var current = DateTime.Now;
    var validation = new Validation();
    DateTime value = current;
    DateTime[] list = [current.AddDays(1), current.AddDays(2), current];

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
    var current = DateTime.Now;
    var validation = new Validation();
    DateTime value = current;
    DateTime[] list = [current.AddDays(1), current.AddDays(2), current.AddDays(3)];

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
    var current = DateTime.Now;
    var validation = new Validation();
    DateTime value = current;
    DateTime[] list = [current.AddDays(1), current, current];

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
    var current = DateTime.Now;
    var validation = new Validation();
    DateTime value = current;
    DateTime[] list = [current, current, current];

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
    var current = DateTime.Now;
    var validation = new Validation();
    DateTime value = current;
    DateTime[] list = [current, current, current];

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
    var current = DateTime.Now;
    var validation = new Validation();
    DateTime value = current;
    DateTime[] list = [current.AddDays(1), current, current];

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
    DateTime? value = DateTime.Now;

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
    DateTime? value = null;

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
    DateTime? value = null;

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
    DateTime? value = DateTime.Now;

    // Act
    validation.IsNotNull(value, property);

    // Assert
    Assert.Empty(validation.Notifications);
  }
}
