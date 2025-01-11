namespace Tooark.ValueObjects;

/// <summary>
/// Representa uma senha válida com complexidade especificada.
/// </summary>
public class Password : ValueObject
{
  /// <summary>
  /// O valor privado da senha.
  /// </summary>
  private readonly string _value;

  /// <summary>
  /// Obtém o valor da senha.
  /// </summary>
  public string Value { get => _value; }
  

  /// <summary>
  /// Inicializa uma nova instância da classe Password com os critérios de complexidade especificados.
  /// </summary>
  /// <param name="value">O valor da senha a ser validado.</param>
  /// <param name="useLowercase">Exige carácter minúsculo. Padrão: true.</param>
  /// <param name="useUppercase">Exige carácter maiúsculo. Padrão: true.</param>
  /// <param name="useNumbers">Exige carácter numérico. Padrão: true.</param>
  /// <param name="useSymbols">Exige carácter especial. Padrão: true.</param>
  /// <param name="passwordLength">Tamanho mínimo da senha. Padrão: 8.</param>
  public Password(string value, bool useLowercase = true, bool useUppercase = true, bool useNumbers = true, bool useSymbols = true, int passwordLength = 8)
  {
    if (string.IsNullOrWhiteSpace(value) || !IsValidPassword(value, useLowercase, useUppercase, useNumbers, useSymbols, passwordLength))
    {
      throw new ArgumentException("Field.Invalid;Password");
    }

    _value = value;
  }

  /// <summary>
  /// Valida a complexidade da senha.
  /// </summary>
  /// <param name="password">A senha a ser validada.</param>
  /// <param name="useLowercase">Exige carácter minúsculo. Padrão: true.</param>
  /// <param name="useUppercase">Exige carácter maiúsculo. Padrão: true.</param>
  /// <param name="useNumbers">Exige carácter numérico. Padrão: true.</param>
  /// <param name="useSymbols">Exige carácter especial. Padrão: true.</param>
  /// <param name="passwordLength">Tamanho mínimo da senha. Padrão: 8.</param>
  /// <returns>Verdadeiro se a senha for válida, falso caso contrário.</returns>
  private static bool IsValidPassword(string password, bool useLowercase, bool useUppercase, bool useNumbers, bool useSymbols, int passwordLength)
  {
    return !(
      password.Length < passwordLength ||
      useLowercase && !password.Any(char.IsLower) ||
      useUppercase && !password.Any(char.IsUpper) ||
      useNumbers && !password.Any(char.IsDigit) ||
      useSymbols && !password.Any(ch => !char.IsLetterOrDigit(ch))
    );
  }


  /// <summary>
  /// Sobrescrita do método <see cref="object.ToString"/> para retornar o valor da senha.
  /// </summary>
  /// <returns>Uma string que representa o valor da senha.</returns>
  public override string ToString() => _value;

  /// <summary>
  /// Define uma conversão implícita de um objeto Password para uma string.
  /// </summary>
  /// <param name="password">O objeto Password a ser convertido.</param>
  /// <returns>A string que representa o valor do password.</returns>
  public static implicit operator string(Password password) => password._value;

  /// <summary>
  /// Define uma conversão implícita de uma string para um objeto Password.
  /// </summary>
  /// <param name="value">A string a ser convertida em um objeto Password.</param>
  /// <returns>O objeto Password criado a partir da string fornecida.</returns>
  public static implicit operator Password(string value) => new(value);
}
