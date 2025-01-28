using Tooark.Validations;

namespace Tooark.ValueObjects;

/// <summary>
/// Representa um CPF.
/// </summary>
public sealed class Cpf : ValueObject
{
  /// <summary>
  /// Valor privado do número do CPF.
  /// </summary>
  private readonly string _number = null!;


  /// <summary>
  /// Inicializa uma nova instância da classe Cpf com o número.
  /// </summary>
  /// <param name="number">O número do CPF a ser validado.</param>
  public Cpf(string number)
  {
    // Adiciona as notificações de validação do CPF
    AddNotifications(new Contract()
      .IsCpf(number, "Cpf.Number", "Field.Invalid;Cpf.Number")
    );

    // Verifica é valido então não existe notificação
    if (IsValid)
    {
      // Verifica se o número do CPF é válido
      if (Validate(number))
      {
        // Define o valor do número do CPF
        _number = number;
      }
      else
      {
        // Adiciona uma notificação de validação do CPF
        AddNotification("Cpf.Number", "Field.Invalid;Cpf.Number");
      }
    }
  }


  /// <summary>
  /// Valor do número do CPF.
  /// </summary>
  public string Number { get => _number; }


  /// <summary>
  /// Valida um número de CPF.
  /// </summary>
  /// <param name="value">O número do CPF a ser validado.</param>
  /// <returns>True se o número do CPF for válido</returns>
  internal static bool Validate(string value)
  {
    // Remove os caracteres especiais do CPF
    value = value.Trim().Replace(".", "").Replace("-", "");

    // Verifica se o número do CPF tem 11 caracteres e se é diferente de 0
    if (value.Length != 11 || long.Parse(value) == 0)
    {
      return false;
    }

    // Cria as variáveis para o cálculo
    int multi1 = 10;
    int multi2 = 11;
    int sum1 = 0;
    int sum2 = 0;

    // Calcula a soma dos dígitos com os multiplicadores
    for (int i = 0; i < 9; i++)
    {// Pega o dígito da iteração
      var digit = int.Parse(value[i].ToString());

      // Calcula a soma dos dígitos
      sum1 += digit * multi1;
      sum2 += digit * multi2;

      // Atualiza os multiplicadores
      multi1--;
      multi2--;
    }

    // Calcula o primeiro dígito verificador
    var digit1 = sum1 % 11;
    digit1 = digit1 < 2 ? 0 : 11 - digit1;

    // Calcula o segundo dígito verificador
    var digit2 = (sum2 + digit1 * multi2) % 11;
    digit2 = digit2 < 2 ? 0 : 11 - digit2;

    // Verifica se os dígitos verificadores são iguais aos dígitos do CPF
    return $"{digit1}{digit2}" == value[9..];
  }

  /// <summary>
  /// Sobrescrita do método <see cref="object.ToString"/> para retornar o valor do CPF.
  /// </summary>
  /// <returns>O valor do CPF.</returns>
  public override string ToString() => _number;

  /// <summary>
  /// Define uma conversão implícita de um objeto Cpf para uma string.
  /// </summary>
  /// <param name="document">O objeto Cpf a ser convertido.</param>
  /// <returns>Uma string que representa o valor do CPF.</returns>
  public static implicit operator string(Cpf document) => document._number;

  /// <summary>
  /// Define uma conversão implícita de uma string para um objeto Cpf.
  /// </summary>
  /// <param name="value">A string a ser convertida em um objeto Cpf.</param>
  /// <returns>Um objeto Cpf criado a partir da string fornecida.</returns>
  public static implicit operator Cpf(string value) => new(value);
}
