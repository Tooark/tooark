using Tooark.Notifications;
using Tooark.Validations;

namespace Tooark.Tests.Validations;

public class ContractTest
{
  // Teste de Join com notificações nulas
  [Fact]
  public void Join_WithNullNotifications_ReturnsSameInstance()
  {
    // Arrange
    var contract = new Contract();

    // Act
    contract.Join(null!);

    // Assert
    Assert.Empty(contract.Notifications);
  }

  // Teste de Join com notificações válidas
  [Fact]
  public void Join_WithValidNotifications_DoesNotAddNotifications()
  {
    // Arrange
    var testContract = new Contract();
    testContract.IsTrue(true, "TestProperty");

    var contract = new Contract();

    // Act
    contract.Join(testContract);

    // Assert
    Assert.Empty(contract.Notifications);
  }

  // Teste de Join com notificações inválidas
  [Fact]
  public void Join_WithInvalidNotifications_AddsNotifications()
  {
    // Arrange
    var testContract = new Contract();
    testContract.IsTrue(false, "TestProperty");

    var contract = new Contract();

    // Act
    contract.Join(testContract);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Same(testContract.Notifications.First(), contract.Notifications.First());
  }

  // Teste de Join com notificações que já existem
  [Fact]
  public void Join_WithExitsNotifications_AddsNotifications()
  {
    // Arrange
    var testContract = new Contract();
    testContract.IsTrue(false, "TestProperty");

    var contract = new Contract();
    contract.IsTrue(false, "TestProperty");

    // Act
    contract.Join(testContract);

    // Assert
    Assert.Equal(2, contract.Notifications.Count);
    Assert.Contains(testContract.Notifications.First(), contract.Notifications);
  }

  // Teste de Join com notificações válidas e inválidas
  [Fact]
  public void Join_WithMixedNotifications_AddsOnlyInvalidNotifications()
  {
    // Arrange
    var validNotification = new Contract();
    validNotification.IsTrue(true, "TestProperty");
    
    var invalidNotification = new Contract();
    invalidNotification.IsTrue(false, "TestProperty");

    var contract = new Contract();

    // Act
    contract.Join(validNotification, invalidNotification);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Same(invalidNotification.Notifications.First(), contract.Notifications.First());
  }

  // Teste de Join com múltiplas notificações inválidas
  [Fact]
  public void Join_WithMultipleInvalidNotifications_AddsAllInvalidNotifications()
  {
    // Arrange
     var invalidNotification1 = new Contract();
    invalidNotification1.IsTrue(false, "TestProperty");
    
    var invalidNotification2 = new Contract();
    invalidNotification2.IsTrue(false, "TestProperty");

    var contract = new Contract();

    // Act
    contract.Join(invalidNotification1, invalidNotification2);

    // Assert
    Assert.Equal(2, contract.Notifications.Count);
    Assert.Contains(invalidNotification1.Notifications.First(), contract.Notifications);
    Assert.Contains(invalidNotification2.Notifications.First(), contract.Notifications);
  }

  // Teste de Join sem notificações
  [Fact]
  public void Join_WithNoNotifications_DoesNotAddAnyNotifications()
  {
    // Arrange
    var contract = new Contract();

    // Act
    contract.Join();

    // Assert
    Assert.Empty(contract.Notifications);
  }

  // Teste de Join com todas as notificações válidas
  [Fact]
  public void Join_WithAllValidNotifications_DoesNotAddAnyNotifications()
  {
    // Arrange
     var validNotification1 = new Contract();
    validNotification1.IsTrue(true, "TestProperty");
    
    var validNotification2 = new Contract();
    validNotification2.IsTrue(true, "TestProperty");

    var contract = new Contract();

    // Act
    contract.Join(validNotification1, validNotification2);

    // Assert
    Assert.Empty(contract.Notifications);
  }
}
