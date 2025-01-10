using System.ComponentModel.DataAnnotations;

namespace Tooark.Attributes;

/// <summary>
/// Atributo de validação que verifica se uma senha atende a critérios específicos de complexidade.
/// </summary>
/// <param name="useLowercase">Exige carácter minúsculo. Padrão: true.</param>
/// <param name="useUppercase">Exige carácter maiúsculo. Padrão: true.</param>
/// <param name="useNumbers">Exige carácter numérico. Padrão: true.</param>
/// <param name="useSymbols">Exige carácter especial. Padrão: true.</param>
/// <param name="passwordLength">Tamanho mínimo da senha. Padrão: 8.</param>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class PasswordComplexityAttribute(
bool useLowercase = true,
bool useUppercase = true,
bool useNumbers = true,
bool useSymbols = true,
int passwordLength = 8) : ValidationAttribute
{
  private readonly bool UseLowercase = useLowercase;
  private readonly bool UseUppercase = useUppercase;
  private readonly bool UseNumbers = useNumbers;
  private readonly bool UseSymbols = useSymbols;
  private readonly int PasswordLength = passwordLength;

  /// <summary>
  /// Valida se o valor fornecido atende aos critérios de complexidade da senha.
  /// </summary>
  /// <param name="value">O valor da senha a ser validado.</param>
  /// <returns>Verdadeiro se a senha atender aos critérios de complexidade, falso caso contrário.</returns>
  public override bool IsValid(object? value)
  {
    if (value == null)
    {
      ErrorMessage = "Field.Required;Password";
      return false;
    }

    string password = value.ToString()!;

    if (!CheckValid(password))
    {
      ErrorMessage = "Field.Invalid;Password";
      return false;
    }

    return true;
  }

  /// <summary>
  /// Verifica se a senha fornecida atende aos critérios de complexidade.
  /// </summary>
  /// <param name="password">A senha a ser verificada.</param>
  /// <returns>Verdadeiro se a senha atender aos critérios, falso caso contrário.</returns>
  private bool CheckValid(string password)
  {
    return !(
      password.Length < PasswordLength ||
      UseLowercase && !password.Any(char.IsLower) ||
      UseUppercase && !password.Any(char.IsUpper) ||
      UseNumbers && !password.Any(char.IsDigit) ||
      UseSymbols && !password.Any(ch => !char.IsLetterOrDigit(ch))
    );
  }
}
