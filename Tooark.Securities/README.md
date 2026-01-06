# Tooark.Securities

Biblioteca de segurança para aplicações .NET, fornecendo serviços de **criptografia AES** e **autenticação JWT** com suporte a múltiplos algoritmos.

## 📦 Conteúdo do Pacote

### Serviços

| Classe                | Descrição                                       |
| --------------------- | ----------------------------------------------- |
| `JwtTokenService`     | Serviço para criação e validação de tokens JWT  |
| `CryptographyService` | Serviço de criptografia/descriptografia AES-256 |

### Interfaces

| Interface          | Descrição                               |
| ------------------ | --------------------------------------- |
| `IJwtTokenService` | Contrato para manipulação de tokens JWT |

### DTOs

| Classe         | Descrição                                         |
| -------------- | ------------------------------------------------- |
| `JwtTokenDto`  | Dados para criação de token (id, login, security) |
| `UserTokenDto` | Resultado da validação do token                   |

### Options

| Classe                | Descrição                                                             |
| --------------------- | --------------------------------------------------------------------- |
| `JwtOptions`          | Configurações do JWT (algoritmo, chaves, issuer, audience, expiração) |
| `CryptographyOptions` | Configurações de criptografia (algoritmo, secret)                     |

### Extensions

| Classe                | Descrição                                     |
| --------------------- | --------------------------------------------- |
| `RoleClaimsExtension` | Extensão para manipulação de claims de função |

---

## 🔧 Instalação

```bash
dotnet add package Tooark.Securities
```

---

## ⚙️ Configuração

### appsettings.json

Para configurar os serviços de segurança, adicione as seções `Jwt` e `Cryptography` no seu arquivo `appsettings.json`:

Configuração exemplo para token JWT com algoritmos simétricos (utiliza _Secret_ para assinatura e validação):

```json
{
  "Jwt": {
    "Algorithm": "HS256",
    "Secret": "sua-chave-secreta-com-pelo-menos-32-caracteres",
    "Issuer": "sua-aplicacao",
    "Audience": "seus-clientes",
    "Expires": 60
  }
}
```

Configuração exemplo para token JWT com algoritmos assimétricos (_PrivateKey_ obrigatória para assinatura, _PublicKey_ obrigatória para validação):

```json
{
  "Jwt": {
    "Algorithm": "RS256",
    "PrivateKey": "sua-chave-privada-em-base64",
    "PublicKey": "sua-chave-publica-em-base64",
    "Issuer": "sua-aplicacao",
    "Audience": "seus-clientes",
    "Expires": 60
  }
}
```

Configuração exemplo para criptografia AES:

```json
{
  "Cryptography": {
    "Algorithm": "GCM",
    "Secret": "sua-chave-secreta-com-32-caracteres"
  }
}
```

### Program.cs

```csharp
using Tooark.Securities.Injections;

var builder = WebApplication.CreateBuilder(args);

// Adiciona os serviços de segurança
builder.Services.AddTooarkSecurities(builder.Configuration);

var app = builder.Build();
```

Ou, para adicionar apenas o serviço de token JWT:

```csharp
using Tooark.Securities.Injections;

var builder = WebApplication.CreateBuilder(args);

// Adiciona os serviços de segurança
builder.Services.AddTooarkJwtToken(builder.Configuration);

var app = builder.Build();
```

Ou, para adicionar apenas o serviço de criptografia:

```csharp
using Tooark.Securities.Injections;

var builder = WebApplication.CreateBuilder(args);

// Adiciona os serviços de segurança
builder.Services.AddTooarkCryptography(builder.Configuration);

var app = builder.Build();
```

---

## 🔐 JWT - Algoritmos Suportados

### Algoritmos Simétricos (HMAC)

| Algoritmo | Descrição   | Requisitos              |
| --------- | ----------- | ----------------------- |
| `HS256`   | HMAC-SHA256 | Secret (≥32 caracteres) |
| `HS384`   | HMAC-SHA384 | Secret (≥48 caracteres) |
| `HS512`   | HMAC-SHA512 | Secret (≥64 caracteres) |

### Algoritmos Assimétricos (RSA)

| Algoritmo | Descrição      | Requisitos                        |
| --------- | -------------- | --------------------------------- |
| `RS256`   | RSA-SHA256     | PrivateKey/PublicKey (≥2048 bits) |
| `RS384`   | RSA-SHA384     | PrivateKey/PublicKey (≥2048 bits) |
| `RS512`   | RSA-SHA512     | PrivateKey/PublicKey (≥2048 bits) |
| `PS256`   | RSA-PSS-SHA256 | PrivateKey/PublicKey (≥2048 bits) |
| `PS384`   | RSA-PSS-SHA384 | PrivateKey/PublicKey (≥2048 bits) |
| `PS512`   | RSA-PSS-SHA512 | PrivateKey/PublicKey (≥2048 bits) |

