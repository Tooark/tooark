using Tooark.Notifications;
using Tooark.ValueObjects;

namespace Tooark.Tests.ValueObjects;

public class ValueObjectTests
{
  // Classe de objeto de valor de teste
  private class TestValueObject : ValueObject
  { }

  // Testa se a classe de objeto de valor herda de notificação
  [Fact]
  public void ValueObject_ShouldInheritFromNotification()
  {
    // Arrange & Act
    var valueObject = new TestValueObject();

    // Assert
    Assert.IsAssignableFrom<Notification>(valueObject);
  }
}
