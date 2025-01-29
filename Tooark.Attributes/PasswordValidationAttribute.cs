using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Tooark.Attributes;

/// <summary>
/// Atributo de validação de senha com critérios específicos de complexidade.
/// </summary>
/// <remarks>
/// A senha é validada verificando se ela atende aos critérios de complexidade especificados.
/// </remarks>
/// <param name="lowercase">Exige carácter minúsculo. Padrão: true.</param>
/// <param name="uppercase">Exige carácter maiúsculo. Padrão: true.</param>
/// <param name="number">Exige carácter numérico. Padrão: true.</param>
/// <param name="symbol">Exige carácter especial. Padrão: true.</param>
/// <param name="length">Tamanho mínimo da senha. Padrão: 8.</param>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class PasswordValidationAttribute(
  bool lowercase = true,
  bool uppercase = true,
  bool number = true,
  bool symbol = true,
  int length = 8
) : ValidationAttribute
{
  /// <summary>
  /// Sobrescreve o método de validação para verificar se a senha atende aos critérios de complexidade.
  /// </summary>
  /// <param name="value">O objeto a ser validado.</param>
  /// <returns>Retornar verdadeiro se o valor for uma senha válida.</returns>
  public override bool IsValid(object? value)
  {
    // Converta o valor para uma string.
    string? password = value?.ToString();

    // Verifique se o valor é nulo.
    if (string.IsNullOrEmpty(password))
    {
      // Defina a mensagem de erro padrão.
      ErrorMessage = "Field.Required;Password";

      // Retorne falso.
      return false;
    }

    // Define a expressão regular para validação da senha
    string regexPassword = MountRegex(lowercase, uppercase, number, symbol, length);

    // Verifique se o valor é um password válido.
    if (!Regex.IsMatch(password, regexPassword, RegexOptions.None, TimeSpan.FromMilliseconds(300)))
    {
      // Defina a mensagem de erro padrão.
      ErrorMessage = "Field.Invalid;Password";

      // Retorne falso.
      return false;
    }

    // Retorne verdadeiro.
    return true;
  }

  /// <summary>
  /// Monta a expressão regular da senha.
  /// </summary>
  /// <param name="lowercase">Exige carácter minúsculo. Padrão: true.</param>
  /// <param name="uppercase">Exige carácter maiúsculo. Padrão: true.</param>
  /// <param name="number">Exige carácter numérico. Padrão: true.</param>
  /// <param name="symbol">Exige carácter especial. Padrão: true.</param>
  /// <param name="length">Tamanho mínimo da senha. Padrão: 8.</param>
  /// <returns>Expressão regular da senha.</returns>
  private static string MountRegex(bool lowercase = true, bool uppercase = true, bool number = true, bool symbol = true, int length = 8)
  {
    // Define a expressão regular para validação da senha.
    string regexPassword = null!;

    // Define o tamanho mínimo da senha.
    length = length < 8 ? 8 : length;

    // Se a senha deve conter carácter minúsculo.
    if (lowercase)
    {
      regexPassword += "(?=.*[a-z])";
    }

    // Se a senha deve conter carácter maiúsculo.
    if (uppercase)
    {
      regexPassword += "(?=.*[A-Z])";
    }

    // Se a senha deve conter carácter numérico.
    if (number)
    {
      regexPassword += "(?=.*[0-9])";
    }

    // Se a senha deve conter carácter especial.
    if (symbol)
    {
      regexPassword += "(?=.*[!@#$%&*()\\-_+\\=\\[\\]{}\\/?;:.>,<|\\\\])";
    }

    // Se não foi definido critérios de complexidade, utiliza o padrão de complexidade.
    regexPassword ??= "(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%&*()\\-_+\\=\\[\\]{}\\/?;:.>,<|\\\\])";

    // Define o tamanho mínimo da senha.
    regexPassword += ".{" + length + ",}";

    // Retorna a expressão regular da senha.
    return regexPassword;
  }
}
