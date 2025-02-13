using System.Text.RegularExpressions;
using Tooark.Validations.Patterns;

namespace Tooark.Tests.Validations.Patterns;

public class RegexPatternTests
{
  // Teste de padrão CPF.
  [Theory]
  [InlineData("123.456.789-00", true)]
  [InlineData("12345678900", false)]
  public void TestCpfPattern(string input, bool expected)
  {
    // Arrange & Act
    var result = Regex.IsMatch(input, RegexPattern.Cpf);

    // Assert
    Assert.Equal(expected, result);
  }

  // Teste de padrão RG.
  [Theory]
  [InlineData("12.345.678", true)]
  [InlineData("12.345.678-9", true)]
  [InlineData("12.345.678-x", true)]
  [InlineData("12.345.678-X", true)]
  [InlineData("123456789", false)]
  public void TestRgPattern(string input, bool expected)
  {
    // Arrange & Act
    var result = Regex.IsMatch(input, RegexPattern.Rg);

    // Assert
    Assert.Equal(expected, result);
  }

  // Teste de padrão CNH.
  [Theory]
  [InlineData("12345678901", true)]
  [InlineData("123.456.789-01", false)]
  public void TestCnhPattern(string input, bool expected)
  {
    // Arrange & Act
    var result = Regex.IsMatch(input, RegexPattern.Cnh);

    // Assert
    Assert.Equal(expected, result);
  }

  // Teste de padrão CPF ou RG.
  [Theory]
  [InlineData("123.456.789-00", true)]
  [InlineData("12345678900", false)]
  [InlineData("12.345.678", true)]
  [InlineData("12.345.678-9", true)]
  [InlineData("12.345.678-x", true)]
  [InlineData("12.345.678-X", true)]
  [InlineData("123456789", false)]
  public void TestCpfRgPattern(string input, bool expected)
  {
    // Arrange & Act
    var result = Regex.IsMatch(input, RegexPattern.CpfRg);

    // Assert
    Assert.Equal(expected, result);
  }

  // Teste de padrão CPF, RG ou CNH.
  [Theory]
  [InlineData("123.456.789-00", true)]
  [InlineData("12.345.678", true)]
  [InlineData("12.345.678-9", true)]
  [InlineData("12.345.678-x", true)]
  [InlineData("12.345.678-X", true)]
  [InlineData("12345678901", true)]
  [InlineData("123456789", false)]
  [InlineData("123.456.789-000", false)]
  public void TestCpfRgCnhPattern(string input, bool expected)
  {
    // Arrange & Act
    var result = Regex.IsMatch(input, RegexPattern.CpfRgCnh);

    // Assert
    Assert.Equal(expected, result);
  }

  // Teste de padrão CNPJ.
  [Theory]
  [InlineData("12.345.678/0001-00", true)]
  [InlineData("12345678000100", false)]
  public void TestCnpjPattern(string input, bool expected)
  {
    // Arrange & Act
    var result = Regex.IsMatch(input, RegexPattern.Cnpj);

    // Assert
    Assert.Equal(expected, result);
  }

  // Teste de padrão CPF ou CNPJ.
  [Theory]
  [InlineData("123.456.789-00", true)]
  [InlineData("12345678900", false)]
  [InlineData("12.345.678/0001-00", true)]
  [InlineData("12345678000100", false)]
  public void TestCpfCnpjPattern(string input, bool expected)
  {
    // Arrange & Act
    var result = Regex.IsMatch(input, RegexPattern.CpfCnpj);

    // Assert
    Assert.Equal(expected, result);
  }

  // Teste de padrão de e-mail.
  [Theory]
  [InlineData("test@example.com", true)]
  [InlineData("test@.com", false)]
  public void TestEmailPattern(string input, bool expected)
  {
    // Arrange & Act
    var result = Regex.IsMatch(input, RegexPattern.Email);

    // Assert
    Assert.Equal(expected, result);
  }

  // Teste de padrão de domínio de e-mail.
  [Theory]
  [InlineData("@example.com", true)]
  [InlineData("example.com", false)]
  public void TestEmailDomainPattern(string input, bool expected)
  {
    // Arrange & Act
    var result = Regex.IsMatch(input, RegexPattern.EmailDomain);

    // Assert
    Assert.Equal(expected, result);
  }

  // Teste de padrão de IP.
  [Theory]
  [InlineData("192.168.0.1", true)]
  [InlineData("999.999.999.999", false)]
  public void TestIpPattern(string input, bool expected)
  {
    // Arrange & Act
    var result = Regex.IsMatch(input, RegexPattern.Ip);

    // Assert
    Assert.Equal(expected, result);
  }

