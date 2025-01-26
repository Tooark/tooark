namespace Tooark.Validations.Patterns;

/// <summary>
/// Classe estática que fornece padrões de expressões regulares.
/// </summary>
public static class RegexPattern
{
  #region Documents
  /// <summary>
  /// Padrão de CPF.
  /// </summary>
  public readonly static string Cpf = @"^\d{3}\.\d{3}\.\d{3}-\d{2}$";

  /// <summary>
  /// Padrão de RG.
  /// </summary>
  public readonly static string Rg = @"^\d{2}\.\d{3}\.\d{3}(-[\dXx]{1})?$";

  /// <summary>
  /// Padrão de CNH.
  /// </summary>
  public readonly static string Cnh = @"^\d{11}$";

  /// <summary>
  /// Padrão de CPF ou RG.
  /// </summary>
  public readonly static string CpfRg = @"^(\d{3}\.\d{3}\.\d{3}-\d{2}|\d{2}\.\d{3}\.\d{3}(-[\dXx]{1})?)$";

  /// <summary>
  /// Padrão de CPF, RG ou CNH.
  /// </summary>
  public readonly static string CpfRgCnh = @"^(\d{3}\.\d{3}\.\d{3}-\d{2}|\d{2}\.\d{3}\.\d{3}(-[\dXx]{1})?|\d{11})$";

  /// <summary>
  /// Padrão de CNPJ.
  /// </summary>
  public readonly static string Cnpj = @"^[0-9]{2}\.[0-9]{3}\.[0-9]{3}\/[0-9]{4}-\d{2}$";

  /// <summary>
  /// Padrão de CPF ou CNPJ.
  /// </summary>
  public readonly static string CpfCnpj = @"^(\d{3}\.\d{3}\.\d{3}-\d{2}|[0-9]{2}\.[0-9]{3}\.[0-9]{3}\/[0-9]{4}-\d{2})$";
  #endregion

  #region Email
  /// <summary>
  /// Padrão de e-mail.
  /// </summary>
  public readonly static string Email = @"^[a-zA-Z0-9]+[a-zA-Z0-9_.-]*[a-zA-Z0-9]+@[a-zA-Z0-9]+[a-zA-Z0-9.-]*[a-zA-Z0-9]+\.[a-zA-Z0-9]+[a-zA-Z0-9.]*[a-zA-Z0-9]$";

  /// <summary>
  /// Padrão de domínio de e-mail.
  /// </summary>
  public readonly static string EmailDomain = @"^@[a-zA-Z0-9]+[a-zA-Z0-9.-]*[a-zA-Z0-9]+\.[a-zA-Z0-9]+[a-zA-Z0-9.]*[a-zA-Z0-9]$";
  #endregion

  #region Network
  /// <summary>
  /// Padrão de IP.
  /// </summary>
  public readonly static string Ip = @"^((([01]?\d\d?|2[0-4]\d|25[0-5])\.([01]?\d\d?|2[0-4]\d|25[0-5])\.([01]?\d\d?|2[0-4]\d|25[0-5])\.([01]?\d\d?|2[0-4]\d|25[0-5]))|(([0-9a-fA-F]{1,4}:){7,7}[0-9a-fA-F]{1,4}))$";

  /// <summary>
  /// Padrão de IPv4.
  /// </summary>
  public readonly static string Ipv4 = @"^([01]?\d\d?|2[0-4]\d|25[0-5])\.([01]?\d\d?|2[0-4]\d|25[0-5])\.([01]?\d\d?|2[0-4]\d|25[0-5])\.([01]?\d\d?|2[0-4]\d|25[0-5])$";

  /// <summary>
  /// Padrão de IPv6.
  /// </summary>
  public readonly static string Ipv6 = @"^([0-9a-fA-F]{1,4}:){7,7}[0-9a-fA-F]{1,4}$";

  /// <summary>
  /// Padrão de MAC Address.
  /// </summary>
  public readonly static string MacAddress = @"^([0-9A-Fa-f]{2}[:-]){5}([0-9A-Fa-f]{2})$";
  #endregion

  #region Types
  /// <summary>
  /// Padrão de GUID.
  /// </summary>
  public readonly static string Guid = @"^({){0,1}[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}(}){0,1}$";

  /// <summary>
  /// Padrão de letras.
  /// </summary>
  public readonly static string Letter = @"^[a-zA-Z]+$";

  /// <summary>
  /// Padrão de letras minúsculas.
  /// </summary>
  public readonly static string LetterLower = @"^[a-z]+$";

