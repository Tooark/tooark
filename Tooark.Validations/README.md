# Tooark.Validations

## Descrição

A biblioteca `Tooark.Validations` fornece uma série de métodos para validação de diferentes tipos de dados e padrões. Ela é projetada para facilitar a validação de dados em aplicações .NET, garantindo que os valores atendam aos critérios especificados.

## Utilização

### Validação de Boolean

```csharp
using Tooark.Validations;

bool value = true;
string property = "IsActive";
var contract = new Contract()
    .IsTrue(value, property, "O valor deve ser verdadeiro.")
    .IsFalse(!value, property, "O valor deve ser falso.");
```

### Validação de Datas

```csharp
using Tooark.Validations;

DateTime date = DateTime.Now;
DateTime comparer = DateTime.Now.AddDays(-1);
string property = "Data";
var contract = new Contract()
    .IsGreater(date, comparer, property, "A data deve ser maior que a data de comparação.")
    .IsLowerOrEquals(comparer, date, property, "A data de comparação deve ser menor ou igual à data.");
```

### Validação de Decimal

```csharp
using Tooark.Validations;

decimal value = 10.5m;
decimal comparer = 5.0m;
string property = "ValorDecimal";
var contract = new Contract()
    .IsGreater(value, comparer, property, "O valor deve ser maior que o valor de comparação.")
    .IsLowerOrEquals(comparer, value, property, "O valor de comparação deve ser menor ou igual ao valor.");
```

### Validação de Documentos (CPF/CNPJ)

```csharp
using Tooark.Validations;

string cpf = "123.456.789-09";
string cnpj = "12.345.678/0001-95";
string propriedadeCpf = "CPF";
string propriedadeCnpj = "CNPJ";
var contract = new Contract()
    .IsCpf(cpf, propriedadeCpf, "O CPF é inválido.")
    .IsCnpj(cnpj, propriedadeCnpj, "O CNPJ é inválido.");
    .IsCpfCnpj(cpf, propriedadeCnpj, "O CPF/CNPJ é inválido.");
    .IsCpfCnpj(cnpj, propriedadeCnpj, "O CPF/CNPJ é inválido.");
```

### Validação de Double

```csharp
using Tooark.Validations;

double value = 10.5;
double comparer = 5.0;
string property = "ValorDouble";
var contract = new Contract()
    .IsGreater(value, comparer, property, "O valor deve ser maior que o valor de comparação.")
    .IsLowerOrEquals(comparer, value, property, "O valor de comparação deve ser menor ou igual ao valor.");
```

### Validação de Email

```csharp
using Tooark.Validations;

string email = "exemplo@dominio.com";
string property = "Email";
var contract = new Contract()
    .IsEmail(email, property, "O email é inválido.")
    .IsEmailOrEmpty(email, property, "O email é inválido.");
```

### Validação de Float

```csharp
using Tooark.Validations;

float value = 10.5f;
float comparer = 5.0f;
string property = "ValorFloat";
var contract = new Contract()
    .IsGreater(value, comparer, property, "O valor deve ser maior que o valor de comparação.")
    .IsLowerOrEquals(comparer, value, property, "O valor de comparação deve ser menor ou igual ao valor.");
```

### Validação de Guid

```csharp
using Tooark.Validations;

Guid guid = Guid.NewGuid();
string property = "Guid";
var contract = new Contract()
    .IsGuid(guid.ToString(), property, "O GUID é inválido.");
```

### Validação de Int

```csharp
using Tooark.Validations;

int value = 10;
int comparer = 5;
string property = "ValorInt";
var contract = new Contract()
    .IsGreater(value, comparer, property, "O valor deve ser maior que o valor de comparação.")
    .IsLowerOrEquals(comparer, value, property, "O valor de comparação deve ser menor ou igual ao valor.");
```

### Validação de Listas

