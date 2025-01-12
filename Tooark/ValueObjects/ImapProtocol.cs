using Tooark.Exceptions;

namespace Tooark.ValueObjects;

/// <summary>
/// Classe que representa um protocolo IMAP/POP3. Protocolo para recebimento de emails.
/// </summary>
public class ImapProtocol
{
  /// <summary>
  /// Valor privado do protocolo IMAP/POP3.
  /// </summary>
  private readonly string _value;

  /// <summary>
  /// Valor do protocolo IMAP/POP3.
  /// </summary>
  public string Value { get => _value; }


  /// <summary>
  /// Construtor padrão.
  /// </summary>
  /// <param name="value">Valor do protocolo IMAP/POP3.</param>
  public ImapProtocol(string value)
  {
    // Verifica se o valor é um protocolo IMAP/POP3 válido.
    if (!IsValidImap(value))
    {
      throw AppException.BadRequest("Field.Invalid;ImapProtocol");
    }

    // Atribui o valor ao campo privado.
    _value = value;
  }


  /// <summary>
  /// Verifica se o valor é um protocolo IMAP/POP3 válido.
  /// </summary>
  /// <param name="value">Valor do protocolo IMAP/POP3.</param>
  /// <returns>Retorna verdadeiro se o valor for um protocolo IMAP/POP3 válido.</returns>
  public static bool IsValidImap(string value) => value.StartsWith("imap://") || value.StartsWith("pop3://");

  /// <summary>
  /// Sobrescrita do método <see cref="object.ToString"/> para retornar o valor do protocolo IMAP/POP3.
  /// </summary>
  /// <returns>Retorna o valor do protocolo IMAP/POP3.</returns>
  public override string ToString() => _value;

  /// <summary>
  /// Conversão implícita de <see cref="ImapProtocol"/> para <see cref="string"/>.
  /// </summary>
  /// <param name="value">Instância de <see cref="ImapProtocol"/>.</param>
  /// <returns>Retorna o valor do protocolo IMAP/POP3.</returns>
  public static implicit operator string(ImapProtocol value) => value.Value;

  /// <summary>
  /// Conversão implícita de <see cref="string"/> para <see cref="ImapProtocol"/>.
  /// </summary>
  /// <param name="value">Valor do protocolo IMAP/POP3.</param>
  /// <returns>Retorna uma instância de <see cref="ImapProtocol"/>.</returns>
  public static implicit operator ImapProtocol(string value) => new(value);
}
