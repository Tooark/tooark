using System.Security.Cryptography;
using Tooark.Securities;
using Tooark.Securities.Dtos;
using Tooark.Securities.Options;
using MEOptions = Microsoft.Extensions.Options;

namespace Tooark.Tests.Securities;

public class JwtTokenServiceTests
{
  // Opções de configuração do JWT para testes simétricos
  private static JwtOptions GetSymmetricOptions() => new()
  {
    Algorithm = "HS256",
    Secret = "mysupersecretkey1234567890ABCDEF",
    Issuer = "TestIssuer",
    Audience = "TestAudience",
    ExpirationTime = 10
  };

  // Opções de configuração do JWT para testes assimétricos RS256
  private static JwtOptions GetAsymmetricRSOptions()
  {
    // Gera um par de chaves RSA temporárias para uso nos testes e exporta
    // para PKCS#8 (private) e SubjectPublicKeyInfo (public), codificando em Base64.
    using var rsa = RSA.Create(2048);

    var privateKeyBytes = rsa.ExportPkcs8PrivateKey();
    var publicKeyBytes = rsa.ExportSubjectPublicKeyInfo();

    return new JwtOptions
    {
      Algorithm = "RS256",
      PrivateKey = Convert.ToBase64String(privateKeyBytes),
      PublicKey = Convert.ToBase64String(publicKeyBytes),
      Issuer = "TestIssuer",
      Audience = "TestAudience",
      ExpirationTime = 10
    };
  }

  // Opções de configuração do JWT para testes assimétricos PS256
  private static JwtOptions GetAsymmetricPSOptions()
  {
    // Gera um par de chaves RSA temporárias para uso nos testes e exporta
    // para PKCS#8 (private) e SubjectPublicKeyInfo (public), codificando em Base64.
    using var rsa = RSA.Create(2048);

    var privateKeyBytes = rsa.ExportPkcs8PrivateKey();
    var publicKeyBytes = rsa.ExportSubjectPublicKeyInfo();

    return new JwtOptions
    {
      Algorithm = "PS256",
      PrivateKey = Convert.ToBase64String(privateKeyBytes),
      PublicKey = Convert.ToBase64String(publicKeyBytes),
      Issuer = "TestIssuer",
      Audience = "TestAudience",
      ExpirationTime = 10
    };
  }

  // Opções de configuração do JWT para testes assimétricos ES256
  private static JwtOptions GetAsymmetricESOptions()
  {
    // Gera um par de chaves RSA temporárias para uso nos testes e exporta
    // para PKCS#8 (private) e SubjectPublicKeyInfo (public), codificando em Base64.
    using var ecdsa = ECDsa.Create();

    var privateKeyBytes = ecdsa.ExportPkcs8PrivateKey();
    var publicKeyBytes = ecdsa.ExportSubjectPublicKeyInfo();

    return new JwtOptions
    {
      Algorithm = "ES256",
      PrivateKey = Convert.ToBase64String(privateKeyBytes),
      PublicKey = Convert.ToBase64String(publicKeyBytes),
      Issuer = "TestIssuer",
      Audience = "TestAudience",
      ExpirationTime = 10
    };
  }

  // DTO de token JWT para testes
  private static JwtTokenDto GetTokenDto() => new(1, "user", 1);

  // Token JWT pré-gerado para testes de expiração
  private static readonly string JwtTokenExpired = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjEiLCJsb2dpbiI6InVzZXIiLCJzZWN1cml0eSI6IjEiLCJuYmYiOjE3NjE5Mjg0MDgsImV4cCI6MTc2MTkyODQ1MywiaWF0IjoxNzYxOTI4NDA4LCJpc3MiOiJUZXN0SXNzdWVyIiwiYXVkIjoiVGVzdEF1ZGllbmNlIn0.M1iGoekiVJ0RnCMgk52Kc8rWjYzkFa5P0xakILzXCsc";

