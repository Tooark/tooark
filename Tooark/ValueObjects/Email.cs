using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Tooark.ValueObjects;

/// <summary>
/// Representa um email válido.
/// </summary>
public partial class Email : ValueObject
{
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
    if (string.IsNullOrWhiteSpace(value) || !new EmailAddressAttribute().IsValid(value) || !IsValidEmail(value))
    {
      throw new ArgumentException("InvalidField;Email");
    }

    value = value.ToLower().Trim();

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
    return regex.IsMatch(email);
  }

  [GeneratedRegex(
    @"^[a-zA-Z0-9]+[a-zA-Z0-9_.-]*[a-zA-Z0-9]+@[a-zA-Z0-9]+[a-zA-Z0-9.-]*[a-zA-Z0-9]+\.[a-zA-Z0-9]+[a-zA-Z0-9.]*[a-zA-Z0-9]$",
    RegexOptions.IgnoreCase,
    matchTimeoutMilliseconds: 250)]
  private static partial Regex EmailRegex();

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
