using Tooark.Validations;

namespace Tooark.ValueObjects;

/// <summary>
/// Representa um protocolo WebSocket. Protocolos WS e WSS.
/// </summary>
public sealed class ProtocolWs : ValueObject
{
  /// <summary>
  /// Valor privado do protocolo WebSocket.
  /// </summary>
  private readonly string _value = null!;


  /// <summary>
  /// Inicializa uma nova instância da classe ProtocolWs com o valor especificado.
  /// </summary>
  /// <param name="value">Valor do protocolo WebSocket.</param>
  public ProtocolWs(string value)
  {
    // Adiciona as notificações de validação do protocolo WebSocket
    AddNotifications(new Validation()
      .IsProtocolWebSocket(value, "ProtocolWs", "Field.Invalid;ProtocolWs")
    );

    // Verifica é valido então não existe notificação
    if (IsValid)
    {
      // Define o valor do protocolo WebSocket
      _value = value;
    }
  }


  /// <summary>
  /// Valor do protocolo WebSocket.
  /// </summary>
  public string Value { get => _value; }


  /// <summary>
  /// Sobrescrita do método <see cref="object.ToString"/> para retornar o valor do protocolo WebSocket.
  /// </summary>
  /// <returns>O valor do protocolo WebSocket.</returns>
  public override string ToString() => _value;

  /// <summary>
  /// Define uma conversão implícita de um objeto ProtocolWs para uma string.
  /// </summary>
  /// <param name="protocol">O objeto ProtocolWs a ser convertido.</param>
  /// <returns>Uma string que representa o valor do protocolo WebSocket.</returns>
  public static implicit operator string(ProtocolWs protocol) => protocol._value;

  /// <summary>
  /// Define uma conversão implícita de uma string para um objeto ProtocolWs.
  /// </summary>
  /// <param name="value">A string a ser convertida em um objeto ProtocolWs.</param>
  /// <returns>Um objeto ProtocolWs criado a partir da string fornecida.</returns>
  public static implicit operator ProtocolWs(string value) => new(value);
}
