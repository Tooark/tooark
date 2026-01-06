using Tooark.Securities.Dtos;

namespace Tooark.Tests.Securities.Dtos;

public class JwtTokenDtoTests
{
  private readonly string IdString = "1";
  private readonly int IdInt = 1;
  private readonly Guid IdGuid = Guid.Parse("0ba9363f-dba9-4b82-9bde-7d04c53eac0d");
  private readonly string LoginString = "2";
  private readonly int LoginInt = 2;
  private readonly Guid LoginGuid = Guid.Parse("6cbcee90-8716-4912-a9cb-2c2578028ec7");
  private readonly string SecurityString = "3";
  private readonly int SecurityInt = 3;
  private readonly Guid SecurityGuid = Guid.Parse("45dc9b84-f7d7-4eb7-8325-86ba15b46bdb");


  // Teste com o construtor múltiplos tipos
  [Fact]
  public void Constructor_WithParameters_SetsPropertiesCorrectly()
  {
    // Arrange
    var id = IdInt;
    var login = LoginString;
    var security = SecurityGuid;

    // Act
    var dto = new JwtTokenDto(id, login, security);

    // Assert
    Assert.Equal(id.ToString(), dto.Id);
    Assert.Equal(login, dto.Login);
    Assert.Equal(security.ToString(), dto.Security);
  }

  // Teste com o construtor que aceita Guid
  [Fact]
  public void Constructor_WithGuidParameters_SetsPropertiesCorrectly()
  {
    // Arrange
    var id = IdGuid;
    var login = LoginGuid;
    var security = SecurityGuid;

    // Act
    var dto = new JwtTokenDto(id, login, security);

    // Assert
    Assert.Equal(id.ToString(), dto.Id);
    Assert.Equal(login.ToString(), dto.Login);
    Assert.Equal(security.ToString(), dto.Security);
  }

  // Teste com o construtor que aceita inteiros
  [Fact]
  public void Constructor_WithIntParameters_SetsPropertiesCorrectly()
  {
    // Arrange
    var id = IdInt;
    var login = LoginInt;
    var security = SecurityInt;

    // Act
    var dto = new JwtTokenDto(id, login, security);

    // Assert
    Assert.Equal(id.ToString(), dto.Id);
    Assert.Equal(login.ToString(), dto.Login);
    Assert.Equal(security.ToString(), dto.Security);
  }

  // Teste com o construtor que aceita strings
  [Fact]
  public void Constructor_WithStringParameters_SetsPropertiesCorrectly()
  {
    // Arrange
    var id = IdString;
    var login = LoginString;
    var security = SecurityString;

    // Act
    var dto = new JwtTokenDto(id, login, security);

    // Assert
    Assert.Equal(id, dto.Id);
    Assert.Equal(login, dto.Login);
    Assert.Equal(security, dto.Security);
  }

  // Teste com o construtor que aceita guid, string e guid
  [Fact]
  public void Constructor_WithGuidStringGuidParameters_SetsPropertiesCorrectly()
  {
    // Arrange
    var id = IdGuid;
    var login = LoginString;
    var security = SecurityGuid;

    // Act
    var dto = new JwtTokenDto(id, login, security);

    // Assert
    Assert.Equal(id.ToString(), dto.Id);
    Assert.Equal(login, dto.Login);
    Assert.Equal(security.ToString(), dto.Security);
  }

  // Teste com o construtor que aceita guid, string e int
  [Fact]
  public void Constructor_WithGuidStringIntParameters_SetsPropertiesCorrectly()
  {
    // Arrange
    var id = IdGuid;
    var login = LoginString;
    var security = SecurityInt;

    // Act
    var dto = new JwtTokenDto(id, login, security);

    // Assert
    Assert.Equal(id.ToString(), dto.Id);
    Assert.Equal(login, dto.Login);
    Assert.Equal(security.ToString(), dto.Security);
  }

  // Teste com o construtor que aceita int, string e guid
  [Fact]
  public void Constructor_WithIntStringGuidParameters_SetsPropertiesCorrectly()
  {
    // Arrange
    var id = IdInt;
    var login = LoginString;
    var security = SecurityGuid;

    // Act
    var dto = new JwtTokenDto(id, login, security);

    // Assert
    Assert.Equal(id.ToString(), dto.Id);
    Assert.Equal(login, dto.Login);
    Assert.Equal(security.ToString(), dto.Security);
  }

  // Teste com o construtor que aceita int, string e int
  [Fact]
  public void Constructor_WithIntStringIntParameters_SetsPropertiesCorrectly()
  {
    // Arrange
    var id = IdInt;
    var login = LoginString;
    var security = SecurityInt;

    // Act
    var dto = new JwtTokenDto(id, login, security);

    // Assert
    Assert.Equal(id.ToString(), dto.Id);
    Assert.Equal(login, dto.Login);
    Assert.Equal(security.ToString(), dto.Security);
  }

  // Teste função para obter Guid do Id com Id válido
  [Fact]
  public void GetGuidId_ReturnsGuid_WhenValid()
  {
    // Arrange
    var id = Guid.NewGuid();

    // Act
    var dto = new JwtTokenDto(id, "user", Guid.NewGuid());

    // Assert
    Assert.Equal(id.ToString(), dto.Id);
    Assert.Equal(id, dto.GetGuidId);
  }

