using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Tooark.Validations.Patterns;

namespace Tooark.Attributes;

/// <summary>
/// Atributo de validação de link de vídeo.
/// </summary>
/// <remarks>
/// O link de vídeo é validado utilizando uma expressão regular.
/// </remarks>
/// <param name="propertyName">Nome da propriedade. Padrão: "Link".</param>
/// <param name="youtube">Permite link do YouTube. Padrão: true.</param>
/// <param name="vimeo">Permite link do Vimeo. Padrão: true.</param>
/// <param name="dailymotion">Permite link do Dailymotion. Padrão: true.</param>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public partial class LinkVideoValidationAttribute(
  string propertyName = "Link",
  bool youtube = true,
  bool vimeo = true,
  bool dailymotion = true
) : ValidationAttribute
{
  private readonly bool _youtube = youtube;
  private readonly bool _vimeo = vimeo;
  private readonly bool _dailymotion = dailymotion;
  private readonly string _propertyName = propertyName;

  /// <summary>
  /// Sobrescreve o método de validação para verificar se o valor é um Link de Vídeo válido.
  /// </summary>
  /// <param name="value">O objeto a ser validada.</param>
  /// <returns>Retornar verdadeiro se o valor for um Link de Vídeo válido.</returns>
  public override bool IsValid(object? value)
  {
    // Defina a mensagem de erro padrão.
    string errorMessage = $"Field.Invalid;{_propertyName}";

    // Converta o valor para uma string.
    string? link = value as string;

    // Verifique se o valor é nulo.
    if (string.IsNullOrEmpty(link))
    {
      // Defina a mensagem de erro de campo obrigatório.
      errorMessage = $"Field.Required;{_propertyName}";
    }
    else
    {
      // Método de validação de expressão regular.
      static bool RegexValidation(string link, string pattern) => Regex.IsMatch(link, pattern, RegexOptions.None, TimeSpan.FromMilliseconds(300));

      // Verifique se o valor é um link válido.
      if (
        (_youtube && RegexValidation(link, RegexPattern.YouTube)) ||
        (_vimeo && RegexValidation(link, RegexPattern.Vimeo)) ||
        (_dailymotion && RegexValidation(link, RegexPattern.Dailymotion))
      )
      {
        // Retorne verdadeiro se o valor for um Link de Vídeo válido.
        return true;
      }
    }

    // Defina a mensagem de erro.
    ErrorMessage = errorMessage;

    // Retorne falso se o valor não for um Link de Vídeo válido.
    return false;
  }
}
