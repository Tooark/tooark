using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Tooark.Exceptions;

namespace Tooark.ValueObjects;

/// <summary>
/// Representa um domínio de email válido.
/// </summary>
public partial class EmailDomain : ValueObject
{
  /// <summary>
  /// Valor privado do domínio de email.
  /// </summary>
  private readonly string _value;

  /// <summary>
  /// Obtém o valor do domínio de email.
  /// </summary>
  public string Value { get => _value; }


  /// <summary>
  /// Inicializa uma nova instância da classe EmailDomain com o valor especificado.
  /// </summary>
  /// <param name="value">O valor do domínio de email a ser validado.</param>
  public EmailDomain(string value)
  {
    // Verifica se o domínio de email é nulo, vazio ou inválido.
    if (string.IsNullOrWhiteSpace(value) || !IsValidEmailDomain(value))
    {
      throw AppException.BadRequest("Field.Invalid;EmailDomain");
    }

    // Normaliza o domínio de email para minúsculas e remove espaços em branco.
    value = value.ToLowerInvariant().Trim();

    // Define o valor do domínio de email.
    _value = value;
  }


  /// <summary>
  /// Valida o formato do domínio de email.
  /// </summary>
  /// <param name="emailDomain">O domínio de email a ser validado.</param>
  /// <returns>Verdadeiro se o domínio de email for válido, falso caso contrário.</returns>
  private static bool IsValidEmailDomain(string emailDomain)
  {
    // Utilize uma expressão regular para validar o formato do domínio de email.
    var regex = EmailDomainRegex();

    // Verifica se o domínio de email atende ao formato especificado.
    return regex.IsMatch(emailDomain);
  }

  /// <summary>
  /// Obtém a expressão regular para validar o formato do domínio de email.
  /// </summary>
  [GeneratedRegex(
    @"^@[a-zA-Z0-9]+[a-zA-Z0-9.-]*[a-zA-Z0-9]+\.[a-zA-Z0-9]+[a-zA-Z0-9.]*[a-zA-Z0-9]$",
    RegexOptions.IgnoreCase,
    matchTimeoutMilliseconds: 250)]
  private static partial Regex EmailDomainRegex();


  /// <summary>
  /// Sobrescrita do método <see cref="object.ToString"/> para retornar o valor do domínio de email.
  /// </summary>
  /// <returns>Uma string que representa o valor do domínio de email.</returns>
  public override string ToString() => _value;

  /// <summary>
  /// Define uma conversão implícita de um objeto EmailDomain para uma string.
  /// </summary>
  /// <param name="emailDomain">O objeto EmailDomain a ser convertido.</param>
  /// <returns>A string que representa o valor do domínio de email.</returns>
  public static implicit operator string(EmailDomain emailDomain) => emailDomain._value;

  /// <summary>
  /// Define uma conversão implícita de uma string para um objeto EmailDomain.
  /// </summary>
  /// <param name="value">A string a ser convertida em um objeto EmailDomain.</param>
  /// <returns>O objeto EmailDomain criado a partir da string fornecida.</returns>
  public static implicit operator EmailDomain(string value) => new(value);
}
