using Tooark.Validations;

namespace Tooark.ValueObjects;

/// <summary>
/// Representa uma CNH.
/// </summary>
public sealed class Cnh : ValueObject
{
  /// <summary>
  /// Valor privado do número da CNH.
  /// </summary>
  private readonly string _number = null!;


  /// <summary>
  /// Inicializa uma nova instância da classe Cnh com o número.
  /// </summary>
  /// <param name="number">O número da CNH a ser validado.</param>
  public Cnh(string number)
  {
    // Adiciona as notificações de validação da CNH
    AddNotifications(new Contract()
      .IsCnh(number, "Cnh.Number", "Field.Invalid;Cnh.Number")
    );

    // Verifica é valido então não existe notificação
    if (IsValid)
    {
      // Verifica se o número da CNH é válido
      if (Validate(number))
      {
        // Define o valor do número da CNH
        _number = number;
      }
      else
      {
        // Adiciona uma notificação de validação da CNH
        AddNotification("Cnh.Number", "Field.Invalid;Cnh.Number");
      }
    }
  }


  /// <summary>
  /// Valor do número da CNH.
  /// </summary>
  public string Number { get => _number; }


  /// <summary>
  /// Valida um número da CNH.
  /// </summary>
  /// <param name="value">O número da CNH a ser validado.</param>
  /// <returns>True se o número da CNH for válido</returns>
  internal static bool Validate(string value)
  {
    // Verifica se o número da CNH tem 11 caracteres e se é diferente de 0
    if (value.Length != 11 || long.Parse(value) == 0)
    {
      return false;
    }

    // Cria as variáveis para o cálculo
    int multi1 = 9;
    int multi2 = 1;
    int sum1 = 0;
    int sum2 = 0;

    // Percorre os 9 primeiros dígitos da CNH
    for (int i = 0; i < 9; i++)
    {
      // Pega o dígito da iteração
      var digit = int.Parse(value[i].ToString());

      // Calcula a soma dos dígitos
      sum1 += digit * multi1;
      sum2 += digit * multi2;

      // Atualiza os multiplicadores
      multi1--;
      multi2++;
    }

    // Calcula os dígitos verificadores
    var digit1 = sum1 % 11;
    var digit2 = sum2 % 11;

    // Verifica se os dígitos verificadores são 10
    digit1 = digit1 == 10 ? 0 : digit1;
    digit2 = digit2 == 10 ? 0 : digit2;

    // Verifica se os dígitos verificadores são iguais aos dígitos da CNH
    return $"{digit1}{digit2}" == value[9..];
  }

  /// <summary>
  /// Sobrescrita do método <see cref="object.ToString"/> para retornar o valor da CNH.
  /// </summary>
  /// <returns>O valor da CNH.</returns>
  public override string ToString() => _number;

  /// <summary>
  /// Define uma conversão implícita de um objeto Cnh para uma string.
  /// </summary>
  /// <param name="document">O objeto Cnh a ser convertido.</param>
  /// <returns>Uma string que representa o valor da CNH.</returns>
  public static implicit operator string(Cnh document) => document._number;

  /// <summary>
  /// Define uma conversão implícita de uma string para um objeto Cnh.
  /// </summary>
  /// <param name="value">A string a ser convertida em um objeto Cnh.</param>
  /// <returns>Um objeto Cnh criado a partir da string fornecida.</returns>
  public static implicit operator Cnh(string value) => new(value);
}