  // Token JWT malformado para testes de erro
  private static readonly string MalformedToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c";

  // Opções inválidas para cenários de erro
  private class NullOptions : MEOptions.IOptions<JwtOptions>
  {
    public JwtOptions Value => null!;
  }

  // Teste de criação e validação de token JWT simétrico
  [Fact]
  public void Create_And_Validate_Token_Symmetric_Should_Succeed()
  {
    // Arrange
    var options = MEOptions.Options.Create(GetSymmetricOptions());
    var service = new JwtTokenService(options);
    var dto = GetTokenDto();

    // Act
    var token = service.Create(dto);
    var result = service.Validate(token);

    // Assert
    Assert.NotNull(token);
    Assert.NotNull(result);
    Assert.Equal("", result.ErrorToken);
    Assert.Equal("1", result.Id);
    Assert.Equal("user", result.Login);
    Assert.Equal("1", result.Security);
  }

  // Teste de criação e validação de token JWT assimétrico RS
  [Fact]
  public void Create_And_Validate_Token_Asymmetric_RS_Should_Succeed()
  {
    // Arrange
    var options = MEOptions.Options.Create(GetAsymmetricRSOptions());
    var service = new JwtTokenService(options);
    var dto = GetTokenDto();

    // Act
    var token = service.Create(dto);
    var result = service.Validate(token);

    // Assert
    Assert.NotNull(token);
    Assert.NotNull(result);
    Assert.Equal("", result.ErrorToken);
    Assert.Equal("1", result.Id);
    Assert.Equal("user", result.Login);
    Assert.Equal("1", result.Security);
  }

  // Teste de criação e validação de token JWT assimétrico PS
  [Fact]
  public void Create_And_Validate_Token_Asymmetric_PS_Should_Succeed()
  {
    // Arrange
    var options = MEOptions.Options.Create(GetAsymmetricPSOptions());
    var service = new JwtTokenService(options);
    var dto = GetTokenDto();

    // Act
    var token = service.Create(dto);
    var result = service.Validate(token);

    // Assert
    Assert.NotNull(token);
    Assert.NotNull(result);
    Assert.Equal("", result.ErrorToken);
    Assert.Equal("1", result.Id);
    Assert.Equal("user", result.Login);
    Assert.Equal("1", result.Security);
  }

  // Teste de criação e validação de token JWT assimétrico ES
  [Fact]
  public void Create_And_Validate_Token_Asymmetric_ES_Should_Succeed()
  {
    // Arrange
    var options = MEOptions.Options.Create(GetAsymmetricESOptions());
    var service = new JwtTokenService(options);
    var dto = GetTokenDto();

    // Act
    var token = service.Create(dto);
    var result = service.Validate(token);

    // Assert
    Assert.NotNull(token);
    Assert.NotNull(result);
    Assert.Equal("", result.ErrorToken);
    Assert.Equal("1", result.Id);
    Assert.Equal("user", result.Login);
    Assert.Equal("1", result.Security);
  }

  // Teste de validação de token JWT com dados inválidos
  [Fact]
  public void Validate_Invalid_Token_Should_Return_Error()
  {
    // Arrange
    var options = MEOptions.Options.Create(GetSymmetricOptions());
    var service = new JwtTokenService(options);
    var invalidToken = "invalid.token.value";

    // Act
    var result = service.Validate(invalidToken);

    // Assert
    Assert.NotNull(result);
    Assert.NotNull(result.ErrorToken);
  }

  // Teste de validação de token JWT com token expirado
  [Fact]
  public void Validate_Expired_Token_Should_Return_ExpiredError()
  {
    // Arrange
    var options = MEOptions.Options.Create(GetSymmetricOptions());
    var service = new JwtTokenService(options);
    var token = JwtTokenExpired;

    // Act
    var result = service.Validate(token);

    // Assert
    Assert.NotNull(result);
    Assert.Equal("Token.Expired", result.ErrorToken);
  }

