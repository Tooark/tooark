using Tooark.Securities.Dtos;
using Tooark.Securities.Extensions;

namespace Tooark.Tests.Securities.Extensions;

public class RoleClaimsExtensionTests
{
  // Teste para buscar os claims de um JwtTokenDto
  [Fact]
  public void GetClaims_DeveRetornarClaimsCorretos()
  {
    // Arrange
    var id = Guid.NewGuid().ToString();
    var login = "usuarioTeste";
    var security = Guid.NewGuid().ToString();
    var jwtToken = new JwtTokenDto(id, login, security);

    // Act
    var claims = jwtToken.GetClaims().ToList();

    // Assert
    Assert.Equal(3, claims.Count);
    Assert.Contains(claims, c => c.Type == "id" && c.Value == id);
    Assert.Contains(claims, c => c.Type == "login" && c.Value == login);
    Assert.Contains(claims, c => c.Type == "security" && c.Value == security);
  }

  // Teste para buscar os claims de um JwtTokenDto com valores vazios
  [Fact]
  public void GetClaims_DeveRetornarClaimsComValoresVazios()
  {
    // Arrange
    var jwtToken = new JwtTokenDto("", "", "");

    // Act
    var claims = jwtToken.GetClaims().ToList();

    // Assert
    Assert.Equal(3, claims.Count);
    Assert.Contains(claims, c => c.Type == "id" && c.Value == "");
    Assert.Contains(claims, c => c.Type == "login" && c.Value == "");
    Assert.Contains(claims, c => c.Type == "security" && c.Value == "");
  }
}
