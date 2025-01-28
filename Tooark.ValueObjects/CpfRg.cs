using Tooark.Validations;

namespace Tooark.ValueObjects;

/// <summary>
/// Representa um CPF ou RG.
/// </summary>
public sealed class CpfRg : ValueObject
{
  /// <summary>
  /// Valor privado do número do CPF ou RG.
  /// </summary>
  private readonly string _number = null!;


  /// <summary>
  /// Inicializa uma nova instância da classe CpfRg com o número.
  /// </summary>
  /// <param name="number">O número do CPF ou RG a ser validado.</param>
  public CpfRg(string number)
  {
    // Adiciona as notificações de validação do CPF ou RG
    AddNotifications(new Contract()
      .IsCpfRg(number, "CpfRg.Number", "Field.Invalid;CpfRg.Number")
    );

    // Verifica é valido então não existe notificação
    if (IsValid)
    {
      // Verifica se o número do CPF ou RG é válido
      if (Validate(number))
      {
        // Define o valor do número do CPF ou RG
        _number = number;
      }
      else
      {
        // Adiciona uma notificação de validação do CPF ou RG
        AddNotification("CpfRg.Number", "Field.Invalid;CpfRg.Number");
      }
    }
  }


  /// <summary>
  /// Valor do número do CPF ou RG.
  /// </summary>
  public string Number { get => _number; }


  /// <summary>
  /// Valida um número de CPF ou RG.
  /// </summary>
  /// <param name="value">O número do CPF ou RG a ser validado.</param>
  /// <returns>True se o número do CPF ou RG for válido</returns>
  internal static bool Validate(string value)
  {
    // Remove os caracteres especiais do CPF ou RG
    value = value.Trim().Replace(".", "").Replace("-", "");

    // Verifica se o valor é maior que 0
    if (long.Parse(value[..8]) > 0)
    {
      // Verifica é um CPF
      if (value.Length == 11)
      {
        // Utiliza a validação do CPF
        return Cpf.Validate(value);
      }
      // Verifica é um RG
      else
      {
        // Utiliza a validação do RG
        return Rg.Validate(value);
      }
    }
    else
    {
      // Retorna falso se não for um CPF ou RG
      return false;
    }
  }

  /// <summary>
  /// Sobrescrita do método <see cref="object.ToString"/> para retornar o valor do CPF ou RG.
  /// </summary>
  /// <returns>O valor do CPF ou RG.</returns>
  public override string ToString() => _number;

  /// <summary>
  /// Define uma conversão implícita de um objeto CpfRg para uma string.
  /// </summary>
  /// <param name="document">O objeto CpfRg a ser convertido.</param>
  /// <returns>Uma string que representa o valor do CPF ou RG.</returns>
  public static implicit operator string(CpfRg document) => document._number;

  /// <summary>
  /// Define uma conversão implícita de uma string para um objeto CpfRg.
  /// </summary>
  /// <param name="value">A string a ser convertida em um objeto CpfRg.</param>
  /// <returns>Um objeto CpfRg criado a partir da string fornecida.</returns>
  public static implicit operator CpfRg(string value) => new(value);
}
