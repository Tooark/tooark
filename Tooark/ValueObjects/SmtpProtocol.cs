using Tooark.Exceptions;

namespace Tooark.ValueObjects;

/// <summary>
/// Classe que representa um protocolo SMTP. Protocolo para envio de emails.
/// </summary>
public class SmtpProtocol
{
  /// <summary>
  /// Valor privado do protocolo SMTP.
  /// </summary>
  private readonly string _value;

  /// <summary>
  /// Valor do protocolo SMTP.
  /// </summary>
  public string Value { get => _value; }


  /// <summary>
  /// Construtor padrão.
  /// </summary>
  /// <param name="value">Valor do protocolo SMTP.</param>
  public SmtpProtocol(string value)
  {
    // Verifica se o valor é um protocolo SMTP válido.
    if (!IsValidSmtp(value))
    {
      throw AppException.BadRequest("Field.Invalid;SmtpProtocol");
    }

    // Atribui o valor ao campo privado.
    _value = value;
  }


  /// <summary>
  /// Verifica se o valor é um protocolo SMTP válido.
  /// </summary>
  /// <param name="value">Valor do protocolo SMTP.</param>
  /// <returns>Retorna verdadeiro se o valor for um protocolo SMTP válido.</returns>
  public static bool IsValidSmtp(string value) => value.StartsWith("smtp://");

  /// <summary>
  /// Sobrescrita do método <see cref="object.ToString"/> para retornar o valor do protocolo SMTP.
  /// </summary>
  /// <returns>Retorna o valor do protocolo SMTP.</returns>
  public override string ToString() => _value;

  /// <summary>
  /// Conversão implícita de <see cref="SmtpProtocol"/> para <see cref="string"/>.
  /// </summary>
  /// <param name="value">Instância de <see cref="SmtpProtocol"/>.</param>
  /// <returns>Retorna o valor do protocolo SMTP.</returns>
  public static implicit operator string(SmtpProtocol value) => value.Value;

  /// <summary>
  /// Conversão implícita de <see cref="string"/> para <see cref="SmtpProtocol"/>.
  /// </summary>
  /// <param name="value">Valor do protocolo SMTP.</param>
  /// <returns>Retorna uma instância de <see cref="SmtpProtocol"/>.</returns>
  public static implicit operator SmtpProtocol(string value) => new(value);
}
