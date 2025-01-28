using Tooark.Validations;

namespace Tooark.ValueObjects;

/// <summary>
/// Representa um protocolo de Recebimento de Email. Protocolos IMAP e POP3.
/// </summary>
public sealed class ProtocolEmailReceiver : ValueObject
{
  /// <summary>
  /// Valor privado do protocolo de Recebimento de Email.
  /// </summary>
  private readonly string _value = null!;


  /// <summary>
  /// Inicializa uma nova instância da classe ProtocolEmailReceiver com o valor especificado.
  /// </summary>
  /// <param name="value">Valor do protocolo de Recebimento de Email.</param>
  public ProtocolEmailReceiver(string value)
  {
    // Adiciona as notificações de validação do protocolo de Recebimento de Email
    AddNotifications(new Contract()
      .IsProtocolEmailReceiver(value, "ProtocolEmailReceiver", "Field.Invalid;ProtocolEmailReceiver")
    );

    // Verifica é valido então não existe notificação
    if (IsValid)
    {
      // Define o valor do protocolo de Recebimento de Email
      _value = value;
    }
  }


  /// <summary>
  /// Valor do protocolo de Recebimento de Email.
  /// </summary>
  public string Value { get => _value; }


  /// <summary>
  /// Sobrescrita do método <see cref="object.ToString"/> para retornar o valor do protocolo de Recebimento de Email.
  /// </summary>
  /// <returns>O valor do protocolo de Recebimento de Email.</returns>
  public override string ToString() => _value;

  /// <summary>
  /// Define uma conversão implícita de um objeto ProtocolEmailReceiver para uma string.
  /// </summary>
  /// <param name="protocol">O objeto ProtocolEmailReceiver a ser convertido.</param>
  /// <returns>Uma string que representa o valor do protocolo de Recebimento de Email.</returns>
  public static implicit operator string(ProtocolEmailReceiver protocol) => protocol._value;

  /// <summary>
  /// Define uma conversão implícita de uma string para um objeto ProtocolEmailReceiver.
  /// </summary>
  /// <param name="value">A string a ser convertida em um objeto ProtocolEmailReceiver.</param>
  /// <returns>Um objeto ProtocolEmailReceiver criado a partir da string fornecida.</returns>
  public static implicit operator ProtocolEmailReceiver(string value) => new(value);
}
