using Tooark.Validations;

namespace Tooark.Tests.Validations;

public class DocumentValidationContractTest
{
  // Teste para validar se o valor corresponde ao padrão e cria notificação, com valor que não corresponde
  [Fact]
  public void IsCpf_ShouldAddNotification_WhenValueNotIsCpf()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = "123";

    // Act
    contract.IsCpf(value, property);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o valor corresponde ao padrão e não cria notificação, com valor que corresponde
  [Fact]
  public void IsCpf_ShouldNotAddNotification_WhenValueIsCpf()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = "123.456.789-00";

    // Act
    contract.IsCpf(value, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }

  // Teste para validar se o valor corresponde ao padrão e cria notificação, com valor que não corresponde
  [Fact]
  public void IsRg_ShouldAddNotification_WhenValueNotIsRg()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = "123";

    // Act
    contract.IsRg(value, property);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o valor corresponde ao padrão e não cria notificação, com valor que corresponde
  [Theory]
  [InlineData("12.345.678-9")]
  [InlineData("12.345.678")]
  [InlineData("12.345.678-x")]
  public void IsRg_ShouldNotAddNotification_WhenValueIsRg(string valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = valueParam;

    // Act
    contract.IsRg(value, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }
  
  // Teste para validar se o valor corresponde ao padrão e cria notificação, com valor que não corresponde
  [Fact]
  public void IsCnh_ShouldAddNotification_WhenValueNotIsCnh()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = "123";

    // Act
    contract.IsCnh(value, property);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o valor corresponde ao padrão e não cria notificação, com valor que corresponde
  [Fact]
  public void IsCnh_ShouldNotAddNotification_WhenValueIsCnh()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = "12345678901";

    // Act
    contract.IsCnh(value, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }
  // Teste para validar se o valor corresponde ao padrão e cria notificação, com valor que não corresponde
  [Fact]
  public void IsCpfRg_ShouldAddNotification_WhenValueNotIsCpfRg()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = "123";

    // Act
    contract.IsCpfRg(value, property);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o valor corresponde ao padrão e não cria notificação, com valor que corresponde
  [Theory]
  [InlineData("123.456.789-00")]
  [InlineData("12.345.678-9")]
  [InlineData("12.345.678")]
  [InlineData("12.345.678-x")]
  public void IsCpfRg_ShouldNotAddNotification_WhenValueIsCpfRg(string valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = valueParam;

    // Act
    contract.IsCpfRg(value, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }
  
  // Teste para validar se o valor corresponde ao padrão e cria notificação, com valor que não corresponde
  [Fact]
  public void IsCpfRgCnh_ShouldAddNotification_WhenValueNotIsCpfRgCnh()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = "123";

    // Act
    contract.IsCpfRgCnh(value, property);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o valor corresponde ao padrão e não cria notificação, com valor que corresponde
  [Theory]
  [InlineData("123.456.789-00")]
  [InlineData("12.345.678-9")]
  [InlineData("12.345.678")]
  [InlineData("12.345.678-x")]
  [InlineData("12345678901")]
  public void IsCpfRgCnh_ShouldNotAddNotification_WhenValueIsCpfRgCnh(string valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = valueParam;

    // Act
    contract.IsCpfRgCnh(value, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }

  // Teste para validar se o valor corresponde ao padrão e cria notificação, com valor que não corresponde
  [Fact]
  public void IsCnpj_ShouldAddNotification_WhenValueNotIsCnpj()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = "123";

    // Act
    contract.IsCnpj(value, property);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o valor corresponde ao padrão e não cria notificação, com valor que corresponde
  [Fact]
  public void IsCnpj_ShouldNotAddNotification_WhenValueIsCnpj()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = "12.345.678/0001-01";

    // Act
    contract.IsCnpj(value, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }
  
  // Teste para validar se o valor corresponde ao padrão e cria notificação, com valor que não corresponde
  [Fact]
  public void IsCpfCnpj_ShouldAddNotification_WhenValueNotIsCpfCnpj()
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = "123";

    // Act
    contract.IsCpfCnpj(value, property);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o valor corresponde ao padrão e não cria notificação, com valor que corresponde
  [Theory]
  [InlineData("123.456.789-00")]
  [InlineData("12.345.678/0001-01")]
  public void IsCpfCnpj_ShouldNotAddNotification_WhenValueIsCpfCnpj(string valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = valueParam;

    // Act
    contract.IsCpfCnpj(value, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }
}
