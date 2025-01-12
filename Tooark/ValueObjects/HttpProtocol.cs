using Tooark.Exceptions;

namespace Tooark.ValueObjects;

/// <summary>
/// Classe que representa um protocolo HTTP. Protocolo para transferência de dados.
/// </summary>
public class HttpProtocol
{
  /// <summary>
  /// Valor privado do protocolo HTTP.
  /// </summary>
  private readonly string _value;

  /// <summary>
  /// Valor do protocolo HTTP.
  /// </summary>
  public string Value { get => _value; }


  /// <summary>
  /// Construtor padrão.
  /// </summary>
  /// <param name="value">Valor do protocolo HTTP.</param>
  public HttpProtocol(string value)
  {
    // Verifica se o valor é um protocolo HTTP válido.
    if (!IsValidHttp(value))
    {
      throw AppException.BadRequest("Field.Invalid;HttpProtocol");
    }

    // Atribui o valor ao campo privado.
    _value = value;
  }


  /// <summary>
  /// Verifica se o valor é um protocolo HTTP válido.
  /// </summary>
  /// <param name="value">Valor do protocolo HTTP.</param>
  /// <returns>Retorna verdadeiro se o valor for um protocolo HTTP válido.</returns>
  public static bool IsValidHttp(string value) => value.StartsWith("http://") || value.StartsWith("https://");

  /// <summary>
  /// Sobrescrita do método <see cref="object.ToString"/> para retornar o valor do protocolo HTTP.
  /// </summary>
  /// <returns>Retorna o valor do protocolo HTTP.</returns>
  public override string ToString() => _value;

  /// <summary>
  /// Conversão implícita de <see cref="HttpProtocol"/> para <see cref="string"/>.
  /// </summary>
  /// <param name="value">Instância de <see cref="HttpProtocol"/>.</param>
  /// <returns>Retorna o valor do protocolo HTTP.</returns>
  public static implicit operator string(HttpProtocol value) => value.Value;

  /// <summary>
  /// Conversão implícita de <see cref="string"/> para <see cref="HttpProtocol"/>.
  /// </summary>
  /// <param name="value">Valor do protocolo HTTP.</param>
  /// <returns>Retorna uma instância de <see cref="HttpProtocol"/>.</returns>
  public static implicit operator HttpProtocol(string value) => new(value);
}
