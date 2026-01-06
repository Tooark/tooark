using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Tooark.Securities.Dtos;

namespace Tooark.Tests.Securities.Dtos;

public class UserTokenDtoTests
{
  // Método auxiliar para criar um JwtSecurityToken com os dados necessários
  private static JwtSecurityToken CreateToken(string id, string login, string security)
  {
    var claims = new[]
    {
      new Claim("id", id),
      new Claim("login", login),
      new Claim("security", security)
    };

    return new JwtSecurityToken(claims: claims);
  }

  // Teste construtor com JwtSecurityToken
  [Fact]
  public void Constructor_WithToken_SetsPropertiesCorrectly()
  {
    // Arrange
    var token = CreateToken("1", "user", "sec");

    // Act
    var dto = new UserTokenDto(token);

    // Assert
    Assert.Equal("1", dto.Id);
    Assert.Equal("user", dto.Login);
    Assert.Equal("sec", dto.Security);
    Assert.Equal(string.Empty, dto.ErrorToken);
  }

  // Teste construtor com string erro
  [Fact]
  public void Constructor_WithError_SetsErrorToken()
  {
    // Arrange
    var errMessage = "error message";

    // Act
    var dto = new UserTokenDto(errMessage);

    // Assert
    Assert.Equal(errMessage, dto.ErrorToken);
    Assert.Equal(string.Empty, dto.Id);
    Assert.Equal(string.Empty, dto.Login);
    Assert.Equal(string.Empty, dto.Security);
  }

  // Teste função para obter Guid do Id com Id válido
  [Fact]
  public void GetGuidId_ReturnsGuid_WhenValid()
  {
    // Arrange
    var id = Guid.NewGuid();
    var token = CreateToken(id.ToString(), "user", "sec");

    // Act
    var dto = new UserTokenDto(token);

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
    var token = CreateToken(id.ToString(), "user", "sec");

    // Act
    var dto = new UserTokenDto(token);

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
    var token = CreateToken(id.ToString(), "user", "sec");

    // Act
    var dto = new UserTokenDto(token);

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
    var token = CreateToken(id.ToString(), "user", "sec");

    // Act
    var dto = new UserTokenDto(token);

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
    var token = CreateToken("sec", "user", security.ToString());

    // Act
    var dto = new UserTokenDto(token);

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
    var token = CreateToken("sec", "user", security.ToString());

    // Act
    var dto = new UserTokenDto(token);

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
    var token = CreateToken("sec", "user", security.ToString());

    // Act
    var dto = new UserTokenDto(token);

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
    var token = CreateToken("sec", "user", security.ToString());

    // Act
    var dto = new UserTokenDto(token);

    // Assert
    Assert.Equal(security, dto.Security);
    Assert.Equal(0, dto.GetIntSecurity);
  }

  // Teste: construtor deve lançar se alguma claim esperada estiver ausente
  [Fact]
  public void Constructor_MissingClaim_ThrowsInvalidOperationException()
  {
    // Arrange: token sem claim "id"
    var claims = new[]
    {
      new Claim("login", "user")
      // note: intentionally omitting "id" and "security"
    };

    var token = new JwtSecurityToken(claims: claims);

    // Act & Assert
    Assert.Throws<InvalidOperationException>(() => new UserTokenDto(token));
  }
}
