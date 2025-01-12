using Tooark.Exceptions;

namespace Tooark.ValueObjects;

/// <summary>
/// Classe que representa um protocolo FTP. Protocolo para transferência de arquivos.
/// </summary>
public class FtpProtocol
{
  /// <summary>
  /// Valor privado do protocolo FTP.
  /// </summary>
  private readonly string _value;

  /// <summary>
  /// Valor do protocolo FTP.
  /// </summary>
  public string Value { get => _value; }


  /// <summary>
  /// Construtor padrão.
  /// </summary>
  /// <param name="value">Valor do protocolo FTP.</param>
  public FtpProtocol(string value)
  {
    // Verifica se o valor é um protocolo FTP válido.
    if (!IsValidFtp(value))
    {
      throw AppException.BadRequest("Field.Invalid;FtpProtocol");
    }

    // Atribui o valor ao campo privado.
    _value = value;
  }


  /// <summary>
  /// Verifica se o valor é um protocolo FTP válido.
  /// </summary>
  /// <param name="value">Valor do protocolo FTP.</param>
  /// <returns>Retorna verdadeiro se o valor for um protocolo FTP válido.</returns>
  public static bool IsValidFtp(string value) => value.StartsWith("ftp://") ||value.StartsWith("sftp://");

  /// <summary>
  /// Sobrescrita do método <see cref="object.ToString"/> para retornar o valor do protocolo FTP.
  /// </summary>
  /// <returns>Retorna o valor do protocolo FTP.</returns>
  public override string ToString() => _value;

  /// <summary>
  /// Conversão implícita de <see cref="FtpProtocol"/> para <see cref="string"/>.
  /// </summary>
  /// <param name="value">Instância de <see cref="FtpProtocol"/>.</param>
  /// <returns>Retorna o valor do protocolo FTP.</returns>
  public static implicit operator string(FtpProtocol value) => value.Value;

  /// <summary>
  /// Conversão implícita de <see cref="string"/> para <see cref="FtpProtocol"/>.
  /// </summary>
  /// <param name="value">Valor do protocolo FTP.</param>
  /// <returns>Retorna uma instância de <see cref="FtpProtocol"/>.</returns>
  public static implicit operator FtpProtocol(string value) => new(value);
}
