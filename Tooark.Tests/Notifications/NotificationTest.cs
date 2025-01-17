using Tooark.Notifications;

namespace Tooark.Tests.Notifications;

public class NotificationTests
{
  // Cria uma classe de notificação para testes
  public class TestNotification : Notification
  { }

  // Testa a adição de uma notificação com uma mensagem
  [Fact]
  public void Should_Add_Notification_With_Message()
  {
    // Arrange
    var message = "Tooark message";

    // Act
    var notification = new TestNotification();
    notification.AddNotification(message);

    // Assert
    Assert.Equal(message, notification.Notifications.First().Message);
    Assert.Single(notification.Notifications);
  }

  // Testa a adição de uma notificação com chave e mensagem
  [Fact]
  public void Should_Add_Notification_With_Key_And_Message()
  {
    // Arrange
    var message = "Tooark message";
    var key = "Tooark Key";

    // Act
    var notification = new TestNotification();
    notification.AddNotification(message, key);

    // Assert
    Assert.Equal(message, notification.Notifications.First().Message);
    Assert.Equal(key.Trim().Replace(" ", string.Empty), notification.Notifications.First().Key);
    Assert.Single(notification.Notifications);
  }

  // Testa a adição de notificação com propriedade e mensagem
  [Fact]
  public void Should_Add_Notification_With_Property_And_Message()
  {
    // Arrange
    var notification = new TestNotification();
    var message = "Tooark message";
    var property = typeof(TestNotification);

    // Act
    notification.AddNotification(property, message);

    // Assert
    Assert.Equal(message, notification.Notifications.First().Message);
    Assert.Equal(property.Name, notification.Notifications.First().Key);
    Assert.Single(notification.Notifications);
  }

  // Testa a adição de uma notificação com uma instância de NotificationItem
  [Fact]
  public void Should_Add_Notification_With_NotificationItem()
  {
    // Arrange
    var message = "Tooark message";
    var notificationItem = new NotificationItem(message);

    // Act
    var notification = new TestNotification();
    notification.AddNotification(notificationItem);

    // Assert
    Assert.Equal(message, notification.Notifications.First().Message);
    Assert.Single(notification.Notifications);
  }

  // Testa a adição de múltiplas notificações a partir de uma lista de NotificationItem
  [Fact]
  public void Should_Add_Multiple_Notifications_With_ListNotificationItem()
  {
    // Arrange
    var listNotificationItem = new List<NotificationItem>
    {
      new("Message 1"),
      new("Message 2")
    };
    var testNotification = new TestNotification();
    testNotification.AddNotifications(listNotificationItem);

    // Act
    var notification = new TestNotification();
    notification.AddNotifications(testNotification);

    // Assert
    Assert.Equal(2, notification.Notifications.Count);
  }

  // Testa a adição de notificações de outra notificação
  [Fact]
  public void Should_Add_Multiple_Notifications_From_Another_Notification()
  {
    // Arrange
    var notification1 = new TestNotification();
    var notification2 = new TestNotification();
    notification1.AddNotification("Message 1");
    notification1.AddNotification("Message 2");

    // Act
    notification2.AddNotifications(notification1);

    // Assert
    Assert.Equal(2, notification2.Notifications.Count);
  }

  // Testa a adição de notificações utilizando outras Notification como parâmetros
  [Fact]
  public void Should_Add_Multiple_Notifications_With_Params_Notification()
  {
    // Arrange
    var notification1 = new TestNotification();
    notification1.AddNotification("Message 1");
    var notification2 = new TestNotification();
    notification2.AddNotification("Message 2");
    var notification = new TestNotification();

    // Act
    notification.AddNotifications(notification1, notification2);

    // Assert
    Assert.Equal(2, notification.Notifications.Count);
  }

  // Testa a limpeza das notificações
  [Fact]
  public void Should_Clear_Notifications()
  {
    // Arrange
    var notification = new TestNotification();
    notification.AddNotification("Message 1");

    // Act
    notification.Clear();

    // Assert
    Assert.Empty(notification.Notifications);
  }

