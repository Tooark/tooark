using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Tooark.Securities.Dtos;
using Tooark.Securities.Extensions;
using Tooark.Securities.Interfaces;
using Tooark.Securities.Options;

namespace Tooark.Securities;

/// <summary>
/// Serviço para manipulação de tokens JWT.
/// </summary>
public class JwtTokenService : IJwtTokenService
{
  #region Private Properties

  /// <summary>
  /// Opções de configuração do JWT.
  /// </summary>
  private readonly JwtOptions _jwtOptions;

  /// <summary>
  /// Algoritmo de assinatura utilizado (ex: HS256, RS256).
  /// </summary>
  private readonly string _algorithm;

  /// <summary>
  /// Chave de segurança para criação de tokens.
  /// </summary>
  private readonly SecurityKey? _createKey;

  /// <summary>
  /// Chave de segurança para validação de tokens.
  /// </summary>
  private readonly SecurityKey? _validationKey;

  /// <summary>
  /// Indica se o token pode ser criado.
  /// </summary>
  private readonly bool _createToken = false;

  /// <summary>
  /// Indica se o token pode ser validado.
  /// </summary>
  private readonly bool _validateToken = false;

  #endregion

  #region Constructors

  /// <summary>
  /// Construtor do serviço de token JWT.
  /// </summary>
  /// <param name="jwtOptions">Opções de configuração do JWT.</param>
  public JwtTokenService(IOptions<JwtOptions> jwtOptions)
  {
    // Valida se as opções foram configuradas corretamente
    _jwtOptions = jwtOptions.Value
      ?? throw new ArgumentNullException(nameof(jwtOptions), "Options.NotConfigured");

    // Suporte a algoritmo e chave simétrica ou assimétrica
    _algorithm = _jwtOptions.Algorithm;

    // Verifica se o algoritmo é simétrico ou assimétrico
    if (_algorithm.StartsWith("HS")) // HMAC (simétrico)
    {
      var secret = _jwtOptions.Secret ?? throw new ArgumentException("Jwt.KeyNotConfigured");
      var keyBytes = Encoding.UTF8.GetBytes(secret);
      _createKey = new SymmetricSecurityKey(keyBytes);
      _validationKey = _createKey;

      _createToken = true;
      _validateToken = true;
    }
    else
    {
      // Verifica se a chave privada existe para permitir criação de token
      if (!string.IsNullOrWhiteSpace(_jwtOptions.PrivateKey))
      {
        _createToken = true;
      }

      // Valida se a chave pública existe para permitir validação de token
      if (!string.IsNullOrWhiteSpace(_jwtOptions.PublicKey))
      {
        _validateToken = true;
      }

      // Carrega chaves pública e privada, caso não exista a privada, usa a pública
      var privateKeyBytes = _createToken ? Convert.FromBase64String(_jwtOptions.PrivateKey!) : null;
      var publicKeyBytes = _validateToken ? Convert.FromBase64String(_jwtOptions.PublicKey!) : null;

      // Configura chaves de assinatura e validação de chave assimétrica
      if (_algorithm.StartsWith("RS") || _algorithm.StartsWith("PS")) // RSA ou PSS (assimétrico)
      {
        try
        {
          // Verifica se ao menos uma chave está configurada
          if(!_createToken && !_validateToken)
          {
            throw new ArgumentException("Jwt.KeyNotConfigured");
          }

          // Verifica se deve criar a chave de criação
          if (_createToken && privateKeyBytes != null)
          {
            // Carrega chave privada
            var privateRsa = RSA.Create();
            privateRsa.ImportPkcs8PrivateKey(privateKeyBytes, out _);

            // Valida tamanho mínimo da chave RSA
            if (privateRsa.KeySize < 2048)
            {
              throw new ArgumentException("Jwt.InvalidKeySize");
            }

            // Configura chave de criação
            _createKey = new RsaSecurityKey(privateRsa);
          }

          // Verifica se deve criar a chave de validação
          if (_validateToken && publicKeyBytes != null)
          {
            // Carrega chave pública
            var publicRsa = RSA.Create();
            publicRsa.ImportSubjectPublicKeyInfo(publicKeyBytes, out _);

            // Valida tamanho mínimo da chave RSA
            if (publicRsa.KeySize < 2048)
            {
              throw new ArgumentException("Jwt.InvalidKeySize");
            }

            // Configura chave de validação
            _validationKey = new RsaSecurityKey(publicRsa);
          }
        }
        catch (CryptographicException ex)
        {
          throw new ArgumentException("Jwt.InvalidKey", ex);
        }
      }
      else if (_algorithm.StartsWith("ES")) // ECDsa (assimétrico)
      {
        try
        {
          // Resolve o tamanho da chave ECDsa conforme o algoritmo (ES256/ES384/ES512)
          var keySize = int.TryParse(_algorithm.Split("ES")[1], out int ks) ? ks : 0;

          // Verifica se deve criar a chave de criação
          if (_createToken && privateKeyBytes != null)
          {
            // Carrega chave privada
            var privateEcdsa = ECDsa.Create();
            privateEcdsa.ImportPkcs8PrivateKey(privateKeyBytes, out _);

            // Valida curva da chave ECDsa conforme o algoritmo
            if (privateEcdsa.KeySize < keySize)
            {
              throw new ArgumentException("Jwt.InvalidKeyCurve");
            }

            // Configura chave de criação
            _createKey = new ECDsaSecurityKey(privateEcdsa);
          }

          // Verifica se deve criar a chave de validação
          if (_validateToken && publicKeyBytes != null)
          {
            // Carrega chave pública
            var publicEcdsa = ECDsa.Create();
            publicEcdsa.ImportSubjectPublicKeyInfo(publicKeyBytes, out _);

            // Valida curva da chave ECDsa conforme o algoritmo
            if (publicEcdsa.KeySize < keySize)
            {
              throw new ArgumentException("Jwt.InvalidKeyCurve");
            }

            // Configura chave de validação
            _validationKey = new ECDsaSecurityKey(publicEcdsa);
          }
        }
        catch (CryptographicException ex)
        {
          throw new ArgumentException("Jwt.InvalidKey", ex);
        }
      }
      else
      {
        throw new ArgumentException("Jwt.AlgorithmNotSupported");
      }
    }
  }