  // Teste de construtor com opções nulas
  [Fact]
  public void Constructor_WithNullOptions_ShouldThrowArgumentNullException()
  {
    // Arrange
    var options = new NullOptions();

    // Act & Assert
    var ex = Assert.Throws<ArgumentNullException>(() => new JwtTokenService(options));
    Assert.Contains("Options.NotConfigured", ex.Message, StringComparison.OrdinalIgnoreCase);
  }

  // Teste de construtor simétrico sem secret configurado
  [Fact]
  public void Constructor_SymmetricAlgorithmWithoutSecret_ShouldThrowArgumentException()
  {
    // Arrange
    var jwtOptions = new JwtOptions
    {
      Algorithm = "HS256",
      Secret = null
    };

    var options = MEOptions.Options.Create(jwtOptions);

    // Act & Assert
    var ex = Assert.Throws<ArgumentException>(() => new JwtTokenService(options));
    Assert.Equal("Jwt.KeyNotConfigured", ex.Message);
  }

  // Teste de construtor assimétrico sem chave pública
  [Fact]
  public void Constructor_AsymmetricWithoutPublicKey_ShouldThrowArgumentException()
  {
    // Arrange
    var jwtOptions = new JwtOptions
    {
      Algorithm = "RS256",
      PublicKey = null,
      PrivateKey = null
    };

    var options = MEOptions.Options.Create(jwtOptions);

    // Act & Assert
    var ex = Assert.Throws<ArgumentException>(() => new JwtTokenService(options));
    Assert.Equal("Jwt.KeyNotConfigured", ex.Message);
  }

  // Teste de construtor assimétrico RSA com chave menor que 2048 bits
  [Fact]
  public void Constructor_AsymmetricRS_WithSmallKey_ShouldThrowInvalidKeySize()
  {
    // Arrange
    using var rsa = RSA.Create(1024);

    var privateKeyBytes = rsa.ExportPkcs8PrivateKey();
    var publicKeyBytes = rsa.ExportSubjectPublicKeyInfo();

    var jwtOptions = new JwtOptions
    {
      Algorithm = "RS256",
      PrivateKey = Convert.ToBase64String(privateKeyBytes),
      PublicKey = Convert.ToBase64String(publicKeyBytes)
    };

    var options = MEOptions.Options.Create(jwtOptions);

    // Act & Assert
    var ex = Assert.Throws<ArgumentException>(() => new JwtTokenService(options));
    Assert.Equal("Jwt.InvalidKeySize", ex.Message);
  }

  // Teste de construtor ECDsa com curva/tamanho incorreto
  [Fact]
  public void Constructor_AsymmetricES_WithWrongCurve_ShouldThrowInvalidKeyCurve()
  {
    // Arrange
    using var ecdsa = ECDsa.Create(ECCurve.NamedCurves.nistP256);

    var privateKeyBytes = ecdsa.ExportPkcs8PrivateKey();
    var publicKeyBytes = ecdsa.ExportSubjectPublicKeyInfo();

    var jwtOptions = new JwtOptions
    {
      Algorithm = "ES384",
      PrivateKey = Convert.ToBase64String(privateKeyBytes),
      PublicKey = Convert.ToBase64String(publicKeyBytes)
    };

    var options = MEOptions.Options.Create(jwtOptions);

    // Act & Assert
    var ex = Assert.Throws<ArgumentException>(() => new JwtTokenService(options));
    Assert.Equal("Jwt.InvalidKeyCurve", ex.Message);
  }

  // Teste de construtor RSA com chaves inválidas
  [Fact]
  public void Constructor_AsymmetricRS_WithInvalidKeys_ShouldThrowInvalidKey()
  {
    // Arrange
    var invalidBase64 = Convert.ToBase64String(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 });

