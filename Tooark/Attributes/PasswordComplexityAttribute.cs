using System.ComponentModel.DataAnnotations;

namespace Tooark.Attributes;

/// <summary>
/// Atributo de validação que verifica se uma senha atende a critérios específicos de complexidade.
/// </summary>
public class PasswordComplexityAttribute : ValidationAttribute
{
  private readonly bool useLowercase;
  private readonly bool useUppercase;
  private readonly bool useNumbers;
  private readonly bool useSymbols;
  private readonly int passwordLength;

  /// <summary>
  /// Inicializa uma nova instância da classe PasswordComplexityAttribute com os critérios de complexidade especificados.
  /// </summary>
  /// <param name="useLowercase">Exige carácter minúsculo. Padrão: true.</param>
  /// <param name="useUppercase">Exige carácter maiúsculo. Padrão: true.</param>
  /// <param name="useNumbers">Exige carácter numérico. Padrão: true.</param>
  /// <param name="useSymbols">Exige carácter especial. Padrão: true.</param>
  /// <param name="passwordLength">Tamanho mínimo da senha. Padrão: 8.</param>
  public PasswordComplexityAttribute(
    bool useLowercase = true,
    bool useUppercase = true,
    bool useNumbers = true,
    bool useSymbols = true,
    int passwordLength = 8)
  {
    this.useLowercase = useLowercase;
    this.useUppercase = useUppercase;
    this.useNumbers = useNumbers;
    this.useSymbols = useSymbols;
    this.passwordLength = passwordLength;
  }

  /// <summary>
  /// Valida se o valor fornecido atende aos critérios de complexidade da senha.
  /// </summary>
  /// <param name="value">O valor da senha a ser validado.</param>
  /// <returns>Verdadeiro se a senha atender aos critérios de complexidade, falso caso contrário.</returns>
  public override bool IsValid(object? value)
  {
    if (value == null)
    {
      ErrorMessage = "RequiredField;Password";
      return false;
    }

    string password = value.ToString()!;

    if (!CheckValid(password))
    {
      ErrorMessage = "InvalidField;Password";
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
      password.Length < passwordLength ||
      useLowercase && !password.Any(char.IsLower) ||
      useUppercase && !password.Any(char.IsUpper) ||
      useNumbers && !password.Any(char.IsDigit) ||
      useSymbols && !password.Any(ch => !char.IsLetterOrDigit(ch))
    );
  }
}
