using Tooark.Exceptions;

namespace Tooark.ValueObjects;

/// <summary>
/// Classe que representa uma Url.
/// </summary>
public class Url
{
  /// <summary>
  /// Valor privado da Url.
  /// </summary>
  private readonly string _value;

  /// <summary>
  /// Valor da Url.
  /// </summary>
  public string Value { get => _value; }


  /// <summary>
  /// Construtor padrão.
  /// </summary>
  /// <param name="value">Valor da Url.</param>
  public Url(string value)
  {
    // Verifica se o valor é uma Url válida.
    if (!IsValidUrl(value))
    {
      throw AppException.BadRequest("Field.Invalid;Url");
    }

    // Atribui o valor ao campo privado.
    _value = value;
  }


  /// <summary>
  /// Verifica se o valor é uma Url válida.
  /// </summary>
  /// <param name="value">Valor da Url.</param>
  /// <returns>Retorna verdadeiro se o valor for uma Url válida.</returns>
  private static bool IsValidUrl(string value) =>
    FtpProtocol.IsValidFtp(value) ||
    HttpProtocol.IsValidHttp(value) ||
    ImapProtocol.IsValidImap(value) ||
    SmtpProtocol.IsValidSmtp(value) ||
    WsProtocol.IsValidWs(value);

  /// <summary>
  /// Sobrescrita do método <see cref="object.ToString"/> para retornar o valor da Url.
  /// </summary>
  /// <returns>Retorna o valor da Url.</returns>
  public override string ToString() => _value;

  /// <summary>
  /// Conversão implícita de <see cref="Url"/> para <see cref="string"/>.
  /// </summary>
  /// <param name="value">Instância de <see cref="Url"/>.</param>
  /// <returns>Retorna o valor da Url.</returns>
  public static implicit operator string(Url value) => value.Value;

  /// <summary>
  /// Conversão implícita de <see cref="string"/> para <see cref="Url"/>.
  /// </summary>
  /// <param name="value">Valor da Url.</param>
  /// <returns>Retorna uma instância de <see cref="Url"/>.</returns>
  public static implicit operator Url(string value) => new(value);
}
