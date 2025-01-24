using Tooark.Notifications;

namespace Tooark.Tests.Notifications;

public class NotificationItemTests
{
  // Teste para construtor da classe NotificationItem com parâmetros padrão.
  [Fact]
  public void Constructor_ShouldNotificationItem_WhenDefaultValues()
  {
    // Arrange
    var message = "Tooark message";

    // Act
    var notification = new NotificationItem(message);

    // Assert
    Assert.Equal(message, notification.Message);
    Assert.Equal("Unknown", notification.Key);
  }

  // Teste para construtor da classe NotificationItem com parâmetro key.
  [Fact]
  public void Constructor_ShouldNotificationItem_WhenKeyParam()
  {
    // Arrange
    var message = "Tooark message";
    var key = "Tooark Key";

    // Act
    var notification = new NotificationItem(message, key);

    // Assert
    Assert.Equal(message, notification.Message);
    Assert.Equal(key.Trim().Replace(" ", string.Empty), notification.Key);
  }

  // Teste para método ToString da classe NotificationItem.
  [Fact]
  public void ToString_ShouldReturnMessage()
  {
    // Arrange
    var message = "Tooark message";
    var notification = new NotificationItem(message);

    // Act
    var result = notification.ToString();

    // Assert
    Assert.Equal(message, result);
  }

  // Teste para operador implícito de conversão de string para NotificationItem.
  [Fact]
  public void ImplicitConversion_FromString_ShouldCreateNotification()
  {
    // Arrange
    var message = "Tooark message";

    // Act
    NotificationItem notification = message;

    // Assert
    Assert.Equal(message, notification.Message);
    Assert.Equal("Unknown", notification.Key);
  }

  // Teste para operador implícito de conversão de NotificationItem para string.
  [Fact]
  public void ImplicitConversion_ToString_ShouldReturnMessage()
  {
    // Arrange
    var message = "Tooark message";
    var notification = new NotificationItem(message);

    // Act
    string result = notification;

    // Assert
    Assert.Equal(message, result);
  }

  // Teste para construtor da classe NotificationItem com mensagem nula retornando 'Unknown'.
  [Fact]
  public void Constructor_ShouldMessageUnknown_WhenMessageNull()
  {
    // Arrange
    string message = null!;

    // Act
    var notification = new NotificationItem(message);

    // Assert
    Assert.Equal("Notifications.MessageUnknown", notification.Message);
    Assert.Equal("Unknown", notification.Key);
  }

  // Teste para construtor da classe NotificationItem com mensagem vazia retornando string.Empty.
  [Fact]
  public void Constructor_ShouldMessageStringEmpty_WhenMessageStringEmpty()
  {
    // Arrange
    string message = string.Empty;

    // Act
    var notification = new NotificationItem(message);

    // Assert
    Assert.Equal(string.Empty, notification.Message);
    Assert.Equal("Unknown", notification.Key);
  }

  // Teste para construtor da classe NotificationItem com chave nula retornando 'Unknown'.
  [Fact]
  public void Constructor_ShouldKeyUnknown_WhenKeyNull()
  {
    // Arrange
    string message = "Tooark Message";
    string key = null!;

    // Act
    var notification = new NotificationItem(message, key);

    // Assert
    Assert.Equal(message, notification.Message);
    Assert.Equal("Unknown", notification.Key);
  }

  // Teste para construtor da classe NotificationItem com chave vazia retornando string.Empty.
  [Fact]
  public void Constructor_ShouldKeyStringEmpty_WhenKeyStringEmpty()
  {
    // Arrange
    string message = "Tooark Message";
    string key = string.Empty;

    // Act
    var notification = new NotificationItem(message, key);

    // Assert
    Assert.Equal(message, notification.Message);
    Assert.Equal(string.Empty, notification.Key);
  }
}
