using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Tooark.Exceptions;

namespace Tooark.ValueObjects;

/// <summary>
/// Representa um email válido.
/// </summary>
public partial class Email : ValueObject
{
  /// <summary>
  /// Valor privado do email.
  /// </summary>
  private readonly string _value;

  /// <summary>
  /// Obtém o valor do email.
  /// </summary>
  public string Value { get => _value; }


  /// <summary>
  /// Inicializa uma nova instância da classe Email com o valor especificado.
  /// </summary>
  /// <param name="value">O valor do email a ser validado.</param>
  public Email(string value)
  {
    // Verifica se o email é nulo, vazio ou inválido.
    if (string.IsNullOrWhiteSpace(value) || !new EmailAddressAttribute().IsValid(value) || !IsValidEmail(value))
    {
      throw AppException.BadRequest("Field.Invalid;Email");
    }

    // Normaliza o email para minúsculas e remove espaços em branco.
    value = value.ToLowerInvariant().Trim();

    // Define o valor do email.
    _value = value;
  }


  /// <summary>
  /// Valida o formato do email.
  /// </summary>
  /// <param name="email">O email a ser validado.</param>
  /// <returns>Verdadeiro se o email for válido, falso caso contrário.</returns>
  private static bool IsValidEmail(string email)
  {
    // Utilize uma expressão regular para validar o formato do email.
    var regex = EmailRegex();

    // Verifica se o email atende ao formato especificado.
    return regex.IsMatch(email);
  }

  /// <summary>
  /// Obtém a expressão regular para validar o formato do email.
  /// </summary>
  [GeneratedRegex(
    @"^[a-zA-Z0-9]+[a-zA-Z0-9_.-]*[a-zA-Z0-9]+@[a-zA-Z0-9]+[a-zA-Z0-9.-]*[a-zA-Z0-9]+\.[a-zA-Z0-9]+[a-zA-Z0-9.]*[a-zA-Z0-9]$",
    RegexOptions.IgnoreCase,
    matchTimeoutMilliseconds: 250)]
  private static partial Regex EmailRegex();


  /// <summary>
  /// Sobrescrita do método <see cref="object.ToString"/> para retornar o valor do email.
  /// </summary>
  /// <returns>Uma string que representa o valor do email.</returns>
  public override string ToString() => _value;

  /// <summary>
  /// Define uma conversão implícita de um objeto Email para uma string.
  /// </summary>
  /// <param name="email">O objeto Email a ser convertido.</param>
  /// <returns>A string que representa o valor do email.</returns>
  public static implicit operator string(Email email) => email._value;

  /// <summary>
  /// Define uma conversão implícita de uma string para um objeto Email.
  /// </summary>
  /// <param name="value">A string a ser convertida em um objeto Email.</param>
  /// <returns>O objeto Email criado a partir da string fornecida.</returns>
  public static implicit operator Email(string value) => new(value);
}
