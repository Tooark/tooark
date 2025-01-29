using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Tooark.Enums;

namespace Tooark.Attributes;

/// <summary>
/// Atributo de validação de documento.
/// </summary>
/// <remarks>
/// O documento é validado utilizando uma expressão regular.
/// </remarks>
/// <param name="type">Tipo de documento a ser validado.</param>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public partial class DocumentValidationAttribute(EDocumentType type) : ValidationAttribute
{
  /// <summary>
  /// Sobrescreve o método de validação para verificar se o valor é um documento válido.
  /// </summary>
  /// <param name="value">O objeto a ser validado.</param>
  /// <returns>Retornar verdadeiro se o valor for um documento válido.</returns>
  public override bool IsValid(object? value)
  {
    // Converta o valor para uma string.
    string? document = value?.ToString();

    // Verifique se o valor é nulo.
    if (string.IsNullOrEmpty(document))
    {
      // Defina a mensagem de erro padrão.
      ErrorMessage = "Field.Required;Document";

      // Retorne falso.
      return false;
    }

    // Verifique se o valor é um document válido.
    if (
      !Regex.IsMatch(document, type.ToRegex(), RegexOptions.None, TimeSpan.FromMilliseconds(300)) ||
      !type.IsValid(document)
    )
    {
      // Defina a mensagem de erro padrão.
      ErrorMessage = "Field.Invalid;Document";

      // Retorne falso.
      return false;
    }

    // Retorne verdadeiro.
    return true;
  }
}
