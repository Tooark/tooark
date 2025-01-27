using Tooark.Validations;

namespace Tooark.Tests.Validations;

public class EmailValidationContractTests
{
  // Teste para validar se o valor corresponde ao padrão e cria notificação, com valor que não corresponde
  [Theory]
  [InlineData(".test@example.com")] // Email iniciado com .
  [InlineData("-test@example.com")] // Email iniciado com -
  [InlineData("_test@example.com")] // Email iniciado com _
  [InlineData("test.@example.com")] // Email terminado com .
  [InlineData("test-@example.com")] // Email terminado com -
  [InlineData("test_@example.com")] // Email terminado com _
  [InlineData("te!st@example.com")] // Email que carácter especial !
  [InlineData("te#st@example.com")] // Email que carácter especial #
  [InlineData("te$st@example.com")] // Email que carácter especial $
  [InlineData("teçst@example.com")] // Email que carácter especial ç
  [InlineData("test@@example.com")] // Email com mais de um @
  [InlineData(".@example.com")] // Email com apenas .
  [InlineData("teste@.")] // Domínio com apenas .
  [InlineData("test@example@.com")] // Domínio com @
  [InlineData("test@.example.com")] // Domínio iniciado com .
  [InlineData("test@-example.com")] // Domínio iniciado com -
  [InlineData("test@_example.com")] // Domínio iniciado com _
  [InlineData("test@example-.com")] // Domínio terminado com -
  [InlineData("test@example_.com")] // Domínio terminado com _
  [InlineData("teste@exa!ple.com")] // Domínio que carácter especial !
  [InlineData("teste@exa#ple.com")] // Domínio que carácter especial #
  [InlineData("teste@exa$ple.com")] // Domínio que carácter especial $
  [InlineData("teste@exaçple.com")] // Domínio que carácter especial ç
  [InlineData("test@example.-com")] // Domínio iniciado com -
  [InlineData("test@example._com")] // Domínio iniciado com _
  [InlineData("test@example.com-")] // Domínio terminado com -
  [InlineData("test@example.com_")] // Domínio terminado com _
  [InlineData("test@example.com.")] // Domínio terminado com .
  [InlineData("test@exaple..com")] // Domínio com dois .
  [InlineData("test@example")] // Domínio sem extensão
  [InlineData("test@.com")] // Domínio ausente apenas extensão
  [InlineData("test@")] // Domínio ausente
  public void IsEmail_ShouldAddNotification_WhenValueNotIsEmail(string? valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = valueParam!;

    // Act
    contract.IsEmail(value, property);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o valor corresponde ao padrão e não cria notificação, com valor que corresponde
  [Theory]
  [InlineData("teste@example.com")] // Email válido
  [InlineData("te.te@example.com")] // Email válido
  [InlineData("te-te@example.com")] // Email válido
  [InlineData("te_te@example.com")] // Email válido
  [InlineData("teste@exa-ple.com")] // Email válido
  public void IsEmail_ShouldNotAddNotification_WhenValueIsEmail(string valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = valueParam;

    // Act
    contract.IsEmail(value, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }

  // Teste para validar se o valor corresponde ao padrão e cria notificação, com valor que não corresponde
  [Theory]
  [InlineData(".test@example.com")] // Email iniciado com .
  [InlineData("-test@example.com")] // Email iniciado com -
  [InlineData("_test@example.com")] // Email iniciado com _
  [InlineData("test.@example.com")] // Email terminado com .
  [InlineData("test-@example.com")] // Email terminado com -
  [InlineData("test_@example.com")] // Email terminado com _
  [InlineData("te!st@example.com")] // Email que carácter especial !
  [InlineData("te#st@example.com")] // Email que carácter especial #
  [InlineData("te$st@example.com")] // Email que carácter especial $
  [InlineData("teçst@example.com")] // Email que carácter especial ç
  [InlineData("test@@example.com")] // Email com mais de um @
  [InlineData(".@example.com")] // Email com apenas .
  [InlineData("teste@.")] // Domínio com apenas .
  [InlineData("test@example@.com")] // Domínio com @
  [InlineData("test@.example.com")] // Domínio iniciado com .
  [InlineData("test@-example.com")] // Domínio iniciado com -
  [InlineData("test@_example.com")] // Domínio iniciado com _
  [InlineData("test@example-.com")] // Domínio terminado com -
  [InlineData("test@example_.com")] // Domínio terminado com _
  [InlineData("teste@exa!ple.com")] // Domínio que carácter especial !
  [InlineData("teste@exa#ple.com")] // Domínio que carácter especial #
  [InlineData("teste@exa$ple.com")] // Domínio que carácter especial $
  [InlineData("teste@exaçple.com")] // Domínio que carácter especial ç
  [InlineData("test@example.-com")] // Domínio iniciado com -
  [InlineData("test@example._com")] // Domínio iniciado com _
  [InlineData("test@example.com-")] // Domínio terminado com -
  [InlineData("test@example.com_")] // Domínio terminado com _
  [InlineData("test@example.com.")] // Domínio terminado com .
  [InlineData("test@exaple..com")] // Domínio com dois .
  [InlineData("test@example")] // Domínio sem extensão
  [InlineData("test@.com")] // Domínio ausente apenas extensão
  [InlineData("test@")] // Domínio ausente
  public void IsEmailOrEmpty_ShouldAddNotification_WhenValueNotIsEmailOrEmpty(string valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = valueParam;

    // Act
    contract.IsEmailOrEmpty(value, property);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o valor corresponde ao padrão e não cria notificação, com valor que corresponde
  [Theory]
  [InlineData("teste@example.com")] // Email válido
  [InlineData("te.te@example.com")] // Email válido
  [InlineData("te-te@example.com")] // Email válido
  [InlineData("te_te@example.com")] // Email válido
  [InlineData("teste@exa-ple.com")] // Email válido
  [InlineData("")] // Email vazio
  [InlineData(null)] // Email nulo
  public void IsEmailOrEmpty_ShouldNotAddNotification_WhenValueIsEmailOrEmpty(string? valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = valueParam!;

    // Act
    contract.IsEmailOrEmpty(value, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }

  // Teste para validar se o valor corresponde ao padrão e cria notificação, com valor que não corresponde
  [Theory]
  [InlineData("teste@example.com")] // Email válido
  [InlineData("teste@exa-ple.com")] // Email válido
  [InlineData("@@example.com")] // Email com mais de um @
  [InlineData(".@example.com")] // Email com apenas .
  [InlineData("@.")] // Domínio com apenas .
  [InlineData("@example@.com")] // Domínio com @
  [InlineData("@.example.com")] // Domínio iniciado com .
  [InlineData("@-example.com")] // Domínio iniciado com -
  [InlineData("@_example.com")] // Domínio iniciado com _
  [InlineData("@example-.com")] // Domínio terminado com -
  [InlineData("@example_.com")] // Domínio terminado com _
  [InlineData("@exa!ple.com")] // Domínio que carácter especial !
  [InlineData("@exa#ple.com")] // Domínio que carácter especial #
  [InlineData("@exa$ple.com")] // Domínio que carácter especial $
  [InlineData("@exaçple.com")] // Domínio que carácter especial ç
  [InlineData("@example.-com")] // Domínio iniciado com -
  [InlineData("@example._com")] // Domínio iniciado com _
  [InlineData("@example.com-")] // Domínio terminado com -
  [InlineData("@example.com_")] // Domínio terminado com _
  [InlineData("@example.com.")] // Domínio terminado com .
  [InlineData("@exaple..com")] // Domínio com dois .
  [InlineData("@example")] // Domínio sem extensão
  [InlineData("@.com")] // Domínio ausente apenas extensão
  [InlineData("@")] // Domínio ausente
  [InlineData("")] // Email vazio
  [InlineData(null)] // Email nulo
  public void IsEmailDomain_ShouldAddNotification_WhenValueNotIsEmailDomain(string? valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = valueParam!;

    // Act
    contract.IsEmailDomain(value, property);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o valor corresponde ao padrão e não cria notificação, com valor que corresponde
  [Theory]
  [InlineData("@example.com")] // Email válido
  [InlineData("@exa-ple.com")] // Email válido
  public void IsEmailDomain_ShouldNotAddNotification_WhenValueIsEmailDomain(string valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = valueParam;

    // Act
    contract.IsEmailDomain(value, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }
  
  // Teste para validar se o valor corresponde ao padrão e cria notificação, com valor que não corresponde
  [Theory]
  [InlineData("teste@example.com")] // Email válido
  [InlineData("teste@exa-ple.com")] // Email válido
  [InlineData("@@example.com")] // Email com mais de um @
  [InlineData(".@example.com")] // Email com apenas .
  [InlineData("@.")] // Domínio com apenas .
  [InlineData("@example@.com")] // Domínio com @
  [InlineData("@.example.com")] // Domínio iniciado com .
  [InlineData("@-example.com")] // Domínio iniciado com -
  [InlineData("@_example.com")] // Domínio iniciado com _
  [InlineData("@example-.com")] // Domínio terminado com -
  [InlineData("@example_.com")] // Domínio terminado com _
  [InlineData("@exa!ple.com")] // Domínio que carácter especial !
  [InlineData("@exa#ple.com")] // Domínio que carácter especial #
  [InlineData("@exa$ple.com")] // Domínio que carácter especial $
  [InlineData("@exaçple.com")] // Domínio que carácter especial ç
  [InlineData("@example.-com")] // Domínio iniciado com -
  [InlineData("@example._com")] // Domínio iniciado com _
  [InlineData("@example.com-")] // Domínio terminado com -
  [InlineData("@example.com_")] // Domínio terminado com _
  [InlineData("@example.com.")] // Domínio terminado com .
  [InlineData("@exaple..com")] // Domínio com dois .
  [InlineData("@example")] // Domínio sem extensão
  [InlineData("@.com")] // Domínio ausente apenas extensão
  [InlineData("@")] // Domínio ausente
  public void IsEmailDomainOrEmpty_ShouldAddNotification_WhenValueNotIsEmailDomainOrEmpty(string valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = valueParam;

    // Act
    contract.IsEmailDomainOrEmpty(value, property);

    // Assert
    Assert.Single(contract.Notifications);
    Assert.Equal(property, contract.Notifications.First().Key);
  }

  // Teste para validar se o valor corresponde ao padrão e não cria notificação, com valor que corresponde
  [Theory]
  [InlineData("@example.com")] // Email válido
  [InlineData("@exa-ple.com")] // Email válido
  [InlineData("")] // Email vazio
  [InlineData(null)] // Email nulo
  public void IsEmailDomainOrEmpty_ShouldNotAddNotification_WhenValueIsEmailDomainOrEmpty(string? valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var contract = new Contract();
    string value = valueParam!;

    // Act
    contract.IsEmailDomainOrEmpty(value, property);

    // Assert
    Assert.Empty(contract.Notifications);
  }
}
