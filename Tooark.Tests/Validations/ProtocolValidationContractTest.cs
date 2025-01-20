using Tooark.Validations;

namespace Tooark.Tests.Validations;

public class ProtocolValidationContractTest
{
  // Teste para validar se o valor corresponde ao padrão e cria notificação, com valor que não corresponde
  [Theory]
  [InlineData("test@example.com")]
  [InlineData("example.com")]
  [InlineData("abc")]
  [InlineData("123")]
  [InlineData("")]
  [InlineData(null)]
  public void IsUrl_ShouldAddNotification_WhenValueNotIsUrl(string? valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = valueParam!;

    // Act
    contract.IsUrl(value, property);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o valor corresponde ao padrão e não cria notificação, com valor que corresponde
  [Theory]
  [InlineData("ftp://example.com")]
  [InlineData("sftp://example.com")]
  [InlineData("http://example.com")]
  [InlineData("https://example.com")]
  [InlineData("imap://example.com")]
  [InlineData("pop3://example.com")]
  [InlineData("smtp://example.com")]
  [InlineData("ws://example.com")]
  [InlineData("wss://example.com")]
  public void IsUrl_ShouldNotAddNotification_WhenValueIsUrl(string valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = valueParam;

    // Act
    contract.IsUrl(value, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }

  // Teste para validar se o valor corresponde ao padrão e cria notificação, com valor que não corresponde
  [Theory]
  [InlineData("test@example.com")]
  [InlineData("example.com")]
  [InlineData("abc")]
  [InlineData("123")]
  [InlineData("")]
  [InlineData(null)]
  [InlineData("sftp://example.com")]
  [InlineData("http://example.com")]
  [InlineData("https://example.com")]
  [InlineData("imap://example.com")]
  [InlineData("pop3://example.com")]
  [InlineData("smtp://example.com")]
  [InlineData("ws://example.com")]
  [InlineData("wss://example.com")]
  public void IsFtp_ShouldAddNotification_WhenValueNotIsFtp(string? valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = valueParam!;

    // Act
    contract.IsFtp(value, property);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o valor corresponde ao padrão e não cria notificação, com valor que corresponde
  [Theory]
  [InlineData("ftp://example.com")]
  public void IsFtp_ShouldNotAddNotification_WhenValueIsFtp(string valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = valueParam;

    // Act
    contract.IsFtp(value, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }

  // Teste para validar se o valor corresponde ao padrão e cria notificação, com valor que não corresponde
  [Theory]
  [InlineData("test@example.com")]
  [InlineData("example.com")]
  [InlineData("abc")]
  [InlineData("123")]
  [InlineData("")]
  [InlineData(null)]
  [InlineData("ftp://example.com")]
  [InlineData("http://example.com")]
  [InlineData("https://example.com")]
  [InlineData("imap://example.com")]
  [InlineData("pop3://example.com")]
  [InlineData("smtp://example.com")]
  [InlineData("ws://example.com")]
  [InlineData("wss://example.com")]
  public void IsSftp_ShouldAddNotification_WhenValueNotIsSftp(string? valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = valueParam!;

    // Act
    contract.IsSftp(value, property);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o valor corresponde ao padrão e não cria notificação, com valor que corresponde
  [Theory]
  [InlineData("sftp://example.com")]
  public void IsSftp_ShouldNotAddNotification_WhenValueIsSftp(string valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = valueParam;

    // Act
    contract.IsSftp(value, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }

  // Teste para validar se o valor corresponde ao padrão e cria notificação, com valor que não corresponde
  [Theory]
  [InlineData("test@example.com")]
  [InlineData("example.com")]
  [InlineData("abc")]
  [InlineData("123")]
  [InlineData("")]
  [InlineData(null)]
  [InlineData("ftp://example.com")]
  [InlineData("sftp://example.com")]
  [InlineData("https://example.com")]
  [InlineData("imap://example.com")]
  [InlineData("pop3://example.com")]
  [InlineData("smtp://example.com")]
  [InlineData("ws://example.com")]
  [InlineData("wss://example.com")]
  public void IsHttp_ShouldAddNotification_WhenValueNotIsHttp(string? valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = valueParam!;

    // Act
    contract.IsHttp(value, property);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o valor corresponde ao padrão e não cria notificação, com valor que corresponde
  [Theory]
  [InlineData("http://example.com")]
  public void IsHttp_ShouldNotAddNotification_WhenValueIsHttp(string valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = valueParam;

    // Act
    contract.IsHttp(value, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }

  // Teste para validar se o valor corresponde ao padrão e cria notificação, com valor que não corresponde
  [Theory]
  [InlineData("test@example.com")]
  [InlineData("example.com")]
  [InlineData("abc")]
  [InlineData("123")]
  [InlineData("")]
  [InlineData(null)]
  [InlineData("ftp://example.com")]
  [InlineData("sftp://example.com")]
  [InlineData("http://example.com")]
  [InlineData("imap://example.com")]
  [InlineData("pop3://example.com")]
  [InlineData("smtp://example.com")]
  [InlineData("ws://example.com")]
  [InlineData("wss://example.com")]
  public void IsHttps_ShouldAddNotification_WhenValueNotIsHttps(string? valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = valueParam!;

    // Act
    contract.IsHttps(value, property);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o valor corresponde ao padrão e não cria notificação, com valor que corresponde
  [Theory]
  [InlineData("https://example.com")]
  public void IsHttps_ShouldNotAddNotification_WhenValueIsHttps(string valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = valueParam;

    // Act
    contract.IsHttps(value, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }

  // Teste para validar se o valor corresponde ao padrão e cria notificação, com valor que não corresponde
  [Theory]
  [InlineData("test@example.com")]
  [InlineData("example.com")]
  [InlineData("abc")]
  [InlineData("123")]
  [InlineData("")]
  [InlineData(null)]
  [InlineData("ftp://example.com")]
  [InlineData("sftp://example.com")]
  [InlineData("http://example.com")]
  [InlineData("https://example.com")]
  [InlineData("pop3://example.com")]
  [InlineData("smtp://example.com")]
  [InlineData("ws://example.com")]
  [InlineData("wss://example.com")]
  public void IsImap_ShouldAddNotification_WhenValueNotIsImap(string? valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = valueParam!;

    // Act
    contract.IsImap(value, property);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o valor corresponde ao padrão e não cria notificação, com valor que corresponde
  [Theory]
  [InlineData("imap://example.com")]
  public void IsImap_ShouldNotAddNotification_WhenValueIsImap(string valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = valueParam;

    // Act
    contract.IsImap(value, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }

  // Teste para validar se o valor corresponde ao padrão e cria notificação, com valor que não corresponde
  [Theory]
  [InlineData("test@example.com")]
  [InlineData("example.com")]
  [InlineData("abc")]
  [InlineData("123")]
  [InlineData("")]
  [InlineData(null)]
  [InlineData("ftp://example.com")]
  [InlineData("sftp://example.com")]
  [InlineData("http://example.com")]
  [InlineData("https://example.com")]
  [InlineData("imap://example.com")]
  [InlineData("smtp://example.com")]
  [InlineData("ws://example.com")]
  [InlineData("wss://example.com")]
  public void IsPop3_ShouldAddNotification_WhenValueNotIsPop3(string? valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = valueParam!;

    // Act
    contract.IsPop3(value, property);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o valor corresponde ao padrão e não cria notificação, com valor que corresponde
  [Theory]
  [InlineData("pop3://example.com")]
  public void IsPop3_ShouldNotAddNotification_WhenValueIsPop3(string valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = valueParam;

    // Act
    contract.IsPop3(value, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }

  // Teste para validar se o valor corresponde ao padrão e cria notificação, com valor que não corresponde
  [Theory]
  [InlineData("test@example.com")]
  [InlineData("example.com")]
  [InlineData("abc")]
  [InlineData("123")]
  [InlineData("")]
  [InlineData(null)]
  [InlineData("ftp://example.com")]
  [InlineData("sftp://example.com")]
  [InlineData("http://example.com")]
  [InlineData("https://example.com")]
  [InlineData("imap://example.com")]
  [InlineData("pop3://example.com")]
  [InlineData("ws://example.com")]
  [InlineData("wss://example.com")]
  public void IsSmtp_ShouldAddNotification_WhenValueNotIsSmtp(string? valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = valueParam!;

    // Act
    contract.IsSmtp(value, property);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o valor corresponde ao padrão e não cria notificação, com valor que corresponde
  [Theory]
  [InlineData("smtp://example.com")]
  public void IsSmtp_ShouldNotAddNotification_WhenValueIsSmtp(string valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = valueParam;

    // Act
    contract.IsSmtp(value, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }

  // Teste para validar se o valor corresponde ao padrão e cria notificação, com valor que não corresponde
  [Theory]
  [InlineData("test@example.com")]
  [InlineData("example.com")]
  [InlineData("abc")]
  [InlineData("123")]
  [InlineData("")]
  [InlineData(null)]
  [InlineData("ftp://example.com")]
  [InlineData("sftp://example.com")]
  [InlineData("http://example.com")]
  [InlineData("https://example.com")]
  [InlineData("imap://example.com")]
  [InlineData("pop3://example.com")]
  [InlineData("smtp://example.com")]
  [InlineData("wss://example.com")]
  public void IsWs_ShouldAddNotification_WhenValueNotIsWs(string? valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = valueParam!;

    // Act
    contract.IsWs(value, property);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o valor corresponde ao padrão e não cria notificação, com valor que corresponde
  [Theory]
  [InlineData("ws://example.com")]
  public void IsWs_ShouldNotAddNotification_WhenValueIsWs(string valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = valueParam;

    // Act
    contract.IsWs(value, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }

  // Teste para validar se o valor corresponde ao padrão e cria notificação, com valor que não corresponde
  [Theory]
  [InlineData("test@example.com")]
  [InlineData("example.com")]
  [InlineData("abc")]
  [InlineData("123")]
  [InlineData("")]
  [InlineData(null)]
  [InlineData("ftp://example.com")]
  [InlineData("sftp://example.com")]
  [InlineData("http://example.com")]
  [InlineData("https://example.com")]
  [InlineData("imap://example.com")]
  [InlineData("pop3://example.com")]
  [InlineData("smtp://example.com")]
  [InlineData("ws://example.com")]
  public void IsWss_ShouldAddNotification_WhenValueNotIsWss(string? valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = valueParam!;

    // Act
    contract.IsWss(value, property);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o valor corresponde ao padrão e não cria notificação, com valor que corresponde
  [Theory]
  [InlineData("wss://example.com")]
  public void IsWss_ShouldNotAddNotification_WhenValueIsWss(string valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = valueParam;

    // Act
    contract.IsWss(value, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }

  // Teste para validar se o valor corresponde ao padrão e cria notificação, com valor que não corresponde
  [Theory]
  [InlineData("test@example.com")]
  [InlineData("example.com")]
  [InlineData("abc")]
  [InlineData("123")]
  [InlineData("")]
  [InlineData(null)]
  [InlineData("http://example.com")]
  [InlineData("https://example.com")]
  [InlineData("imap://example.com")]
  [InlineData("pop3://example.com")]
  [InlineData("smtp://example.com")]
  [InlineData("ws://example.com")]
  [InlineData("wss://example.com")]
  public void IsProtocolFtp_ShouldAddNotification_WhenValueNotIsProtocolFtp(string? valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = valueParam!;

    // Act
    contract.IsProtocolFtp(value, property);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o valor corresponde ao padrão e não cria notificação, com valor que corresponde
  [Theory]
  [InlineData("ftp://example.com")]
  [InlineData("sftp://example.com")]
  public void IsProtocolFtp_ShouldNotAddNotification_WhenValueIsProtocolFtp(string valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = valueParam;

    // Act
    contract.IsProtocolFtp(value, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }

  // Teste para validar se o valor corresponde ao padrão e cria notificação, com valor que não corresponde
  [Theory]
  [InlineData("test@example.com")]
  [InlineData("example.com")]
  [InlineData("abc")]
  [InlineData("123")]
  [InlineData("")]
  [InlineData(null)]
  [InlineData("ftp://example.com")]
  [InlineData("sftp://example.com")]
  [InlineData("imap://example.com")]
  [InlineData("pop3://example.com")]
  [InlineData("smtp://example.com")]
  [InlineData("ws://example.com")]
  [InlineData("wss://example.com")]
  public void IsProtocolHttp_ShouldAddNotification_WhenValueNotIsProtocolHttp(string? valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = valueParam!;

    // Act
    contract.IsProtocolHttp(value, property);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o valor corresponde ao padrão e não cria notificação, com valor que corresponde
  [Theory]
  [InlineData("http://example.com")]
  [InlineData("https://example.com")]
  public void IsProtocolHttp_ShouldNotAddNotification_WhenValueIsProtocolHttp(string valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = valueParam;

    // Act
    contract.IsProtocolHttp(value, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }

  // Teste para validar se o valor corresponde ao padrão e cria notificação, com valor que não corresponde
  [Theory]
  [InlineData("test@example.com")]
  [InlineData("example.com")]
  [InlineData("abc")]
  [InlineData("123")]
  [InlineData("")]
  [InlineData(null)]
  [InlineData("ftp://example.com")]
  [InlineData("sftp://example.com")]
  [InlineData("http://example.com")]
  [InlineData("https://example.com")]
  [InlineData("smtp://example.com")]
  [InlineData("ws://example.com")]
  [InlineData("wss://example.com")]
  public void IsProtocolEmailReceiver_ShouldAddNotification_WhenValueNotIsProtocolEmailReceiver(string? valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = valueParam!;

    // Act
    contract.IsProtocolEmailReceiver(value, property);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o valor corresponde ao padrão e não cria notificação, com valor que corresponde
  [Theory]
  [InlineData("imap://example.com")]
  [InlineData("pop3://example.com")]
  public void IsProtocolEmailReceiver_ShouldNotAddNotification_WhenValueIsProtocolEmailReceiver(string valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = valueParam;

    // Act
    contract.IsProtocolEmailReceiver(value, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }

  // Teste para validar se o valor corresponde ao padrão e cria notificação, com valor que não corresponde
  [Theory]
  [InlineData("test@example.com")]
  [InlineData("example.com")]
  [InlineData("abc")]
  [InlineData("123")]
  [InlineData("")]
  [InlineData(null)]
  [InlineData("ftp://example.com")]
  [InlineData("sftp://example.com")]
  [InlineData("http://example.com")]
  [InlineData("https://example.com")]
  [InlineData("imap://example.com")]
  [InlineData("pop3://example.com")]
  [InlineData("ws://example.com")]
  [InlineData("wss://example.com")]
  public void IsProtocolEmailSender_ShouldAddNotification_WhenValueNotIsProtocolEmailSender(string? valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = valueParam!;

    // Act
    contract.IsProtocolEmailSender(value, property);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o valor corresponde ao padrão e não cria notificação, com valor que corresponde
  [Theory]
  [InlineData("smtp://example.com")]
  public void IsProtocolEmailSender_ShouldNotAddNotification_WhenValueIsProtocolEmailSender(string valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = valueParam;

    // Act
    contract.IsProtocolEmailSender(value, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }

  // Teste para validar se o valor corresponde ao padrão e cria notificação, com valor que não corresponde
  [Theory]
  [InlineData("test@example.com")]
  [InlineData("example.com")]
  [InlineData("abc")]
  [InlineData("123")]
  [InlineData("")]
  [InlineData(null)]
  [InlineData("ftp://example.com")]
  [InlineData("sftp://example.com")]
  [InlineData("http://example.com")]
  [InlineData("https://example.com")]
  [InlineData("imap://example.com")]
  [InlineData("pop3://example.com")]
  [InlineData("smtp://example.com")]
  public void IsProtocolWebSocket_ShouldAddNotification_WhenValueNotIsProtocolWebSocket(string? valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = valueParam!;

    // Act
    contract.IsProtocolWebSocket(value, property);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o valor corresponde ao padrão e não cria notificação, com valor que corresponde
  [Theory]
  [InlineData("ws://example.com")]
  [InlineData("wss://example.com")]
  public void IsProtocolWebSocket_ShouldNotAddNotification_WhenValueIsProtocolWebSocket(string valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = valueParam;

    // Act
    contract.IsProtocolWebSocket(value, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }

}