  // Testa a validação de notificações
  [Fact]
  public void Should_Return_ValidAndInvalid()
  {
    // Arrange
    var validNotification = new TestNotification();
    var invalidNotification = new TestNotification();
    

    // Act
    invalidNotification.AddNotification("Tooark Message");

    // Assert
    Assert.True(validNotification.IsValid);
    Assert.False(invalidNotification.IsValid);
  }

  // Testa a contagem de notificações
  [Fact]
  public void Should_Return_Count()
  {
    // Arrange
    var notification1 = new TestNotification();
    notification1.AddNotification("Message 1");
    var notification2 = new TestNotification();
    notification2.AddNotification("Message 2");
    var notification = new TestNotification();

    // Act
    notification.AddNotifications(notification1, notification2);

    // Assert
    Assert.Equal(2, notification.Count);
  }

  // Testa a obtenção de chaves
  [Fact]
  public void Should_Return_Key()
  {
    // Arrange
    var message = "Tooark message";
    var key = "Tooark Key";

    // Act
    var notification = new TestNotification();
    notification.AddNotification(message, key);

    // Assert
    Assert.Equal(key.Trim().Replace(" ", string.Empty), notification.Keys[0]);
    Assert.Single(notification.Keys);
  }

  // Testa a obtenção de mensagens
  [Fact]
  public void Should_Return_Message()
  {
    // Arrange
    var message = "Tooark message";
    var key = "Tooark Key";

    // Act
    var notification = new TestNotification();
    notification.AddNotification(message, key);

    // Assert
    Assert.Equal(message, notification.Messages[0]);
    Assert.Single(notification.Messages);
  }  

  // Testa a exceção ao adicionar NotificationItem nula
  [Fact]
  public void Should_Throw_Exception_When_Adding_Null_NotificationItem()
  {
    // Arrange
    var notification = new TestNotification();

    // Act
    var exception = Assert.Throws<ArgumentNullException>(() => notification.AddNotification((NotificationItem)null!));

    // Assert
    Assert.Equal("Value cannot be null. (Parameter 'notification')", exception.Message);
  }

  // Testa a exceção ao adicionar mensagem nula ou vazia
  [Fact]
  public void Should_Throw_Exception_When_Adding_Null_Or_Empty_Message()
  {
    // Arrange
    var notification = new TestNotification();

    // Act
    var exceptionNull = Assert.Throws<ArgumentNullException>(() => notification.AddNotification((string)null!));
    var exceptionEmpty = Assert.Throws<ArgumentNullException>(() => notification.AddNotification(""));

    // Assert
    Assert.Equal("Value cannot be null. (Parameter 'message')", exceptionNull.Message);
    Assert.Equal("Value cannot be null. (Parameter 'message')", exceptionEmpty.Message);
  }

  // Testa a exceção ao adicionar notificação com mensagem nula ou vazia
  [Fact]
  public void Should_Throw_Exception_When_Adding_Notification_With_Null_Or_Empty_Message_And_Property()
  {
    // Arrange
    var notification = new TestNotification();
    var property = typeof(TestNotification);

    // Act
    var exceptionNull = Assert.Throws<ArgumentNullException>(() => notification.AddNotification(property, null!));
    var exceptionEmpty = Assert.Throws<ArgumentNullException>(() => notification.AddNotification(property, ""));

    // Assert
    Assert.Equal("Value cannot be null. (Parameter 'message')", exceptionNull.Message);
    Assert.Equal("Value cannot be null. (Parameter 'message')", exceptionEmpty.Message);
  }

  // Testa a exceção ao adicionar notificação nula
  [Fact]
  public void Should_Throw_Exception_When_Adding_Null_Notification()
  {
    // Arrange
    var notification = new TestNotification();

    // Act
    var exception = Assert.Throws<ArgumentNullException>(() => notification.AddNotifications((Notification)null!));

    // Assert
    Assert.Equal("Value cannot be null. (Parameter 'notification')", exception.Message);
  }

  
}
