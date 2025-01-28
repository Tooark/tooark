using Tooark.Validations;

namespace Tooark.ValueObjects;

/// <summary>
/// Representa um CNPJ.
/// </summary>
public sealed class Cnpj : ValueObject
{
  /// <summary>
  /// Valor privado do número do CNPJ.
  /// </summary>
  private readonly string _number = null!;


  /// <summary>
  /// Inicializa uma nova instância da classe Cnpj com o número.
  /// </summary>
  /// <param name="number">O número do CNPJ a ser validado.</param>
  public Cnpj(string number)
  {
    // Adiciona as notificações de validação do CNPJ
    AddNotifications(new Contract()
      .IsCnpj(number, "Cnpj.Number", "Field.Invalid;Cnpj.Number")
    );

    // Verifica é valido então não existe notificação
    if (IsValid)
    {
      // Verifica se o número do CNPJ é válido
      if (Validate(number))
      {
        // Define o valor do número do CNPJ
        _number = number;
      }
      else
      {
        // Adiciona uma notificação de validação do CNPJ
        AddNotification("Cnpj.Number", "Field.Invalid;Cnpj.Number");
      }
    }
  }


  /// <summary>
  /// Valor do número do CNPJ.
  /// </summary>
  public string Number { get => _number; }


  /// <summary>
  /// Valida um número de CNPJ.
  /// </summary>
  /// <param name="value">O número do CNPJ a ser validado.</param>
  /// <returns>True se o número do CNPJ for válido</returns>
  internal static bool Validate(string value)
  {
    // Remove os caracteres especiais do CNPJ
    value = value.Trim().Replace(".", "").Replace("/", "").Replace("-", "");

    // Verifica se o número do CNPJ tem 18 caracteres e se é diferente de 0
    if (value.Length != 14 || long.Parse(value) == 0)
    {
      return false;
    }

    // Cria as variáveis para o cálculo
    int multi1 = 5;
    int multi2 = 6;
    int sum1 = 0;
    int sum2 = 0;

    // Percorre os 13 primeiros dígitos do CNPJ
    for (int i = 0; i < 13; i++)
    {
      // Pega o dígito da iteração
      var digit = int.Parse(value[i].ToString());

      // Calcula a soma dos dígitos
      sum1 = i < 12 ? sum1 + digit * multi1 : sum1;
      sum2 += digit * multi2;

      // Atualiza os multiplicadores
      multi1 = multi1 <= 2 ? 9 : multi1 - 1;
      multi2 = multi2 <= 2 ? 9 : multi2 - 1;
    }

    // Calcula os dígitos verificadores
    var digit1 = sum1 % 11;
    var digit2 = sum2 % 11;

    // Verifica se os dígitos são menores que 2
    digit1 = digit1 < 2 ? 0 : 11 - digit1;
    digit2 = digit2 < 2 ? 0 : 11 - digit2;

    // Verifica se os dígitos verificadores são iguais aos dígitos do CNPJ
    return $"{digit1}{digit2}" == value[12..];
  }

  /// <summary>
  /// Sobrescrita do método <see cref="object.ToString"/> para retornar o valor do CNPJ.
  /// </summary>
  /// <returns>O valor do CNPJ.</returns>
  public override string ToString() => _number;

  /// <summary>
  /// Define uma conversão implícita de um objeto Cnpj para uma string.
  /// </summary>
  /// <param name="document">O objeto Cnpj a ser convertido.</param>
  /// <returns>Uma string que representa o valor do CNPJ.</returns>
  public static implicit operator string(Cnpj document) => document._number;

  /// <summary>
  /// Define uma conversão implícita de uma string para um objeto Cnpj.
  /// </summary>
  /// <param name="value">A string a ser convertida em um objeto Cnpj.</param>
  /// <returns>Um objeto Cnpj criado a partir da string fornecida.</returns>
  public static implicit operator Cnpj(string value) => new(value);
}
