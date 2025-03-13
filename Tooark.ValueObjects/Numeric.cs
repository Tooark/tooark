using Tooark.Validations;

namespace Tooark.ValueObjects;

/// <summary>
/// Representa uma string de apenas números válido.
/// </summary>
public sealed class Numeric : ValueObject
{
  /// <summary>
  /// Valor privado da string de números.
  /// </summary>
  private readonly string _value = null!;

  /// <summary>
  /// Inicializa uma nova instância da classe Numeric com o valor especificado.
  /// </summary>
  /// <param name="value">O valor da string de números a ser validado.</param>
  public Numeric(string value)
  {
    // Adiciona as notificações de validação da string de números.
    AddNotifications(new Validation()
      .IsNumeric(value, "Numeric", "Field.Invalid;Numeric")
    );

    // Verifica é valido então não existe notificação
    if (IsValid)
    {
      // Define o valor da string de números
      _value = value;
    }
  }


  /// <summary>
  /// Obtém o valor da string de números.
  /// </summary>
  public string Value { get => _value; }


  /// <summary>
  /// Sobrescrita do método <see cref="object.ToString"/> para retornar o valor da string de números.
  /// </summary>
  /// <returns>Uma string que representa o valor da string de números.</returns>
  public override string ToString() => _value;

  /// <summary>
  /// Define uma conversão implícita de um objeto Numeric para uma string.
  /// </summary>
  /// <param name="email">O objeto Numeric a ser convertido.</param>
  /// <returns>Uma string que representa o valor do Numeric.</returns>
  public static implicit operator string(Numeric email) => email._value;

  /// <summary>
  /// Define uma conversão implícita de uma string para um objeto Numeric.
  /// </summary>
  /// <param name="value">A string a ser convertida em um objeto Numeric.</param>
  /// <returns>Um objeto Numeric criado a partir da string fornecida.</returns>
  public static implicit operator Numeric(string value) => new(value);
}