### Algoritmos Assimétricos (ECDsa)

| Algoritmo | Descrição    | Curva Requerida   |
| --------- | ------------ | ----------------- |
| `ES256`   | ECDSA-SHA256 | P-256 (secp256r1) |
| `ES384`   | ECDSA-SHA384 | P-384 (secp384r1) |
| `ES512`   | ECDSA-SHA512 | P-521 (secp521r1) |

---

## 🔒 Criptografia - Algoritmos Suportados

| Algoritmo   | Modo        | Descrição                                  |
| ----------- | ----------- | ------------------------------------------ |
| `GCM`       | AES-256-GCM | **Recomendado** - Authenticated encryption |
| `CBC`       | AES-256-CBC | Modo tradicional com IV aleatório          |
| `CBCUnsafe` | AES-256-CBC | ⚠️ Legado - IV zerado (não recomendado)    |

---

## 📝 Exemplos de Uso

### JWT - Criação e Validação de Token

#### Configuração com HMAC (Simétrico)

```json
{
  "Jwt": {
    "Algorithm": "HS256",
    "Secret": "minha-chave-secreta-super-segura-32chars",
    "Issuer": "minha-api",
    "Audience": "meus-clientes",
    "Expires": 60
  }
}
```

#### Configuração com RSA (Assimétrico)

```json
{
  "Jwt": {
    "Algorithm": "RS256",
    "PrivateKey": "MIIEvQIBADANBgkqh...chave-privada-base64...",
    "PublicKey": "MIIBIjANBgkqhkiG9w...chave-publica-base64...",
    "Issuer": "minha-api",
    "Audience": "meus-clientes",
    "Expires": 60
  }
}
```

#### Criando um Token

```csharp
public class AuthController : ControllerBase
{
    private readonly IJwtTokenService _jwtService;

    public AuthController(IJwtTokenService jwtService)
    {
        _jwtService = jwtService;
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        // Validar credenciais...

        // Criar dados do token
        var tokenData = new JwtTokenDto(
            id: user.Id,
            login: user.Email,
            security: user.SecurityStamp
        );

        // Gerar token
        var token = _jwtService.Create(tokenData);

        return Ok(new { Token = token });
    }

    // Com audience customizada e claims extras
    [HttpPost("login-custom")]
    public IActionResult LoginCustom(LoginRequest request)
    {
        var tokenData = new JwtTokenDto(user.Id, user.Email, user.SecurityStamp);

        var extraClaims = new[]
        {
            new Claim("role", "admin"),
            new Claim("department", "IT")
        };

        var token = _jwtService.Create(tokenData, audience: "app-mobile", extraClaims: extraClaims);

        return Ok(new { Token = token });
    }
}
```

#### Validando um Token

```csharp
[HttpGet("validate")]
public IActionResult ValidateToken([FromHeader] string authorization)
{
    var token = authorization.Replace("Bearer ", "");

    var result = _jwtService.Validate(token);

    if (!string.IsNullOrEmpty(result.ErrorToken))
    {
        return Unauthorized(new { Error = result.ErrorToken });
    }

    return Ok(new
    {
        UserId = result.Id,
        Login = result.Login,
        // Ou usando helpers
        GuidId = result.GetGuidId,
        IntId = result.GetIntId
    });
}
```

---

### Criptografia - Encrypt e Decrypt

#### Configuração

```json
{
  "Cryptography": {
    "Algorithm": "GCM",
    "Secret": "minha-chave-secreta-32-caracteres"
  }
}
```

#### Usando CryptographyService

```csharp
public class DataProtectionService
{
    private readonly CryptographyService _crypto;

    public DataProtectionService(IOptions<CryptographyOptions> options)
    {
        _crypto = new CryptographyService(options);
    }

    public string ProtectSensitiveData(string plainText)
    {
        // Retorna texto criptografado em Base64
        return _crypto.Encrypt(plainText);
    }

    public string UnprotectData(string encryptedText)
    {
        // Retorna texto original descriptografado
        return _crypto.Decrypt(encryptedText);
    }
}
```

#### Exemplo Completo