    var jwtOptions = new JwtOptions
    {
      Algorithm = "RS256",
      PrivateKey = invalidBase64,
      PublicKey = invalidBase64
    };

    var options = MEOptions.Options.Create(jwtOptions);

    // Act & Assert
    var ex = Assert.Throws<ArgumentException>(() => new JwtTokenService(options));
    Assert.Equal("Jwt.InvalidKey", ex.Message);
  }

  // Teste de criação de token com chave pública apenas (sem chave privada)
  [Fact]
  public void Create_AsymmetricPublicKeyOnly_ShouldThrowKeyNotConfigured()
  {
    // Arrange
    using var rsa = RSA.Create(2048);

    var publicKeyBytes = rsa.ExportSubjectPublicKeyInfo();

    var jwtOptions = new JwtOptions
    {
      Algorithm = "RS256",
      PublicKey = Convert.ToBase64String(publicKeyBytes),
      PrivateKey = null
    };

    var options = MEOptions.Options.Create(jwtOptions);
    var service = new JwtTokenService(options);

    // Act & Assert
    var ex = Assert.Throws<ArgumentException>(() => service.Create(GetTokenDto()));
    Assert.Equal("Jwt.KeyNotConfigured", ex.Message);
  }

  // Teste de validação com assinatura inválida
  [Fact]
  public void Validate_WithWrongSignature_ShouldReturnInvalidSignatureError()
  {
    // Arrange
    var optionsSigning = MEOptions.Options.Create(new JwtOptions
    {
      Algorithm = "HS256",
      Secret = "mysupersecretkey1234567890ABCDEF",
      Issuer = "TestIssuer",
      Audience = "TestAudience",
      ExpirationTime = 10
    });

    var signingService = new JwtTokenService(optionsSigning);
    var token = signingService.Create(GetTokenDto());
    var optionsValidation = MEOptions.Options.Create(new JwtOptions
    {
      Algorithm = "HS256",
      Secret = "anothersecretkey1234567890ABCDE",
      Issuer = "TestIssuer",
      Audience = "TestAudience",
      ExpirationTime = 10
    });

    var validationService = new JwtTokenService(optionsValidation);

    // Act
    var result = validationService.Validate(token);

    // Assert
    Assert.Equal("Token.InvalidSignature", result.ErrorToken);
  }

  // Teste de construtor RSA com chave pública menor que 2048 bits
  [Fact]
  public void Constructor_AsymmetricRS_WithSmallPublicKey_ShouldThrowInvalidKeySize()
  {
    // Arrange
    using var rsaValid = RSA.Create(2048);
    using var rsaSmall = RSA.Create(1024);

    var privateKeyBytes = rsaValid.ExportPkcs8PrivateKey();
    var publicKeyBytes = rsaSmall.ExportSubjectPublicKeyInfo();

    var jwtOptions = new JwtOptions
    {
      Algorithm = "RS256",
      PrivateKey = Convert.ToBase64String(privateKeyBytes),
      PublicKey = Convert.ToBase64String(publicKeyBytes)
    };

    var options = MEOptions.Options.Create(jwtOptions);

    // Act & Assert
    var ex = Assert.Throws<ArgumentException>(() => new JwtTokenService(options));
    Assert.Equal("Jwt.InvalidKeySize", ex.Message);
  }

  // Teste de construtor ECDsa com curva incorreta apenas na chave pública
  [Fact]
  public void Constructor_AsymmetricES_WithWrongPublicKeyCurve_ShouldThrowInvalidKeyCurve()
  {
    // Arrange
    using var ecdsaValid = ECDsa.Create(ECCurve.NamedCurves.nistP384);
    using var ecdsaWrong = ECDsa.Create(ECCurve.NamedCurves.nistP256);

    var privateKeyBytes = ecdsaValid.ExportPkcs8PrivateKey();
    var publicKeyBytes = ecdsaWrong.ExportSubjectPublicKeyInfo();

    var jwtOptions = new JwtOptions
    {
      Algorithm = "ES384",
      PrivateKey = Convert.ToBase64String(privateKeyBytes),
      PublicKey = Convert.ToBase64String(publicKeyBytes)
    };

    var options = MEOptions.Options.Create(jwtOptions);

    // Act & Assert
    var ex = Assert.Throws<ArgumentException>(() => new JwtTokenService(options));
    Assert.Equal("Jwt.InvalidKeyCurve", ex.Message);
  }

  // Teste de construtor ECDsa com chaves inválidas
  [Fact]
  public void Constructor_AsymmetricES_WithInvalidKeys_ShouldThrowInvalidKey()
  {
    // Arrange
    var invalidBase64 = Convert.ToBase64String(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 });

    var jwtOptions = new JwtOptions
    {
      Algorithm = "ES256",
      PrivateKey = invalidBase64,
      PublicKey = invalidBase64
    };

    var options = MEOptions.Options.Create(jwtOptions);

    // Act & Assert
    var ex = Assert.Throws<ArgumentException>(() => new JwtTokenService(options));
    Assert.Equal("Jwt.InvalidKey", ex.Message);
  }

  // Teste de validação com apenas chave privada configurada
  [Fact]
  public void Validate_WithPrivateKeyOnly_ShouldThrowKeyNotConfigured()
  {
    // Arrange
    using var rsa = RSA.Create(2048);
    var privateKeyBytes = rsa.ExportPkcs8PrivateKey();

    var jwtOptions = new JwtOptions
    {
      Algorithm = "RS256",
      PrivateKey = Convert.ToBase64String(privateKeyBytes)
    };

    var options = MEOptions.Options.Create(jwtOptions);
    var service = new JwtTokenService(options);

    // Act & Assert
    var ex = Assert.Throws<ArgumentException>(() => service.Validate("any.token.here"));
    Assert.Equal("Jwt.KeyNotConfigured", ex.Message);
  }

  // Teste de validação que retorna Token.InvalidSignature
  [Fact]
  public void Validate_WithMalformedToken_ShouldReturnTokenInvalidSignature()
  {
    // Arrange
    var options = MEOptions.Options.Create(GetSymmetricOptions());
    var service = new JwtTokenService(options);
    var malformedToken = MalformedToken;

    // Act
    var result = service.Validate(malformedToken);

    // Assert
    Assert.NotNull(result.ErrorToken);
    Assert.Equal("Token.InvalidSignature", result.ErrorToken);
  }

  // Teste de validação com token que causa ArgumentException
  [Fact]
  public void Validate_WithEmptyToken_ShouldReturnTokenInvalid()
  {
    // Arrange
    var options = MEOptions.Options.Create(GetSymmetricOptions());
    var service = new JwtTokenService(options);

    // Act
    var result = service.Validate("");

    // Assert
    Assert.Equal("Token.Invalid", result.ErrorToken);
  }

  // Teste de criação de token com audience customizado
  [Fact]
  public void Create_WithCustomAudience_ShouldSucceed()
  {
    // Arrange
    var options = MEOptions.Options.Create(GetSymmetricOptions());
    var service = new JwtTokenService(options);
    var dto = GetTokenDto();

    // Act
    var token = service.Create(dto, audience: "CustomAudience");
    var result = service.Validate(token, audience: "CustomAudience");

    // Assert
    Assert.NotNull(token);
    Assert.Equal("", result.ErrorToken);
  }

  // Teste de criação de token com claims extras
  [Fact]
  public void Create_WithExtraClaims_ShouldSucceed()
  {
    // Arrange
    var options = MEOptions.Options.Create(GetSymmetricOptions());
    var service = new JwtTokenService(options);
    var dto = GetTokenDto();
    var extraClaims = new[] { new System.Security.Claims.Claim("custom", "value") };

    // Act
    var token = service.Create(dto, extraClaims: extraClaims);
    var result = service.Validate(token);

    // Assert
    Assert.NotNull(token);
    Assert.Equal("", result.ErrorToken);
  }

  // Teste de criação e validação sem Issuer e Audience configurados
  [Fact]
  public void Create_And_Validate_WithoutIssuerAndAudience_ShouldSucceed()
  {
    // Arrange
    var jwtOptions = new JwtOptions
    {
      Algorithm = "HS256",
      Secret = "mysupersecretkey1234567890ABCDEF",
      ExpirationTime = 10
    };
    var options = MEOptions.Options.Create(jwtOptions);
    var service = new JwtTokenService(options);
    var dto = GetTokenDto();

    // Act
    var token = service.Create(dto);
    var result = service.Validate(token);

    // Assert
    Assert.NotNull(token);
    Assert.Equal("", result.ErrorToken);
  }

  // Teste de validação com Issuers array configurado
  [Fact]
  public void Validate_WithIssuersArray_ShouldSucceed()
  {
    // Arrange
    var jwtOptions = new JwtOptions
    {
      Algorithm = "HS256",
      Secret = "mysupersecretkey1234567890ABCDEF",
      Issuer = "TestIssuer",
      Issuers = ["TestIssuer", "AnotherIssuer"],
      Audience = "TestAudience",
      ExpirationTime = 10
    };
    var options = MEOptions.Options.Create(jwtOptions);
    var service = new JwtTokenService(options);
    var dto = GetTokenDto();

    // Act
    var token = service.Create(dto);
    var result = service.Validate(token);

    // Assert
    Assert.NotNull(token);
    Assert.Equal("", result.ErrorToken);
  }

  // Teste de validação com Audiences array configurado
  [Fact]
  public void Validate_WithAudiencesArray_ShouldSucceed()
  {
    // Arrange
    var jwtOptions = new JwtOptions
    {
      Algorithm = "HS256",
      Secret = "mysupersecretkey1234567890ABCDEF",
      Issuer = "TestIssuer",
      Audience = "TestAudience",
      Audiences = ["TestAudience", "AnotherAudience"],
      ExpirationTime = 10
    };
    var options = MEOptions.Options.Create(jwtOptions);
    var service = new JwtTokenService(options);
    var dto = GetTokenDto();

    // Act
    var token = service.Create(dto);
    var result = service.Validate(token);

    // Assert
    Assert.NotNull(token);
    Assert.Equal("", result.ErrorToken);
  }

  // Teste de validação ECDsa com apenas chave pública (somente validação)
  [Fact]
  public void Validate_AsymmetricES_WithPublicKeyOnly_ShouldSucceed()
  {
    // Arrange
    using var ecdsa = ECDsa.Create(ECCurve.NamedCurves.nistP256);

    var privateKeyBytes = ecdsa.ExportPkcs8PrivateKey();
    var publicKeyBytes = ecdsa.ExportSubjectPublicKeyInfo();

    var createOptions = MEOptions.Options.Create(new JwtOptions
    {
      Algorithm = "ES256",
      PrivateKey = Convert.ToBase64String(privateKeyBytes),
      PublicKey = Convert.ToBase64String(publicKeyBytes),
      Issuer = "TestIssuer",
      Audience = "TestAudience",
      ExpirationTime = 10
    });

    var createService = new JwtTokenService(createOptions);
    var token = createService.Create(GetTokenDto());

    var validateOptions = MEOptions.Options.Create(new JwtOptions
    {
      Algorithm = "ES256",
      PublicKey = Convert.ToBase64String(publicKeyBytes),
      PrivateKey = null,
      Issuer = "TestIssuer",
      Audience = "TestAudience",
      ExpirationTime = 10
    });

    var validateService = new JwtTokenService(validateOptions);

    // Act
    var result = validateService.Validate(token);

    // Assert
    Assert.Equal("", result.ErrorToken);
  }
}
