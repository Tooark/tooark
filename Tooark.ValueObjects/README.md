# Tooark.ValueObjects

Esta pasta contém os Value Objects disponíveis no pacote. Abaixo estão descritos os Value Objects, suas funcionalidades, métodos e exemplos de uso.

## Value Objects

Os Value Objects disponíveis são:

### 1. Cpf

**Funcionalidade:**
Representa um CPF (Cadastro de Pessoas Físicas).

**Métodos:**

- `Cpf.Number`: Obtém o valor do número do CPF.
- `ToString()`: Retorna o valor do CPF.
- `string(Cpf document)`: Converte implicitamente um objeto Cpf para uma string.
- `Cpf(string value)`: Converte implicitamente uma string para um objeto Cpf.

[**Exemplo de Uso**](#cpf)

### 2. Cnh

**Funcionalidade:**
Representa uma CNH (Carteira Nacional de Habilitação).

**Métodos:**

- `Cnh.Number`: Obtém o valor do número da CNH.
- `ToString()`: Retorna o valor da CNH.
- `string(Cnh document)`: Converte implicitamente um objeto Cnh para uma string.
- `Cnh(string value)`: Converte implicitamente uma string para um objeto Cnh.

[**Exemplo de Uso**](#cnh)

### 3. Rg

**Funcionalidade:**
Representa um RG (Registro Geral).

**Métodos:**

- `Rg.Number`: Obtém o valor do número do RG.
- `ToString()`: Retorna o valor do RG.
- `string(Rg document)`: Converte implicitamente um objeto Rg para uma string.
- `Rg(string value)`: Converte implicitamente uma string para um objeto Rg.

[**Exemplo de Uso**](#rg)

### 4. Cnpj

**Funcionalidade:**
Representa um CNPJ.

**Métodos:**

- `Cnpj.Number`: Obtém o valor do número do CNPJ.
- `ToString()`: Retorna o valor do CNPJ.
- `string(Cnpj document)`: Converte implicitamente um objeto Cnpj para uma string.
- `Cnpj(string value)`: Converte implicitamente uma string para um objeto Cnpj.

[**Exemplo de Uso**](#cnpj)

### 5. CpfRg

**Funcionalidade:**
Representa um CPF ou RG.

**Métodos:**

- `CpfRg.Number`: Obtém o valor do número do CPF ou RG.
- `ToString()`: Retorna o valor do CPF ou RG.
- `string(CpfRg document)`: Converte implicitamente um objeto CpfRg para uma string.
- `CpfRg(string value)`: Converte implicitamente uma string para um objeto CpfRg.

[**Exemplo de Uso**](#cpfrg)

### 6. CpfRgCnh

**Funcionalidade:**
Representa um CPF, RG ou CNH.

**Métodos:**

- `CpfRgCnh.Number`: Obtém o valor do número do CPF, RG ou CNH.
- `ToString()`: Retorna o valor do CPF, RG ou CNH.
- `string(CpfRgCnh document)`: Converte implicitamente um objeto CpfRgCnh para uma string.
- `CpfRgCnh(string value)`: Converte implicitamente uma string para um objeto CpfRgCnh.

[**Exemplo de Uso**](#cpfrgcnh)

### 7. CpfCnpj

**Funcionalidade:**
Representa um CPF ou CNPJ.

**Métodos:**

- `CpfCnpj.Number`: Obtém o valor do número do CPF ou CNPJ.
- `ToString()`: Retorna o valor do CPF ou CNPJ.
- `string(CpfCnpj document)`: Converte implicitamente um objeto CpfCnpj para uma string.
- `CpfCnpj(string value)`: Converte implicitamente uma string para um objeto CpfCnpj.

[**Exemplo de Uso**](#cpfcnpj)

### 8. Document

**Funcionalidade:**
Representa um Documento (CPF, CNH, RG ou CNPJ).

**Métodos:**

- `Document.Number`: Obtém o valor do número do CPF, CNH, RG ou CNPJ.
- `ToString()`: Retorna o valor do CPF, CNH, RG ou CNPJ.
- `string(Document document)`: Converte implicitamente um objeto Document para uma string.
- `Document(string value)`: Converte implicitamente uma string para um objeto Document.

[**Exemplo de Uso**](#document)

### 9. DocumentType

**Funcionalidade:**
Representa um tipo de documento (CPF, CNH, RG, CNPJ ou Genérico).

**Métodos:**

- `DocumentType.None`: Representa um tipo de documento qualquer. Id = 0. Utiliza o regex `^[a-zA-Z0-9.-]*$`.
- `DocumentType.CPF`: Representa um documento do tipo CPF. Id = 1. Utiliza o regex `RegexPattern.CPF`.
- `DocumentType.RG`: Representa um documento do tipo RG. Id = 2. Utiliza o regex `RegexPattern.RG`.
- `DocumentType.CNH`: Representa um documento do tipo  CNH. Id = 3. Utiliza o regex `RegexPattern.CNH`.
- `DocumentType.CNPJ`: Representa um documento do tipo CNPJ. Id = 4. Utiliza o regex `RegexPattern.CNPJ`.
- `DocumentType.CPF_CNPJ`: Representa um documento do tipo CPF ou CNPJ. Id = 5. Utiliza o regex `RegexPattern.CPF_CNPJ`.
- `DocumentType.CPF_RG`: Representa um documento do tipo CPF ou RG. Id = 6. Utiliza o regex `RegexPattern.CPF_RG`.
- `DocumentType.CPF_RG_CNH`: Representa um documento do tipo CPF, RG ou CNH. Id = 7. Utiliza o regex `RegexPattern.CPF_RG_CNH`.
- `ToString()`: Retorna a descrição do tipo de documento.
- `ToInt()`: Retorna o ID do tipo de documento.
- `ToRegex()`: Retorna o regex do tipo de documento.
- `string(DocumentType document)`: Converte implicitamente um objeto DocumentType para uma string.
- `int(DocumentType document)`: Converte implicitamente um objeto DocumentType para um int.
- `DocumentType(string value)`: Converte implicitamente uma string para um objeto DocumentType.
- `DocumentType(int value)`: Converte implicitamente uma int para um objeto DocumentType.

[**Exemplo de Uso**](#documenttype)

### 10. Email

**Funcionalidade:**
Representa um endereço de email válido.

**Métodos:**

- `Email.Value`: Obtém o valor do domínio de email.
- `ToString()`: Retorna o valor do domínio de email.
- `string(Email email)`: Converte implicitamente um objeto Email para uma string.
- `Email(string value)`: Converte implicitamente uma string para um objeto Email.

[**Exemplo de Uso**](#email)

### 11. EmailDomain

**Funcionalidade:**
Representa um domínio de email válido.

**Métodos:**

- `EmailDomain.Value`: Obtém o valor do domínio de email.
- `ToString()`: Retorna o valor do domínio de email.
- `string(EmailDomain emailDomain)`: Converte implicitamente um objeto EmailDomain para uma string.
- `EmailDomain(string value)`: Converte implicitamente uma string para um objeto EmailDomain.

[**Exemplo de Uso**](#emaildomain)

### 12. LanguageCode

**Funcionalidade:**
Representa um código de idioma válido.

**Métodos:**

- `LanguageCode.Code`: Obtém o código do idioma.
- `ToString()`: Retorna o código do idioma.
- `string(LanguageCode languageCode)`: Converte implicitamente um objeto LanguageCode para uma string.
- `LanguageCode(string value)`: Converte implicitamente uma string para um objeto LanguageCode.

[**Exemplo de Uso**](#languagecode)

### 13. Letter

**Funcionalidade:**
Representa uma string com apenas letras válida.

**Métodos:**

- `Letter.Value`: Obtém o valor da string com apenas letras.
- `ToString()`: Retorna o valor da string com apenas letras.
- `string(Letter letter)`: Converte implicitamente um objeto Letter para uma string.
- `Letter(string value)`: Converte implicitamente uma string para um objeto Letter.

[**Exemplo de Uso**](#letter)

### 14. LetterNumeric

**Funcionalidade:**
Representa uma string com apenas letras e números válida.

**Métodos:**

- `LetterNumeric.Value`: Obtém o valor da string com apenas letras e números.
- `ToString()`: Retorna o valor da string com apenas letras e números.
- `string(LetterNumeric letterNumeric)`: Converte implicitamente um objeto LetterNumeric para uma string.
- `LetterNumeric(string value)`: Converte implicitamente uma string para um objeto LetterNumeric.

[**Exemplo de Uso**](#letternumeric)

### 15. Numeric

**Funcionalidade:**
Representa uma string com apenas números válida.

**Métodos:**

- `Numeric.Value`: Obtém o valor da string com apenas números.
- `ToString()`: Retorna o valor da string com apenas números.
- `string(Numeric numeric)`: Converte implicitamente um objeto Numeric para uma string.
- `Numeric(string value)`: Converte implicitamente uma string para um objeto Numeric.

[**Exemplo de Uso**](#numeric)

### 16. Password

**Funcionalidade:**
Representa uma senha válida com complexidade especificada.
Parâmetros suportados para a complexidade da senha:

- `lowercase`: A senha deve conter letras minúsculas.
- `uppercase`: A senha deve conter letras maiúsculas.
- `number`: A senha deve conter números.
- `symbol`: A senha deve conter símbolos.
- `length`: A senha deve ter um tamanho mínimo especificado. Mínimo suportado: 8.

**Métodos:**

- `Password.Value`: Obtém o valor da senha.
- `ToString()`: Retorna o valor da senha.
- `string(Password password)`: Converte implicitamente um objeto Password para uma string.
- `Password(string value)`: Converte implicitamente uma string para um objeto Password.

[**Exemplo de Uso**](#password)

### 17. ZipCode

**Funcionalidade:**
Representa um código postal válido.

**Métodos:**

- `ZipCode.Value`: Obtém o valor do código do idioma.
- `ToString()`: Retorna o valor do código do idioma.
- `string(ZipCode zipCode)`: Converte implicitamente um objeto ZipCode para uma string.
- `ZipCode(string value)`: Converte implicitamente uma string para um objeto ZipCode.

[**Exemplo de Uso**](#zipcode)

### 18. ProtocolEmailReceiver

**Funcionalidade:**
Representa um protocolo de recebimento de email válido.

**Métodos:**

- `ProtocolEmailReceiver.Value`: Obtém o valor do protocolo de recebimento de email.
- `ToString()`: Retorna o valor do protocolo de recebimento de email.
- `string(ProtocolEmailReceiver protocol)`: Converte implicitamente um objeto ProtocolEmailReceiver para uma string.
- `ProtocolEmailReceiver(string value)`: Converte implicitamente uma string para um objeto ProtocolEmailReceiver.

[**Exemplo de Uso**](#protocolemailreceiver)

### 19. ProtocolEmailSender

**Funcionalidade:**
Representa um protocolo de envio de email válido.

**Métodos:**

- `ProtocolEmailSender.Value`: Obtém o valor do protocolo de envio de email.
- `ToString()`: Retorna o valor do protocolo de envio de email.
- `string(ProtocolEmailSender protocol)`: Converte implicitamente um objeto ProtocolEmailSender para uma string.
- `ProtocolEmailSender(string value)`: Converte implicitamente uma string para um objeto ProtocolEmailSender.

[**Exemplo de Uso**](#protocolemailsender)

### 20. ProtocolFtp

**Funcionalidade:**
Representa um protocolo de FTP válido.

**Métodos:**

- `ProtocolFtp.Value`: Obtém o valor do protocolo de FTP.
- `ToString()`: Retorna o valor do protocolo de FTP.
- `string(ProtocolFtp protocol)`: Converte implicitamente um objeto ProtocolFtp para uma string.
- `ProtocolFtp(string value)`: Converte implicitamente uma string para um objeto ProtocolFtp.

[**Exemplo de Uso**](#protocolftp)

### 21. ProtocolHttp

**Funcionalidade:**
Representa um protocolo de HTTP válido.

**Métodos:**

- `ProtocolHttp.Value`: Obtém o valor do protocolo de HTTP.
- `ToString()`: Retorna o valor do protocolo de HTTP.
- `string(ProtocolHttp protocol)`: Converte implicitamente um objeto ProtocolHttp para uma string.
- `ProtocolHttp(string value)`: Converte implicitamente uma string para um objeto ProtocolHttp.

[**Exemplo de Uso**](#protocolhttp)

### 22. ProtocolWs

**Funcionalidade:**
Representa um protocolo Websocket válido.

**Métodos:**

- `ProtocolWs.Value`: Obtém o valor do protocolo Websocket.
- `ToString()`: Retorna o valor do protocolo Websocket.
- `string(ProtocolWs protocol)`: Converte implicitamente um objeto ProtocolWs para uma string.
- `ProtocolWs(string value)`: Converte implicitamente uma string para um objeto ProtocolWs.

[**Exemplo de Uso**](#protocolws)

### 23. Url

**Funcionalidade:**
Representa uma URL válida.

**Métodos:**

- `Url.Value`: Obtém o valor da URL.
- `ToString()`: Retorna o valor da URL.
- `string(Url url)`: Converte implicitamente um objeto Url para uma string.
- `Url(string value)`: Converte implicitamente uma string para um objeto Url.

[**Exemplo de Uso**](#url)

## Exemplos de Uso

Os exemplos de uso abaixo:

### Cpf

[Informações](#1-cpf)

```csharp
var documento = new Cpf("118.214.830-14");

if (cpf.IsValid)
{
  Console.WriteLine($"CPF válido: {documento.Number}");
}
```

```csharp
Cpf documento = "118.214.830-14";

if (cpf.IsValid)
{
  Console.WriteLine($"CPF válido: {documento.Number}");
}
```

### Cnh

[Informações](#2-cnh)

```csharp
var documento = new Cnh("17932463758");

if (cnh.IsValid)
{
  Console.WriteLine($"CNH válida: {documento.Number}");
}
```

```csharp
Cnh documento = "17932463758";

if (cnh.IsValid)
{
  Console.WriteLine($"CNH válida: {documento.Number}");
}
```

### Rg

[Informações](#3-rg)

```csharp
var documento = new Rg("28.589.200-9");

if (rg.IsValid)
{
  Console.WriteLine($"RG válido: {documento.Number}");
}
```

```csharp
Rg documento = "28.589.200-9";

if (rg.IsValid)
{
  Console.WriteLine($"RG válido: {documento.Number}");
}
```

### Cnpj

[Informações](#4-cnpj)

```csharp
var documento = new Cnpj("78.293.721/0001-24");

if (cnpj.IsValid)
{
  Console.WriteLine($"CNPJ válido: {documento.Number}");
}
```

```csharp
Cnpj documento = "78.293.721/0001-24";

if (cnpj.IsValid)
{
  Console.WriteLine($"CNPJ válido: {documento.Number}");
}
```

### CpfRg

[Informações](#5-cpfrg)

```csharp
var documento = new CpfRg("118.214.830-14");

if (documento.IsValid)
{
  Console.WriteLine($"Documento válido: {documento.Number}");
}
```

```csharp
CpfRg documento = "118.214.830-14";

if (documento.IsValid)
{
  Console.WriteLine($"Documento válido: {documento.Number}");
}
```

### CpfRgCnh

[Informações](#6-cpfrgcnh)

```csharp
var documento = new CpfRgCnh("118.214.830-14");

if (documento.IsValid)
{
  Console.WriteLine($"Documento válido: {documento.Number}");
}
```

```csharp
CpfRgCnh documento = "118.214.830-14";

if (documento.IsValid)
{
  Console.WriteLine($"Documento válido: {documento.Number}");
}
```

### CpfCnpj

[Informações](#7-cpfcnpj)

```csharp
var documento = new CpfCnpj("78.293.721/0001-24");

if (documento.IsValid)
{
  Console.WriteLine($"Documento válido: {documento.Number}");
}
```

```csharp
CpfCnpj documento = "78.293.721/0001-24";

if (documento.IsValid)
{
  Console.WriteLine($"Documento válido: {documento.Number}");
}
```

### Document

[Informações](#8-document)

```csharp
var documento = new Document("78.293.721/0001-24");

if (documento.IsValid)
{
  Console.WriteLine($"Documento válido: {documento.Number}");
}
```

```csharp
Document documento = "78.293.721/0001-24";

if (documento.IsValid)
{
  Console.WriteLine($"Documento válido: {documento.Number}");
}
```

### DocumentType

[Informações](#9-documenttype)

```csharp
string tipoDocumento = DocumentType.CNPJ;

Console.WriteLine($"Tipo de documento: {tipoDocumento}"); // Tipo de documento: CNPJ
```

```csharp
int tipoDocumento = DocumentType.CNPJ;

Console.WriteLine($"Tipo de documento: {tipoDocumento}"); // Tipo de documento: 4
```

### Email

[Informações](#10-email)

```csharp
var email = new Email("test@example.com");

if (email.IsValid)
{
  Console.WriteLine($"Endereço de email válido: {email.Value}");
}
```

```csharp
Email email = "test@example.com";

if (email.IsValid)
{
  Console.WriteLine($"Endereço de email válido: {email.Value}");
}
```

### EmailDomain

[Informações](#11-emaildomain)

```csharp
var emailDomain = new EmailDomain("example.com");

if (emailDomain.IsValid)
{
  Console.WriteLine($"Domínio de email válido: {emailDomain.Value}");
}
```

```csharp
EmailDomain emailDomain = "example.com";

if (emailDomain.IsValid)
{
  Console.WriteLine($"Domínio de email válido: {emailDomain.Value}");
}
```

### LanguageCode

[Informações](#12-languagecode)

```csharp
var languageCode = new LanguageCode("pt-BR");

if (languageCode.IsValid)
{
  Console.WriteLine($"Código de idioma válido: {languageCode.Code}");
}
```

```csharp
LanguageCode languageCode = "pt-BR";

if (languageCode.IsValid)
{
  Console.WriteLine($"Código de idioma válido: {languageCode.Code}");
}
```

### Letter

[Informações](#13-letter)

```csharp
var letter = new Letter("abc");

if (letter.IsValid)
{
  Console.WriteLine($"Valor válido: {letter.Value}");
}
```

```csharp
Letter letter = "abc";

if (letter.IsValid)
{
  Console.WriteLine($"Valor válido: {letter.Value}");
}
```

### LetterNumeric

[Informações](#14-letternumeric)

```csharp
var letterNumeric = new LetterNumeric("abc123");

if (letterNumeric.IsValid)
{
  Console.WriteLine($"Valor válido: {letterNumeric.Value}");
}
```

```csharp
LetterNumeric letterNumeric = "abc123";

if (letterNumeric.IsValid)
{
  Console.WriteLine($"Valor válido: {letterNumeric.Value}");
}
```

### Numeric

[Informações](#15-numeric)

```csharp
var numeric = new Numeric("123456");

if (numeric.IsValid)
{
  Console.WriteLine($"Número válido: {numeric.Value}");
}
```

```csharp
Numeric numeric = "123456";

if (numeric.IsValid)
{
  Console.WriteLine($"Número válido: {numeric.Value}");
}
```

### Password

[Informações](#16-password)

```csharp
var password = new Password("P@ssw0rd");

if (password.IsValid)
{
  Console.WriteLine($"Senha válida: {password.Value}");
}
```

```csharp
Password password = "P@ssw0rd";

if (password.IsValid)
{
  Console.WriteLine($"Senha válida: {password.Value}");
}
```

### ZipCode

[Informações](#17-zipcode)

```csharp
var zipCode = new ZipCode("12345-678");

if (zipCode.IsValid)
{
  Console.WriteLine($"Código postal válido: {zipCode.Value}");
}
```

```csharp
ZipCode zipCode = "12345-678";

if (zipCode.IsValid)
{
  Console.WriteLine($"Código postal válido: {zipCode.Value}");
}
```

### ProtocolEmailReceiver

[Informações](#18-protocolemailreceiver)

```csharp
var protocol = new ProtocolEmailReceiver("imap://example.com");

if (protocol.IsValid)
{
  Console.WriteLine($"Protocolo válido: {protocol.Value}");
}
```

```csharp
ProtocolEmailReceiver protocol = "imap://example.com";

if (protocol.IsValid)
{
  Console.WriteLine($"Protocolo válido: {protocol.Value}");
}
```

### ProtocolEmailSender

[Informações](#19-protocolemailsender)

```csharp
var protocol = new ProtocolEmailSender("smtp://example.com");

if (protocol.IsValid)
{
  Console.WriteLine($"Protocolo válido: {protocol.Value}");
}
```

```csharp
ProtocolEmailSender protocol = "smtp://example.com";

if (protocol.IsValid)
{
  Console.WriteLine($"Protocolo válido: {protocol.Value}");
}
```

### ProtocolFtp

[Informações](#20-protocolftp)

```csharp
var protocol = new ProtocolEmailReceiver("sftp://example.com");

if (protocol.IsValid)
{
  Console.WriteLine($"Protocolo válido: {protocol.Value}");
}
```

```csharp
ProtocolEmailReceiver protocol = "sftp://example.com";

if (protocol.IsValid)
{
  Console.WriteLine($"Protocolo válido: {protocol.Value}");
}
```

### ProtocolHttp

[Informações](#21-protocolhttp)

```csharp
var protocol = new ProtocolHttp("https://example.com");

if (protocol.IsValid)
{
  Console.WriteLine($"Protocolo válido: {protocol.Value}");
}
```

```csharp
ProtocolHttp protocol = "https://example.com";

if (protocol.IsValid)
{
  Console.WriteLine($"Protocolo válido: {protocol.Value}");
}
```

### ProtocolWs

[Informações](#22-protocolws)

```csharp
var protocol = new ProtocolWs("wss://example.com");

if (protocol.IsValid)
{
  Console.WriteLine($"Protocolo válido: {protocol.Value}");
}
```

```csharp
ProtocolWs protocol = "wss://example.com";

if (protocol.IsValid)
{
  Console.WriteLine($"Protocolo válido: {protocol.Value}");
}
```

### Url

[Informações](#23-url)

```csharp
var url = new Url("https://example.com");

if (url.IsValid)
{
  Console.WriteLine($"Protocolo válido: {url.Value}");
}
```

```csharp
Url url = "https://example.com";

if (url.IsValid)
{
  Console.WriteLine($"Protocolo válido: {url.Value}");
}
```
