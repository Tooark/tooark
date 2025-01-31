# Tooark.Validations

Biblioteca para validação de tipos e padrões, fornecendo métodos para garantir a integridade e conformidade dos dados para projetos .NET.

## Conteúdo

- [Validação de Boolean](#validação-de-boolean)
- [Validação de Datas](#validação-de-datas)
- [Validação de Decimal](#validação-de-decimal)
- [Validação de Documentos](#validação-de-documentos)
- [Validação de Double](#validação-de-double)
- [Validação de Email](#validação-de-email)
- [Validação de Float](#validação-de-float)
- [Validação de Guid](#validação-de-guid)
- [Validação de Int](#validação-de-int)
- [Validação de Listas](#validação-de-listas)
- [Validação de Long](#validação-de-long)
- [Validação de Rede (Network)](#validação-de-rede-network)
- [Validação de Objeto](#validação-de-objeto)
- [Validação de Protocolo](#validação-de-protocolo)
- [Validação de Regex](#validação-de-regex)
- [Validação de String](#validação-de-string)
- [Validação de TimeSpan](#validação-de-timespan)
- [Validação de Tipo](#validação-de-tipo)

## Utilização

### Validação de Boolean

```csharp
using Tooark.Validations;

bool value = true;
string property = "Booleano";
var contract = new Contract()
    .IsTrue(value, property, "O valor deve ser verdadeiro.")
    .IsFalse(value, property, "O valor deve ser falso.")
    .Contains(value, bool[], property, "O valor deve estar na lista.")
    .NotContains(value, bool[], property, "O valor não deve estar na lista.")
    .All(value, bool[], property, "Todos os valores da lista devem igual ao valor.")
    .NotAll(value, bool[], property, "Nenhum valor da lista deve igual ao valor.")
    .IsNull(value, property, "O valor deve ser nulo.")
    .IsNotNull(value, property, "O valor não deve ser nulo.")
```

### Validação de Datas

```csharp
using Tooark.Validations;

DateTime date = DateTime.Now;
string property = "Data";
var contract = new Contract()
    .IsGreater(date, Comparer, property, "A data deve ser maior que a data comparada.")
    .IsGreaterOrEquals(date, Comparer, property, "A data deve ser maior ou igual que a data comparada.")
    .IsLower(date, Comparer, property, "A data deve ser menor que a data comparada.")
    .IsLowerOrEquals(date, Comparer, property, "A data deve ser menor ou igual que a data comparada.")
    .IsBetween(date, Start, End, property, "A data deve estar entre as datas.")
    .IsNotBetween(date, Start, End, property, "A data não deve estar entre as datas.")
    .IsMin(date, property, "A data deve ser o valor mínimo do tipo.")
    .IsNotMin(date, property, "A data não deve ser o valor mínimo do tipo.")
    .IsMax(date, property, "A data deve ser o valor máximo do tipo.")
    .IsNotMax(date, property, "A data não deve ser o valor máximo do tipo.")
    .AreEquals(date, Comparer, property, "As datas devem ser iguais.")
    .AreNotEquals(date, Comparer, property, "As datas não devem ser iguais.")
    .Contains(date, Datetime[], property, "A data deve estar na lista.")
    .NotContains(date, Datetime[], property, "A data não deve estar na lista.")
    .All(date, Datetime[], property, "Todos os valores da lista devem ser iguais a data.")
    .NotAll(date, Datetime[], property, "Nenhum valor da lista deve ser igual a data.")
    .IsNull(date, property, "A data deve ser nula.")
    .IsNotNull(date, property, "A data não deve ser nula.")
```

### Validação de Decimal

```csharp
using Tooark.Validations;

decimal value = 10.5m;
string property = "Decimal";
var contract = new Contract()
    .IsGreater(value, Comparer, property, "O valor deve ser maior que o valor comparado.")
    .IsGreaterOrEquals(value, Comparer, property, "O valor deve ser maior ou igual que o valor comparado.")
    .IsLower(value, Comparer, property, "O valor deve ser menor que o valor comparado.")
    .IsLowerOrEquals(value, Comparer, property, "O valor deve ser menor ou igual que o valor comparado.")
    .IsBetween(value, Start, End, property, "O valor deve estar entre os valores.")
    .IsNotBetween(value, Start, End, property, "O valor não deve estar entre os valores.")
    .IsMin(value, property, "O valor deve ser o valor mínimo do tipo.")
    .IsNotMin(value, property, "O valor não deve ser o valor mínimo do tipo.")
    .IsMax(value, property, "O valor deve ser o valor máximo do tipo.")
    .IsNotMax(value, property, "O valor não deve ser o valor máximo do tipo.")
    .AreEquals(value, Comparer, property, "Os valores devem ser iguais.")
    .AreNotEquals(value, Comparer, property, "Os valores não devem ser iguais.")
    .Contains(value, decimal[], property, "O valor deve estar na lista.")
    .NotContains(value, decimal[], property, "O valor não deve estar na lista.")
    .All(value, decimal[], property, "Todos os valores da lista devem ser iguais o valor.")
    .NotAll(value, decimal[], property, "Nenhum valor da lista deve ser igual o valor.")
    .IsNull(value, property, "O valor deve ser nulo.")
    .IsNotNull(value, property, "O valor não deve ser nulo.");
```

### Validação de Documentos

```csharp
using Tooark.Validations;

string property = "Document";
var contract = new Contract()
    .IsCpf(document, property, "Tem que ser um CPF válido")
    .IsRg(document, property, "Tem que ser um RG válido")
    .IsCnh(document, property, "Tem que ser um CNH válido")
    .IsCpfRg(document, property, "Tem que ser um CPF ou RG válido")
    .IsCpfRgCnh(document, property, "Tem que ser um CPF, RG ou CNH válido")
    .IsCnpj(document, property, "Tem que ser um CNPJ válido")
    .IsCpfCnpj(document, property, "Tem que ser um CPF ou CNPJ válido").
```

### Validação de Double

```csharp
using Tooark.Validations;

double value = 10.5;
string property = " Double";
var contract = new Contract()
    .IsGreater(value, Comparer, property, "O valor deve ser maior que o valor comparado.")
    .IsGreaterOrEquals(value, Comparer, property, "O valor deve ser maior ou igual que o valor comparado.")
    .IsLower(value, Comparer, property, "O valor deve ser menor que o valor comparado.")
    .IsLowerOrEquals(value, Comparer, property, "O valor deve ser menor ou igual que o valor comparado.")
    .IsBetween(value, Start, End, property, "O valor deve estar entre os valores.")
    .IsNotBetween(value, Start, End, property, "O valor não deve estar entre os valores.")
    .IsMin(value, property, "O valor deve ser o valor mínimo do tipo.")
    .IsNotMin(value, property, "O valor não deve ser o valor mínimo do tipo.")
    .IsMax(value, property, "O valor deve ser o valor máximo do tipo.")
    .IsNotMax(value, property, "O valor não deve ser o valor máximo do tipo.")
    .AreEquals(value, Comparer, property, "Os valores devem ser iguais.")
    .AreNotEquals(value, Comparer, property, "Os valores não devem ser iguais.")
    .Contains(value, double[], property, "O valor deve estar na lista.")
    .NotContains(value, double[], property, "O valor não deve estar na lista.")
    .All(value, double[], property, "Todos os valores da lista devem ser iguais o valor.")
    .NotAll(value, double[], property, "Nenhum valor da lista deve ser igual o valor.")
    .IsNull(value, property, "O valor deve ser nulo.")
    .IsNotNull(value, property, "O valor não deve ser nulo.");
```

### Validação de Email

```csharp
using Tooark.Validations;

string email = "exemplo@dominio.com";
string property = "Email";
var contract = new Contract()
    .IsEmail(email, property, "Tem que ser um email válido")
    .IsEmailOrEmpty(email, property, "Tem que ser um email válido ou vazio");
```

### Validação de Domínio de Email

```csharp
using Tooark.Validations;

string email = "@domain.com";
string property = "EmailDomain";
var contract = new Contract()
    .IsEmailDomain(email, property, "Tem que ser domínio de email válido")
    .IsEmailDomainOrEmpty(email, property, "Tem que ser domínio de email válido ou vazio");
```

### Validação de Float

```csharp
using Tooark.Validations;

float value = 10.5f;
string property = "Float";
var contract = new Contract()
    .IsGreater(value, Comparer, property, "O valor deve ser maior que o valor comparado.")
    .IsGreaterOrEquals(value, Comparer, property, "O valor deve ser maior ou igual que o valor comparado.")
    .IsLower(value, Comparer, property, "O valor deve ser menor que o valor comparado.")
    .IsLowerOrEquals(value, Comparer, property, "O valor deve ser menor ou igual que o valor comparado.")
    .IsBetween(value, Start, End, property, "O valor deve estar entre os valores.")
    .IsNotBetween(value, Start, End, property, "O valor não deve estar entre os valores.")
    .IsMin(value, property, "O valor deve ser o valor mínimo do tipo.")
    .IsNotMin(value, property, "O valor não deve ser o valor mínimo do tipo.")
    .IsMax(value, property, "O valor deve ser o valor máximo do tipo.")
    .IsNotMax(value, property, "O valor não deve ser o valor máximo do tipo.")
    .AreEquals(value, Comparer, property, "Os valores devem ser iguais.")
    .AreNotEquals(value, Comparer, property, "Os valores não devem ser iguais.")
    .Contains(value, float[], property, "O valor deve estar na lista.")
    .NotContains(value, float[], property, "O valor não deve estar na lista.")
    .All(value, float[], property, "Todos os valores da lista devem ser iguais o valor.")
    .NotAll(value, float[], property, "Nenhum valor da lista deve ser igual o valor.")
    .IsNull(value, property, "O valor deve ser nulo.")
    .IsNotNull(value, property, "O valor não deve ser nulo.");
```

### Validação de Guid

```csharp
using Tooark.Validations;

Guid guid = Guid.NewGuid();
string property = "Guid";
var contract = new Contract()
    .AreEquals(guid, Comparer, property, "Os valores devem ser iguais.")
    .AreNotEquals(guid, Comparer, property, "Os valores não devem ser iguais.")
    .Contains(guid, Guid[], property, "O valor deve estar na lista.")
    .NotContains(guid, Guid[], property, "O valor não deve estar na lista.")
    .All(guid, Guid[], property, "Todos os valores da lista devem ser iguais o valor.")
    .NotAll(guid, Guid[], property, "Nenhum valor da lista deve ser igual o valor.")
    .IsNull(guid, property, "O valor deve ser nulo.")
    .IsNotNull(guid, property, "O valor não deve ser nulo.")
    .IsEmpty(guid, property, "O valor deve ser vazio.")
    .IsNotEmpty(guid, property, "O valor não deve ser vazio.");
```

### Validação de Int

```csharp
using Tooark.Validations;

int value = 10;
string property = "Int";
var contract = new Contract()    
    .IsGreater(value, Comparer, property, "O valor deve ser maior que o valor comparado.")
    .IsGreaterOrEquals(value, Comparer, property, "O valor deve ser maior ou igual que o valor comparado.")
    .IsLower(value, Comparer, property, "O valor deve ser menor que o valor comparado.")
    .IsLowerOrEquals(value, Comparer, property, "O valor deve ser menor ou igual que o valor comparado.")
    .IsBetween(value, Start, End, property, "O valor deve estar entre os valores.")
    .IsNotBetween(value, Start, End, property, "O valor não deve estar entre os valores.")
    .IsMin(value, property, "O valor deve ser o valor mínimo do tipo.")
    .IsNotMin(value, property, "O valor não deve ser o valor mínimo do tipo.")
    .IsMax(value, property, "O valor deve ser o valor máximo do tipo.")
    .IsNotMax(value, property, "O valor não deve ser o valor máximo do tipo.")
    .AreEquals(value, Comparer, property, "Os valores devem ser iguais.")
    .AreNotEquals(value, Comparer, property, "Os valores não devem ser iguais.")
    .Contains(value, int[], property, "O valor deve estar na lista.")
    .NotContains(value, int[], property, "O valor não deve estar na lista.")
    .All(value, int[], property, "Todos os valores da lista devem ser iguais o valor.")
    .NotAll(value, int[], property, "Nenhum valor da lista deve ser igual o valor.")
    .IsNull(value, property, "O valor deve ser nulo.")
    .IsNotNull(value, property, "O valor não deve ser nulo.");
```

### Validação de Listas

```csharp
using Tooark.Validations;

int[] list = [1, 2, 3];
string property = "Valores";
var contract = new Contract()
    .IsGreater(list, Value, property, "A lista tem que ser maior que o valor permitido.")
    .IsGreaterOrEquals(list, Value, property, "A lista tem que ser maior ou igual que o valor permitido.")
    .IsLower(list, Value, property, "A lista tem que ser menor que o valor permitido.")
    .IsLowerOrEquals(list, Value, property, "A lista tem que ser menor ou igual que o valor permitido.")
    .AreEquals(list, ListComparer, property, "As listas tem que ser iguais.")
    .AreNotEquals(list, value, property, "As listas não podem ser iguais.")
    .IsNull(list, property, "A lista tem que ser nula.")
    .IsNotNull(list, property, "A lista não pode ser nula.")
    .IsEmpty(list, value, property, "A lista tem que ser vazia.")
    .IsNotEmpty(list, value, property, "A lista não pode ser vazia.");
```

### Validação de Long

```csharp
using Tooark.Validations;

long value = 10L;
string property = "Long";
var contract = new Contract()
    .IsGreater(value, Comparer, property, "O valor deve ser maior que o valor comparado.")
    .IsGreaterOrEquals(value, Comparer, property, "O valor deve ser maior ou igual que o valor comparado.")
    .IsLower(value, Comparer, property, "O valor deve ser menor que o valor comparado.")
    .IsLowerOrEquals(value, Comparer, property, "O valor deve ser menor ou igual que o valor comparado.")
    .IsBetween(value, Start, End, property, "O valor deve estar entre os valores.")
    .IsNotBetween(value, Start, End, property, "O valor não deve estar entre os valores.")
    .IsMin(value, property, "O valor deve ser o valor mínimo do tipo.")
    .IsNotMin(value, property, "O valor não deve ser o valor mínimo do tipo.")
    .IsMax(value, property, "O valor deve ser o valor máximo do tipo.")
    .IsNotMax(value, property, "O valor não deve ser o valor máximo do tipo.")
    .AreEquals(value, Comparer, property, "Os valores devem ser iguais.")
    .AreNotEquals(value, Comparer, property, "Os valores não devem ser iguais.")
    .Contains(value, long[], property, "O valor deve estar na lista.")
    .NotContains(value, long[], property, "O valor não deve estar na lista.")
    .All(value, long[], property, "Todos os valores da lista devem ser iguais o valor.")
    .NotAll(value, long[], property, "Nenhum valor da lista deve ser igual o valor.")
    .IsNull(value, property, "O valor deve ser nulo.")
    .IsNotNull(value, property, "O valor não deve ser nulo.");
```

### Validação de Rede (Network)

```csharp
using Tooark.Validations;

string property = "IP";
var contract = new Contract()
    .IsIp(Value, property, "Tem que ser um IP válido.")
    .IsIpv4(Value, property, "Tem que ser um IPV4 válido.")
    .IsIpv6(Value, property, "Tem que ser um IPV6 válido.")
    .IsMacAddress(Value, property, "Tem que ser um MacAddress válido.");
```

### Validação de Objeto

```csharp
using Tooark.Validations;

object obj = new object();
string property = "Objeto";
var contract = new Contract()
    .AreEquals(obj, Comparer, property, "Os objetos devem ser iguais.")
    .AreNotEquals(obj, Comparer, property, "Os objetos não devem ser iguais.")
    .IsNull(obj, property, "O objeto deve ser nulo.")
    .IsNotNull(obj, property, "O objeto não deve ser nulo.");
```

### Validação de Protocolo

```csharp
using Tooark.Validations;

string property = "Protocolo";
var contract = new Contract()
    .IsUrl(Protocol, property, "O protocolo deve ser uma Url válida.")
    .IsFtp(Protocol, property, "O protocolo deve ser um Ftp válido.")
    .IsSftp(Protocol, property, "O protocolo deve ser um Sftp válido.")
    .IsProtocolFtp(Protocol, property, "O protocolo deve ser um ProtocolFtp válido.")
    .IsHttp(Protocol, property, "O protocolo deve ser um Http válido.")
    .IsHttps(Protocol, property, "O protocolo deve ser um Https válido.")
    .IsProtocolHttp(Protocol, property, "O protocolo deve ser um ProtocolHttp válido.")
    .IsImap(Protocol, property, "O protocolo deve ser um Imap válido.")
    .IsPop3(Protocol, property, "O protocolo deve ser um Pop3 válido.")
    .IsProtocolEmailReceiver(Protocol, property, "O protocolo deve ser um ProtocolEmailReceiver válido.")
    .IsSmtp(Protocol, property, "O protocolo deve ser um Smtp válido.")
    .IsProtocolEmailSender(Protocol, property, "O protocolo deve ser um ProtocolEmailSender válido.")
    .IsWs(Protocol, property, "O protocolo deve ser um Ws válido.")
    .IsWss(Protocol, property, "O protocolo deve ser um Wss válido.")
    .IsProtocolWebSocket(Protocol, property, "O protocolo deve ser um ProtocolWebSocket válido.");
```

### Validação de Regex

```csharp
using Tooark.Validations;

string property = "Email";
var contract = new Contract()
    .Match(Value, Pattern, property, "O valor deve corresponder ao padrão.")
    .NotMatch(Value, Pattern, property, "O valor não deve corresponder ao padrão.");
```

### Validação de String

```csharp
using Tooark.Validations;

string value = "exemplo";
string property = "Texto";
var contract = new Contract()
    .IsGreater(value, Comparer, property, "O tamanho da string do valor deve ser maior que o valor comparado.")
    .IsGreaterOrEquals(value, Comparer, property, "O tamanho da string do valor deve ser maior ou igual que o valor comparado.")
    .IsLower(value, Comparer, property, "O tamanho da string do valor deve ser menor que o valor comparado.")
    .IsLowerOrEquals(value, Comparer, property, "O tamanho da string do valor deve ser menor ou igual que o valor comparado.")
    .IsBetween(value, Start, End, property, "O tamanho da string do valor deve estar entre os valores.")
    .IsNotBetween(value, Start, End, property, "O tamanho da string do valor não deve estar entre os valores.")
    .AreEquals(value, Comparer, property, "Os valores devem ser iguais.")
    .AreNotEquals(value, Comparer, property, "Os valores não devem ser iguais.")
    .Contains(value, string[], property, "O valor deve estar na lista.")
    .NotContains(value, string[], property, "O valor não deve estar na lista.")
    .All(value, string[], property, "Todos os valores da lista devem ser iguais o valor.")
    .NotAll(value, string[], property, "Nenhum valor da lista deve ser igual o valor.")
    .IsNull(value, property, "O valor deve ser nulo.")
    .IsNotNull(value, property, "O valor não deve ser nulo.")
    .IsNullOrEmpty(value, property, "O valor deve ser nulo ou vazio.")
    .IsNotNullOrEmpty(value, property, "O valor não deve ser nulo ou vazio.")
    .IsNullOrWhiteSpace(value, property, "O valor deve ser nulo, vazio ou espaço em branco.")
    .IsNotNull(value, property, "O valor não deve ser nulo, vazio ou espaço em branco.");
```

### Validação de TimeSpan

```csharp
TimeSpan value = TimeSpan.FromHours(2);
string property = "Duração";
var contract = new Contract()
    .IsGreater(value, Comparer, property, "O valor deve ser maior que o valor comparado.")
    .IsGreaterOrEquals(value, Comparer, property, "O valor deve ser maior ou igual que o valor comparado.")
    .IsLower(value, Comparer, property, "O valor deve ser menor que o valor comparado.")
    .IsLowerOrEquals(value, Comparer, property, "O valor deve ser menor ou igual que o valor comparado.")
    .IsBetween(value, Start, End, property, "O valor deve estar entre os valores.")
    .IsNotBetween(value, Start, End, property, "O valor não deve estar entre os valores.")
    .IsMin(value, property, "O valor deve ser o valor mínimo do tipo.")
    .IsNotMin(value, property, "O valor não deve ser o valor mínimo do tipo.")
    .IsMax(value, property, "O valor deve ser o valor máximo do tipo.")
    .IsNotMax(value, property, "O valor não deve ser o valor máximo do tipo.")
    .AreEquals(value, Comparer, property, "Os valores devem ser iguais.")
    .AreNotEquals(value, Comparer, property, "Os valores não devem ser iguais.")
    .Contains(value, TimeSpan[], property, "O valor deve estar na lista.")
    .NotContains(value, TimeSpan[], property, "O valor não deve estar na lista.")
    .All(value, TimeSpan[], property, "Todos os valores da lista devem ser iguais o valor.")
    .NotAll(value, TimeSpan[], property, "Nenhum valor da lista deve ser igual o valor.")
    .IsNull(value, property, "O valor deve ser nulo.")
    .IsNotNull(value, property, "O valor não deve ser nulo.");
```

### Validação de Tipo

```csharp
string value = "abc";
string zipCode = "10000-000";
string property = "Tipos";
var contract = new Contract()
    .IsGuid(value, property, "Tem que ser um Guid válido.")
    .IsLetter(value, property, "Tem que ser letras.")
    .IsLetterLower(value, property, "Tem que ser letras minúsculas.")
    .IsLetterUpper(value, property, "Tem que ser letras maiúsculas.")
    .IsNumeric(value, property, "Tem que ser números.")
    .IsLetterNumeric(value, property, "Tem que ser letras ou números.")
    .IsHexadecimal(value, property, "Tem que ser hexadecimal.")
    .IsZipCode(value, property, "Tem que ser um código postal.")
    .IsBase64(value, property, "Tem que ser um Base64.")
    .IsPassword(value, property, "Tem que ser uma senha complexa.")
    .IsCulture(value, property, "Tem que ser uma cultura.")
    .IsCultureIgnoreCase(value, property, "Tem que ser uma cultura ignorando case sensitive.");
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
