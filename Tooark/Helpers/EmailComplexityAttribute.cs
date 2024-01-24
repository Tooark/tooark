using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Tooark.Helpers;

public class EmailComplexityAttribute : ValidationAttribute
{
  public override bool IsValid(object? value)
  {
    if (value == null)
    {
      ErrorMessage = "RequiredField;Email";
      return false;
    }

    string email = value.ToString()!;

    if (new EmailAddressAttribute().IsValid(email))
    {
      string strModel = @"^[a-zA-Z0-9_.-]+@[a-zA-Z0-9_-]+\.[a-zA-Z0-9_.-]+[^.]$";

      if (Regex.IsMatch(email, strModel, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)))
      {
        return true;
      }
      else
      {
        ErrorMessage = "InvalidField;Email";
        return false;
      }
    }
    else
    {
      ErrorMessage = "InvalidField;Email";
      return false;
    }
  }
}
