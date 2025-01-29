using Tooark.Enums;

namespace Tooark.ValueObjects;

/// <summary>
/// Representa um CPF ou CNPJ.
/// </summary>
public sealed class CpfCnpj : ValueObject
{
  /// <summary>
  /// Valor privado do número do CPF ou CNPJ.
  /// </summary>
  private readonly string _number = null!;


  /// <summary>
  /// Inicializa uma nova instância da classe CpfCnpj com o número.
  /// </summary>
  /// <param name="number">O número do CPF ou CNPJ a ser validado.</param>
  public CpfCnpj(string number)
  {
    // Valida documento do tipo CPF ou CNPJ
    string value = new Document(number, EDocumentType.CPF_CNPJ);

    // Verifica se é válido, então não existe notificação
    if (value != null)
    {
      // Define o valor do número da CPF ou CNPJ
      _number = value;
    }
    else
    {
      // Adiciona uma notificação de validação do CPF ou CNPJ
      AddNotification("CpfCnpj", "Field.Invalid;CpfCnpj");
    }
  }


  /// <summary>
  /// Valor do número do CPF ou CNPJ.
  /// </summary>
  public string Number { get => _number; }


  /// <summary>
  /// Sobrescrita do método <see cref="object.ToString"/> para retornar o valor do CPF ou CNPJ.
  /// </summary>
  /// <returns>O valor do CPF ou CNPJ.</returns>
  public override string ToString() => _number;

  /// <summary>
  /// Define uma conversão implícita de um objeto CpfCnpj para uma string.
  /// </summary>
  /// <param name="document">O objeto CpfCnpj a ser convertido.</param>
  /// <returns>Uma string que representa o valor do CPF ou CNPJ.</returns>
  public static implicit operator string(CpfCnpj document) => document._number;

  /// <summary>
  /// Define uma conversão implícita de uma string para um objeto CpfCnpj.
  /// </summary>
  /// <param name="value">A string a ser convertida em um objeto CpfCnpj.</param>
  /// <returns>Um objeto CpfCnpj criado a partir da string fornecida.</returns>
  public static implicit operator CpfCnpj(string value) => new(value);
}
