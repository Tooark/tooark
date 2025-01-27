using Tooark.Validations;

namespace Tooark.Tests.Validations;

public class NetworkValidationContractTests
{
  // Teste para validar se o valor corresponde ao padrão e cria notificação, com valor que não corresponde
  [Theory]
  [InlineData(null)]
  [InlineData("")]
  [InlineData("x.x.x.x")]
  [InlineData("256.256.256.256")]
  public void IsIp_ShouldAddNotification_WhenValueNotIsIp(string? valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = valueParam!;

    // Act
    contract.IsIp(value, property);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o valor corresponde ao padrão e não cria notificação, com valor que corresponde
  [Theory]
  [InlineData("0.0.0.0")]
  [InlineData("255.255.255.255")]
  public void IsIp_ShouldNotAddNotification_WhenValueIsIp(string valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = valueParam;

    // Act
    contract.IsIp(value, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }

  // Teste para validar se o valor corresponde ao padrão e cria notificação, com valor que não corresponde
  [Theory]
  [InlineData(null)]
  [InlineData("")]
  [InlineData("x.x.x.x")]
  [InlineData("256.256.256.256")]
  public void IsIpv4_ShouldAddNotification_WhenValueNotIsIpv4(string? valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = valueParam!;

    // Act
    contract.IsIpv4(value, property);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o valor corresponde ao padrão e não cria notificação, com valor que corresponde
  [Theory]
  [InlineData("0.0.0.0")]
  [InlineData("255.255.255.255")]
  public void IsIpv4_ShouldNotAddNotification_WhenValueIsIpv4(string valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = valueParam;

    // Act
    contract.IsIpv4(value, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }

  // Teste para validar se o valor corresponde ao padrão e cria notificação, com valor que não corresponde
  [Theory]
  [InlineData(null)]
  [InlineData("")]
  [InlineData("::1")]
  [InlineData("2001:db8:85a3::8a2e:370:7334")]
  [InlineData("x:x:x:x:x:x:x:x")]
  [InlineData("x:x:x:x:x:x:x:x:x")]
  public void IsIpv6_ShouldAddNotification_WhenValueNotIsIpv6(string? valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = valueParam!;

    // Act
    contract.IsIpv6(value, property);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o valor corresponde ao padrão e não cria notificação, com valor que corresponde
  [Theory]
  [InlineData("2001:0db8:85a3:0000:0000:8a2e:0370:7334")]
  [InlineData("2001:db8:85a3:0:0:8a2e:370:7334")]
  public void IsIpv6_ShouldNotAddNotification_WhenValueIsIpv6(string valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = valueParam;

    // Act
    contract.IsIpv6(value, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }
  
  // Teste para validar se o valor corresponde ao padrão e cria notificação, com valor que não corresponde
  [Theory]
  [InlineData(null)]
  [InlineData("")]
  [InlineData("GG:GG:GG:GG:GG:GG")]
  public void IsMacAddress_ShouldAddNotification_WhenValueNotIsMacAddress(string? valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = valueParam!;

    // Act
    contract.IsMacAddress(value, property);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o valor corresponde ao padrão e não cria notificação, com valor que corresponde
  [Theory]
  [InlineData("00:00:00:00:00:00")]
  [InlineData("FF:FF:FF:FF:FF:FF")]
  public void IsMacAddress_ShouldNotAddNotification_WhenValueIsMacAddress(string valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = valueParam;

    // Act
    contract.IsMacAddress(value, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }
}