  // Teste de padrão de IPv4.
  [Theory]
  [InlineData("192.168.0.1", true)]
  [InlineData("999.999.999.999", false)]
  public void TestIpv4Pattern(string input, bool expected)
  {
    // Arrange & Act
    var result = Regex.IsMatch(input, RegexPattern.Ipv4);

    // Assert
    Assert.Equal(expected, result);
  }

  // Teste de padrão de IPv6.
  [Theory]
  [InlineData("2001:0db8:85a3:0000:0000:8a2e:0370:7334", true)]
  [InlineData("2001:db8:85a3::8a2e:370:7334", false)]
  public void TestIpv6Pattern(string input, bool expected)
  {
    // Arrange & Act
    var result = Regex.IsMatch(input, RegexPattern.Ipv6);

    // Assert
    Assert.Equal(expected, result);
  }

  // Teste de padrão de MAC Address.
  [Theory]
  [InlineData("00:1A:2B:3C:4D:5E", true)]
  [InlineData("001A2B3C4D5E", false)]
  public void TestMacAddressPattern(string input, bool expected)
  {
    // Arrange & Act
    var result = Regex.IsMatch(input, RegexPattern.MacAddress);

    // Assert
    Assert.Equal(expected, result);
  }

  // Teste de padrão de GUID.
  [Theory]
  [InlineData("123e4567-e89b-12d3-a456-426614174000", true)]
  [InlineData("123e4567e89b12d3a456426614174000", false)]
  public void TestGuidPattern(string input, bool expected)
  {
    // Arrange & Act
    var result = Regex.IsMatch(input, RegexPattern.Guid);

    // Assert
    Assert.Equal(expected, result);
  }

  // Teste de padrão de letras.
  [Theory]
  [InlineData("abcDEF", true)]
  [InlineData("abc123", false)]
  public void TestLetterPattern(string input, bool expected)
  {
    // Arrange & Act
    var result = Regex.IsMatch(input, RegexPattern.Letter);

    // Assert
    Assert.Equal(expected, result);
  }

  // Teste de padrão de letras minúsculas.
  [Theory]
  [InlineData("abcdef", true)]
  [InlineData("ABCDEF", false)]
  public void TestLetterLowerPattern(string input, bool expected)
  {
    // Arrange & Act
    var result = Regex.IsMatch(input, RegexPattern.LetterLower);

    // Assert
    Assert.Equal(expected, result);
  }

  // Teste de padrão de letras maiúsculas.
  [Theory]
  [InlineData("ABCDEF", true)]
  [InlineData("abcdef", false)]
  public void TestLetterUpperPattern(string input, bool expected)
  {
    // Arrange & Act
    var result = Regex.IsMatch(input, RegexPattern.LetterUpper);

    // Assert
    Assert.Equal(expected, result);
  }

  // Teste de padrão de números.
  [Theory]
  [InlineData("123456", true)]
  [InlineData("123abc", false)]
  public void TestNumericPattern(string input, bool expected)
  {
    // Arrange & Act
    var result = Regex.IsMatch(input, RegexPattern.Numeric);

    // Assert
    Assert.Equal(expected, result);
  }

  // Teste de padrão de letras e números.
  [Theory]
  [InlineData("abcABC123", true)]
  [InlineData("abc-ABC-123", false)]
  public void TestLetterNumericPattern(string input, bool expected)
  {
    // Arrange & Act
    var result = Regex.IsMatch(input, RegexPattern.LetterNumeric);

    // Assert
    Assert.Equal(expected, result);
  }

  // Teste de padrão hexadecimal.
  [Theory]
  [InlineData("1a2b3c", true)]
  [InlineData("1A2B3C", true)]
  [InlineData("9z8y7x", false)]
  [InlineData("9Z8Y7X", false)]
  public void TestHexadecimalPattern(string input, bool expected)
  {
    // Arrange & Act
    var result = Regex.IsMatch(input, RegexPattern.Hexadecimal);

    // Assert
    Assert.Equal(expected, result);
  }

  // Teste de padrão ZIP Code.
  [Theory]
  [InlineData("12345-678", true)]
  [InlineData("12345", true)]
  [InlineData("12345678", false)]
  public void TestZipCodePattern(string input, bool expected)
  {
    // Arrange & Act
    var result = Regex.IsMatch(input, RegexPattern.ZipCode);

    // Assert
    Assert.Equal(expected, result);
  }