  /// <summary>
  /// Padrão de letras maiúsculas.
  /// </summary>
  public readonly static string LetterUpper = @"^[A-Z]+$";

  /// <summary>
  /// Padrão de números.
  /// </summary>
  public readonly static string Numeric = @"^[0-9]+$";

  /// <summary>
  /// Padrão de letras e números.
  /// </summary>
  public readonly static string LetterNumeric = @"^[a-zA-Z0-9]+$";

  /// <summary>
  /// Padrão de hexadecimal.
  /// </summary>
  public readonly static string Hexadecimal = @"^[0-9a-fA-F]+$";

  /// <summary>
  /// Padrão de ZIP Code.
  /// </summary>
  public readonly static string ZipCode = @"^[0-9]{5}(-[0-9]{3})?$";

  /// <summary>
  /// Padrão de Base64.
  /// </summary>
  public readonly static string Base64 = @"^data:(?<mime>.+?);base64,(?<data>.+)$";

  /// <summary>
  /// Padrão de Senha Complexa. Exige carácter minúsculo, maiúsculo, numérico e especial e tamanho mínimo de 8.
  /// </summary>
  public readonly static string Password = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%&*()\-_+\=\[\]{}\/?;:.>,<|\\]).{8,}$";

  /// <summary>
  /// Padrão de Cultura para Internacionalização.
  /// </summary>
  public readonly static string Culture = @"^[a-z]{2}-[A-Z]{2}$";

  /// <summary>
  /// Padrão de Cultura para Internacionalização (Ignorando Case Sensitivity).
  /// </summary>
  public readonly static string CultureIgnoreCase = @"^[a-zA-Z]{2}-[a-zA-Z]{2}$";
  #endregion

  #region Protocols
  /// <summary>
  /// Padrão de URL.
  /// </summary>
  public readonly static string Url = @"^((s)?ftp|http(s)?|imap|pop3|smtp|ws(s)?):\/\/(((www\.[a-zA-Z0-9]+[a-zA-Z0-9.-]*[a-zA-Z0-9]+)|([a-zA-Z0-9]+[a-zA-Z0-9.-]*[a-zA-Z0-9]+))\.[a-zA-Z0-9]+[a-zA-Z0-9.]*[a-zA-Z0-9])$";

  /// <summary>
  /// Padrão de FTP.
  /// </summary>
  public readonly static string Ftp = @"^ftp:\/\/.*";

  /// <summary>
  /// Padrão de SFTP.
  /// </summary>
  public readonly static string Sftp = @"^sftp:\/\/.*";

  /// <summary>
  /// Padrão de Protocolo FTP.
  /// </summary>
  public readonly static string ProtocolFtp = @"^(s)?ftp:\/\/.*";

  /// <summary>
  /// Padrão de HTTP.
  /// </summary>
  public readonly static string Http = @"^http:\/\/.*";

  /// <summary>
  /// Padrão de HTTPS.
  /// </summary>
  public readonly static string Https = @"^https:\/\/.*";

  /// <summary>
  /// Padrão de Protocolo HTTP.
  /// </summary>
  public readonly static string ProtocolHttp = @"^http(s)?:\/\/.*";

  /// <summary>
  /// Padrão de IMAP.
  /// </summary>
  public readonly static string Imap = @"^imap:\/\/.*";

  /// <summary>
  /// Padrão de POP3.
  /// </summary>
  public readonly static string Pop3 = @"^pop3:\/\/.*";

  /// <summary>
  /// Padrão de Protocolo para Receber Email.
  /// </summary>
  public readonly static string ProtocolEmailReceiver = @"^(imap|pop3):\/\/.*";

  /// <summary>
  /// Padrão de SMTP.
  /// </summary>
  public readonly static string Smtp = @"^smtp:\/\/.*";
  
  /// <summary>
  /// Padrão de Protocolo para Enviar Email.
  /// </summary>
  public readonly static string ProtocolEmailSender = @"^smtp:\/\/.*";

  /// <summary>
  /// Padrão de Web Socket.
  /// </summary>
  public readonly static string Ws = @"^ws:\/\/.*";

  /// <summary>
  /// Padrão de Web Socket Seguro.
  /// </summary>
  public readonly static string Wss = @"^wss:\/\/.*";
  
  /// <summary>
  /// Padrão de Protocolo Web Socket.
  /// </summary>
  public readonly static string ProtocolWebSocket = @"^ws(s)?:\/\/.*";
  #endregion
}
