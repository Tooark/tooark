using Tooark.Validations;
using Tooark.Validations.Patterns;

namespace Tooark.ValueObjects;

/// <summary>
/// Representa uma senha válida com complexidade especificada.
/// </summary>
public sealed class Password : ValueObject
{
  /// <summary>
  /// O valor privado da senha.
  /// </summary>
  private readonly string _value = null!;


  /// <summary>
  /// Inicializa uma nova instância da classe Password com os critérios de complexidade especificados.
  /// </summary>
  /// <param name="value">O valor da senha a ser validado.</param>
  /// <param name="lowercase">Exige carácter minúsculo. Padrão: true.</param>
  /// <param name="uppercase">Exige carácter maiúsculo. Padrão: true.</param>
  /// <param name="number">Exige carácter numérico. Padrão: true.</param>
  /// <param name="symbol">Exige carácter especial. Padrão: true.</param>
  /// <param name="length">Tamanho mínimo da senha. Padrão: 8.</param>
  public Password(string value, bool lowercase = true, bool uppercase = true, bool number = true, bool symbol = true, int length = 8)
  {
    // Define a expressão regular para validação da senha
    string regexPassword = MountRegex(lowercase, uppercase, number, symbol, length);

    // Adiciona as notificações de validação da senha
    AddNotifications(new Validation()
      .Match(value, regexPassword, "Password", "Field.Invalid;Password")
    );

    // Verifica é valido então não existe notificação
    if(IsValid)
    {
      // Define o valor da senha
      _value = value;
    }
  }


  /// <summary>
  /// Obtém o valor da senha.
  /// </summary>
  public string Value { get => _value; }


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
      regexPassword += RegexPattern.PassLower;
    }

    // Se a senha deve conter carácter maiúsculo.
    if (uppercase)
    {
      regexPassword += RegexPattern.PassUpper;
    }

    // Se a senha deve conter carácter numérico.
    if (number)
    {
      regexPassword += RegexPattern.PassNumber;
    }

    // Se a senha deve conter carácter especial.
    if (symbol)
    {
      regexPassword += RegexPattern.PassSymbol;
    }

    // Verifica se não foi definido critérios de complexidade.
    if (string.IsNullOrEmpty(regexPassword))
    {
      // Utiliza o padrão de complexidade.
      regexPassword = RegexPattern.PassComplex;

      // Define o tamanho mínimo da senha.
      regexPassword = regexPassword.Replace(".{8,}", $".{{{length},}}");
    }
    else
    {
      // Define o tamanho mínimo da senha.
      regexPassword += ".{" + length + ",}";
    }

    // Retorna a expressão regular da senha.
    return regexPassword;
  }

  /// <summary>
  /// Sobrescrita do método <see cref="object.ToString"/> para retornar o valor da senha.
  /// </summary>
  /// <returns>O valor da senha.</returns>
  public override string ToString() => _value;

  /// <summary>
  /// Define uma conversão implícita de um objeto Password para uma string.
  /// </summary>
  /// <param name="password">O objeto Password a ser convertido.</param>
  /// <returns>Uma string que representa o valor da senha.</returns>
  public static implicit operator string(Password password) => password._value;

  /// <summary>
  /// Define uma conversão implícita de uma string para um objeto Password.
  /// </summary>
  /// <param name="value">A string a ser convertida em um objeto Password.</param>
  /// <returns>O objeto Password criado a partir da string fornecida.</returns>
  public static implicit operator Password(string value) => new(value);
}