  // Teste de padrão Base64.
  [Theory]
  [InlineData("data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAUA", true)]
  [InlineData("data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAUA==", true)]
  [InlineData("data:;base64,iVBORw0KGgoAAAANSUhEUgAAAAUA=", false)]
  [InlineData("data:image/png;base64,", false)]
  [InlineData("data:;base64,", false)]
  public void TestBase64Pattern(string input, bool expected)
  {
    // Arrange & Act
    var result = Regex.IsMatch(input, RegexPattern.Base64);

    // Assert
    Assert.Equal(expected, result);
  }

  // Teste de padrão Password.
  [Theory]
  [InlineData("aA0!@#$%¨&*()_+-=[{]]<,>.:;?/\\", true)]
  [InlineData("aA0123456789!", true)]
  [InlineData("aABCDEFGHIJKLMNOPQRSTUVWXYZ0!", true)]
  [InlineData("abcdefghijklmnopqrstuvwxyzA0!", true)]
  [InlineData("abAB01!@", true)]
  [InlineData("abcdefghijklmnopqrstuvwxyz", false)]
  [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", false)]
  [InlineData("0123456789", false)]
  [InlineData("!@#$%¨&*()_+-=[{]]<,>.:;?/\\", false)]
  [InlineData("aA0!", false)]
  public void TestPasswordPattern(string input, bool expected)
  {
    // Arrange & Act
    var result = Regex.IsMatch(input, RegexPattern.ComplexPasswordPattern);

    // Assert
    Assert.Equal(expected, result);
  }

  // Teste de padrão Culture.
  [Theory]
  [InlineData("aa-AA", true)]
  [InlineData("pt-BR", true)]
  [InlineData("pt-br", false)]
  [InlineData("PT-br", false)]
  [InlineData("PT-BR", false)]
  [InlineData("12-12", false)]
  [InlineData("ptbr", false)]
  [InlineData("ptBR", false)]
  [InlineData("PTBR", false)]
  [InlineData("pt", false)]
  [InlineData("PT", false)]
  public void TestCulturePattern(string input, bool expected)
  {
    // Arrange & Act
    var result = Regex.IsMatch(input, RegexPattern.Culture);

    // Assert
    Assert.Equal(expected, result);
  }

  // Teste de padrão de URL.
  [Theory]
  [InlineData("ftp://example.com", true)]
  [InlineData("sftp://example.com", true)]
  [InlineData("http://example.com", true)]
  [InlineData("https://example.com", true)]
  [InlineData("imap://example.com", true)]
  [InlineData("pop3://example.com", true)]
  [InlineData("smtp://example.com", true)]
  [InlineData("ws://example.com", true)]
  [InlineData("wss://example.com", true)]
  [InlineData("example.com", false)]
  public void TestUrlPattern(string input, bool expected)
  {
    // Arrange & Act
    var result = Regex.IsMatch(input, RegexPattern.Url);

    // Assert
    Assert.Equal(expected, result);
  }

  // Teste de padrão de FTP.
  [Theory]
  [InlineData("ftp://example.com", true)]
  [InlineData("sftp://example.com", false)]
  [InlineData("http://example.com", false)]
  public void TestFtpPattern(string input, bool expected)
  {
    // Arrange & Act
    var result = Regex.IsMatch(input, RegexPattern.Ftp);

    // Assert
    Assert.Equal(expected, result);
  }

  // Teste de padrão de SFTP.
  [Theory]
  [InlineData("sftp://example.com", true)]
  [InlineData("ftp://example.com", false)]
  [InlineData("http://example.com", false)]
  public void TestSftpPattern(string input, bool expected)
  {
    // Arrange & Act
    var result = Regex.IsMatch(input, RegexPattern.Sftp);

    // Assert
    Assert.Equal(expected, result);
  }

  // Teste de padrão de FTP e SFTP.
  [Theory]
  [InlineData("ftp://example.com", true)]
  [InlineData("sftp://example.com", true)]
  [InlineData("http://example.com", false)]
  public void TestProtocolFtpPattern(string input, bool expected)
  {
    // Arrange & Act
    var result = Regex.IsMatch(input, RegexPattern.ProtocolFtp);

    // Assert
    Assert.Equal(expected, result);
  }  

  // Teste de padrão de HTTP.
  [Theory]
  [InlineData("http://example.com", true)]
  [InlineData("https://example.com", false)]
  [InlineData("ftp://example.com", false)]
  public void TestHttpPattern(string input, bool expected)
  {
    // Arrange & Act
    var result = Regex.IsMatch(input, RegexPattern.Http);

    // Assert
    Assert.Equal(expected, result);
  }

  // Teste de padrão de HTTPS.
  [Theory]
  [InlineData("https://example.com", true)]
  [InlineData("http://example.com", false)]
  [InlineData("ftp://example.com", false)]
  public void TestHttpsPattern(string input, bool expected)
  {
    // Arrange & Act
    var result = Regex.IsMatch(input, RegexPattern.Https);

    // Assert
    Assert.Equal(expected, result);
  }

  // Teste de padrão de HTTP e HTTPS.
  [Theory]
  [InlineData("http://example.com", true)]
  [InlineData("https://example.com", true)]
  [InlineData("ftp://example.com", false)]
  public void TestProtocolHttpPattern(string input, bool expected)
  {
    // Arrange & Act
    var result = Regex.IsMatch(input, RegexPattern.ProtocolHttp);

    // Assert
    Assert.Equal(expected, result);
  }

  // Teste de padrão de IMAP.
  [Theory]
  [InlineData("imap://example.com", true)]
  [InlineData("pop3://example.com", false)]
  [InlineData("http://example.com", false)]
  public void TestImapPattern(string input, bool expected)
  {
    // Arrange & Act
    var result = Regex.IsMatch(input, RegexPattern.Imap);

    // Assert
    Assert.Equal(expected, result);
  }

  // Teste de padrão de POP3.
  [Theory]
  [InlineData("pop3://example.com", true)]
  [InlineData("imap://example.com", false)]
  [InlineData("http://example.com", false)]
  public void TestPopPattern(string input, bool expected)
  {
    // Arrange & Act
    var result = Regex.IsMatch(input, RegexPattern.Pop3);

    // Assert
    Assert.Equal(expected, result);
  }

  // Teste de padrão de protocolo de recebimento de email.
  [Theory]
  [InlineData("imap://example.com", true)]
  [InlineData("pop3://example.com", true)]
  [InlineData("http://example.com", false)]
  public void TestProtocolEmailReceiverPattern(string input, bool expected)
  {
    // Arrange & Act
    var result = Regex.IsMatch(input, RegexPattern.ProtocolEmailReceiver);

    // Assert
    Assert.Equal(expected, result);
  }

  // Teste de padrão de SMTP.
  [Theory]
  [InlineData("smtp://example.com", true)]
  [InlineData("http://example.com", false)]
  public void TestSmtpPattern(string input, bool expected)
  {
    // Arrange & Act
    var result = Regex.IsMatch(input, RegexPattern.Smtp);

    // Assert
    Assert.Equal(expected, result);
  }

  // Teste de padrão de protocolo de envio de email.
  [Theory]
  [InlineData("smtp://example.com", true)]
  [InlineData("http://example.com", false)]
  public void TestProtocolEmailSenderPattern(string input, bool expected)
  {
    // Arrange & Act
    var result = Regex.IsMatch(input, RegexPattern.ProtocolEmailSender);

    // Assert
    Assert.Equal(expected, result);
  }

  // Teste de padrão de WS.
  [Theory]
  [InlineData("ws://example.com", true)]
  [InlineData("wss://example.com", false)]
  [InlineData("http://example.com", false)]
  public void TestWsPattern(string input, bool expected)
  {
    // Arrange & Act
    var result = Regex.IsMatch(input, RegexPattern.Ws);

    // Assert
    Assert.Equal(expected, result);
  }

  // Teste de padrão de WSS.
  [Theory]
  [InlineData("wss://example.com", true)]
  [InlineData("ws://example.com", false)]
  [InlineData("http://example.com", false)]
  public void TestWssPattern(string input, bool expected)
  {
    // Arrange & Act
    var result = Regex.IsMatch(input, RegexPattern.Wss);

    // Assert
    Assert.Equal(expected, result);
  }

  // Teste de padrão de protocolo Web Socket.
  [Theory]
  [InlineData("ws://example.com", true)]
  [InlineData("wss://example.com", true)]
  [InlineData("http://example.com", false)]
  public void TestProtocolWebSocketPattern(string input, bool expected)
  {
    // Arrange & Act
    var result = Regex.IsMatch(input, RegexPattern.ProtocolWebSocket);

    // Assert
    Assert.Equal(expected, result);
  }
}
