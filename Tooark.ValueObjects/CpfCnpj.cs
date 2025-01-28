using Tooark.Validations;

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
    // Adiciona as notificações de validação do CPF ou CNPJ
    AddNotifications(new Contract()
      .IsCpfCnpj(number, "CpfCnpj.Number", "Field.Invalid;CpfCnpj.Number")
    );

    // Verifica é valido então não existe notificação
    if (IsValid)
    {
      // Verifica se o número do CPF ou CNPJ é válido
      if (Validate(number))
      {
        // Define o valor do número do CPF ou CNPJ
        _number = number;
      }
      else
      {
        // Adiciona uma notificação de validação do CPF ou CNPJ
        AddNotification("CpfCnpj.Number", "Field.Invalid;CpfCnpj.Number");
      }
    }
  }


  /// <summary>
  /// Valor do número do CPF ou CNPJ.
  /// </summary>
  public string Number { get => _number; }


  /// <summary>
  /// Valida um número de CPF ou CNPJ.
  /// </summary>
  /// <param name="value">O número do CPF ou CNPJ a ser validado.</param>
  /// <returns>True se o número do CPF ou CNPJ for válido</returns>
  internal static bool Validate(string value)
  {
    // Remove os caracteres especiais do CPF ou CNPJ
    value = value.Trim().Replace(".", "").Replace("/", "").Replace("-", "");

    // Verifica se o valor é maior que 0
    if (long.Parse(value) > 0)
    {
      // Verifica é um CPF
      if (value.Length == 11)
      {
        // Utiliza a validação do CPF
        return Cpf.Validate(value);
      }
      // Verifica é um CNPJ
      else
      {
        // Utiliza a validação do CNPJ
        return Cnpj.Validate(value);
      }
    }
    else
    {
      // Retorna falso se não for um CPF ou CNPJ
      return false;
    }
  }

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
