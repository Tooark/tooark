using Tooark.Exceptions;

namespace Tooark.ValueObjects;

/// <summary>
/// Classe que representa um protocolo WS. Protocolo WebSocket para comunicação em tempo real.
/// </summary>
public class WsProtocol
{
  /// <summary>
  /// Valor privado do protocolo WS.
  /// </summary>
  private readonly string _value;

  /// <summary>
  /// Valor do protocolo WS.
  /// </summary>
  public string Value { get => _value; }


  /// <summary>
  /// Construtor padrão.
  /// </summary>
  /// <param name="value">Valor do protocolo WS.</param>
  public WsProtocol(string value)
  {
    // Verifica se o valor é um protocolo WS válido.
    if (!IsValidWs(value))
    {
      throw AppException.BadRequest("Field.Invalid;WsProtocol");
    }

    // Atribui o valor ao campo privado.
    _value = value;
  }


  /// <summary>
  /// Verifica se o valor é um protocolo WS válido.
  /// </summary>
  /// <param name="value">Valor do protocolo WS.</param>
  /// <returns>Retorna verdadeiro se o valor for um protocolo WS válido.</returns>
  public static bool IsValidWs(string value) => value.StartsWith("ws://") || value.StartsWith("wss://");

  /// <summary>
  /// Sobrescrita do método <see cref="object.ToString"/> para retornar o valor do protocolo WS.
  /// </summary>
  /// <returns>Retorna o valor do protocolo WS.</returns>
  public override string ToString() => _value;

  /// <summary>
  /// Conversão implícita de <see cref="WsProtocol"/> para <see cref="string"/>.
  /// </summary>
  /// <param name="value">Instância de <see cref="WsProtocol"/>.</param>
  /// <returns>Retorna o valor do protocolo WS.</returns>
  public static implicit operator string(WsProtocol value) => value.Value;

  /// <summary>
  /// Conversão implícita de <see cref="string"/> para <see cref="WsProtocol"/>.
  /// </summary>
  /// <param name="value">Valor do protocolo WS.</param>
  /// <returns>Retorna uma instância de <see cref="WsProtocol"/>.</returns>
  public static implicit operator WsProtocol(string value) => new(value);
}
