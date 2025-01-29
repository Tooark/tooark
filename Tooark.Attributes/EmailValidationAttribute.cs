using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Tooark.Validations.Patterns;

namespace Tooark.Attributes;

/// <summary>
/// Atributo de validação de endereço de email.
/// </summary>
/// <remarks>
/// O endereço de email é validado utilizando uma expressão regular.
/// </remarks>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public partial class EmailValidationAttribute : ValidationAttribute
{

  /// <summary>
  /// Sobrescreve o método de validação para verificar se o valor é um email válido.
  /// </summary>
  /// <param name="value">O objeto a ser validado.</param>
  /// <returns>Retornar verdadeiro se o valor for um email válido.</returns>
  public override bool IsValid(object? value)
  {
    // Converta o valor para uma string.
    string? email = value?.ToString();

    // Verifique se o valor é nulo.
    if (string.IsNullOrEmpty(email))
    {
      // Defina a mensagem de erro padrão.
      ErrorMessage = "Field.Required;Email";

      // Retorne falso.
      return false;
    }

    // Verifique se o valor é um email válido.
    if (!Regex.IsMatch(email, RegexPattern.Email, RegexOptions.None, TimeSpan.FromMilliseconds(300)))
    {
      // Defina a mensagem de erro padrão.
      ErrorMessage = "Field.Invalid;Email";

      // Retorne falso.
      return false;
    }

    // Retorne verdadeiro.
    return true;
  }
}
