using Tooark.Validations;

namespace Tooark.ValueObjects;

/// <summary>
/// Representa um código postal válido.
/// </summary>
public class ZipCode : ValueObject
{
  /// <summary>
  /// Valor privado do código postal.
  /// </summary>
  private readonly string _value = null!;

  /// <summary>
  /// Inicializa uma nova instância da classe ZipCode com o valor especificado.
  /// </summary>
  /// <param name="value">O valor do código postal a ser validado.</param>
  public ZipCode(string value)
  {
    // Adiciona as notificações de validação do código postal.
    AddNotifications(new Contract()
      .IsZipCode(value, "ZipCode", "Field.Invalid;ZipCode")
    );

    // Verifica é valido então não existe notificação
    if (IsValid)
    {
      // Define o valor do código postal
      _value = value;
    }
  }


  /// <summary>
  /// Obtém o valor do código postal.
  /// </summary>
  public string Value { get => _value; }


  /// <summary>
  /// Sobrescrita do método <see cref="object.ToString"/> para retornar o valor do código postal.
  /// </summary>
  /// <returns>Uma string que representa o valor do código postal.</returns>
  public override string ToString() => _value;

  /// <summary>
  /// Define uma conversão implícita de um objeto ZipCode para uma string.
  /// </summary>
  /// <param name="email">O objeto ZipCode a ser convertido.</param>
  /// <returns>Uma string que representa o valor do ZipCode.</returns>
  public static implicit operator string(ZipCode email) => email._value;

  /// <summary>
  /// Define uma conversão implícita de uma string para um objeto ZipCode.
  /// </summary>
  /// <param name="value">A string a ser convertida em um objeto ZipCode.</param>
  /// <returns>Um objeto ZipCode criado a partir da string fornecida.</returns>
  public static implicit operator ZipCode(string value) => new(value);
}
