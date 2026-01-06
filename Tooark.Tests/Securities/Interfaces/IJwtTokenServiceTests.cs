using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Moq;
using Tooark.Securities.Dtos;
using Tooark.Securities.Interfaces;

namespace Tooark.Tests.Securities.Interfaces;

public class IJwtTokenServiceTests
{
  // Teste para criar um token JWT
  [Fact]
  public void Create_ShouldReturnTokenString()
  {
    // Arrange
    var mockService = new Mock<IJwtTokenService>();
    var jwtData = new JwtTokenDto("a", "b", "c");
    var expectedToken = "fake.jwt.token";
    mockService.Setup(s => s.Create(It.IsAny<JwtTokenDto>(), It.IsAny<string>(), It.IsAny<IEnumerable<Claim>>())).Returns(expectedToken);

    // Act
    var result = mockService.Object.Create(jwtData);

    // Assert
    Assert.Equal(expectedToken, result);
  }

  // Teste para validar um token JWT
  [Fact]
  public void Validate_ShouldReturnUserTokenDto()
  {
    // Arrange
    var claims = new[]
    {
      new Claim("id", "1"),
      new Claim("login", "user-test"),
      new Claim("security", "sec")
    };
    var mockService = new Mock<IJwtTokenService>();
    var token = "fake.jwt.token";
    var expectedUserToken = new UserTokenDto(new JwtSecurityToken(claims: claims));
    mockService.Setup(s => s.Validate(token, It.IsAny<string>())).Returns(expectedUserToken);

    // Act
    var result = mockService.Object.Validate(token);

    // Assert
    Assert.Equal(expectedUserToken, result);
  }
}
