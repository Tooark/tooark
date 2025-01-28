using Tooark.Validations;

namespace Tooark.ValueObjects;

/// <summary>
/// Representa uma string de apenas letras válido.
/// </summary>
public sealed class Letter : ValueObject
{
  /// <summary>
  /// Valor privado da string de letras.
  /// </summary>
  private readonly string _value = null!;

  /// <summary>
  /// Inicializa uma nova instância da classe Letter com o valor especificado.
  /// </summary>
  /// <param name="value">O valor da string de letras a ser validado.</param>
  public Letter(string value)
  {
    // Adiciona as notificações de validação da string de letras.
    AddNotifications(new Contract()
      .IsLetter(value, "Letter", "Field.Invalid;Letter")
    );

    // Verifica é valido então não existe notificação
    if (IsValid)
    {
      // Define o valor da string de letras
      _value = value;
    }
  }


  /// <summary>
  /// Obtém o valor da string de letras.
  /// </summary>
  public string Value { get => _value; }


  /// <summary>
  /// Sobrescrita do método <see cref="object.ToString"/> para retornar o valor da string de letras.
  /// </summary>
  /// <returns>Uma string que representa o valor da string de letras.</returns>
  public override string ToString() => _value;

  /// <summary>
  /// Define uma conversão implícita de um objeto Letter para uma string.
  /// </summary>
  /// <param name="email">O objeto Letter a ser convertido.</param>
  /// <returns>Uma string que representa o valor do Letter.</returns>
  public static implicit operator string(Letter email) => email._value;

  /// <summary>
  /// Define uma conversão implícita de uma string para um objeto Letter.
  /// </summary>
  /// <param name="value">A string a ser convertida em um objeto Letter.</param>
  /// <returns>Um objeto Letter criado a partir da string fornecida.</returns>
  public static implicit operator Letter(string value) => new(value);
}
