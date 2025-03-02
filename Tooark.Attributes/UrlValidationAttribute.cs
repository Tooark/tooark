using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Tooark.Validations.Patterns;

namespace Tooark.Attributes;

/// <summary>
/// Atributo de validação de URL.
/// </summary>
/// <remarks>
/// A URL é validada utilizando uma expressão regular.
/// </remarks>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public partial class UrlValidationAttribute : ValidationAttribute
{
  /// <summary>
  /// Sobrescreve o método de validação para verificar se o valor é uma URL válida.
  /// </summary>
  /// <param name="value">O objeto a ser validada.</param>
  /// <returns>Retornar verdadeiro se o valor for uma URL válida.</returns>
  public override bool IsValid(object? value)
  {
    // Converta o valor para uma string.
    string? url = value?.ToString();

    // Método de validação de expressão regular.
    static bool RegexValidation(string url, string pattern) => Regex.IsMatch(url, pattern, RegexOptions.None, TimeSpan.FromMilliseconds(300));

    // Verifique se o valor é nulo.
    if (string.IsNullOrEmpty(url))
    {
      // Defina a mensagem de erro padrão.
      ErrorMessage = "Field.Required;Url";

      // Retorne falso.
      return false;
    }

    // Verifique se o valor é um url válida.
    if (
      RegexValidation(url, RegexPattern.ProtocolEmailReceiver) ||
      RegexValidation(url, RegexPattern.ProtocolEmailSender) ||
      RegexValidation(url, RegexPattern.ProtocolFtp) ||
      RegexValidation(url, RegexPattern.ProtocolHttp) ||
      RegexValidation(url, RegexPattern.ProtocolWebSocket)
    )
    {
      // Retorne verdadeiro.
      return true;
    }

    // Defina a mensagem de erro padrão.
    ErrorMessage = "Field.Invalid;Url";

    // Retorne falso.
    return false;
  }
}