```csharp
// Configurar no Program.cs
builder.Services.Configure<CryptographyOptions>(
    builder.Configuration.GetSection(CryptographyOptions.Section));
builder.Services.AddSingleton<CryptographyService>();

// Usar no serviço
public class UserService
{
    private readonly CryptographyService _crypto;

    public UserService(CryptographyService crypto)
    {
        _crypto = crypto;
    }

    public void SaveUser(User user)
    {
        // Criptografar dados sensíveis antes de salvar
        user.CreditCard = _crypto.Encrypt(user.CreditCard);
        user.SSN = _crypto.Encrypt(user.SSN);

        // Salvar no banco...
    }

    public User GetUser(int id)
    {
        var user = // Buscar do banco...

        // Descriptografar dados sensíveis
        user.CreditCard = _crypto.Decrypt(user.CreditCard);
        user.SSN = _crypto.Decrypt(user.SSN);

        return user;
    }
}
```

---

## 🔑 Gerando Chaves

### Chave para HMAC (HS256/HS384/HS512)

```bash
# Gerar chave aleatória de 32 bytes (256 bits) para HS256
openssl rand -base64 32

# Gerar chave aleatória de 64 bytes (512 bits) para HS512
openssl rand -base64 64
```

### Chaves RSA (RS256/PS256)

```bash
# Gerar chave privada RSA de 2048 bits
openssl genrsa -out private.pem 2048

# Extrair chave pública
openssl rsa -in private.pem -pubout -out public.pem

# Converter para formato PKCS8 (recomendado)
openssl pkcs8 -topk8 -inform PEM -outform PEM -nocrypt -in private.pem -out private_pkcs8.pem

# Obter chave em Base64 (sem headers)
cat private_pkcs8.pem | grep -v "BEGIN\|END" | tr -d '\n'
cat public.pem | grep -v "BEGIN\|END" | tr -d '\n'
```

### Chaves ECDsa (ES256/ES384/ES512)

```bash
# ES256 (P-256)
openssl ecparam -genkey -name prime256v1 -noout -out ec_private.pem
openssl ec -in ec_private.pem -pubout -out ec_public.pem

# ES384 (P-384)
openssl ecparam -genkey -name secp384r1 -noout -out ec_private.pem

# ES512 (P-521)
openssl ecparam -genkey -name secp521r1 -noout -out ec_private.pem

# Converter para PKCS8
openssl pkcs8 -topk8 -nocrypt -in ec_private.pem -out ec_private_pkcs8.pem
```

---

## 📋 Dependências

| Pacote                                          | Versão | Descrição                          |
| ----------------------------------------------- | ------ | ---------------------------------- |
| `Microsoft.AspNetCore.Authentication.JwtBearer` | 8.x    | Autenticação JWT para ASP.NET Core |

---

## 🎯 Boas Práticas

### JWT

1. **Use algoritmos assimétricos (RS/PS/ES) em produção** - Permite validar tokens sem expor a chave de assinatura
2. **Configure `Expires` apropriadamente** - Tokens de curta duração são mais seguros
3. **Use `Issuer` e `Audience`** - Previne uso indevido de tokens entre aplicações
4. **Armazene chaves em Secret Manager** - Nunca commite chaves no código fonte

### Criptografia

1. **Prefira GCM sobre CBC** - GCM fornece autenticação integrada
2. **Nunca use CBCUnsafe** - Apenas para compatibilidade com sistemas legados
3. **Use chaves de 32 caracteres** - Garante AES-256
4. **Gere chaves aleatórias** - Use `openssl rand` ou `RandomNumberGenerator`

---

## ⚠️ Erros Comuns

| Código                             | Descrição                     | Solução                                                           |
| ---------------------------------- | ----------------------------- | ----------------------------------------------------------------- |
| `Jwt.KeyNotConfigured`             | Chave não configurada         | Configure `Secret` (HMAC) ou `PrivateKey`/`PublicKey` (RSA/ECDsa) |
| `Jwt.InvalidKeySize`               | Tamanho da chave RSA inválido | Use chaves RSA de pelo menos 2048 bits                            |
| `Jwt.InvalidKeyCurve`              | Curva ECDsa incompatível      | Use a curva correta para o algoritmo (P-256 para ES256, etc.)     |
| `Jwt.InvalidKey`                   | Chave malformada              | Verifique se a chave está em Base64 válido                        |
| `Jwt.AlgorithmNotSupported`        | Algoritmo não suportado       | Use HS/RS/PS/ES + 256/384/512                                     |
| `Token.Expired`                    | Token expirado                | Gere um novo token                                                |
| `Token.Invalid`                    | Token inválido                | Verifique formato e assinatura                                    |
| `Cryptography.SecretNotConfigured` | Secret não configurado        | Configure `Secret` nas opções                                     |

---

## Contribuição

Contribuições são bem-vindas! Sinta-se à vontade para abrir issues e pull requests no repositório [Tooark.Entities](https://github.com/Tooark/tooark/issues).

## 📄 Licença

Este projeto está licenciado sob a licença BSD 3-Clause. Veja o arquivo [LICENSE](../LICENSE) para mais detalhes.