  // Teste função para obter Guid do Id com Id inválido
  [Fact]
  public void GetGuidId_ReturnsEmpty_WhenInvalid()
  {
    // Arrange
    var id = "not-a-guid";

    // Act
    var dto = new JwtTokenDto(id, "user", "sec");

    // Assert
    Assert.Equal(id, dto.Id);
    Assert.Equal(Guid.Empty, dto.GetGuidId);
  }

  // Teste função para obter Int do Id com Id válido
  [Fact]
  public void GetIntId_ReturnsInt_WhenValid()
  {
    // Arrange
    var id = 123;

    // Act
    var dto = new JwtTokenDto(id, "user", 0);

    // Assert
    Assert.Equal(id.ToString(), dto.Id);
    Assert.Equal(id, dto.GetIntId);
  }

  // Teste função para obter Int do Id com Id inválido
  [Fact]
  public void GetIntId_ReturnsZero_WhenInvalid()
  {
    // Arrange
    var id = "not-a-int";

    // Act
    var dto = new JwtTokenDto(id, "user", "sec");

    // Assert
    Assert.Equal(id, dto.Id);
    Assert.Equal(0, dto.GetIntId);
  }

  // Teste função para obter Guid do Security com Security válido
  [Fact]
  public void GetGuidSecurity_ReturnsGuid_WhenValid()
  {
    // Arrange
    var security = Guid.NewGuid();

    // Act
    var dto = new JwtTokenDto(Guid.NewGuid(), "user", security);

    // Assert
    Assert.Equal(security.ToString(), dto.Security);
    Assert.Equal(security, dto.GetGuidSecurity);
  }

  // Teste função para obter Guid do Security com Security inválido
  [Fact]
  public void GetGuidSecurity_ReturnsEmpty_WhenInvalid()
  {
    // Arrange
    var security = "not-a-guid";

    // Act
    var dto = new JwtTokenDto("sec", "user", security);

    // Assert
    Assert.Equal(security, dto.Security);
    Assert.Equal(Guid.Empty, dto.GetGuidSecurity);
  }

  // Teste função para obter Int do Security com Security válido
  [Fact]
  public void GetIntSecurity_ReturnsInt_WhenValid()
  {
    // Arrange
    var security = 123;

    // Act
    var dto = new JwtTokenDto(0, "user", security);

    // Assert
    Assert.Equal(security.ToString(), dto.Security);
    Assert.Equal(security, dto.GetIntSecurity);
  }

  // Teste função para obter Int do Security com Security inválido
  [Fact]
  public void GetIntSecurity_ReturnsZero_WhenInvalid()
  {
    // Arrange
    var security = "not-a-int";

    // Act
    var dto = new JwtTokenDto("sec", "user", security);

    // Assert
    Assert.Equal(security, dto.Security);
    Assert.Equal(0, dto.GetIntSecurity);
  }

  // Teste com Id nulo — deve retornar Guid.Empty e 0 para os conversores
  [Fact]
  public void NullId_ParsersReturnDefaults()
  {
    // Arrange
    string? id = null;

    // Act
    var dto = new JwtTokenDto(id!, "user", "sec");

    // Assert
    Assert.Null(dto.Id);
    Assert.Equal(Guid.Empty, dto.GetGuidId);
    Assert.Equal(0, dto.GetIntId);
  }

  // Teste com Security nulo — deve retornar Guid.Empty e 0 para os conversores
  [Fact]
  public void NullSecurity_ParsersReturnDefaults()
  {
    // Arrange
    string? security = null;

    // Act
    var dto = new JwtTokenDto("1", "user", security!);

    // Assert
    Assert.Null(dto.Security);
    Assert.Equal(Guid.Empty, dto.GetGuidSecurity);
    Assert.Equal(0, dto.GetIntSecurity);
  }

  // Teste: alterar propriedades após criação e verificar parsers
  [Fact]
  public void SetProperties_AfterConstruction_ParsersReflectChanges()
  {
    // Arrange
    var dto = new JwtTokenDto("1", "user", "3")
    {
      // Act
      Id = Guid.NewGuid().ToString(),
      Security = 999.ToString()
    };

    // Assert
    Assert.NotEqual("1", dto.Id);
    Assert.NotEqual("3", dto.Security);
    Assert.NotEqual(Guid.Empty, dto.GetGuidId);
    Assert.Equal(999, dto.GetIntSecurity);
  }

  // Teste: Login nulo deve ser aceito e não afetar parsers
  [Fact]
  public void NullLogin_IsAllowed_ParsersUnaffected()
  {
    // Arrange
    var dto = new JwtTokenDto("1", null!, "3");

    // Act / Assert
    Assert.Null(dto.Login);
    Assert.Equal("1", dto.Id);
    Assert.Equal("3", dto.Security);
    Assert.Equal(1, dto.GetIntId);
    Assert.Equal(3, dto.GetIntSecurity);
  }
}
