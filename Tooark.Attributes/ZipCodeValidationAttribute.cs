using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Tooark.Validations.Patterns;

namespace Tooark.Attributes;

/// <summary>
/// Atributo de validação de código postal.
/// </summary>
/// <remarks>
/// O código postal é validado utilizando uma expressão regular.
/// </remarks>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public partial class ZipCodeValidationAttribute : ValidationAttribute
{

  /// <summary>
  /// Sobrescreve o método de validação para verificar se o valor é um código postal válido.
  /// </summary>
  /// <param name="value">O objeto a ser validado.</param>
  /// <returns>Retornar verdadeiro se o valor for um código postal válido.</returns>
  public override bool IsValid(object? value)
  {
    // Converta o valor para uma string.
    string? zipCode = value?.ToString();

    // Verifique se o valor é nulo.
    if (string.IsNullOrEmpty(zipCode))
    {
      // Defina a mensagem de erro padrão.
      ErrorMessage = "Field.Required;ZipCode";

      // Retorne falso.
      return false;
    }

    // Verifique se o valor é um código postal válido.
    if (!Regex.IsMatch(zipCode, RegexPattern.ZipCode, RegexOptions.None, TimeSpan.FromMilliseconds(300)))
    {
      // Defina a mensagem de erro padrão.
      ErrorMessage = "Field.Invalid;ZipCode";

      // Retorne falso.
      return false;
    }

    // Retorne verdadeiro.
    return true;
  }
}
