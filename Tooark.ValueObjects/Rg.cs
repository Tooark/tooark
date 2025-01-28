using Tooark.Validations;

namespace Tooark.ValueObjects;

/// <summary>
/// Representa um RG.
/// </summary>
public sealed class Rg : ValueObject
{
  /// <summary>
  /// Valor privado do número do RG.
  /// </summary>
  private readonly string _number = null!;


  /// <summary>
  /// Inicializa uma nova instância da classe Rg com o número.
  /// </summary>
  /// <param name="number">O número do RG a ser validado.</param>
  public Rg(string number)
  {
    // Adiciona as notificações de validação do RG
    AddNotifications(new Contract()
      .IsRg(number, "Rg.Number", "Field.Invalid;Rg.Number")
    );

    // Verifica é valido então não existe notificação
    if (IsValid)
    {
      // Verifica se o número do RG é válido
      if (Validate(number))
      {
        // Define o valor do número do RG
        _number = number;
      }
      else
      {
        // Adiciona uma notificação de validação do RG
        AddNotification("Rg.Number", "Field.Invalid;Rg.Number");
      }
    }
  }


  /// <summary>
  /// Valor do número do RG.
  /// </summary>
  public string Number { get => _number; }


  /// <summary>
  /// Valida um número de RG.
  /// </summary>
  /// <param name="value">O número do RG a ser validado.</param>
  /// <returns>True se o número do RG for válido</returns>
  internal static bool Validate(string value)
  {
    // Remove os caracteres especiais do RG
    value = value.Trim().Replace(".", "").Replace("-", "");

    // Verifica se o número do RG tem 8 ou 9 caracteres e se é diferente de 0
    if ((value.Length != 8 && value.Length != 9) || long.Parse(value[..8]) == 0)
    {
      return false;
    }

    // Se o número do RG for 8 caracteres, então é um RG sem dígito verificador
    if(value.Length == 8)
    {
      return true;
    }

    // Cria as variáveis para o cálculo
    int multi = 2;
    int sum = 0;

    // Calcula a soma dos dígitos com os multiplicadores
    for (int i = 0; i < 8; i++)
    {// Pega o dígito da iteração
      var dig = int.Parse(value[i].ToString());

      // Calcula a soma dos dígitos
      sum += dig * multi;

      // Atualiza os multiplicadores
      multi++;
    }

    // Calcula o dígito verificador
    var digit = sum % 11;
    digit = (11 - digit) % 11;

    // Verifica se o dígito verificador é 10
    if (digit == 10)
    {
      // Verifica se o dígito verificador é X ou x
      return value[8] == 'X' || value[8] == 'x';
    }

    // Verifica se os dígitos verificadores são iguais aos dígitos do RG
    return $"{digit}" == value[8..];
  }

  /// <summary>
  /// Sobrescrita do método <see cref="object.ToString"/> para retornar o valor do RG.
  /// </summary>
  /// <returns>O valor do RG.</returns>
  public override string ToString() => _number;

  /// <summary>
  /// Define uma conversão implícita de um objeto Rg para uma string.
  /// </summary>
  /// <param name="document">O objeto Rg a ser convertido.</param>
  /// <returns>Uma string que representa o valor do RG.</returns>
  public static implicit operator string(Rg document) => document._number;

  /// <summary>
  /// Define uma conversão implícita de uma string para um objeto Rg.
  /// </summary>
  /// <param name="value">A string a ser convertida em um objeto Rg.</param>
  /// <returns>Um objeto Rg criado a partir da string fornecida.</returns>
  public static implicit operator Rg(string value) => new(value);
}
