using Tooark.Validations;

namespace Tooark.ValueObjects;

/// <summary>
/// Representa um CPF, RG ou CNH.
/// </summary>
public sealed class CpfRgCnh : ValueObject
{
  /// <summary>
  /// Valor privado do número do CPF, RG ou CNH.
  /// </summary>
  private readonly string _number = null!;


  /// <summary>
  /// Inicializa uma nova instância da classe CpfRgCnh com o número.
  /// </summary>
  /// <param name="number">O número do CPF, RG ou CNH a ser validado.</param>
  public CpfRgCnh(string number)
  {
    // Adiciona as notificações de validação do CPF, RG ou CNH
    AddNotifications(new Contract()
      .IsCpfRgCnh(number, "CpfRgCnh.Number", "Field.Invalid;CpfRgCnh.Number")
    );

    // Verifica é valido então não existe notificação
    if (IsValid)
    {
      // Verifica se o número do CPF, RG ou CNH é válido
      if (Validate(number))
      {
        // Define o valor do número do CPF, RG ou CNH
        _number = number;
      }
      else
      {
        // Adiciona uma notificação de validação do CPF, RG ou CNH
        AddNotification("CpfRgCnh.Number", "Field.Invalid;CpfRgCnh.Number");
      }
    }
  }


  /// <summary>
  /// Valor do número do CPF, RG ou CNH.
  /// </summary>
  public string Number { get => _number; }


  /// <summary>
  /// Valida um número de CPF, RG ou CNH.
  /// </summary>
  /// <param name="value">O número do CPF, RG ou CNH a ser validado.</param>
  /// <returns>True se o número do CPF, RG ou CNH for válido</returns>
  internal static bool Validate(string value)
  {
    // Verifica se o valor contém ponto
    var cpf = value.Contains('.');

    // Remove os caracteres especiais do CPF, RG ou CNH
    value = value.Trim().Replace(".", "").Replace("-", "");

    // Verifica se o valor é maior que 0
    if (long.Parse(value[..8]) > 0)
    {
      // Verifica se o valor tem 11 caracteres
      if (value.Length == 11)
      {
        // Verifica é um CPF
        if (cpf)
        {
          // Utiliza a validação do CPF
          return Cpf.Validate(value);
        }
        // É uma CNH
        else
        {
          // Utiliza a validação do CNH
          return Cnh.Validate(value);
        }
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
      // Retorna falso se não for um CPF, RG ou CNH
      return false;
    }
  }

  /// <summary>
  /// Sobrescrita do método <see cref="object.ToString"/> para retornar o valor do CPF, RG ou CNH.
  /// </summary>
  /// <returns>O valor do CPF, RG ou CNH.</returns>
  public override string ToString() => _number;

  /// <summary>
  /// Define uma conversão implícita de um objeto CpfRgCnh para uma string.
  /// </summary>
  /// <param name="document">O objeto CpfRgCnh a ser convertido.</param>
  /// <returns>Uma string que representa o valor do CPF, RG ou CNH.</returns>
  public static implicit operator string(CpfRgCnh document) => document._number;

  /// <summary>
  /// Define uma conversão implícita de uma string para um objeto CpfRgCnh.
  /// </summary>
  /// <param name="value">A string a ser convertida em um objeto CpfRgCnh.</param>
  /// <returns>Um objeto CpfRgCnh criado a partir da string fornecida.</returns>
  public static implicit operator CpfRgCnh(string value) => new(value);
}