  #endregion

  #region Methods

  /// <summary>
  /// Cria um token JWT.
  /// </summary>
  /// <param name="data">Dados para incluir no token.</param>
  /// <param name="audience">Destinatário do token. Parâmetro opcional que sobrescreve o destinatário padrão do Options.</param>
  /// <param name="extraClaims">Claims adicionais para incluir no token. Parâmetro opcional para incluir claims extras no token.</param>
  /// <returns>Token JWT.</returns>
  public string Create(JwtTokenDto data, string? audience = null, IEnumerable<Claim>? extraClaims = null)
  {
    // Valida se pode criar o token
    if (!_createToken || _createKey == null)
    {
      throw new ArgumentException("Jwt.KeyNotConfigured");
    }

    // Configura propriedades do token
    var expiryTime = _jwtOptions.ExpirationTime;
    var baseClaims = data.GetClaims();

    // Adiciona claims extras, se fornecidos
    var claims = extraClaims is null
    ? baseClaims
    : baseClaims.Concat(extraClaims);

    var tokenHandler = new JwtSecurityTokenHandler();
    var tokenDescriptor = new SecurityTokenDescriptor
    {
      Expires = DateTime.UtcNow.AddMinutes(expiryTime),
      SigningCredentials = new SigningCredentials(_createKey, _algorithm),
      Subject = new ClaimsIdentity(claims)
    };

    // Define o público do token, se fornecido, caso contrário usa o padrão do Options
    var targetAudience = !string.IsNullOrWhiteSpace(audience)
    ? audience
    : _jwtOptions.Audience;

    // Define o público, se configurados
    if (!string.IsNullOrWhiteSpace(targetAudience))
    {
      tokenDescriptor.Audience = targetAudience;
    }

    // Define o emissor, se configurado
    if (!string.IsNullOrWhiteSpace(_jwtOptions.Issuer))
    {
      tokenDescriptor.Issuer = _jwtOptions.Issuer;
    }

    // Cria o token JWT
    var token = tokenHandler.CreateToken(tokenDescriptor);

    // Retorna o token como string
    return tokenHandler.WriteToken(token);
  }

  /// <summary>
  /// Valida um token JWT.
  /// </summary>
  /// <param name="token">Token JWT a ser validado.</param>
  /// <param name="audience">Destinatário do token. Parâmetro opcional que sobrescreve o destinatário padrão do Options.</param>
  /// <returns>Resultado da validação do token.</returns>
  public UserTokenDto Validate(string token, string? audience = null)
  {
    // Valida se pode validar o token
    if (!_validateToken || _validationKey == null)
    {
      throw new ArgumentException("Jwt.KeyNotConfigured");
    }

    // Configura o validador de token
    var tokenHandler = new JwtSecurityTokenHandler();

    // Define issuer e audiences efetivos com base nas opções e parâmetros
    var effectiveIssuer = _jwtOptions.Issuer;
    var effectiveIssuers = _jwtOptions.Issuers;

    // Se um audience for informado no parâmetro, ele tem prioridade sobre as opções
    var effectiveAudience = !string.IsNullOrWhiteSpace(audience)
      ? audience
      : _jwtOptions.Audience;
    var effectiveAudiences = !string.IsNullOrWhiteSpace(audience)
      ? [audience]
      : _jwtOptions.Audiences;

    // Verifica se deve validar issuer/audience com base em qualquer configuração disponível
    var issuers = !string.IsNullOrWhiteSpace(effectiveIssuer) || effectiveIssuers?.Length > 0;
    var audiences = !string.IsNullOrWhiteSpace(effectiveAudience) || effectiveAudiences?.Length > 0;

    try
    {
      var tokenParams = new TokenValidationParameters
      {
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = _validationKey,
        RequireExpirationTime = true,
        RequireSignedTokens = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        // Issuer
        ValidIssuer = effectiveIssuer,
        ValidIssuers = effectiveIssuers,
        // Audience
        ValidAudience = effectiveAudience,
        ValidAudiences = effectiveAudiences,
        ValidateIssuer = issuers,
        ValidateAudience = audiences,
        RequireAudience = audiences
      };

      tokenHandler.ValidateToken(token, tokenParams, out SecurityToken validatedToken);

      var jwtToken = (JwtSecurityToken)validatedToken;

      return new UserTokenDto(jwtToken);
    }
    catch (SecurityTokenExpiredException)
    {
      return new UserTokenDto("Token.Expired");
    }
    catch (SecurityTokenInvalidSignatureException)
    {
      return new UserTokenDto("Token.InvalidSignature");
    }
    catch (ArgumentException)
    {
      return new UserTokenDto("Token.Invalid");
    }
    catch (Exception)
    {
      return new UserTokenDto("InternalServerError");
    }
  }

  #endregion
}
