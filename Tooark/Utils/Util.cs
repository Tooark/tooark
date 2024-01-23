using System.Globalization;

namespace Tooark.Utils;

public static class Util
{
  public static class Languages
  {
    public static readonly string Default = "en-US";
    public static string Current => CultureInfo.CurrentCulture.Name;
  }

  public static string GetName<T>(IList<T> listValue)
  {
    return GetLanguageCode(listValue, "Name");
  }

  public static string GetTitle<T>(IList<T> listValue)
  {
    return GetLanguageCode(listValue, "Title");
  }

  public static string GetDescription<T>(IList<T> listValue)
  {
    return GetLanguageCode(listValue, "Description");
  }

  public static string GetLanguageCode<T>(IList<T> listValue, string property)
  {
    var currentLanguageCode = Languages.Current;
    var defaultLanguageCode = Languages.Default;

    var languageCodeProperty = typeof(T).GetProperty("LanguageCode");
    var nameProperty = typeof(T).GetProperty(property);

    if (languageCodeProperty == null)
    {
      throw new InvalidOperationException($"PropertyNotExist;LanguageCode");
    }

    if (nameProperty == null)
    {
      throw new InvalidOperationException($"PropertyNotExist;{property}");
    }

    var currentLanguageItem = listValue.FirstOrDefault(x => (string)languageCodeProperty.GetValue(x)! == currentLanguageCode) ??
                              listValue.FirstOrDefault(x => (string)languageCodeProperty.GetValue(x)! == defaultLanguageCode) ??
                              listValue.FirstOrDefault();

    return currentLanguageItem != null ?
      (string)nameProperty.GetValue(currentLanguageItem)! :
      string.Empty;
  }
}
