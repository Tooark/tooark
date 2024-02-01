using System.Globalization;

namespace Tooark.Utils;

public static partial class Util
{
  public static class Languages
  {
    public static readonly string Default = "en-US";
    public static string Current => CultureInfo.CurrentCulture.Name;
  }
}