```csharp
using Tooark.Validations;

int[] values = [1, 2, 3];
int value = 2;
string property = "Valores";
var contract = new Contract()
    .Contains(value, valores, property, "O valor deve estar na lista.")
    .NotContains(4, valores, property, "O valor não deve estar na lista.");
```

### Validação de Long

```csharp
using Tooark.Validations;

long value = 10L;
long comparer = 5L;
string property = "ValorLong";
var contract = new Contract()
    .IsGreater(value, comparer, property, "O valor deve ser maior que o valor de comparação.")
    .IsLowerOrEquals(comparer, value, property, "O valor de comparação deve ser menor ou igual ao valor.");
```

### Validação de Rede (Network)

```csharp
using Tooark.Validations;

string ipv4 = "192.168.0.1";
string ipv6 = "2001:0db8:85a3:0000:0000:8a2e:0370:7334";
string property = "IP";
var contract = new Contract()
    .IsIp(ipv4, property, "O endereço IP é inválido.");
    .IsIp(ipv6, property, "O endereço IP é inválido.");
```

### Validação de Objeto

```csharp
using Tooark.Validations;

object obj = new object();
string property = "Objeto";
var contract = new Contract()
    .IsNotNull(obj, property, "O objeto não deve ser nulo.");
```

### Validação de Protocolo

```csharp
using Tooark.Validations;

string protocolo = "http";
string property = "Protocolo";
var contract = new Contract()
    .IsProtocol(protocolo, property, "O protocolo é inválido.");
```

### Validação de Regex

```csharp
using Tooark.Validations;

string value = "ABCDEF";
string number = "123456";
string pattern = @"^[a-zA-Z]+$";
string property = "Email";
var contract = new Contract()
    .Match(value, pattern, property, "O valor deve corresponder ao padrão.")
    .NotMatch(number, pattern, property, "O valor não deve corresponder ao padrão.");
```

### Validação de String

```csharp
using Tooark.Validations;

string value = "exemplo";
string comparer = "exemplo";
string property = "Texto";
var contract = new Contract()
    .IsGreater(value, 5, property, "O tamanho do valor deve ser maior que 5.")
    .AreEquals(value, comparer, property, "O valor deve ser igual ao valor comparado.")
```

### Validação de TimeSpan

```csharp
TimeSpan value = TimeSpan.FromHours(2);
TimeSpan comparer = TimeSpan.FromHours(1);
string property = "Duração";
var contract = new Contract()
    .IsGreater(value, comparer, property, "O valor deve ser maior que o valor comparado.")
    .IsLowerOrEquals(value, comparer, property, "O valor deve ser menor ou igual ao valor comparado.")
```

### Validação de Tipo

```csharp
string value = "abc";
string zipCode = "10000-000";
string property = "Tipos";
var contract = new Contract()
    .IsLetterLower(value, property, "O valor deve ser apenas letras minúsculas.")
    .IsZipCode(value, property, "O valor deve ser um CEP.")
```

## Métodos Disponíveis

A biblioteca `Tooark.Validations` oferece uma ampla gama de métodos de validação, incluindo, mas não se limitando a:

- `Join`: Junta as mensagens de notificação.

