using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Tooark.Helpers;

public class PasswordComplexityAttribute : ValidationAttribute
{
  private readonly bool useLowercase;
  private readonly bool useUppercase;
  private readonly bool useNumbers;
  private readonly bool useSymbols;
  private readonly int passwordLength;

  public PasswordComplexityAttribute(bool useLowercase = true, bool useUppercase = true, bool useNumbers = true, bool useSymbols = true, int passwordLength = 8)
  {
    this.useLowercase = useLowercase;
    this.useUppercase = useUppercase;
    this.useNumbers = useNumbers;
    this.useSymbols = useSymbols;
    this.passwordLength = passwordLength;
  }

  public override bool IsValid(object? value)
  {
    if (value == null)
    {
      ErrorMessage = "RequiredField;Password";
      return false;
    }

    string password = value.ToString()!;
        
    if (CheckValid(password))
    {
      return true;
    }
    else
    {
      ErrorMessage = "InvalidField;Password";
      return false;
    }
  }

  public bool IsValidNew(object? value, bool regex = false)
  {
    if (value == null)
    {
      ErrorMessage = "RequiredField;Password";
      return false;
    }

    string password = value.ToString()!;
        
    if (regex && CheckValidRegex(password) || CheckValid(password))
    {
      return true;
    }
    else
    {
      ErrorMessage = "InvalidField;Password";
      return false;
    }
  }

  private bool CheckValid(string password)
  {
    return password.Length < passwordLength ||
      useLowercase && !password.Any(char.IsLower) ||
      useUppercase && !password.Any(char.IsUpper) ||
      useNumbers && !password.Any(char.IsDigit) || 
      useSymbols && !password.Any(ch => !char.IsLetterOrDigit(ch));
  }

  private bool CheckValidRegex(string password)
  {
    string strModel = "^";

    // Letra minuscula
    if (useLowercase)
    {
      strModel += "(?=.*[a-z])";
    }

    // Letra maiúscula
    if (useUppercase)
    {
      strModel += "(?=.*[A-Z])";
    }

    // Número
    if (useNumbers)
    {
      strModel += "(?=.*\\d)";
    }

    // Símbolo
    if (useSymbols)
    {
      strModel += "(?=.*\\W)";
    }

    // Tamanho da senha
    strModel += ".{" + passwordLength + ",}$";

    return Regex.IsMatch(password, strModel, RegexOptions.None, TimeSpan.FromMilliseconds(250));
  }

}
