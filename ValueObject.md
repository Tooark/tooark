# ValueObjects

Este documento fornece uma visão geral dos ValueObjects disponíveis no projeto e exemplos de como utilizá-los.

## ValueObjects Disponíveis

### 1. Email

Representa um email válido.

#### Exemplo de Uso

```csharp
var email = new Email("exemplo@dominio.com");
Console.WriteLine(email.Value); // Saída: exemplo@dominio.com
```

### 2. Password

Representa uma senha válida com complexidade especificada.

#### Exemplo de Uso

```csharp
var password = new Password("SenhaForte123!");
Console.WriteLine(password.Value); // Saída: SenhaForte123!
```

### 3. Url

Representa uma URL válida.

#### Exemplo de Uso

```csharp
var url = new Url("https://www.exemplo.com");
Console.WriteLine(url.Value); // Saída: https://www.exemplo.com
```

### 4. LanguageCode

Representa o código de um idioma.

#### Exemplo de Uso

```csharp
var languageCode = new LanguageCode("pt-BR");
Console.WriteLine(languageCode.Code); // Saída: pt-BR
```

### 5. FtpProtocol

Representa um protocolo FTP.

#### Exemplo de Uso

```csharp
var ftpProtocol = new FtpProtocol("ftp://exemplo.com");
Console.WriteLine(ftpProtocol.Value); // Saída: ftp://exemplo.com
```

### 6. HttpProtocol

Representa um protocolo HTTP.

#### Exemplo de Uso

```csharp
var httpProtocol = new HttpProtocol("http://exemplo.com");
Console.WriteLine(httpProtocol.Value); // Saída: http://exemplo.com
```

### 7. ImapProtocol

Representa um protocolo IMAP/POP3.

#### Exemplo de Uso

```csharp
var imapProtocol = new ImapProtocol("imap://exemplo.com");
Console.WriteLine(imapProtocol.Value); // Saída: imap://exemplo.com
```

### 8. SmtpProtocol

Representa um protocolo SMTP.

#### Exemplo de Uso

```csharp
var smtpProtocol = new SmtpProtocol("smtp://exemplo.com");
Console.WriteLine(smtpProtocol.Value); // Saída: smtp://exemplo.com
```

### 9. WsProtocol

Representa um protocolo WebSocket.

#### Exemplo de Uso

```csharp
var wsProtocol = new WsProtocol("ws://exemplo.com");
Console.WriteLine(wsProtocol.Value); // Saída: ws://exemplo.com
```

## Conclusão

Os ValueObjects fornecem uma maneira robusta de representar e validar valores específicos no seu domínio. Utilize os exemplos acima para integrar esses objetos no seu projeto.