- `All`: Validação de todos os valores de uma lista.
- `AreEquals`: Validação de valores iguais.
- `AreNotEquals`: Validação de valores diferentes.
- `Contains`: Validação de valor contido em uma lista ou em uma string.
- `IsBase64`: Validação de Base64.
- `IsBetween`: Validação de valor entre dois valores ou tamanho da lista está entre dois tamanhos.
- `IsCnh`: Validação de CNH.
- `IsCnpj`: Validação de CNPJ.
- `IsCpf`: Validação de CPF.
- `IsCpfCnpj`: Validação de CPF ou CNPJ.
- `IsCpfRg`: Validação de CPF ou RG.
- `IsCpfRgCnh`: Validação de CPF, RG ou CNH.
- `IsCulture` Validação de Cultura.
- `IsCultureIgnoreCase` Validação de Cultura ignorando maiúsculas e minúsculas.
- `IsEmail`: Validação de e-mail.
- `IsEmailDomain`: Validação de domínio de e-mail.
- `IsEmailDomainOrEmpty`: Validação de domínio de e-mail ou vazio.
- `IsEmailOrEmpty`: Validação de e-mail ou vazio.
- `IsEmpty`: Validação se é vazia.
- `IsFalse`: Validação de valor falso.
- `IsFtp`: Validação de FTP.
- `IsGreater`: Validação de valor maior ou tamanho da lista maior.
- `IsGreaterOrEquals`: Validação de valor maior ou igual ou tamanho da lista maior ou igual.
- `IsGuid`: Validação de GUID.
- `IsHexadecimal` Validação de hexadecimal.
- `IsHttp`: Validação de HTTP.
- `IsHttps`: Validação de HTTPS.
- `IsImap`: Validação de IMAP.
- `IsIp`: Validação de endereço IPV4 ou IPV6.
- `IsIpv4`: Validação de endereço IPV4.
- `IsIpv6`: Validação de endereço IPV6.
- `IsLetter`: Validação de apenas letras.
- `IsLetterLower` Validação de apenas letras minúsculas.
- `IsLetterNumeric` Validação de letras e números.
- `IsLetterUpper` Validação de apenas letras maiúsculas.
- `IsLower`: Validação de valor menor ou tamanho da lista menor.
- `IsLowerOrEquals`: Validação de valor menor ou igual ou tamanho da lista menor ou igual.
- `IsMacAddress`: Validação de endereço MAC.
- `IsMax`: Validação de valor máximo.
- `IsMin`: Validação de valor mínimo.
- `IsNotBetween`: Validação de valor não está entre dois valores ou tamanho da lista não está entre dois tamanhos.
- `IsNotEmpty`: Validação se não é vazia.
- `IsNotMax`: Validação de valor não máximo.
- `IsNotMin`: Validação de valor não mínimo.
- `IsNotNullOrEmpty`: Validação se não é nulo ou vazio.
- `IsNotNullOrWhiteSpace`: Validação se não é nulo, vazio ou espaço em branco.
- `IsNull`: Validação se é nulo.
- `IsNullOrEmpty`: Validação se é nulo ou vazio.
- `IsNullOrWhiteSpace`: Validação se é nulo, vazio ou espaço em branco.
- `IsNumeric` Validação de apenas números.
- `IsPassword`: Validação de Senha.
- `IsPop3`: Validação de POP3.
- `IsProtocolEmailReceiver`: Validação de Protocolo de Recebimento de E-mail.
- `IsProtocolEmailSender`: Validação de Protocolo de Envio de E-mail.
- `IsProtocolFtp`: Validação de Protocolo FTP.
- `IsProtocolHttp`: Validação de Protocolo HTTP.
- `IsProtocolWebSocket`: Validação de Protocolo WebSocket.
- `IsRg`: Validação de RG.
- `IsSftp`: Validação de SFTP.
- `IsSmtp`: Validação de SMTP.
- `IsTrue`: Validação de valor verdadeiro.
- `IsUrl`: Validação de URL.
- `IsWs`: Validação de WS.
- `IsWss`: Validação de WSS.
- `IsZipCode` Validação de Código Postal.
- `Match`: Validação de valor correspondente a um padrão.
- `NotAll`: Validação de nenhum valor de uma lista.
- `NotContains`: Validação de valor não contido em uma lista ou em uma string.
- `NotMatch`: Validação de valor não correspondente a um padrão.

Para uma lista completa de métodos e suas descrições, consulte a documentação XML gerada com a biblioteca.

## Contribuição

Contribuições são bem-vindas! Sinta-se à vontade para abrir issues e pull requests no repositório [Tooark.Validations](https://github.com/Tooark/tooark).

## Licença

Este projeto está licenciado sob a licença MIT. Veja o arquivo [LICENSE](../LICENSE) para mais detalhes.
