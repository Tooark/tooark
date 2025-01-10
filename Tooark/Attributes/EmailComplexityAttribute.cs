using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Tooark.Attributes;

/// <summary>
/// Atributo de validação para verificar a complexidade de um endereço de email.
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public partial class EmailComplexityAttribute : ValidationAttribute
{
  /// <summary>
  /// Expressão regular gerada para validar o formato do email.
  /// </summary>
  [GeneratedRegex(
    @"^[a-zA-Z0-9]+[a-zA-Z0-9_.-]*[a-zA-Z0-9]+@[a-zA-Z0-9]+[a-zA-Z0-9.-]*[a-zA-Z0-9]+\.[a-zA-Z0-9]+[a-zA-Z0-9.]*[a-zA-Z0-9]$",
    RegexOptions.IgnoreCase,
    matchTimeoutMilliseconds: 250)]
  private static partial Regex EmailRegex();

  /// <summary>
  /// Valida o valor de um objeto como um endereço de email.
  /// </summary>
  /// <param name="value">O objeto a ser validado.</param>
  /// <returns>Verdadeiro se o valor for um email válido, falso caso contrário.</returns>
  public override bool IsValid(object? value)
  {
    if (value == null)
    {
      ErrorMessage = "Field.Required;Email";
      return false;
    }

    string email = value.ToString()!;
    var regex = EmailRegex();

    // Utilize uma expressão regular para validar o formato do email.
    if (!new EmailAddressAttribute().IsValid(email) || !regex.IsMatch(email))
    {
      ErrorMessage = "Field.Invalid;Email";
      return false;
    }

    return true;
  }
}
