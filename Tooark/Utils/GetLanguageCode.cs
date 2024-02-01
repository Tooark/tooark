namespace Tooark.Utils;

public static partial class Util
{
  public static string GetName<T>(IList<T> listValue, string? languageCode = null!)
  {
    languageCode ??= Languages.Current;
    return GetLanguageCode(listValue, "Name", languageCode);
  }

  public static string GetTitle<T>(IList<T> listValue, string? languageCode = null!)
  {
    languageCode ??= Languages.Current;
    return GetLanguageCode(listValue, "Title", languageCode);
  }

  public static string GetDescription<T>(IList<T> listValue, string? languageCode = null!)
  {
    languageCode ??= Languages.Current;
    return GetLanguageCode(listValue, "Description", languageCode);
  }

  public static string GetLanguageCode<T>(IList<T> listValue, string property, string currentLanguageCode = null!)
  {
    currentLanguageCode ??= Languages.Current;
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
